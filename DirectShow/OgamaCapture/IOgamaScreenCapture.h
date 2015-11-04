#ifndef __IOGAMASCREENCAPTURE__
#define __IOGAMASCREENCAPTURE__

#ifdef __cplusplus
extern "C" {
#endif

  //
  // IID_IOgamaScreenCapture's GUID
  //
  // {E86F68BE-BEC5-4d7b-9CB9-6954ACE63C87}
  DEFINE_GUID(IID_IOgamaScreenCapture,
    0xe86f68be, 0xbec5, 0x4d7b, 0x9c, 0xb9, 0x69, 0x54, 0xac, 0xe6, 0x3c, 0x87);

  //
  // IGargle
  //
  DECLARE_INTERFACE_(IOgamaScreenCapture, IUnknown) {

    // Compare these with the functions in class CGargle in gargle.h
    STDMETHOD(get_Monitor)
      (THIS_
      int* index  // [out] monitor index
      ) PURE;

    STDMETHOD(set_Monitor)
      (THIS_
      int index  // [in] monitor index
      ) PURE;

    STDMETHOD(get_Framerate)
      (THIS_
      int* framrate // [in] framerate
      ) PURE;

    STDMETHOD(set_Framerate)
      (THIS_
      int framrate  // [out] framerate
      ) PURE;
  };


#ifdef __cplusplus
}
#endif

#endif // __IOGAMASCREENCAPTURE__
