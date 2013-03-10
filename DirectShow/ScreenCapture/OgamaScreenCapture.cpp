//------------------------------------------------------------------------------
// File: OgamaScreenCaptureDesktop.cpp
//
// Desc: DirectShow sample code - In-memory push mode source filter
//       Provides an image of the user's desktop as a continuously updating stream.
//
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------------------------

#include <streams.h>
#include <time.h>
#include "OgamaScreenCapture.h"
#include "OgamaScreenCaptureGuids.h"
#include "DibHelper.h"

/**********************************************
 *
 *  CPushPinDesktop Class
 *  
 *
 **********************************************/

COgamaScreenCapturePin::COgamaScreenCapturePin(HRESULT *phr, CSource *pFilter)
        : CSourceStream(NAME("Ogama Screen Capture"), phr, pFilter, L"Out"),
        m_FramesWritten(0),
        m_bZeroMemory(0),
        m_iFrameNumber(0),
        m_rtFrameLength(FPS_10), // Capture and display desktop 10 times per second by default
        m_nCurrentBitDepth(32),
		m_MonitorIndex(0),
		m_lastSampleTime(0)
{
	// The main point of this sample is to demonstrate how to take a DIB
	// in host memory and insert it into a video stream. 

	// In the filter graph, we connect this filter to the AVI Mux, which creates 
    // the AVI file with the video frames we pass to it. In this case, 
    // the end result is a screen capture video (GDI images only, with no
    // support for overlay surfaces).
	m_Stopwatch.startTimer();
}

COgamaScreenCapturePin::~COgamaScreenCapturePin()
{   
	// clean up
    DbgLog((LOG_TRACE, 3, TEXT("Frames written %d"), m_iFrameNumber));

    DeleteDC(m_ScreenDC);
    DeleteDC(m_MemoryDC);
    DeleteObject(m_ScreenBitmap);
}

STDMETHODIMP COgamaScreenCapturePin::get_Monitor(int* index)
{
    CAutoLock foo(&m_cSharedState);
    *index = m_MonitorIndex;
    return NOERROR;
}
STDMETHODIMP COgamaScreenCapturePin::set_Monitor(int index)
{
    CAutoLock foo(&m_cSharedState);
    m_MonitorIndex = index;
	UpdateTargetScreen();
    return NOERROR;
}
STDMETHODIMP COgamaScreenCapturePin::get_Framerate(int* framerate)
{
    CAutoLock foo(&m_cSharedState);
    *framerate = (int)(UNITS / m_rtFrameLength);
    return NOERROR;
}
STDMETHODIMP COgamaScreenCapturePin::set_Framerate(int framerate)
{
    CAutoLock foo(&m_cSharedState);
	REFERENCE_TIME newFPS = UNITS / framerate;
    m_rtFrameLength = newFPS;
    return NOERROR;
}

STDMETHODIMP COgamaScreenCapturePin::UpdateTargetScreen(void)
{
	// Free resources if applicable
	ReleaseScreen();

    // create a DC for the screen 
	if(m_MonitorIndex == 0)
	{
		m_ScreenDC = CreateDC(TEXT("DISPLAY"), TEXT("DISPLAY"), NULL, NULL);
	}
	else
	{
		m_ScreenDC = CreateDC(TEXT("\\\\.\\DISPLAY2"), TEXT("\\\\.\\DISPLAY2"), NULL, NULL);
	}

	m_MemoryDC = CreateCompatibleDC(m_ScreenDC);

    // get screen resolution
    m_ScreenWidth = GetDeviceCaps(m_ScreenDC, HORZRES);
    m_ScreenHeight = GetDeviceCaps(m_ScreenDC, VERTRES);

    // create a bitmap compatible with the screen DC
    m_ScreenBitmap = CreateCompatibleBitmap(m_ScreenDC, m_ScreenWidth, m_ScreenHeight);

	return S_OK;
}

