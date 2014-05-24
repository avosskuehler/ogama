namespace OgamaControls.Media
{
  /// <summary>
  /// A callback interface that can be implemented by callers to DESCombine 
  /// who wish to perform processing on video or audio frames.
  /// </summary>
  /// <remarks>
  /// Classes which implement this interfaces can be passed to <see cref="DESCombine.RenderToWindow"/>
  /// or <see cref="DESCombine.RenderToAVI"/>.  Each audio or video frame that is processed by DES
  /// will be passed to this callback which can perform additional processing.
  /// </remarks>
  public interface IDESCombineCB
  {
    /// <summary>
    /// Callback routine - called once for each audio or video frame
    /// </summary>
    /// <remarks>
    /// The buffer can be examined or modified.
    /// </remarks>
    /// <param name="sFileName">Filename currently being processed</param>
    /// <param name="SampleTime">Time stamp in seconds</param>
    /// <param name="pBuffer">Pointer to the buffer</param>
    /// <param name="BufferLen">Length of the buffer</param>
    /// <returns>Return S_OK if successful, or an HRESULT error code otherwise.  This value is sent as 
    /// the return value to ISampleGrabberCB::BufferCB</returns>
    int BufferCB(
        string sFileName,
        double SampleTime,
        System.IntPtr pBuffer,
        int BufferLen
        );
  }
}
