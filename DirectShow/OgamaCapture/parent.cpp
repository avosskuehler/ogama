#include <streams.h>
#include "OgamaCapture.h"
#include "OgamaCaptureGuids.h"
#include "DibHelper.h"

/**********************************************
 *
 *  COgamaCaptureDesktop Class Parent
 *
 **********************************************/

COgamaCaptureDesktop::COgamaCaptureDesktop(IUnknown *pUnk, HRESULT *phr)
  : CSource(NAME("OgamaCaptureDesktop Parent"), pUnk, CLSID_OgamaCaptureDesktop)
{
  // The pin magically adds itself to our pin array.
  // except its not an array since we just have one [?]
  m_pPin = new COgamaCapturePinDesktop(phr, this);

  if (phr)
  {
    if (m_pPin == NULL)
      *phr = E_OUTOFMEMORY;
    else
      *phr = S_OK;
  }
}

COgamaCaptureDesktop::~COgamaCaptureDesktop() // parent destructor
{
  // COM should call this when the refcount hits 0...
  // but somebody should make the refcount 0...
  delete m_pPin;
}

STDMETHODIMP COgamaCaptureDesktop::get_Monitor(int* primary)
{
  if (m_pPin == NULL)
    return S_FALSE;
  else
  {
    m_pPin->get_Monitor(primary);
    return S_OK;
  }
}

STDMETHODIMP COgamaCaptureDesktop::set_Monitor(int primary)
{
  if (m_pPin == NULL)
    return S_FALSE;
  else
  {
    m_pPin->set_Monitor(primary);
    return S_OK;
  }
}

STDMETHODIMP COgamaCaptureDesktop::get_Framerate(int* framerate)
{
  if (m_pPin == NULL)
    return S_FALSE;
  else
  {
    m_pPin->get_Framerate(framerate);
    return S_OK;
  }
}

STDMETHODIMP COgamaCaptureDesktop::set_Framerate(int framerate)
{
  if (m_pPin == NULL)
    return S_FALSE;
  else
  {
    m_pPin->set_Framerate(framerate);
    return S_OK;
  }
}

HRESULT STDMETHODCALLTYPE COgamaCaptureDesktop::SetFormat(AM_MEDIA_TYPE *pmt)
{
  if (m_pPin == NULL)
    return S_FALSE;
  else
  {
    return m_pPin->SetFormat(pmt);
  }
}

HRESULT STDMETHODCALLTYPE COgamaCaptureDesktop::GetFormat(AM_MEDIA_TYPE **ppmt)
{
  if (m_pPin == NULL)
    return S_FALSE;
  else
  {
    return m_pPin->GetFormat(ppmt);
  }
}
HRESULT STDMETHODCALLTYPE COgamaCaptureDesktop::GetNumberOfCapabilities(int *piCount, int *piSize)
{
  if (m_pPin == NULL)
    return S_FALSE;
  else
  {
    return m_pPin->GetNumberOfCapabilities(piCount, piSize);
  }
}

HRESULT STDMETHODCALLTYPE COgamaCaptureDesktop::GetStreamCaps(int iIndex, AM_MEDIA_TYPE **pmt, BYTE *pSCC)
{
  if (m_pPin == NULL)
    return S_FALSE;
  else
  {
    return m_pPin->GetStreamCaps(iIndex, pmt, pSCC);
  }
}

CUnknown * WINAPI COgamaCaptureDesktop::CreateInstance(IUnknown *pUnk, HRESULT *phr)
{
  // the first entry point
  COgamaCaptureDesktop *pNewFilter = new COgamaCaptureDesktop(pUnk, phr);

  if (phr)
  {
    if (pNewFilter == NULL)
      *phr = E_OUTOFMEMORY;
    else
      *phr = S_OK;
  }
  return pNewFilter;
}

HRESULT COgamaCaptureDesktop::QueryInterface(REFIID riid, void **ppv)
{
  //Forward request for IAMStreamConfig & IKsPropertySet to the pin
  if (riid == _uuidof(IAMStreamConfig) || riid == _uuidof(IKsPropertySet)) {
    return m_paStreams[0]->QueryInterface(riid, ppv);
  }
  else {

    return CSource::QueryInterface(riid, ppv);
  }
}

//
// NonDelegatingQueryInterface
//
// Override CUnknown method.
// Reveal our persistent stream, property pages and IGargle interfaces.
// Anyone can call our private interface so long as they know the private UUID.
//
STDMETHODIMP COgamaCaptureDesktop::NonDelegatingQueryInterface(REFIID riid, void **ppv)
{
  CheckPointer(ppv, E_POINTER);

  if (riid == IID_IOgamaScreenCapture) {
    return GetInterface((IOgamaScreenCapture *)this, ppv);
  }

  if (riid == _uuidof(IAMStreamConfig) || riid == _uuidof(IKsPropertySet)) {
    return m_paStreams[0]->QueryInterface(riid, ppv);
  }

  return CSource::NonDelegatingQueryInterface(riid, ppv);
} // NonDelegatingQueryInterface