STDMETHODIMP COgamaScreenCapturePin::ReleaseScreen(void)
{
	if (m_ScreenDC != NULL)
	{
		DeleteDC(m_ScreenDC);
	}

	if (m_MemoryDC != NULL)
	{
		DeleteDC(m_MemoryDC);
	}
	
	if (m_ScreenBitmap != NULL)
	{
		DeleteObject(m_ScreenBitmap);
	}

	return S_OK;
}
//
// GetMediaType
//
// Prefer 5 formats - 8, 16 (*2), 24 or 32 bits per pixel
//
// Prefered types should be ordered by quality, with zero as highest quality.
// Therefore, iPosition =
//      0    Return a 32bit mediatype
//      1    Return a 24bit mediatype
//      2    Return 16bit RGB565
//      3    Return a 16bit mediatype (rgb555)
//      4    Return 8 bit palettised format
//      >4   Invalid
//
HRESULT COgamaScreenCapturePin::GetMediaType(int iPosition, CMediaType *pmt)
{
    CheckPointer(pmt,E_POINTER);
    CAutoLock cAutoLock(m_pFilter->pStateLock());

    if(iPosition < 0)
        return E_INVALIDARG;

    // Have we run off the end of types?
    if(iPosition > 4)
        return VFW_S_NO_MORE_ITEMS;

    VIDEOINFO *pvi = (VIDEOINFO *) pmt->AllocFormatBuffer(sizeof(VIDEOINFO));
    if(NULL == pvi)
        return(E_OUTOFMEMORY);

    // Initialize the VideoInfo structure before configuring its members
    ZeroMemory(pvi, sizeof(VIDEOINFO));

    switch(iPosition)
    {
        case 0:
        {    
            // Return our highest quality 32bit format

            // Since we use RGB888 (the default for 32 bit), there is
            // no reason to use BI_BITFIELDS to specify the RGB
            // masks. Also, not everything supports BI_BITFIELDS
            pvi->bmiHeader.biCompression = BI_RGB;
            pvi->bmiHeader.biBitCount    = 32;
            break;
        }

        case 1:
        {   // Return our 24bit format
            pvi->bmiHeader.biCompression = BI_RGB;
            pvi->bmiHeader.biBitCount    = 24;
            break;
        }

        case 2:
        {       
            // 16 bit per pixel RGB565

            // Place the RGB masks as the first 3 doublewords in the palette area
            for(int i = 0; i < 3; i++)
                pvi->TrueColorInfo.dwBitMasks[i] = bits565[i];

            pvi->bmiHeader.biCompression = BI_BITFIELDS;
            pvi->bmiHeader.biBitCount    = 16;
            break;
        }

        case 3:
        {   // 16 bits per pixel RGB555

            // Place the RGB masks as the first 3 doublewords in the palette area
            for(int i = 0; i < 3; i++)
                pvi->TrueColorInfo.dwBitMasks[i] = bits555[i];

            pvi->bmiHeader.biCompression = BI_BITFIELDS;
            pvi->bmiHeader.biBitCount    = 16;
            break;
        }

        case 4:
        {   // 8 bit palettised

            pvi->bmiHeader.biCompression = BI_RGB;
            pvi->bmiHeader.biBitCount    = 8;
            pvi->bmiHeader.biClrUsed     = iPALETTE_COLORS;
            break;
        }
    }

    // Adjust the parameters common to all formats
    pvi->bmiHeader.biSize       = sizeof(BITMAPINFOHEADER);
    pvi->bmiHeader.biWidth      = m_ScreenWidth;
    pvi->bmiHeader.biHeight     = m_ScreenHeight;
    pvi->bmiHeader.biPlanes     = 1;
    pvi->bmiHeader.biSizeImage  = GetBitmapSize(&pvi->bmiHeader);
    pvi->bmiHeader.biClrImportant = 0;
	pvi->AvgTimePerFrame		= m_rtFrameLength;

    SetRectEmpty(&(pvi->rcSource)); // we want the whole image area rendered.
    SetRectEmpty(&(pvi->rcTarget)); // no particular destination rectangle

	pmt->SetType(&MEDIATYPE_Video);
    pmt->SetFormatType(&FORMAT_VideoInfo);
    pmt->SetTemporalCompression(FALSE);

    // Work out the GUID for the subtype from the header info.
    const GUID SubTypeGUID = GetBitmapSubtype(&pvi->bmiHeader);
    pmt->SetSubtype(&SubTypeGUID);
    pmt->SetSampleSize(pvi->bmiHeader.biSizeImage);

    return NOERROR;

} // GetMediaType


//
// CheckMediaType
//
// We will accept 8, 16, 24 or 32 bit video formats, in any
// image size that gives room to bounce.
// Returns E_INVALIDARG if the mediatype is not acceptable
//
HRESULT COgamaScreenCapturePin::CheckMediaType(const CMediaType *pMediaType)
{
    CheckPointer(pMediaType,E_POINTER);

    if((*(pMediaType->Type()) != MEDIATYPE_Video) ||   // we only output video
        !(pMediaType->IsFixedSize()))                  // in fixed size samples
    {                                                  
        return E_INVALIDARG;
    }

    // Check for the subtypes we support
    const GUID *SubType = pMediaType->Subtype();
    if (SubType == NULL)
        return E_INVALIDARG;

    if(    (*SubType != MEDIASUBTYPE_RGB8)
        && (*SubType != MEDIASUBTYPE_RGB565)
        && (*SubType != MEDIASUBTYPE_RGB555)
        && (*SubType != MEDIASUBTYPE_RGB24)
        && (*SubType != MEDIASUBTYPE_RGB32))
    {
        return E_INVALIDARG;
    }

    // Get the format area of the media type
    VIDEOINFO *pvi = (VIDEOINFO *) pMediaType->Format();

    if(pvi == NULL)
        return E_INVALIDARG;

    // Check if the image width & height have changed
    if(    pvi->bmiHeader.biWidth   != m_ScreenWidth || 
       abs(pvi->bmiHeader.biHeight) != m_ScreenHeight)
    {
        // If the image width/height is changed, fail CheckMediaType() to force
        // the renderer to resize the image.
        return E_INVALIDARG;
    }

    // Don't accept formats with negative height, which would cause the desktop
    // image to be displayed upside down.
    if (pvi->bmiHeader.biHeight < 0)
        return E_INVALIDARG;

    return S_OK;  // This format is acceptable.

} // CheckMediaType


