////==========================================================================;
////
////  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
////  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
////  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
////  PURPOSE.
////
////  Copyright (c) 1992 - 1998  Microsoft Corporation.  All Rights Reserved.
////
////--------------------------------------------------------------------------;
//
////
////  Video capture stream source filter
////
//
//// Uses the AVICap window to capture video data to pass downstream.
//// By using the video callback it avoids AVICap sending data to a file
//// - AVICap is merely capturing buffers for us to pass on.
////
//
//// Caveats
////
//// ** Should reject going active when the user has format dialogs up.
//
//#include <streams.h>
//#include <initguid.h>
//#include <olectl.h>
//#include <mmsystem.h>
//#include <vfw.h>
//#include <string.h>
//#include <stddef.h>     // for offsetof macro
//
//#include "vidcap.h"
//
//// setup data
//
//const AMOVIESETUP_MEDIATYPE sudOpPinTypes =
//{ &MEDIATYPE_Video       // clsMajorType
//, &MEDIASUBTYPE_NULL };  // clsMinorType
//
//const AMOVIESETUP_PIN sudOpPin =
//{ L"Output"          // strName
//, FALSE              // bRendered
//, TRUE               // bOutput
//, FALSE              // bZero
//, FALSE              // bMany
//, &CLSID_NULL        // clsConnectsToFilter
//, NULL               // strConnectsToPin
//, 1                  // nMediaTypes
//, &sudOpPinTypes };  // lpMediaType
//
//const AMOVIESETUP_FILTER sudVidCapax =
//{ &CLSID_VidCap              // clsID
//, L"Video Capture (AVICap)"  // strName
//, MERIT_UNLIKELY             // dwMerit
//, 1                          // nPins
//, &sudOpPin };               // lpPin
//
//// COM global table of objects available in this dll
//CFactoryTemplate g_Templates[1] = {
//
//    { L"Video Capture (AVICap)"
//    , &CLSID_VidCap
//    , CVidCap::CreateInstance
//    , NULL
//    , NULL }
//};
//int g_cTemplates = sizeof(g_Templates) / sizeof(g_Templates[0]);
//
//
//// exported entry points for registration and
//// unregistration (in this case they only call
//// through to default implmentations).
////
//STDAPI DllRegisterServer()
//{
//  HRESULT hr = AMovieDllRegisterServer2( TRUE );
//  if(SUCCEEDED(hr))
//  {
//      
//      // locate each installed VFW capture driver and register an
//      // entry in the Video Capture entry that maps to this filter. We
//      // expect other capture filters to register just once
//      // 
//      IFilterMapper2 *pFm2;
//      HRESULT hr = CoCreateInstance(
//          CLSID_FilterMapper2, NULL, CLSCTX_INPROC_SERVER,
//          IID_IFilterMapper2, (void **)&pFm2);
//      if(SUCCEEDED(hr))
//      {
//      
//          for(UINT i = 0; i < 10; i++)
//          {
//              TCHAR szName[100], szDesc[100];
//              if(capGetDriverDescription(
//                  i, szName, sizeof(szName), szDesc, sizeof(szDesc)))
//              {
//                  // generate unique instance value (avicap - name). This
//                  // should be something that doesn't change from machine
//                  // to machine as it's used to persist this particular
//                  // device (see documentation for
//                  // IMoniker::GetDisplayName). Behind the scenes, this is
//                  // the name of the registry key for this device. We'll
//                  // use this name for both the instance name and the
//                  // friendly-name
//                  TCHAR szUniq[200];
//                  wsprintf(szUniq, "avicap SDK Sample - %s", szName);
//                  
//                  WCHAR wszUniq[200];
//                  MultiByteToWideChar(CP_ACP, 0, szUniq, -1, wszUniq, NUMELMS(wszUniq));
//
//                  // RegisterFilter returns a moniker here.
//                  IMoniker *pMoniker = 0;
//
//                  REGFILTER2 rf2;
//                  rf2.dwVersion = 1;
//                  rf2.dwMerit = MERIT_DO_NOT_USE;
//                  rf2.cPins = 0;
//                  rf2.rgPins = 0;
//                  
//              
//                  hr = pFm2->RegisterFilter(
//                      CLSID_VidCap,
//                      wszUniq,
//                      &pMoniker,
//                      &CLSID_VideoInputDeviceCategory,
//                      wszUniq,
//                      &rf2);
//
//                  if(SUCCEEDED(hr))
//                  {
//                      // write out the device number. when the device
//                      // is picked this filter needs to know which
//                      // device to open. It does that by reading the
//                      // AviCapIndex value.
//                      IPropertyBag *pPropBag;
//                      hr = pMoniker->BindToStorage(
//                          0, 0, IID_IPropertyBag, (void **)&pPropBag);
//                      if(SUCCEEDED(hr))
//                      {
//                          VARIANT var;
//                          var.vt = VT_I4;
//                          var.lVal = i;
//                          hr = pPropBag->Write(L"AviCapIndex", &var);
//
//                          pPropBag->Release();
//                      }
//                      pMoniker->Release();
//                  }
//
//              } //capGetDriverDescription
//          
//              if(FAILED(hr))
//                  break;
//
//          } // for loop
//
//          pFm2->Release();
//      } // CoCreateInstance
//      
//  } // AMovieDllRegisterServer2
//
//  return hr;
//}
//
//STDAPI DllUnregisterServer()
//{
//    HRESULT hr = AMovieDllRegisterServer2( FALSE );
//    if(SUCCEEDED(hr))
//    {
//      
//        // remove each entry
//
//        IFilterMapper2 *pFm2;
//        HRESULT hr = CoCreateInstance(
//            CLSID_FilterMapper2, NULL, CLSCTX_INPROC_SERVER,
//            IID_IFilterMapper2, (void **)&pFm2);
//        if(SUCCEEDED(hr))
//        {
//      
//            for(UINT i = 0; i < 10; i++)
//            {
//                TCHAR szName[100], szDesc[100];
//                if(capGetDriverDescription(
//                    i, szName, sizeof(szName), szDesc, sizeof(szDesc)))
//                {
//                    // generate unique instance value (avicap - name). This
//                    // should be something that doesn't change from machine
//                    // to machine as it's used to persist this particular
//                    // device (see documentation for
//                    // IMoniker::GetDisplayName). Behind the scenes, this is
//                    // the name of the registry key for this device. We'll
//                    // use this name for both the instance name and the
//                    // friendly-name
//                    TCHAR szUniq[200];
//                    wsprintf(szUniq, "avicap SDK Sample - %s", szName);
//                  
//                    WCHAR wszUniq[200];
//                    MultiByteToWideChar(CP_ACP, 0, szUniq, -1, wszUniq, NUMELMS(wszUniq));
//
//                    hr = pFm2->UnregisterFilter(
//                        &CLSID_VideoInputDeviceCategory,
//                        wszUniq,
//                        CLSID_VidCap);
//
//                } //capGetDriverDescription
//
//            } // for loop
//
//            pFm2->Release();
//        } // CoCreateInstance
//      
//    } // AMovieDllRegisterServer2
//
//    return hr;
//}
//
//
////
//// CVidCap::Constructor
////
//// don't create any pins yet, until we are told which device to use
//// (through IPersistPropertyBag or CPersistStream)
//CVidCap::CVidCap(TCHAR *pName, LPUNKNOWN lpunk, HRESULT *phr)
//    : CSource(pName, lpunk, CLSID_VidCap),
//      CPersistStream(lpunk, phr),
//      m_pCapturePin(NULL),
//      m_pOverlayPin(NULL),
//      m_pPreviewPin(NULL),
//      m_iVideoId(-1)
//{
//
//    CAutoLock l(&m_cStateLock);
//
//    DbgLog((LOG_TRACE, 1, TEXT("CVidCap filter created")));
//}
//
//
////
//// CVidCap::Destructor
////
//CVidCap::~CVidCap(void)
//{
//    DbgLog((LOG_TRACE, 1, TEXT("CVidCap filter destroyed")) );
//
//    if (m_pCapturePin)
//        delete m_pCapturePin;
//    if (m_pOverlayPin)
//        delete m_pOverlayPin;
//    if (m_pPreviewPin)
//        delete m_pPreviewPin;
//}
//
//
////
//// CreateInstance
////
//// Called by CoCreateInstance to create a vidcap filter.
//CUnknown * WINAPI CVidCap::CreateInstance(LPUNKNOWN lpunk, HRESULT *phr)
//{
//
//    CUnknown *punk = new CVidCap(TEXT("Video capture filter"), lpunk, phr);
//    if (punk == NULL) {
//        *phr = E_OUTOFMEMORY;
//    }
//    return punk;
//}
//
//
//// give out our interfaces
//STDMETHODIMP CVidCap::NonDelegatingQueryInterface(REFIID riid, void ** ppv)
//{
//    if (riid == IID_IAMVfwCaptureDialogs) {
//        return GetInterface((LPUNKNOWN)(IAMVfwCaptureDialogs *)this, ppv);
//    } else if (riid == IID_IPersistPropertyBag) {
//        return GetInterface((IPersistPropertyBag *)this, ppv);
//    } else if(riid == IID_IPersistStream) {
//        return GetInterface((IPersistStream *)this, ppv);
//    }
//
//   return CSource::NonDelegatingQueryInterface(riid, ppv);
//}
//
//
//// how many pins do we have? maybe 2, maybe 1, maybe 0
////
//int CVidCap::GetPinCount()
//{
//   DbgLog((LOG_TRACE,5,TEXT("CVidCap::GetPinCount")));
//
//   // we have a preview pin (one or the other)
//   if (m_pOverlayPin || m_pPreviewPin)
//	return 2;
//   else if (m_pCapturePin)
//	return 1;
//   else
//        return 0;
//}
//
//
//// Give out pointers to our pins.  We might have an overlay preview pin
//// or a non-overlay preview pin
////
//CBasePin * CVidCap::GetPin(int ii)
//{
//   DbgLog((LOG_TRACE,5,TEXT("CVidCap::GetPin")));
//
//   if (ii == 0 && m_pCapturePin)
//      return m_pCapturePin;
//   if (ii == 1 && m_pOverlayPin)
//      return m_pOverlayPin;
//   if (ii == 1 && m_pPreviewPin)
//      return m_pPreviewPin;
//   return NULL;
//}
//
//
//// IPersistPropertyBag stuff
////
//// Load is called to tell us what device to use.  There may be several
//// capture cards on the system that we could use
//STDMETHODIMP CVidCap::Load(LPPROPERTYBAG pPropBag, LPERRORLOG pErrorLog)
//{
//    HRESULT hr;
//    CAutoLock l(pStateLock());
//
//    DbgLog((LOG_TRACE,1,TEXT("Load...")));
//
//    // We already have some pins, thank you
//    if (m_pCapturePin)
//	return E_UNEXPECTED;
//
//    // Default to capture device #0
//    if (pPropBag == NULL) {
//        m_iVideoId = 0;
//        DbgLog((LOG_TRACE,1,TEXT("Using default device ID=%d"), m_iVideoId));
//        CreatePins(&hr);
//	return hr;
//    }
//
//    // find out what device to use
//    // different filters look in different places to find this info
//    VARIANT var;
//    var.vt = VT_I4;
//    hr = pPropBag->Read(L"AviCapIndex", &var, 0);
//    if(SUCCEEDED(hr))
//    {
//        hr = S_OK;
//        m_iVideoId = var.lVal;
//        DbgLog((LOG_TRACE,1,TEXT("Using device ID=%d"), m_iVideoId));
//        CreatePins(&hr);
//    }
//    return hr;
//}
//
//
//STDMETHODIMP CVidCap::Save(
//    LPPROPERTYBAG pPropBag, BOOL fClearDirty,
//    BOOL fSaveAllProperties)
//{
//    // E_NOTIMPL is not really a valid return code as any object implementing
//    // this interface must support the entire functionality of the
//    // interface.
//    return E_NOTIMPL;
//}
//
//
//// have we been initialized yet?  (Has somebody called Load)
//STDMETHODIMP CVidCap::InitNew()
//{
//   if(m_pCapturePin)
//   {
//       ASSERT(m_iVideoId != -1);
//       return HRESULT_FROM_WIN32(ERROR_ALREADY_INITIALIZED);
//   }
//   else
//   {
//       return S_OK;
//   }
//}
//
//
//// CPersistStream stuff
////
//// what is our class ID?
//STDMETHODIMP CVidCap::GetClassID(CLSID *pClsid)
//{
//    CheckPointer(pClsid, E_POINTER);
//    *pClsid = CLSID_VidCap;
//    return S_OK;
//}
//
//// CSource expects all its pins to derive from CSourceStream. Since
//// this sample doesn't do this, we implement QueryId and FindPin to
//// provide matching implementations
//
//STDMETHODIMP CVidCap::FindPin(
//    LPCWSTR Id, IPin ** ppPin)
//{
//    return CBaseFilter::FindPin(Id, ppPin);
//}
//
//
//HRESULT CVidCap::WriteToStream(IStream *pStream)
//{
//    ASSERT(m_iVideoId >= -1 && m_iVideoId < 10);
//    return pStream->Write(&m_iVideoId, sizeof(LONG), 0);
//}
//
//
//// what device should we use?  Used to re-create a .GRF file that we
//// are in
//HRESULT CVidCap::ReadFromStream(IStream *pStream)
//{
//    if(m_pCapturePin)
//    {
//        ASSERT(m_iVideoId != -1);
//        return HRESULT_FROM_WIN32(ERROR_ALREADY_INITIALIZED);
//    }
//
//    ASSERT(m_iVideoId == -1);
//
//    LONG iVideoId;
//    HRESULT hr = pStream->Read(&iVideoId, sizeof(LONG), 0);
//    if(FAILED(hr))
//        return hr;
//
//    m_iVideoId = iVideoId;
//    DbgLog((LOG_TRACE,1,TEXT("Using device ID=%d"), m_iVideoId));
//
//    hr = S_OK;
//    CreatePins(&hr);
//    return hr;
//}
//
//
//// How long is our data?  Just a long int (m_iVideoId)
//int CVidCap::SizeMax()
//{
//    return sizeof(LONG);
//}
//
//
//// Now we can create our output pins, after a device is chosen
////
//void CVidCap::CreatePins(HRESULT *phr)
//{
//    if (FAILED(*phr))
//        return;
//
//    CAutoLock l(pStateLock());
//
//    if (m_pCapturePin)
//        *phr = HRESULT_FROM_WIN32(ERROR_ALREADY_INITIALIZED);
//
//    ASSERT(m_iVideoId != -1);	// no device chosen yet?!
//
//    // Our capture pin MUST be called L"Capture"
//    m_pCapturePin = new CVidStream(NAME("Video capture stream"),
//					phr, this, m_iVideoId, L"Capture");
//    if (m_pCapturePin == NULL) {
//        *phr = E_OUTOFMEMORY;
//        return;
//    }
//
//    if (FAILED(*phr)) {
//	delete m_pCapturePin;
//	m_pCapturePin = NULL;
//        return;
//    }
//
//    // We can do overlay, so let's make a preview pin that does overlay
//    // Otherwise, do a preview pin that will fake up a preview
//    if (m_pCapturePin->m_HasOverlay)
//	m_pOverlayPin = CreateOverlayPin(phr);
//    else
//	m_pPreviewPin = CreatePreviewPin(phr);
//}
//
//
//// tell CBaseStreamControl what clock to use
////
//STDMETHODIMP CVidCap::SetSyncSource(IReferenceClock *pClock)
//{
//    if (m_pCapturePin)
//	m_pCapturePin->SetSyncSource(pClock);
//    if (m_pPreviewPin)
//	m_pPreviewPin->SetSyncSource(pClock);
//    return CSource::SetSyncSource(pClock);
//}
//
//
//// tell CBaseStreamControl what sink to use
////
//STDMETHODIMP CVidCap::JoinFilterGraph(IFilterGraph * pGraph, LPCWSTR pName)
//{
//    HRESULT hr = CSource::JoinFilterGraph(pGraph, pName);
//    if (hr == S_OK && m_pCapturePin)
//	m_pCapturePin->SetFilterGraph(m_pSink);
//    if (hr == S_OK && m_pPreviewPin)
//	m_pPreviewPin->SetFilterGraph(m_pSink);
//    return hr;
//}
//
//
//// we don't send any data during PAUSE, so to avoid hanging renderers, we
//// need to return VFW_S_CANT_CUE when paused
////
//STDMETHODIMP CVidCap::GetState(DWORD dwMSecs, FILTER_STATE *State)
//{
//    UNREFERENCED_PARAMETER(dwMSecs);
//    CheckPointer(State,E_POINTER);
//    ValidateReadWritePtr(State,sizeof(FILTER_STATE));
//
//    *State = m_State;
//    if (m_State == State_Paused)
//	return VFW_S_CANT_CUE;
//    else
//        return S_OK;
//}
//
//
//// Run
////
//// Activate the pin, letting it know that we are moving to State_Running
////
//STDMETHODIMP CVidCap::Run(REFERENCE_TIME tStart) {
//    CAutoLock l(pStateLock());
//
//    DbgLog((LOG_TRACE,2,TEXT("::Run")));
//
//    HRESULT hr;
//
//    m_tStart = tStart;  // remember the stream time offset
//
//    hr = CSource::Run(tStart);
//
//    // Tell CBaseStreamControl what's going on
//    m_pCapturePin->NotifyFilterState(State_Running, tStart);
//    if (m_pPreviewPin)
//        m_pPreviewPin->NotifyFilterState(State_Running, tStart);
//
//    if (SUCCEEDED(hr)) {
//	// start us running
//	m_pCapturePin->Run();
//	// overlay pin wants to know too
//	if (m_pOverlayPin && m_pOverlayPin->IsConnected())
//	    m_pOverlayPin->ActiveRun(tStart);
//	// preview pin wants to know too
//	if (m_pPreviewPin && m_pPreviewPin->IsConnected())
//	    m_pPreviewPin->ActiveRun(tStart);
//    }
//
//    return S_OK;
//}
//
//
////
//// Pause
////
//// Activate the pin, letting it know that Paused will
//// be the next state
//STDMETHODIMP CVidCap::Pause(void) {
//    CAutoLock l(pStateLock());
//
//    BOOL fWasStopped = FALSE;
//
//    if (m_State == State_Paused) {
//        return S_OK;
//    }
//
//    DbgLog((LOG_TRACE,2,TEXT("::Pause")));
//
//    // The video renderer will start blocking Deliver() when it goes from
//    // run to pause (or any filter could potentially do that) making us
//    // hang, so we need to tell it to release the sample by flushing
//    // it before we tell the thread to pause, or the thread will be hung
//    // and never get our message
//    if (m_State == State_Running) {
//        m_pCapturePin->DeliverBeginFlush();
//        m_pCapturePin->DeliverEndFlush();
//
//	// our overlay pin wants to know when we stop running
//	if (m_pOverlayPin)
//	    m_pOverlayPin->ActivePause();
//	// our preview pin wants to know when we stop running
//	if (m_pPreviewPin)
//	    m_pPreviewPin->ActivePause();
//    }
//
//    // we're streaming the graph now, so we better close our temporary
//    // window, so we can open it for real later, with the real # of buffers
//    if (m_State == State_Stopped && m_pCapturePin->m_hwCapCapturing) {
//        m_pCapturePin->DestroyCaptureWindow(m_pCapturePin->m_hwCapCapturing);
//        m_pCapturePin->m_hwCapCapturing = NULL;
//    }
//
//    // the capture pin is in fact going to start streaming... tell the
//    // preview pin to release the hardware
//    if (m_State == State_Stopped && m_pCapturePin->IsConnected()) {
//	fWasStopped = TRUE;
//	if (m_pPreviewPin)
//	    m_pPreviewPin->CapturePinActive(TRUE);
//    }
//
//    // need to change state before sending the pause request
//    // or the thread will not be created when we try to signal it.
//    HRESULT hr = CSource::Pause();
//    if (FAILED(hr)) {
//	// error, never mind
//	if (fWasStopped && m_pPreviewPin)
//	    m_pPreviewPin->CapturePinActive(FALSE);
//        return hr;
//    }
//
//    // the source stream base class seems to freak out if we pause it when not
//    // connected
//    if (m_pCapturePin->IsConnected()) {
//        // Tell CBaseStreamControl what's going on
//        m_pCapturePin->NotifyFilterState(State_Paused, 0);
//	if (m_pPreviewPin)
//            m_pPreviewPin->NotifyFilterState(State_Paused, 0);
//
//        hr = m_pCapturePin->Pause();
//	// error, never mind
//	if (FAILED(hr) && fWasStopped && m_pPreviewPin)
//	    m_pPreviewPin->CapturePinActive(FALSE);
//	return hr;
//    }
//
//    return NOERROR;
//}
//
//
////
//// Stop
////
//// Pass the current state to the pins Inactive method
//STDMETHODIMP CVidCap::Stop(void) {
//    HRESULT hr;
//    CAutoLock l(pStateLock());
//
//    DbgLog((LOG_TRACE,2,TEXT("::Stop")));
//
//    // Shame on the base classes, they don't take care of this, and it's very
//    // important for us that we go through pause on our way to stop
//    if (m_State == State_Running) {
//	hr = Pause();
//	if (FAILED(hr))
//	    return hr;
//    }
//
//    // Tell stream control we're stopped, so he will stop blocking the thread
//    // that captures the video (if we're in discarding mode).  Calling 
//    // CSource::Stop below will hang if the AVICAP capture thread is blocked,
//    // because it's going to destroy that thread.
//    m_pCapturePin->NotifyFilterState(State_Stopped, 0);
//    if (m_pPreviewPin)
//        m_pPreviewPin->NotifyFilterState(State_Stopped, 0);
//
//    hr = CSource::Stop();
//
//    // tell our preview pin that it can have the h/w if it wants it.
//    // we're through
//    if (m_pPreviewPin)
//	m_pPreviewPin->CapturePinActive(FALSE);
//
//    m_tStart = CRefTime(0L);
//
//    return hr;
//}
//
//
//// create the preview pin we use if we have overlay hardware
////
//CVidOverlay * CVidCap::CreateOverlayPin(HRESULT * phr)
//{
//   DbgLog((LOG_TRACE,2,TEXT("CreateOverlayPin")));
//
//   WCHAR wszPinName[16];
//   lstrcpyW(wszPinName, L"Preview");
//
//   CVidOverlay * pOverlay = new CVidOverlay(NAME("Video Overlay Stream"),
//				this, phr, wszPinName);
//   if (!pOverlay)
//      *phr = E_OUTOFMEMORY;
//
//   // if initialization failed, delete the stream array
//   // and return the error
//   //
//   if (FAILED(*phr) && pOverlay)
//      delete pOverlay, pOverlay = NULL;
//
//   return pOverlay;
//}
//
//
//// create the preview pin we use if we DO NOT have overlay hardware
////
//CVidPreview * CVidCap::CreatePreviewPin(HRESULT * phr)
//{
//   DbgLog((LOG_TRACE,2,TEXT("CreatePreviewPin")));
//
//   WCHAR wszPinName[16];
//   lstrcpyW(wszPinName, L"Preview");
//
//   CVidPreview * pPreview = new CVidPreview(NAME("Video Preview Stream"),
//				this, phr, wszPinName);
//   if (!pPreview)
//      *phr = E_OUTOFMEMORY;
//
//   // if initialization failed, delete the stream array
//   // and return the error
//   //
//   if (FAILED(*phr) && pPreview)
//      delete pPreview, pPreview = NULL;
//
//   return pPreview;
//}
//
//
//
////
//// IAMVfwCaptureDialogs  implementation
////
//
//// Does this driver support a particular dialog box?
////
//HRESULT CVidCap::HasDialog(int iDialog)
//{
//    if (iDialog == VfwCaptureDialog_Source)
//	return (m_pCapturePin->m_SupportsVideoSourceDialog ? S_OK : S_FALSE);
//    else if (iDialog == VfwCaptureDialog_Format)
//	return (m_pCapturePin->m_SupportsVideoFormatDialog ? S_OK : S_FALSE);
//    else if (iDialog == VfwCaptureDialog_Display)
//	return (m_pCapturePin->m_SupportsVideoDisplayDialog ? S_OK : S_FALSE);
//    else
//	return E_INVALIDARG;
//}
//
//
//// Show a particular dialog box of the driver
////
//HRESULT CVidCap::ShowDialog(int iDialog, HWND hwnd)
//{
//    HRESULT hr;
//
//    // we can't hold any critical sections while the dialog box is up
//
//    // before bringing up a dialog that could change our format, make
//    // sure we're not streaming
//    if (m_State != State_Stopped) {
//	return E_UNEXPECTED;
//    }
//
//    // !!! If the filter starts streaming while the dialog is up, this
//    // could cause problems! We aren't protecting against this
//
//    // open the device temporarily - if we don't put the hwnd in our
//    // m_hwCapCapturing variable, GetMediaType below won't get the
//    // format set by the dialog box
//    if (m_pCapturePin->m_hwCapCapturing == NULL)
//        m_pCapturePin->m_hwCapCapturing = m_pCapturePin->CreateCaptureWindow(0);
//    if (m_pCapturePin->m_hwCapCapturing == NULL)
//        return E_FAIL;
//
//    if (iDialog == VfwCaptureDialog_Source)
//	hr = capDlgVideoSource(m_pCapturePin->m_hwCapCapturing) == TRUE ?
//							NOERROR : E_FAIL;
//    else if (iDialog == VfwCaptureDialog_Format)
//	hr = capDlgVideoFormat(m_pCapturePin->m_hwCapCapturing) == TRUE ?
//							NOERROR : E_FAIL;
//    else if (iDialog == VfwCaptureDialog_Display)
//	hr = capDlgVideoDisplay(m_pCapturePin->m_hwCapCapturing) == TRUE ?
//							NOERROR : E_FAIL;
//    else {
//	hr = E_INVALIDARG;
//    }
//
//    // bringing up the Format Dialog can change the format we are capturing
//    // with.
//    if (hr == NOERROR && iDialog == VfwCaptureDialog_Format) {
//        DbgLog((LOG_TRACE,1,TEXT("Dialog changed our output format")));
//
//	// now get the new format chosen in the dialog.  First of all,
// 	// forget any remembered type in m_mt, so that GetMediaType will
//	// quiz the driver for the format set by the dialog
//	m_pCapturePin->m_mt.SetType(&GUID_NULL);
//	CMediaType cmt;
//	m_pCapturePin->GetMediaType(&cmt);
//	// now put the media type back in case this new type doesn't take
//	if (m_pCapturePin->IsConnected())
//	    m_pCapturePin->m_mt.SetType(&MEDIATYPE_Video);
//
//        // If we are connected to somebody, make sure they like it
//        if (m_pCapturePin->IsConnected())
//	    hr = m_pCapturePin->GetConnected()->QueryAccept(&cmt);
//	
//	if (hr == NOERROR) {
//	    hr = m_pCapturePin->SetMediaType(&cmt);
//            // Now reconnect us so the graph starts using the new format
//	    if (hr == NOERROR)
//                m_pCapturePin->Reconnect(TRUE);
//	}
//    }
//
//    // leave the driver open so we can connect quickly.  We'll close the driver
//    // before we start streaming
//
//    return hr;
//}
//
//
//// used to send secret messages to a capture driver. Use at your own risk
////
//HRESULT CVidCap::SendDriverMessage(int iDialog, int uMsg, long dw1, long dw2)
//{
//	return E_NOTIMPL;	// too scary
//}
//
//
//
//
//// *
//// * Implements CVidStream - manages the output pin
//// *
//
//
////
//// CVidStream::Constructor
////
//// keep the driver index to open.
//// Well behaved filters are supposed to only hold resources (like opening
//// the capture driver) when they are streaming, so we will open the
//// capture driver temporarily only (until we are streaming), and then close
//// it again right away.  Drivers usually have no memory across opens, so
//// we'll have to remember what format we want to capture with and when we
//// finally open it for real, send that format to the driver, as it will
//// have long forgotten.
//// !!! If we really cared about performance, we wouldn't open and close
//// the driver so much.  That takes forever.  Also, we do extra memory copies
//// that slow things down.  But this is only a sample driver
////
//CVidStream::CVidStream(TCHAR            *pObjectName
//                      , HRESULT         *phr
//                      , CVidCap         *pParentFilter
//                      , unsigned int    uiDriverIndex
//                      , LPCWSTR          pPinName
//                      )
//    :CSourceStream(pObjectName, phr, pParentFilter, pPinName),
//     m_uiDriverIndex(uiDriverIndex),
//     m_plFilled(NULL),
//     m_hwCapCapturing(NULL),
//     m_fSetFormatCalled(FALSE),
//     m_dwMicroSecPerFrame(66667),      // default to 15 fps
//     m_uiFramesCaptured(0),
//     m_uiFramesSkipped(0),
//     m_llTotalFrameSize(0),
//     m_uiFramesDelivered(0)
//    {
//
//    CAutoLock lock(m_pFilter->pStateLock());
//
//    // for IAMBufferNegotiation - no suggestions so far
//    m_propSuggested.cBuffers = -1;
//    m_propSuggested.cbBuffer = -1;
//    m_propSuggested.cbAlign = -1;
//    m_propSuggested.cbPrefix = -1;
//
//    // open the driver temporarily
//    m_hwCapCapturing = CreateCaptureWindow(0);
//    if (m_hwCapCapturing == NULL) {
//        *phr = E_FAIL;
//        return;
//    }
//
//    // get the name and version of the driver
//
//#ifdef UNICODE
//    capDriverGetName(m_hwCapCapturing, m_szName, sizeof(m_szName));
//    capDriverGetVersion(m_hwCapCapturing, m_szVersion, sizeof(m_szVersion));
//#else
//    char sz[giDriverNameStrLen];
//    capDriverGetName(m_hwCapCapturing, sz, sizeof(sz));
//    MultiByteToWideChar(CP_ACP, 0,
//                            sz, -1,
//                            m_szName, giDriverNameStrLen);
//
//    capDriverGetVersion(m_hwCapCapturing, sz, sizeof(sz));
//    MultiByteToWideChar(CP_ACP, 0,
//                            sz, -1,
//                            m_szVersion, giDriverVerStrLen);
//#endif
//    // Establish what dialogs this driver can display.
//
//    CAPDRIVERCAPS DriverCaps;
//    capDriverGetCaps(m_hwCapCapturing, &DriverCaps, sizeof(DriverCaps) );
//    m_SupportsVideoSourceDialog  = DriverCaps.fHasDlgVideoSource;
//    m_SupportsVideoDisplayDialog = DriverCaps.fHasDlgVideoDisplay;
//    m_SupportsVideoFormatDialog  = DriverCaps.fHasDlgVideoFormat;
//    m_HasOverlay  = DriverCaps.fHasOverlay;
//#if 0
//    m_SuppliesPalettes = DriverCaps.fDriverSuppliesPalettes;
//#endif
//
//    // leave the driver open so we can connect quickly.  We'll close the driver
//    // before we start streaming
//
//    DbgLog( (LOG_TRACE, 1, TEXT("CVidStream created") ) );
//}
//
//
////
//// CVidStream::Destructor
////
//// we should be inactive before this is called.
//CVidStream::~CVidStream(void) {
//
//    CAutoLock lock(m_pFilter->pStateLock());
//
//    ASSERT(!m_pFilter->IsActive());
//
//    if (m_hwCapCapturing)
//	DestroyCaptureWindow(m_hwCapCapturing);
//
//    DbgLog( (LOG_TRACE, 1, TEXT("CVidStream destroyed") ) );
//
//}
//
//
//// set the new media type
////
//HRESULT CVidStream::SetMediaType(const CMediaType* pmt)
//{
//    DbgLog((LOG_TRACE,2,TEXT("SetMediaType %x %dbit %dx%d"),
//		HEADER(pmt->Format())->biCompression,
//		HEADER(pmt->Format())->biBitCount,
//		HEADER(pmt->Format())->biWidth,
//		HEADER(pmt->Format())->biHeight));
//
//    ASSERT(((CVidCap *)m_pFilter)->m_State == State_Stopped);
//
//    // We are being told the frame rate to use.  It is in the VIDEOINFOHEADER
//    if (((VIDEOINFOHEADER *)(pmt->pbFormat))->AvgTimePerFrame) {
//	m_dwMicroSecPerFrame = (DWORD)(((VIDEOINFOHEADER *)(pmt->pbFormat))->
//						AvgTimePerFrame / 10);
//        DbgLog((LOG_TRACE,2,TEXT("SetMediaType: New frame rate is %d us per frame"),
//				m_dwMicroSecPerFrame));
//    }
//
//    // !!! The bit rate to use is in the VIDEOINFOHEADER too, but we can't
//    // obey it... we have no programmatic way of setting it, only through
//    // a dialog box
//
//    // now reconnect our preview pin to use the same format as us
//    Reconnect(FALSE);
//
//    // this will remember the media type in m_mt, and when we open the
//    // driver for real, we'll send it this format to use
//    return CSourceStream::SetMediaType(pmt);
//}
//
//
//// stop remembering what media type we are supposed to use once we start
//// streaming - all bets are off until we connect again... unless somebody
//// called SetFormat... we will always use that format from now on
////
//HRESULT CVidStream::BreakConnect()
//{
//    if (m_fSetFormatCalled == FALSE)
//        m_mt.SetType(&GUID_NULL);
//    return CSourceStream::BreakConnect();
//}
//
//
////
//// CheckMediaType
////
//// Queries the video driver to see if the format is acceptable
//// The only way to query if we support a given format is to set the driver
//// to use that format and see if it succeeds or fails.  So we better only
//// accept queries until we start streaming, because then we'd actually
//// affect the capture!
////
//HRESULT CVidStream::CheckMediaType(const CMediaType *pmt) {
//
//    DbgLog((LOG_TRACE,3,TEXT("CheckMediaType")));
//
//    CAutoLock l(&m_cSharedState);
//    CAutoLock lock(m_pFilter->pStateLock());
//
//    // bad idea to set the capture format while capturing...
//    if (((CVidCap *)m_pFilter)->m_State != State_Stopped)
//	return E_UNEXPECTED;
//
//    if (pmt == NULL || pmt->Format() == NULL) {
//        DbgLog((LOG_TRACE,3,TEXT("Rejecting: type/format is NULL")));
//	return E_INVALIDARG;
//    }
//
//    // we only support MEDIATYPE_Video
//    if (*pmt->Type() != MEDIATYPE_Video) {
//        DbgLog((LOG_TRACE,3,TEXT("Rejecting: not VIDEO")));
//	return E_INVALIDARG;
//    }
//
//    // check this is a VIDEOINFOHEADER type
//    if (*pmt->FormatType() != FORMAT_VideoInfo) {
//        DbgLog((LOG_TRACE,3,TEXT("Rejecting: format not VIDINFO")));
//        return E_INVALIDARG;
//    }
//
//    RECT rcS = ((VIDEOINFOHEADER *)pmt->Format())->rcSource;
//    RECT rcT = ((VIDEOINFOHEADER *)pmt->Format())->rcTarget;
//    if (!IsRectEmpty(&rcT) && (rcT.left != 0 || rcT.top != 0 ||
//			HEADER(pmt->Format())->biWidth != rcT.right ||
//			HEADER(pmt->Format())->biHeight != rcT.bottom)) {
//        DbgLog((LOG_TRACE,3,TEXT("Rejecting: can't use funky rcTarget")));
//        return VFW_E_INVALIDMEDIATYPE;
//    }
//    // We don't know what this would be relative to... reject everything
//    if (!IsRectEmpty(&rcS)) {
//        DbgLog((LOG_TRACE,3,TEXT("Rejecting: can't use funky rcSource")));
//        return VFW_E_INVALIDMEDIATYPE;
//    }
//
//    // open the driver temporarily
//    if (m_hwCapCapturing == NULL)
//        m_hwCapCapturing = CreateCaptureWindow(0);
//    if (m_hwCapCapturing == NULL)
//        return E_FAIL;
//
//    // the only way we can see if the driver supports a given format is to
//    // try and set it to use that format.
//    LPBITMAPINFOHEADER lpbiCheck = HEADER(pmt->Format());
//    DWORD dw = capSetVideoFormat(m_hwCapCapturing, lpbiCheck,
//		lpbiCheck->biSize +
//		((lpbiCheck->biBitCount > 8 || lpbiCheck->biClrUsed) ?
//		(lpbiCheck->biClrUsed * sizeof(PALETTEENTRY)) :
//		2 ^ lpbiCheck->biBitCount * sizeof(PALETTEENTRY)));
//
//    // leave the driver open so we can connect quickly.  We'll close the driver
//    // before we start streaming
//
//    return (dw == TRUE ? NOERROR : VFW_E_INVALIDMEDIATYPE);
//}
//
//
////
//// GetMediaType
////
//// Queries the video driver and places an appropriate media type in *pmt
//// If we have remembered a type that we are supposed to be using,
//// return that one
////
//HRESULT CVidStream::GetMediaType(CMediaType *pmt) {
//
//    CAutoLock l(&m_cSharedState);
//
//    // We've been told by somebody to use a particular media type.
//    // That's what we'll return
//    if (m_mt.IsValid()) {
//	*pmt = m_mt;
//	return NOERROR;
//    }
//
//    // We may or may not be streaming and have the driver open already
//    if (m_hwCapCapturing == NULL)
//        m_hwCapCapturing = CreateCaptureWindow(0);
//    if (m_hwCapCapturing == NULL)
//        return E_FAIL;
//
//    pmt->SetType(&MEDIATYPE_Video);
//    pmt->SetFormatType(&FORMAT_VideoInfo);
//
//    DWORD dwFormatSize;
//    VIDEOINFOHEADER *pvi;
//
//    dwFormatSize = capGetVideoFormatSize(m_hwCapCapturing);
//
//    ASSERT(dwFormatSize > 0);
//
//    // Find out how big we need to allocate the buffer
//#define AllocBufferSize (max(sizeof(VIDEOINFOHEADER) + sizeof(TRUECOLORINFO), \
//			dwFormatSize+offsetof(VIDEOINFOHEADER,bmiHeader)))
//
//    // Set up the format section of the mediatype to be the right size
//    pvi = (VIDEOINFOHEADER *) pmt->AllocFormatBuffer(AllocBufferSize);
//#undef AllocBufferSize
//    if (pvi == NULL) {
//        return E_OUTOFMEMORY;
//    }
//
//    // make sure all fields are initially zero
//    ZeroMemory((void *)pvi, sizeof(VIDEOINFOHEADER));
//
//    // make a note of the current fps we're doing
//    pvi->AvgTimePerFrame = m_dwMicroSecPerFrame * 10;
//
//    // grab the BITMAPINFOHEADER straight in
//    // will leave the memory after the last palette entry as zeros.
//    capGetVideoFormat(m_hwCapCapturing, &(pvi->bmiHeader), dwFormatSize);
//
//    const GUID SubTypeGUID = GetBitmapSubtype(&pvi->bmiHeader);
//    pmt->SetSubtype(&SubTypeGUID);
//    pmt->SetSampleSize(GetSampleSize(&pvi->bmiHeader));
//    pmt->SetTemporalCompression(FALSE);
//
//    // leave the driver open so we can connect quickly.  We'll close the driver
//    // before we start streaming
//
//    return NOERROR;
//}
//
//
////
//// OnThreadCreate
////
//// Start streaming & reset time samples are stamped with.
//HRESULT CVidStream::OnThreadCreate(void) {
//
//    CAutoLock l(&m_cSharedState);
//
//    m_ThreadState = Stopped;
//
//    // we are starting to stream now.  Open the capture driver FOR REAL!
//    // Use however many buffers we're supposed to use
//    ASSERT(m_hwCapCapturing == NULL);
//    m_hwCapCapturing = CreateCaptureWindow(m_propActual.cBuffers);
//    if (m_hwCapCapturing == NULL) {
//            return E_FAIL;
//    }
//
//    // m_mt is the format we connected with.  Tell the driver to use
//    // that format as its capture format
//    LPBITMAPINFOHEADER lpbi = HEADER(m_mt.Format());
//    DWORD dwSize = lpbi->biSize +
//		((lpbi->biBitCount > 8 || lpbi->biClrUsed) ?
//		(lpbi->biClrUsed * sizeof(PALETTEENTRY)) :
//		(2 ^ lpbi->biBitCount * sizeof(PALETTEENTRY)));
//    DWORD dw = capSetVideoFormat(m_hwCapCapturing, lpbi, dwSize);
//    ASSERT(dw == TRUE);	// we were promised this would work!
//
//    m_plFilled = new CVideoBufferList( m_mt.lSampleSize
//                                     , m_dwMicroSecPerFrame
//                                     , (CVidCap *)m_pFilter
//				     , m_propActual.cBuffers
//                                     );
//    if (m_plFilled == NULL) {
//        return E_OUTOFMEMORY;
//    }
//
//    // IAMDroppedFrames:  every time you start streaming, reset your stats
//    m_uiFramesCaptured = 0;
//    m_uiFramesSkipped = 0;
//    m_llTotalFrameSize = 0;
//    m_uiFramesDelivered = 0;
//
//    return NOERROR;
//}
//
//
//// Inactive - overridden to replace the call to CAMThread::Close with
//// different code that will dispatch messages waiting for the thread to
//// die.  (explained below)
////
//HRESULT CVidStream::Inactive(void) {
//
//    CAutoLock lock(m_pFilter->pStateLock());
//
//    HRESULT hr;
//
//    // do nothing if not connected - its ok not to connect to
//    // all pins of a source filter
//    if (!IsConnected()) {
//        return NOERROR;
//    }
//
//    // !!! need to do this before trying to stop the thread, because
//    // we may be stuck waiting for our own allocator!!!
//
//    hr = CBaseOutputPin::Inactive();  // call this first to Decommit the allocator
//    if (FAILED(hr)) {
//	return hr;
//    }
//
//    if (ThreadExists()) {
//	hr = Stop();
//
//	if (FAILED(hr)) {
//	    return hr;
//	}
//
//	hr = Exit();
//	if (FAILED(hr)) {
//	    return hr;
//	}
//
//        // when our main thread shuts down the capture thread, the capture
//        // thread will destroy the capture window it made, and this will cause
//	// USER to send messages to our main thread, and if we're blocked
//	// waiting for the capture thread to go away, we will deadlock
//	// preventing user from sending us the messages, and thus our thread
//	// will never go away.  We need to dispatch messages while waiting
//        if (m_hThread) {
//	    // This helper function will only dispatch Sent messages, not
//	    // posted ones, because that could cause scary re-entrancy and hangs
//            WaitDispatchingMessages(m_hThread, INFINITE, NULL, 0);
//            CloseHandle(m_hThread);
//            m_hThread = 0;
//        }
//    }
//
//    // hr = CBaseOutputPin::Inactive();  // call this first to Decommit the allocator
//    //if (FAILED(hr)) {
//    //	return hr;
//    //}
//
//    return NOERROR;
//}
//
//
////
//// OnThreadDestroy
////
//// Free the list of completed buffers and stop streaming
//// Attempts to stop streaming and destroy the window, even in error
//// cases.
//HRESULT CVidStream::OnThreadDestroy(void) {
//
//    CAutoLock l(&m_cSharedState);
//
//    ASSERT(m_ThreadState == Stopped);
//
//    // no longer streaming.  close the driver
//    BOOL fWindowGone = DestroyCaptureWindow(m_hwCapCapturing);
//    m_hwCapCapturing = NULL;
//
//    delete m_plFilled, m_plFilled = NULL;
//
//    DbgLog((LOG_TRACE, 1, TEXT("Frames Captured: %d"), m_uiFramesCaptured));
//    DbgLog((LOG_TRACE, 1, TEXT("Frames Skipped: %d"), m_uiFramesSkipped));
//    DbgLog((LOG_TRACE, 1, TEXT("Frames Delivered: %d"), m_uiFramesDelivered));
//
//    if (!fWindowGone) {
//        return E_UNEXPECTED;
//    } else {
//        return NOERROR;
//    }
//}
//
//
////
//// DecideBufferSize
////
//// Get an allocator that we have been asked to get (through
//// IAMBufferNegotiation) or that we will be happy with
//// Check that the allocator can give us appropriately sized buffers
//// Always called after format negotiation.
////
//HRESULT CVidStream::DecideBufferSize(IMemAllocator *pAlloc,
//                                     ALLOCATOR_PROPERTIES *pProperties)
//{
//    CAutoLock lock(m_pFilter->pStateLock());
//    CAutoLock l(&m_cSharedState);
//    ASSERT(pAlloc);
//    ASSERT(pProperties);
//    HRESULT hr = NOERROR;
//
//    // use the app's numbers, if we were given some through IAMBufferNegotiation
//    // otherwise, use some default
//
//    if (m_propSuggested.cBuffers > 0)
//        pProperties->cBuffers = m_propSuggested.cBuffers;
//    else
//        pProperties->cBuffers = 32;	// !!!	or whatever
//
//    if (m_propSuggested.cbBuffer > 0)
//        pProperties->cbBuffer = m_propSuggested.cbBuffer;
//    else
//        pProperties->cbBuffer = m_mt.lSampleSize;
//
//    if (m_propSuggested.cbAlign > 0)
//        pProperties->cbAlign = m_propSuggested.cbAlign;
//
//    if (m_propSuggested.cbPrefix > 0)
//        pProperties->cbPrefix = m_propSuggested.cbPrefix;
//
//    ASSERT(pProperties->cbBuffer);
//
//    // Ask the allocator to reserve us some sample memory, NOTE the function
//    // can succeed (that is return NOERROR) but still not have allocated the
//    // memory that we requested, so we must check we got whatever we wanted
//
//    ALLOCATOR_PROPERTIES Actual;
//    hr = pAlloc->SetProperties(pProperties,&Actual);
//    if (SUCCEEDED(hr)) {
//
//        // Is this allocator unsuitable
//        if (Actual.cbBuffer < pProperties->cbBuffer) {
//            hr = E_FAIL;
//        }
//
//	// remember what properties the allocator is using... somebody
//	// might ask later
//	m_propActual = Actual;
//    }
//    return hr;
//}
//
//
////
//// GetSampleSize
////
//// Given a BITMAPINFOHEADER, calculates the sample size needed.
//long CVidStream::GetSampleSize(LPBITMAPINFOHEADER pbmi) {
//
//    long lSize;
//
//    if (pbmi->biSizeImage > 0) {
//
//        lSize = pbmi->biSizeImage;
//
//    }
//    else {      // biSizeImage is allowed to be zero for uncompressed formats,
//                // so do the maths ourselves...
//
//        lSize = (pbmi->biWidth *
//                 pbmi->biHeight *
//                 pbmi->biBitCount) / 8 + 1;
//        if (lSize < 0) { // biHeight was negative
//            lSize *= -1;
//        }
//    }
//
//    // round up to nearest DWORD
//    int rem = lSize % sizeof(DWORD);
//    if (rem)
//        lSize += sizeof(DWORD) - rem;
//
//    return lSize;
//}
//
//
////
//// CreateCaptureWindow
////
//// Create a hidden AVICap window, and make sure it is configured appropriately
//// Returns NULL on failure.
//// successful calls should be balanced with calls to DestroyCaptureWindow()
//// Creates the AVICap window with (up to) lBufferCount no. of buffers.
//// use lBufferCount = 0, when you only wish to interrgoate the the driver, or
//// specify a number of buffers, if you actually want to capture.
////
//HWND CVidStream::CreateCaptureWindow(long lBufferCount)
//{
//
//    CAutoLock lock(&m_cSharedState);
//
//    BOOL bErr;  //return code of capXXX calls
//
//    HWND hwndCapture;   // The window to return
//
//    hwndCapture = capCreateCaptureWindow(NULL,       // No name
//                                         0,          // no style.
//                                                     // defaults to invisible
//                                         0, 0, 150, 150, // an arbitrary size
//                                         0,          // no parent
//                                         0);         // don't care about the id
//
//    if (!hwndCapture) {
//        DbgLog((LOG_ERROR|LOG_TRACE, 1, TEXT("Window could not be created") ));
//        return NULL;
//    }
//
//    bErr = capDriverConnect(hwndCapture, m_uiDriverIndex);
//    if (!bErr) {
//        DestroyWindow(hwndCapture);
//        DbgLog((LOG_ERROR|LOG_TRACE, 1, TEXT("Driver failed to connect") ) );
//        return NULL;
//    }
//    DbgLog((LOG_TRACE, 2, TEXT("Driver Connected") ));
//
//    CAPTUREPARMS cp;
//    capCaptureGetSetup(hwndCapture, &cp, sizeof(cp) ); // get the current defaults
//
//    cp.dwRequestMicroSecPerFrame = m_dwMicroSecPerFrame; // Set desired frame rate
//    cp.fMakeUserHitOKToCapture   = FALSE;
//    cp.fYield                    = TRUE;                 // we want capture on a
//                                                         // background thread.
//    cp.wNumVideoRequested        = (WORD) lBufferCount;  // we may get less than
//                                                         // this - no problem
//    cp.fCaptureAudio             = FALSE;
//    cp.vKeyAbort                 = 0;                    // If no key is provided,
//                                                         // it won't stop...
//    cp.fAbortLeftMouse           = FALSE;
//    cp.fAbortRightMouse          = FALSE;
//    cp.fLimitEnabled             = FALSE;                // we want to stop
//    cp.fMCIControl               = FALSE;
//
//    capCaptureSetSetup(hwndCapture, &cp, sizeof(cp) );
//
//    capSetCallbackOnVideoStream(hwndCapture, &VideoCallback);
//    capSetCallbackOnFrame(hwndCapture, &VideoCallback);  // also use for single
//                                                         // frame capture
//
//#if 0
//    CAPSTATUS    cs;
//    ZeroMemory(&cs, sizeof(cs));
//    capGetStatus(hwndCapture, &cs, sizeof(cs));
//
//    // try to see if the driver uses palettes
//    if (((cs.hPalCurrent != NULL) || (cs.fUsingDefaultPalette))) {
//	m_UsesPalettes = TRUE;
//    } else {
//        m_UsesPalettes = FALSE;
//    }
//    if (m_UsesPalettes && m_SuppliesPalettes) {
//	capPaletteAuto(hwndCapture, 10, 236);
//    }
//#endif
//
//    SetWindowLong(hwndCapture, GWL_USERDATA, (LONG) this);
//
//    return hwndCapture;
//}
//
//
////
//// DestroyCaptureWindow()
////
//// Disconnect the driver before destroying the window.
//BOOL CVidStream::DestroyCaptureWindow(HWND hwnd)
//{
//
//    ASSERT(hwnd != NULL);
//
//    // !!! why is this failing?
//    BOOL bDriverDisconnected = capDriverDisconnect(hwnd);
//    DbgLog(( LOG_ERROR|LOG_TRACE, 2
//           , TEXT("Driver disconnect: %x"), bDriverDisconnected) );
//
//    BOOL bWindowGone = DestroyWindow(hwnd);
//    DbgLog((LOG_ERROR|LOG_TRACE, 2, TEXT("Window destroy: %x"), bWindowGone) );
//
//    return (bDriverDisconnected && bWindowGone);
//}
//
//
////
//// VideoCallback
////
//// The AVICap Video callback. Keep a copy of the buffer we are given
//// May be called after the worker thread, or even the pin has gone away,
//// depending on AVICap's internal timing. Therefore be very careful with the
//// pointers we use.
////
//LRESULT CALLBACK CVidStream::VideoCallback(HWND hwnd, LPVIDEOHDR lpVHdr)
//{
//
//    CVidStream *pThis = (CVidStream *) GetWindowLong(hwnd, GWL_USERDATA);
//
//    ASSERT(pThis);
//
//    if (pThis->m_plFilled == NULL) {    // The filled list has gone away.
//                                        // ignore this buffer
//        return (LRESULT) TRUE;
//    }
//    else {
//        pThis->m_plFilled->Add(lpVHdr);
//    }
//
//    //DbgLog((LOG_TRACE,5,TEXT("Got a buffer back!")));
//
//    return (LRESULT) TRUE;
//}
//
//
//
//// Override to handle quality messages
//STDMETHODIMP CVidStream::Notify(IBaseFilter * pSender, Quality q)
//{
//    // if q.Late.RefTime.QuadPart >0 then skip ahead that much.
//    // thereafter adjust the time per frame by a factor of
//    // 1000/q.Proportion (watch for truncation of fractions
//    // do the multiply first!
//
//    // Not Yet Implemented :-)
//
//    return NOERROR;
//}
//
//// CSource expects all its pins to derive from CSourceStream. Since
//// this sample doesn't do this, we implment QueryId here and FindPin
//// on the filter to provide matching implementations
//
//STDMETHODIMP CVidStream::QueryId(
//    LPWSTR * Id)
//{
//    return CBaseOutputPin::QueryId(Id);
//}
//
//
//
//
////
//// DoBufferProcessingLoop
////
//// Replace the loop in CSourceStream with something of my own, so that I can
//// wait for buffers & commands.
//HRESULT CVidStream::DoBufferProcessingLoop(void) {
//
//    DbgLog((LOG_TRACE,2,TEXT("*** Entering DoBufferProcessingLoop")));
//
//    HANDLE haWaitObjects[2];
//    {
//        CAutoLock l(&m_cSharedState);
//
//        haWaitObjects[0] = GetRequestHandle();  // command handle first so that
//                                                // it has priority over buffers
//        haWaitObjects[1] = m_plFilled->GetWaitHandle();
//    }
//
//    for (;;) {
//
//        // wait for commands or buffers.  This thread created the capture
//	// window, so we have to dispatch messages frequently on this thread
//	// or any other thread sending any message to the capture window will
//	// hang
//	// !!! It's more efficient to use MsgWaitForMultipleObjects, I know
//        DWORD dwWaitObject =WaitForMultipleObjects(2, haWaitObjects, FALSE, 50);
//
//	// If we're busy capturing at a high frame rate, we'll never time out.
//	// ALWAYS dispatch
//	MSG msg;
//	while (PeekMessage(&msg, NULL, 0, 0, PM_REMOVE)) {
//	    TranslateMessage(&msg);
//	    DispatchMessage(&msg);
//	}
//
//	if (dwWaitObject == WAIT_OBJECT_0) {    // thread command request
//
//	    Command com;
//	
//            EXECUTE_ASSERT(CheckRequest(&com));
//            switch (com) {
//            case CMD_RUN:
//
//		com = GetRequest();
//
//                if (m_ThreadState != Running) {
//                    DbgLog((LOG_TRACE,1,TEXT("*** STARTING CAPTURE")));
//		    // !!! This function may take a while to initialize 
//		    // capture, so we may not start seeing frames for 1/3 of
//		    // a second or so.  Unfortunately, unless we want a dialog
//		    // box to pop up saying "press OK to start" there's no good
//		    // way of starting capture at any known point in time
//                    capCaptureSequenceNoFile(m_hwCapCapturing);
//                }
//                m_ThreadState = Running;
//                Reply(NOERROR);
//                break;
//
//            case CMD_PAUSE:
//
//                DbgLog((LOG_TRACE,1,TEXT("CMD_PAUSE")));
//
//		com = GetRequest();
//		
//                switch (m_ThreadState) {
//                case Stopped:
//		    // first thing sent will be a discontinuity
//		    m_plFilled->m_fLastSampleDiscarded = TRUE;
//
//		    // init our time stamping variables
//		    m_plFilled->m_rtLastStartTime = -1;
//		    m_plFilled->m_llLastFrame = -1;
//		    m_plFilled->m_llFrameOffset = 0;
//		    m_plFilled->m_fReRun = FALSE;
//
//		    m_ThreadState = Paused; // mark that we are paused
//                    break;
//                case Running:
//                    DbgLog((LOG_TRACE,1,TEXT("*** STOPPING CAPTURE")));
//		    // we just went from RUN to PAUSE.  If we go to
//		    // RUN again without STOPping first, we will be in
//		    // a situation where the graph was run->pause->run
//		    m_plFilled->m_fReRun = TRUE;
//                    capCaptureStop(m_hwCapCapturing);
//                    break;
//                default:
//                    // null op
//                    break;
//                }
//                m_ThreadState = Paused;
//                Reply(NOERROR);
//                break;
//
//            case CMD_STOP:
//
//                if (m_ThreadState == Running) {
//                    DbgLog((LOG_TRACE,1,TEXT("*** STOPPING CAPTURE")));
//                    capCaptureStop(m_hwCapCapturing);
//                }
//                m_ThreadState = Stopped;
//                //DbgLog((LOG_TRACE, 1, TEXT("Seen Stop command")));
//                // don't reply here as that is done by CSourceStream
//                return NOERROR;
//
//            default:
//
//                ASSERT(!"Unexpected thread command");
//		com = GetRequest();
//                break;
//            }
//
//        } else if (dwWaitObject == (WAIT_OBJECT_0 + 1)) { // m_plFilled
//
//            // Process the buffer we've just been signalled on
//            IMediaSample *pSample;
//
//            // get a buffer to put this video data in
//            HRESULT hr = GetDeliveryBuffer(&pSample,NULL,NULL,0);
//            if (FAILED(hr)) {
//                continue;
//            }
//
//            hr = FillBuffer(pSample);
//
//	    // !!! If this fails, we're supposed to stop delivering
//            Deliver(pSample);
//
//            pSample->Release();
//
//	// get out - otherwise we will infinite loop...
//        } else if (dwWaitObject != WAIT_TIMEOUT) {
//
//            DbgLog((LOG_TRACE, 1
//                   , TEXT("Unexpected buffer/command wait return: %d")
//                   , dwWaitObject));
//            return E_UNEXPECTED;
//        }
//    }
//
//    ASSERT(m_ThreadState == Stopped);
//
//    return NOERROR;
//}
//
//
////
//// FillBuffer
////
//// Take the buffer from the head of the video buffer list.
//// We will only be called when there is such a buffer
////
//HRESULT CVidStream::FillBuffer(IMediaSample *pSample) {
//
//    CAutoLock l(&m_cSharedState);
//
//    HRESULT hr = m_plFilled->RemoveHeadIntoSample(pSample);
//
//    return hr;
//}
//
//
//
////
//// NonDelegatingQueryInterface
////
//// Expose all of our interfaces on the pin
////
//STDMETHODIMP CVidStream::NonDelegatingQueryInterface(REFIID riid, void ** ppv)
//{
//    CheckPointer(ppv,E_POINTER);
//
//    if (riid == IID_IAMStreamControl) {
//	return GetInterface((LPUNKNOWN)(IAMStreamControl *)this, ppv);
//    } else if (riid == IID_IAMStreamConfig) {
//	return GetInterface((LPUNKNOWN)(IAMStreamConfig *)this, ppv);
//    } else if (riid == IID_IAMVideoCompression) {
//	return GetInterface((LPUNKNOWN)(IAMVideoCompression *)this, ppv);
//    } else if (riid == IID_IAMDroppedFrames) {
//	return GetInterface((LPUNKNOWN)(IAMDroppedFrames *)this, ppv);
//    } else if (riid == IID_IAMBufferNegotiation) {
//	return GetInterface((LPUNKNOWN)(IAMBufferNegotiation *)this, ppv);
//    } else if (riid == IID_IKsPropertySet) {
//	return GetInterface((LPUNKNOWN)(IKsPropertySet *)this, ppv);
//    }
//
//    return CSourceStream::NonDelegatingQueryInterface(riid, ppv);
//}
//
//
//// *
//// * CVideoBufferList
//// *
//
//
////
//// CVideoBufferList::Constructor
////
//CVideoBufferList::CVideoBufferList( int iBufferSize
//                                  , DWORD dwMicroSecPerFrame
//                                  , CVidCap *pFilter
//                                  , int iBuffers
//                                  )
//    :m_dwMicroSecPerFrame(dwMicroSecPerFrame),
//     m_FirstBuffer(TRUE),
//     m_pFilter(pFilter),
//     m_evList(TRUE),
//     m_iPreviewCount(0),
//     m_lFilled(NAME("Pending, full, buffers"),
//               DEFAULTCACHE),   // default cache
//     m_lFree(NAME("Empty buffers"),
//               DEFAULTCACHE)    // default cache
//{
//    for (int i=0; i < iBuffers; i++) {
//
//        CBuffer *pBuffer = new CBuffer(iBufferSize);
//        if (pBuffer == NULL) {
//            return;
//        }
//        m_lFree.AddTail(pBuffer);
//
//    }
//}
//
//
////
//// CVideoBufferList::Destructor
////
//CVideoBufferList::~CVideoBufferList() {
//
//    while (m_lFree.GetCount() > 0) {            // free buffers on the free list...
//
//        CBuffer *pBuff = m_lFree.RemoveHead();
//        delete pBuff;
//    }
//
//
//    DbgLog((LOG_TRACE, 1
//           , TEXT("Filled frames not sent before stop issued: %d")
//           , m_lFilled.GetCount()));
//
//    while (m_lFilled.GetCount() > 0) { //... then free buffers on filled list
//                                       // - we don't care about the data they hold.
//
//        CBuffer *pBuff = m_lFilled.RemoveHead();
//        delete pBuff;
//    }
//}
//
//
////
//// Add
////
//// Add a video buffer to this list. gets a free buffer from m_lFree, copies
//// the video data into it and then puts it on m_lFilled.
//// if m_lFree is empty, fail silently, effectively skipping the buffer.
//HRESULT CVideoBufferList::Add(LPVIDEOHDR lpVHdr) {
//
//    CAutoLock lck(&m_ListCrit);
//    CRefTime rt;
//
//    //DbgLog((LOG_TRACE,4,TEXT("driver time: %dms"), lpVHdr->dwTimeCaptured));
//
//    // Time stamp this sample with the graph's clock time when this sample
//    // is captured. (no clock? use the driver time)
//    // !!! hopefully the driver latency isn't too bad! It should really be
//    // compensated for
//    if (FAILED(m_pFilter->StreamTime(rt)))		// current time
//        rt = CRefTime((LONG)lpVHdr->dwTimeCaptured);    // init with ms
//
//    // now ask IAMStreamControl if we need to bother delivering this sample
//    // I don't have a sample around right now, so I'll make up one.  All it
//    // needs is the time stamp.
//    // !!! this is kinda hacky
//    CMemAllocator all(TEXT("Test"), NULL, NULL);
//    CMediaSample Sample(NULL, &all, NULL);
//    CRefTime rtEnd = rt + CRefTime((LONGLONG)m_dwMicroSecPerFrame * 10);
//    Sample.SetTime((REFERENCE_TIME *)&rt, (REFERENCE_TIME *)&rtEnd);
//    int iStreamState = m_pFilter->m_pCapturePin->CheckStreamState(&Sample);
//    if (iStreamState == m_pFilter->m_pCapturePin->STREAM_FLOWING) {
//	  // DbgLog((LOG_TRACE,3,TEXT("*VIDCAP ON")));
//    } else {
//	  // DbgLog((LOG_TRACE,3,TEXT("*VIDCAP OFF")));
//	  m_fLastSampleDiscarded = TRUE;	// next one is discontinuity
//    }
//
//    // we are faking up a preview pin and supposed to send it a frame to
//    // give out every once in a while when we have free time and copying
//    // the data for preview will NOT HURT CAPTURE PERFORMANCE.  Well, if
//    // our capture pin is currently off, we have nothing to lose.  If
//    // we have no frames on the filled list pending delivery, we'll assume
//    // we have spare time and can afford to send a preview frame.  In any
//    // case, send a preview frame at least once every 30 frames. (!!!)
//    // !!! Ideally, we don't necessarily want to send this frame as a preview,
//    // it may be many seconds old if we have lots of buffering, we want to
//    // send the most recently captured frame.  I don't do that here.
//    if (m_pFilter->m_pPreviewPin) {
//	if (iStreamState == m_pFilter->m_pCapturePin->STREAM_DISCARDING ||
//		m_lFilled.GetCount() == 0 || m_iPreviewCount++ == 30) {
//	    m_iPreviewCount = 0;	// reset
//	    m_pFilter->m_pPreviewPin->ReceivePreviewFrame(lpVHdr->lpData,
//					lpVHdr->dwBytesUsed);
//	}
//    }
//
//    // what frame number is this (based on the time captured)?  Round
//    // such that if frames 1 and 2 are expected at 33 and 66ms, anything
//    // from 17 to 49 will be considered frame 1.
//    //
//    // frame = (us + 1/2(us per frame)) / (us / frame)
//    //
//    // then we add an offset if we so desire
//    //
//    LONGLONG llFrame = ((lpVHdr->dwTimeCaptured * 1000 +
//			m_dwMicroSecPerFrame / 2) / m_dwMicroSecPerFrame)
//			+ m_llFrameOffset;
//
//    // NOTE:  If the graph is RUN->PAUSE->RUN again we need to
//    // continue sending frame numbers (in the MediaTime) where we
//    // left off before pausing.  BUT... the driver has been stopped
//    // and started and is numbering the frames back at zero again!
//    // So we need to notice that the graph has been paused and re-run
//    // without stopping in between, and then add as an offset to all
//    // future frame numbers, the last frame number sent before the
//    // pause.
//    if (m_fReRun && llFrame < m_llLastFrame) {
//	m_llFrameOffset = m_llLastFrame;
//	llFrame += m_llFrameOffset;
//	m_fReRun = FALSE;
//    }
//
//    // the time stamps we get from the drivers are not always accurate,
//    // and we may think we see frame 0, 2, 2, 3, 5, 5, 6, 7, but if
//    // we just pretended the first 2 was frame 1 and the first 5 was
//    // frame 4, we wouldn't skip any frames or send the same frame
//    // twice
//    if (llFrame == m_llLastFrame + 2)
//	llFrame -= 1;
//
//    // we're supposed to deliver this frame, and we can
//    if (iStreamState == m_pFilter->m_pCapturePin->STREAM_FLOWING &&
//						m_lFree.GetCount() > 0) {
//
//	// Never send something backwards in time from the last thing
//	// sent.  This can happen with live sources if you run, then
//	// pause, then run the graph again.  The first time stamp after
//	// the second run might have a time less than the last time stamp
//	// delivered before the pause.  Also, because the graph is run
//	// 50ms or so before time 0, it's possible for a frame captured
//	// right away to have a time stamp less than zero.  We don't want
//	// to deliver such samples, but we don't want to consider them
//	// dropped frames either.  We just pretend they never existed.
//	if (rt >= 0 && rt >= m_rtLastStartTime) {
//
//	    // Just like above, where we didn't deliver or skip any frame
//	    // whose time stampes seemed to go backwards in time, we can't
//	    // deliver a frame whose MediaTime stamps don't increase
//	    // monotonically.  We don't want to deliver a duplicate media time
//	    // that we already sent, but we don't want to consider them
//	    // dropped frames either.  We just pretend they never existed.
//	    if (llFrame > m_llLastFrame) {
//
//		// oops, this frame number is more than one frame later than
//		// the last frame; we've skipped some frames, probably due
//		// to starving the driver who couldn't capture in time
//		if (llFrame > m_llLastFrame + 1) {
//        	    m_pFilter->m_pCapturePin->m_uiFramesSkipped += 
//					(DWORD)(llFrame - m_llLastFrame - 1);
//		}
//	
//                CBuffer *pBuff = m_lFree.RemoveHead();
//                pBuff->CopyBuffer(lpVHdr, rt, llFrame);
//
//	        // remember how to set the sample flags later
//	        pBuff->m_fSyncPoint = lpVHdr->dwFlags & VHDR_KEYFRAME;
//	        // !!! Technically speaking, you should set a discontinuity
//	        // after you drop (skip) a frame, too, but I'm probably dealing
//	        // with all keyframes and it doesn't matter so much if a frame
//	        // is skipped
//	        pBuff->m_fDiscontinuity = m_fLastSampleDiscarded;
//
//                m_lFilled.AddTail(pBuff);
//                if (m_lFilled.GetCount() == 1) {
//                    m_evList.Set();
//                }
//                m_pFilter->m_pCapturePin->m_uiFramesCaptured++;
// 	        m_fLastSampleDiscarded = FALSE;
//
//	        // remember the last Time and MediaTime we used
//	        m_rtLastStartTime = rt;
//	        m_llLastFrame = llFrame;
//	    }
//	}
//
//    // we're supposed to deliver this frame, but we can't... it's skipped
//    } else if (iStreamState == m_pFilter->m_pCapturePin->STREAM_FLOWING) {
//
//        m_pFilter->m_pCapturePin->m_uiFramesSkipped++;
//
//    // we are discarding this frame.  Don't count it as captured or skipped
//    // We must make a note that we've seen this frame, so we don't think that
//    // we skipped all the frames we didn't send because stream control was off
//    } else {
//	m_llLastFrame = llFrame;
//    }
//
//    return NOERROR;
//}
//
//
////
//// RemoveHeadIntoSample
////
//// Copy the head of the filled list into the supplied IMediaSample.
//// Fail with E_UNEXPECTED if called on an empty m_lFilled;
//// return S_FALSE if you don't want the sample delivered
////
//HRESULT CVideoBufferList::RemoveHeadIntoSample(IMediaSample *pSample)
//{
//    HRESULT hr;
//    CAutoLock lck(&m_ListCrit);
//
//    if (m_lFilled.GetCount() < 1) {
//        hr = E_UNEXPECTED;
//    } else {
//
//        CBuffer *pBuff = m_lFilled.RemoveHead();
//        if (m_lFilled.GetCount() == 0) {
//            m_evList.Reset();
//        }
//
//        BYTE *pSampleBuffer;
//        hr = pSample->GetPointer(&pSampleBuffer);
//        if (SUCCEEDED(hr)) {
//
//	    LONG lSampleSize = pBuff->GetSize();
//            ASSERT(pBuff->GetSize() <= pSample->GetSize());
//
//	    // Copy the captured data
//            CopyMemory((void *)pSampleBuffer, pBuff->GetPointer(),
//							pBuff->GetSize());
//
//            // set all the sample flags
//	    pSample->SetActualDataLength(lSampleSize);
//      	    pSample->SetSyncPoint(pBuff->m_fSyncPoint);
//      	    pSample->SetDiscontinuity(pBuff->m_fDiscontinuity);
//      	    pSample->SetPreroll(FALSE);
//
//
//            FILTER_STATE State;
//            m_pFilter->GetState(0, &State);
//
//            CRefTime rtStart;
//
//	    // set the time stamp of this sample
// 	    rtStart = pBuff->GetCaptureTime();
//            CRefTime rtEnd = rtStart + CRefTime((LONGLONG)m_dwMicroSecPerFrame *
//									10);
//            DbgLog((LOG_TRACE,4,TEXT("Sample Time: %dms %dms"),
//			rtStart.Millisecs(), rtEnd.Millisecs()));
//            ASSERT(rtStart <= rtEnd);
//            pSample->SetTime((REFERENCE_TIME*)&rtStart,
//                             (REFERENCE_TIME*)&rtEnd);
//
//	    // also set the media time... which is supposed to be the
//	    // frame number
// 	    LONGLONG llFrame = pBuff->GetCaptureFrame();
//	    LONGLONG llFrameE = llFrame + 1;
//            pSample->SetMediaTime(&llFrame, &llFrameE);
//            DbgLog((LOG_TRACE,4,TEXT("Media Time: %d"),llFrame));
//
//            m_pFilter->m_pCapturePin->m_uiFramesDelivered++;
//            m_pFilter->m_pCapturePin->m_llTotalFrameSize += lSampleSize;
//            m_lFree.AddTail(pBuff);
//        }
//    }
//
//    return hr;
//}
//
//
////
//// CBuffer::Constructor
////
////Get a new buffer of the maximum size we will handle
//CVideoBufferList::CBuffer::CBuffer(int iBufferSize) {
//
//    m_pData = new BYTE[iBufferSize];
//
//    // Set the two length fields to the maximum size
//    m_iCaptureDataLength = m_iDataLength = iBufferSize;
//
//}
//
//
////
//// CVideoDataBuffer::Destructor
////
//CVideoBufferList::CBuffer::~CBuffer() {
//
//    delete m_pData;
//}
//
//
////
//// CopyBuffer
////
//// Copy the supplied data in lpVHdr->lpData to this Buffer
//// rt is the time stamp
//void CVideoBufferList::CBuffer::CopyBuffer(LPVIDEOHDR lpVHdr, CRefTime& rt, LONGLONG llFrame) {
//
//    ASSERT((DWORD) m_iDataLength >= lpVHdr->dwBytesUsed);
//
//    m_rt = rt;		// time stamp
//    m_llFrame = llFrame;// frame number
//
//    // Copy the captured video buffer, and remember its length
//    CopyMemory(m_pData, lpVHdr->lpData, (m_iCaptureDataLength = lpVHdr->dwBytesUsed));
//
//}
//
//
//
//// Reconnect our pin.  We would like to change our output format
//// Sometimes extra filters have to be placed in between, like when you
//// switch from 8 bit to 16 bit RGB and you have to put a colour converter
//// between you and the renderer you were connected to.  In this case,
//// reconnect won't work, you have to explicitly intelligently connect
//// them yourself.
////
//void CVidStream::Reconnect(BOOL fCapturePinToo)
//{
//    if (IsConnected() && fCapturePinToo) {
//        DbgLog((LOG_TRACE,1,TEXT("Need to reconnect our streaming pin")));
//        CMediaType cmt;
//	GetMediaType(&cmt);
//	if (S_OK == GetConnected()->QueryAccept(&cmt)) {
//            ((CVidCap *)m_pFilter)->m_pGraph->Reconnect(this);
//	} else {
//            // This will fail if we switch from 8 bit to 16 bit RGB connected
//            // to a renderer that needs a colour converter inserted to do 16 bit
//	    // Oh boy.  We're going to have to get clever and insert some
//	    // filters between us to help us reconnect
//            DbgLog((LOG_TRACE,1,TEXT("Whoa! We *really* need to reconnect!")));
//	    IPin *pCon = GetConnected();
//	    pCon->AddRef();	// or it will go away in Disconnect
//	    ((CVidCap *)m_pFilter)->m_pGraph->Disconnect(GetConnected());
//	    ((CVidCap *)m_pFilter)->m_pGraph->Disconnect(this);
//	    IGraphBuilder *pFG;
//	    HRESULT hr = ((CVidCap *)m_pFilter)->m_pGraph->QueryInterface(
//					IID_IGraphBuilder, (void **)&pFG);
//	    if (hr == NOERROR) {
//	        hr = pFG->Connect(this, pCon);
//		pFG->Release();
//	    }
//	    pCon->Release();
//	    if (hr != NOERROR)
//                DbgLog((LOG_ERROR,1,TEXT("*** RECONNECT FAILED! ***")));
//	    // !!! We need to notify application that graph is different
//	 }
//	 // when this pin gets reconnected it will call us again to do the
//	 // other two pins
//	 return;
//    }
//
//    // Now reconnect the overlay pin
//    CVidOverlay *pOverlayPin = ((CVidCap *)m_pFilter)->m_pOverlayPin;
//    if (pOverlayPin && pOverlayPin->IsConnected()) {
//        DbgLog((LOG_TRACE,1,TEXT("Need to reconnect our overlay pin")));
//        CMediaType cmt;
//	pOverlayPin->GetMediaType(0, &cmt);
//	if (S_OK == pOverlayPin->GetConnected()->QueryAccept(&cmt)) {
//	    ((CVidCap *)m_pFilter)->m_pGraph->Reconnect(pOverlayPin);
//	} else {
//	    // Huh?
//	    ASSERT(FALSE);
//	}
//    }
//
//    // Now reconnect the non-overlay preview pin
//    CVidPreview *pPreviewPin = ((CVidCap *)m_pFilter)->m_pPreviewPin;
//    if (pPreviewPin && pPreviewPin->IsConnected()) {
//        DbgLog((LOG_TRACE,1,TEXT("Need to reconnect our preview pin")));
//        CMediaType cmt;
//	pPreviewPin->GetMediaType(0, &cmt);
//	if (S_OK == pPreviewPin->GetConnected()->QueryAccept(&cmt)) {
//	    ((CVidCap *)m_pFilter)->m_pGraph->Reconnect(pPreviewPin);
//	} else {
//	    // Oh boy.  We're going to have to get clever and insert some
//	    // filters between us to help us reconnect
//            DbgLog((LOG_TRACE,1,TEXT("Whoa! We *really* need to reconnect!")));
//	    IPin *pCon = pPreviewPin->GetConnected();
//	    pCon->AddRef();	// or it will go away in Disconnect
//	    ((CVidCap *)m_pFilter)->m_pGraph->Disconnect(
//						pPreviewPin->GetConnected());
//	    ((CVidCap *)m_pFilter)->m_pGraph->Disconnect(pPreviewPin);
//	    IGraphBuilder *pFG;
//	    HRESULT hr = ((CVidCap *)m_pFilter)->m_pGraph->QueryInterface(
//					IID_IGraphBuilder, (void **)&pFG);
//	    if (hr == NOERROR) {
//	        hr = pFG->Connect(pPreviewPin, pCon);
//		pFG->Release();
//	    }
//	    pCon->Release();
//	    if (hr != NOERROR)
//                DbgLog((LOG_ERROR,1,TEXT("*** RECONNECT FAILED! ***")));
//	    // !!! We need to notify application that graph is different
//	}
//    }
//}
//
//
//
//
////=============================================================================
//
////
//// IAMStreamConfig stuff
////
//
//
//// Tell the capture card to capture a specific format.  If it isn't connected,
//// then it will use that format to connect when it does.  If already connected,
//// then it will reconnect with the new format.
////
//HRESULT CVidStream::SetFormat(AM_MEDIA_TYPE *pmt)
//{
//    HRESULT hr;
//
//    if (pmt == NULL)
//	return E_POINTER;
//
//    // To make sure we're not in the middle of start/stop streaming
//    CAutoLock lock(m_pFilter->pStateLock());
//    CAutoLock l(&m_cSharedState);
//
//    DbgLog((LOG_TRACE,2,TEXT("IAMStreamConfig::SetFormat %x %dbit %dx%d"),
//		HEADER(pmt->pbFormat)->biCompression,
//		HEADER(pmt->pbFormat)->biBitCount,
//		HEADER(pmt->pbFormat)->biWidth,
//		HEADER(pmt->pbFormat)->biHeight));
//
//    if (((CVidCap *)m_pFilter)->m_State != State_Stopped)
//	return E_UNEXPECTED;
//
//    // If this is the same format as we already are using, don't bother
//    CMediaType mt;
//    GetMediaType(&mt);
//    if (mt == *pmt) {
//	return NOERROR;
//    }
//
//    // see if we like this type
//    if ((hr = CheckMediaType((CMediaType *)pmt)) != NOERROR) {
//	DbgLog((LOG_TRACE,2,TEXT("SetFormat rejected")));
//	return hr;
//    }
//
//    // If we are connected to somebody, make sure they like it
//    // !!! A video renderer might reject going to 16 bit from 8 bit, but
//    // reconnecting would still work, by pulling in a colour converter
//    if (IsConnected()) {
//	hr = GetConnected()->QueryAccept(pmt);
//	if (hr != NOERROR) {
//	    DbgLog((LOG_TRACE,2,TEXT("SetFormat rejected by the other pin")));
//	    return E_INVALIDARG;
//	}
//    }
//
//    // OK, we're using it
//    hr = SetMediaType((CMediaType *)pmt);
//
//    // Changing the format means reconnecting if necessary
//    if (hr == NOERROR) {
//	m_fSetFormatCalled = TRUE;	// from now on, this is the format
//					// we must use
//        Reconnect(TRUE);
//    }
//
//    return hr;
//}
//
//
//// What format is the capture card capturing right now?
//// The caller must free it with DeleteMediaType(*ppmt)
////
//HRESULT CVidStream::GetFormat(AM_MEDIA_TYPE **ppmt)
//{
//    DbgLog((LOG_TRACE,3,TEXT("IAMStreamConfig::GetFormat")));
//
//    if (ppmt == NULL)
//	return E_POINTER;
//
//    *ppmt = (AM_MEDIA_TYPE *)CoTaskMemAlloc(sizeof(AM_MEDIA_TYPE));
//    if (*ppmt == NULL)
//	return E_OUTOFMEMORY;
//    ZeroMemory(*ppmt, sizeof(AM_MEDIA_TYPE));
//    HRESULT hr = GetMediaType((CMediaType *)*ppmt);
//    if (hr != NOERROR) {
//	CoTaskMemFree(*ppmt);
//	*ppmt = NULL;
//	return hr;
//    }
//    return NOERROR;
//}
//
//
////
////
//HRESULT CVidStream::GetNumberOfCapabilities(int *piCount, int *piSize)
//{
//    DbgLog((LOG_TRACE,3,TEXT("IAMStreamConfig::GetNumberOfCapabilities")));
//
//    if (piCount == NULL || piSize == NULL)
//	return E_POINTER;
//
//    *piCount = 0;
//    *piSize = 0;
//
//    return NOERROR;
//}
//
//
//// find out some capabilities of this capture device
////
//HRESULT CVidStream::GetStreamCaps(int i, AM_MEDIA_TYPE **ppmt, LPBYTE pSCC)
//{
//    DbgLog((LOG_TRACE,3,TEXT("IAMStreamConfig::GetStreamCaps")));
//
//    // sorry, I have no clue what to say
//    return E_NOTIMPL;
//}
//
//
////=============================================================================
//
//// IAMVideoCompression stuff
//
//// Get some information about the driver
////
//HRESULT CVidStream::GetInfo(LPWSTR pszVersion, int *pcbVersion, LPWSTR pszDescription, int *pcbDescription, long *pDefaultKeyFrameRate, long *pDefaultPFramesPerKey, double *pDefaultQuality, long *pCapabilities)
//{
//    DbgLog((LOG_TRACE,3,TEXT("IAMVideoCompression::GetInfo")));
//
//    // we can't do anything programmatically
//    if (pCapabilities)
//        *pCapabilities = 0;
//    if (pDefaultKeyFrameRate)
//        *pDefaultKeyFrameRate = 0;
//    if (pDefaultPFramesPerKey)
//        *pDefaultPFramesPerKey = 0;
//    if (pDefaultQuality)
//        *pDefaultQuality = 0;
//
//    // we can give them a driver name and version
//    if (pszVersion && pcbVersion)
//	lstrcpynW(pszVersion, m_szVersion, *pcbVersion / 2);
//    if (pszDescription && pcbDescription)
//	lstrcpynW(pszDescription, m_szName, *pcbDescription / 2);
//
//    // return the number of bytes this unicode string is, incl. NULL char
//    if (pcbVersion)
//	*pcbVersion = lstrlenW(m_szVersion) * 2 + 2;
//    if (pcbDescription)
//	*pcbDescription = lstrlenW(m_szName) * 2 + 2;
//
//    return NOERROR;
//}
//
//
////=============================================================================
//
///* IAMDroppedFrames stuff */
//
//// How many frames did we drop?
////
//HRESULT CVidStream::GetNumDropped(long *plDropped)
//{
//    DbgLog((LOG_TRACE,5,TEXT("IAMDroppedFrames::GetNumDropped - %d dropped"),
//			(int)m_uiFramesSkipped));
//
//    if (plDropped == NULL)
//	return E_POINTER;
//
//    *plDropped = (long)m_uiFramesSkipped;
//    return NOERROR;
//}
//
//
//// How many frames did we not drop?
////
//HRESULT CVidStream::GetNumNotDropped(long *plNotDropped)
//{
//    DbgLog((LOG_TRACE,5,TEXT("IAMDroppedFrames::GetNumNotDropped - %d not dropped"),
//					(int)m_uiFramesDelivered));
//
//    if (plNotDropped == NULL)
//	return E_POINTER;
//
//    *plNotDropped = (long)m_uiFramesDelivered;
//    return NOERROR;
//}
//
//
//// Which frames did we drop (give me up to lSize of them - we got lNumCopied)
////
//HRESULT CVidStream::GetDroppedInfo(long lSize, long *plArray, long *plNumCopied)
//{
//    DbgLog((LOG_TRACE,5,TEXT("IAMDroppedFrames::GetDroppedInfo")));
//
//    return E_NOTIMPL;	// !!! Do this someday?
//
//#if 0
//    if (lSize <= 0)
//	return E_INVALIDARG;
//    if (plArray == NULL || plNumCopied == NULL)
//	return E_POINTER;
//
//    *plNumCopied = min(lSize, NUM_DROPPED);
//    *plNumCopied = (long)min(*plNumCopied, m_capstats.dwlNumDropped);
//
//    LONG l;
//    for (l = 0; l < *plNumCopied; l++) {
//	plArray[l] = (long)m_capstats.dwlDropped[l];
//    }
//
//    return NOERROR;
//#endif
//}
//
//
//HRESULT CVidStream::GetAverageFrameSize(long *plAverageSize)
//{
//    DbgLog((LOG_TRACE,5,TEXT("IAMDroppedFrames::GetAverageFrameSize - %d"),
//	m_uiFramesDelivered ?
//	(long)(m_llTotalFrameSize / m_uiFramesDelivered)
//	: 0));
//
//    if (plAverageSize == NULL)
//	return E_POINTER;
//
//    *plAverageSize = m_uiFramesDelivered ?
//	(long)(m_llTotalFrameSize / m_uiFramesDelivered)
//	: 0;
//
//    return NOERROR;
//}
//
//
/////////////////////////////////
//// IAMBufferNegotiation methods
/////////////////////////////////
//
//// Somebody wants us to use allocator properties like these when we
//// connect
////
//HRESULT CVidStream::SuggestAllocatorProperties(const ALLOCATOR_PROPERTIES *pprop)
//{
//    DbgLog((LOG_TRACE,2,TEXT("SuggestAllocatorProperties")));
//
//    // to make sure we're not in the middle of connecting
//    CAutoLock lock(m_pFilter->pStateLock());
//    CAutoLock l(&m_cSharedState);
//
//    if (pprop == NULL)
//	return E_POINTER;
//
//    // sorry, too late, we've made up our mind already
//    if (IsConnected())
//	return VFW_E_ALREADY_CONNECTED;
//
//    m_propSuggested = *pprop;
//
//    DbgLog((LOG_TRACE,2,TEXT("cBuffers-%d  cbBuffer-%d  cbAlign-%d  cbPrefix-%d"),
//		pprop->cBuffers, pprop->cbBuffer, pprop->cbAlign, pprop->cbPrefix));
//
//    return NOERROR;
//}
//
//
//// what properties is the allocator using right now?
////
//HRESULT CVidStream::GetAllocatorProperties(ALLOCATOR_PROPERTIES *pprop)
//{
//    DbgLog((LOG_TRACE,2,TEXT("GetAllocatorProperties")));
//
//    // to make sure we're not in the middle of connecting
//    CAutoLock lock(m_pFilter->pStateLock());
//    CAutoLock l(&m_cSharedState);
//
//    // we are not connected... we have no allocator!
//    if (!IsConnected())
//	return E_UNEXPECTED;
//
//    if (pprop == NULL)
//	return E_POINTER;
//
//    // tell them what they've won, Johnny
//    *pprop = m_propActual;
//
//    return NOERROR;
//}
//
//
////
//// PIN CATEGORIES - let the world know that we are a CAPTURE pin
////
//
//HRESULT CVidStream::Set(REFGUID guidPropSet, DWORD dwPropID, LPVOID pInstanceData, DWORD cbInstanceData, LPVOID pPropData, DWORD cbPropData)
//{
//    return E_NOTIMPL;
//}
//
//// To get a property, the caller allocates a buffer which the called
//// function fills in.  To determine necessary buffer size, call Get with
//// pPropData=NULL and cbPropData=0.
//HRESULT CVidStream::Get(REFGUID guidPropSet, DWORD dwPropID, LPVOID pInstanceData, DWORD cbInstanceData, LPVOID pPropData, DWORD cbPropData, DWORD *pcbReturned)
//{
//    if (guidPropSet != AMPROPSETID_Pin)
//	return E_PROP_SET_UNSUPPORTED;
//
//    if (dwPropID != AMPROPERTY_PIN_CATEGORY)
//	return E_PROP_ID_UNSUPPORTED;
//
//    if (pPropData == NULL && pcbReturned == NULL)
//	return E_POINTER;
//
//    if (pcbReturned)
//	*pcbReturned = sizeof(GUID);
//
//    if (pPropData == NULL)
//	return S_OK;
//
//    if (cbPropData < sizeof(GUID))
//	return E_UNEXPECTED;
//
//    *(GUID *)pPropData = PIN_CATEGORY_CAPTURE;
//    return S_OK;
//}
//
//
//// QuerySupported must either return E_NOTIMPL or correctly indicate
//// if getting or setting the property set and property is supported.
//// S_OK indicates the property set and property ID combination is
//HRESULT CVidStream::QuerySupported(REFGUID guidPropSet, DWORD dwPropID, DWORD *pTypeSupport)
//{
//    if (guidPropSet != AMPROPSETID_Pin)
//	return E_PROP_SET_UNSUPPORTED;
//
//    if (dwPropID != AMPROPERTY_PIN_CATEGORY)
//	return E_PROP_ID_UNSUPPORTED;
//
//    if (pTypeSupport)
//	*pTypeSupport = KSPROPERTY_SUPPORT_GET;
//    return S_OK;
//}
//
