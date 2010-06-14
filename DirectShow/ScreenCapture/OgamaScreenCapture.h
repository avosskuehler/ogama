//------------------------------------------------------------------------------
// File: OgamaScreenCapture.H
//
// Desc: DirectShow sample code - In-memory push mode source filter
//
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------------------------

#include <windows.h>
#include <strsafe.h>
#include "IOgamaScreenCapture.h"

// UNITS = 10 ^ 7  
// UNITS / 30 = 30 fps;
// UNITS / 20 = 20 fps, etc
const REFERENCE_TIME FPS_30 = UNITS / 30;
const REFERENCE_TIME FPS_20 = UNITS / 20;
const REFERENCE_TIME FPS_10 = UNITS / 10;
const REFERENCE_TIME FPS_5  = UNITS / 5;
const REFERENCE_TIME FPS_4  = UNITS / 4;
const REFERENCE_TIME FPS_3  = UNITS / 3;
const REFERENCE_TIME FPS_2  = UNITS / 2;
const REFERENCE_TIME FPS_1  = UNITS / 1;

const REFERENCE_TIME rtDefaultFrameLength = FPS_10;

// Filter name strings
#define g_wszOgamaScreenCapture    L"OgamaScreenCapture Filter"

/**********************************************
 *
 *  Class declarations
 *
 **********************************************/

class COgamaScreenCapturePin : public CSourceStream,
	public IOgamaScreenCapture
{
protected:

    int m_FramesWritten;				// To track where we are in the file
    BOOL m_bZeroMemory;                 // Do we need to clear the buffer?
    CRefTime m_rtSampleTime;	        // The time stamp for each sample

    int m_iFrameNumber;
    REFERENCE_TIME m_rtFrameLength;

    int m_iRepeatTime;                  // Time in msec between frames
    int m_nCurrentBitDepth;             // Screen bit depth
    int m_MonitorIndex;                     // The current captured monitor (true if is primary)
    int m_ScreenWidth;
	int m_ScreenHeight;					// screen resolution
	HDC m_ScreenDC;
    HDC m_MemoryDC;						// memory DC
    HBITMAP m_ScreenBitmap;				// handles to deice-dependent bitmaps

    CMediaType m_MediaType;
    CCritSec m_cSharedState;            // Protects our internal state
    CImageDisplay m_Display;            // Figures out our media type for us

public:

    COgamaScreenCapturePin(HRESULT *phr, CSource *pFilter);
    ~COgamaScreenCapturePin();

    DECLARE_IUNKNOWN;

    // Override the version that offers exactly one media type
    HRESULT DecideBufferSize(IMemAllocator *pAlloc, ALLOCATOR_PROPERTIES *pRequest);
    HRESULT FillBuffer(IMediaSample *pSample);
    
    // Set the agreed media type and set up the necessary parameters
    HRESULT SetMediaType(const CMediaType *pMediaType);

    // Support multiple display formats
    HRESULT CheckMediaType(const CMediaType *pMediaType);
    HRESULT GetMediaType(int iPosition, CMediaType *pmt);

    STDMETHODIMP get_Monitor(int *index);
    STDMETHODIMP set_Monitor(int index);
    STDMETHODIMP get_Framerate(int *framerate);
    STDMETHODIMP set_Framerate(int framerate);
	HRESULT CopyMonitorToBitmap(BYTE *pData, BITMAPINFO *pHeader);

	STDMETHODIMP UpdateTargetScreen(void);
	STDMETHODIMP ReleaseScreen(void);

    // Quality control
	// Not implemented because we aren't going in real time.
	// If the file-writing filter slows the graph down, we just do nothing, which means
	// wait until we're unblocked. No frames are ever dropped.
    STDMETHODIMP Notify(IBaseFilter *pSelf, Quality q)
    {
        return E_FAIL;
    }
};

class COgamaScreenCapture : public CSource,
		public IOgamaScreenCapture
{

private:
    // Constructor is private because you have to use CreateInstance
    COgamaScreenCapture(IUnknown *pUnk, HRESULT *phr);
    ~COgamaScreenCapture();

    COgamaScreenCapturePin *m_pPin;

public:
    static CUnknown * WINAPI CreateInstance(IUnknown *pUnk, HRESULT *phr);  
	STDMETHODIMP NonDelegatingQueryInterface(REFIID riid, void **ppv);
    DECLARE_IUNKNOWN;

    STDMETHODIMP get_Monitor(int *index);
    STDMETHODIMP set_Monitor(int index);
    STDMETHODIMP get_Framerate(int *framerate);
    STDMETHODIMP set_Framerate(int framerate);
};