//
// DecideBufferSize
//
// This will always be called after the format has been sucessfully
// negotiated. So we have a look at m_mt to see what size image we agreed.
// Then we can ask for buffers of the correct size to contain them.
//
HRESULT COgamaScreenCapturePin::DecideBufferSize(IMemAllocator *pAlloc,
                                      ALLOCATOR_PROPERTIES *pProperties)
{
    CheckPointer(pAlloc,E_POINTER);
    CheckPointer(pProperties,E_POINTER);

    CAutoLock cAutoLock(m_pFilter->pStateLock());
    HRESULT hr = NOERROR;

    VIDEOINFO *pvi = (VIDEOINFO *) m_mt.Format();
    pProperties->cBuffers = 1;
    pProperties->cbBuffer = pvi->bmiHeader.biSizeImage;

    ASSERT(pProperties->cbBuffer);

    // Ask the allocator to reserve us some sample memory. NOTE: the function
    // can succeed (return NOERROR) but still not have allocated the
    // memory that we requested, so we must check we got whatever we wanted.
    ALLOCATOR_PROPERTIES Actual;
    hr = pAlloc->SetProperties(pProperties,&Actual);
    if(FAILED(hr))
    {
        return hr;
    }

    // Is this allocator unsuitable?
    if(Actual.cbBuffer < pProperties->cbBuffer)
    {
        return E_FAIL;
    }

    // Make sure that we have only 1 buffer (we erase the ball in the
    // old buffer to save having to zero a 200k+ buffer every time
    // we draw a frame)
    ASSERT(Actual.cBuffers == 1);
    return NOERROR;

} // DecideBufferSize


//
// SetMediaType
//
// Called when a media type is agreed between filters
//
HRESULT COgamaScreenCapturePin::SetMediaType(const CMediaType *pMediaType)
{
    CAutoLock cAutoLock(m_pFilter->pStateLock());

    // Pass the call up to my base class
    HRESULT hr = CSourceStream::SetMediaType(pMediaType);

    if(SUCCEEDED(hr))
    {
        VIDEOINFO * pvi = (VIDEOINFO *) m_mt.Format();
        if (pvi == NULL)
            return E_UNEXPECTED;

        switch(pvi->bmiHeader.biBitCount)
        {
            case 8:     // 8-bit palettized
            case 16:    // RGB565, RGB555
            case 24:    // RGB24
            case 32:    // RGB32
                // Save the current media type and bit depth
                m_MediaType = *pMediaType;
                m_nCurrentBitDepth = pvi->bmiHeader.biBitCount;
                hr = S_OK;
                break;

            default:
                // We should never agree any other media types
                ASSERT(FALSE);
                hr = E_INVALIDARG;
                break;
        }
    } 

    return hr;

} // SetMediaType


// This is where we insert the DIB bits into the video stream.
// FillBuffer is called once for every sample in the stream.
HRESULT COgamaScreenCapturePin::FillBuffer(IMediaSample *pSample)
{
	BYTE *pData;
    long cbData;

	double currentTime=m_Stopwatch.getElapsedTime()*1000;
	if (currentTime-m_lastSampleTime>m_rtFrameLength/UNITS)
	{
		m_iFrameNumber++;
		return S_FALSE;
	}

    CheckPointer(pSample, E_POINTER);

    CAutoLock cAutoLockShared(&m_cSharedState);

    // Access the sample's data buffer
    pSample->GetPointer(&pData);
    cbData = pSample->GetSize();

    // Check that we're still using video
    ASSERT(m_mt.formattype == FORMAT_VideoInfo);

    VIDEOINFOHEADER *pVih = (VIDEOINFOHEADER*)m_mt.pbFormat;

	// Copy the DIB bits over into our filter's output buffer.
    // Since sample size may be larger than the image size, bound the copy size.
    int nSize = min(pVih->bmiHeader.biSizeImage, (DWORD) cbData);
    CopyMonitorToBitmap(pData, (BITMAPINFO *) &(pVih->bmiHeader));

	// Set the timestamps that will govern playback frame rate.
	// If this file is getting written out as an AVI,
	// then you'll also need to configure the AVI Mux filter to 
	// set the Average Time Per Frame for the AVI Header.
    // The current time is the sample's start.
    //REFERENCE_TIME rtStart = m_iFrameNumber * m_rtFrameLength;
    //REFERENCE_TIME rtStop  = rtStart + m_rtFrameLength;

    //pSample->SetTime(&rtStart, &rtStop);
    //m_iFrameNumber++;

	CRefTime rtStart;
	m_pFilter->StreamTime(rtStart);
	CRefTime rtEnd = rtStart + m_rtFrameLength;
	pSample->SetTime((REFERENCE_TIME *)&rtStart, (REFERENCE_TIME *)&rtEnd);


	// Set TRUE on every sample for uncompressed frames
    pSample->SetSyncPoint(TRUE);

	m_lastSampleTime=currentTime;

    return S_OK;
}

