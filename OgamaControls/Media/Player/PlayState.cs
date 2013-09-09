namespace OgamaControls
{
  /// <summary>
  /// This enumeration describes the possible replay states
  /// of the <see cref="AVPlayer"/> control.
  /// </summary>
  public enum PlayState
  {
    /// <summary>
    /// No state set.
    /// </summary>
    None,

    /// <summary>
    /// The player ist stopped.
    /// </summary>
    Stopped,

    /// <summary>
    /// The replay is paused.
    /// </summary>
    Paused,

    /// <summary>
    /// The replay is running.
    /// </summary>
    Running,

    /// <summary>
    /// The <see cref="AVPlayer"/> is initializing.
    /// </summary>
    Init,

    /// <summary>
    /// The <see cref="AVPlayer"/> is beeing cancelled.
    /// </summary>
    Cancelling,

    /// <summary>
    /// The <see cref="AVPlayer"/> is exiting.
    /// </summary>
    Exiting,
  };
}