HRESULT COgamaScreenCapturePin::CopyMonitorToBitmap(BYTE *pData, BITMAPINFO *pHeader)
{
    HBITMAP hOldBitmap;    // handles to deice-dependent bitmaps

	// select new bitmap into memory DC
    hOldBitmap = (HBITMAP) SelectObject(m_MemoryDC, m_ScreenBitmap);
	
	// dwRop |= CAPTUREBLT;

    // bitblt screen DC to memory DC
    BitBlt(m_MemoryDC, 0, 0, m_ScreenWidth, m_ScreenHeight, m_ScreenDC, 0, 0, SRCCOPY);

    // select old bitmap back into memory DC and get handle to
    // bitmap of the screen   
    m_ScreenBitmap = (HBITMAP)  SelectObject(m_MemoryDC, hOldBitmap);

    // Copy the bitmap data into the provided BYTE buffer
    GetDIBits(m_ScreenDC, m_ScreenBitmap, 0, m_ScreenHeight, pData, pHeader, DIB_RGB_COLORS);

	return S_OK;
}

/**********************************************
 *
 *  COgamaScreenCapture Class
 *
 **********************************************/

COgamaScreenCapture::COgamaScreenCapture(IUnknown *pUnk, HRESULT *phr)
           : CSource(NAME("OgamaScreenCapture"), pUnk, CLSID_OgamaScreenCapture)
{
    // The pin magically adds itself to our pin array.
    m_pPin = new COgamaScreenCapturePin(phr, this);

	if (phr)
	{
		if (m_pPin == NULL)
			*phr = E_OUTOFMEMORY;
		else
			*phr = S_OK;
	}  
}

COgamaScreenCapture::~COgamaScreenCapture()
{
    delete m_pPin;
}

STDMETHODIMP COgamaScreenCapture::get_Monitor(int* primary)
{
		if (m_pPin == NULL)
			return S_FALSE;
		else
		{
			m_pPin->get_Monitor(primary);
			return S_OK;
		}
}
STDMETHODIMP COgamaScreenCapture::set_Monitor(int primary)
{
		if (m_pPin == NULL)
			return S_FALSE;
		else
		{
			m_pPin->set_Monitor(primary);
			return S_OK;
		}
}
STDMETHODIMP COgamaScreenCapture::get_Framerate(int* framerate)
{
		if (m_pPin == NULL)
			return S_FALSE;
		else
		{
			m_pPin->get_Framerate(framerate);
			return S_OK;
		}
}
STDMETHODIMP COgamaScreenCapture::set_Framerate(int framerate)
{
		if (m_pPin == NULL)
			return S_FALSE;
		else
		{
			m_pPin->set_Framerate(framerate);
			return S_OK;
		}
}

//
// NonDelegatingQueryInterface
//
// Override CUnknown method.
// Reveal our persistent stream, property pages and IGargle interfaces.
// Anyone can call our private interface so long as they know the private UUID.
//
STDMETHODIMP COgamaScreenCapture::NonDelegatingQueryInterface(REFIID riid, void **ppv)
{
    CheckPointer(ppv,E_POINTER);

    if (riid == IID_IOgamaScreenCapture) {
        return GetInterface((IOgamaScreenCapture *)this, ppv);
    }

	return CSource::NonDelegatingQueryInterface(riid, ppv);
} // NonDelegatingQueryInterface


CUnknown * WINAPI COgamaScreenCapture::CreateInstance(IUnknown *pUnk, HRESULT *phr)
{
    COgamaScreenCapture *pNewFilter = new COgamaScreenCapture(pUnk, phr );

	if (phr)
	{
		if (pNewFilter == NULL) 
			*phr = E_OUTOFMEMORY;
		else
			*phr = S_OK;
	}
    return pNewFilter;

}

