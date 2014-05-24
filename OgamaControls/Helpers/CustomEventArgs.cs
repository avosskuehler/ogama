using System;
using System.Drawing;

using DirectShowLib;
using VectorGraphics.Tools;

internal class resfinder
{

}

namespace OgamaControls
{

  /// <summary>
  /// Class that contains the data for the AudioPropertiesChanged event. Derives from System.EventArgs.
  /// </summary>
  public class AudioPropertiesChangedEventArgs : EventArgs
  {
    /// <summary>
    /// Constructor. Initializes fields.
    /// </summary>
    /// <param name="newAudioFile">New <see cref="AudioFile"/> that should be used.</param>
    public AudioPropertiesChangedEventArgs(AudioFile newAudioFile)
    {
      _audioFile = newAudioFile;
    }

    private AudioFile _audioFile;

    /// <summary>
    /// Gets the <see cref="AudioFile"/> property. 
    /// </summary>
    public AudioFile AudioFile
    {
      get { return _audioFile; }
    }
  }

  /// <summary>
  /// Class that contains the data for the ColorChanged event. Derives from System.EventArgs.
  /// </summary>
  public class ColorChangedEventArgs : EventArgs
  {
    private Color _Color;

    /// <summary>
    /// Constructor. Initializes fields.
    /// </summary>
    /// <param name="newColor">new Color</param>
    public ColorChangedEventArgs(Color newColor)
    {
      _Color = newColor;
    }

    /// <summary>
    /// Gets the Color property. This is the new created color.
    /// </summary>
    public Color Color
    {
      get { return _Color; }
    }
  }

  /// <summary>
  /// Class that contains the data for the AudioSamplesReceived event. Derives from System.EventArgs.
  /// </summary>
  public class FFTEventArgs : EventArgs
  {
    private int[] _amplitudes;

    /// <summary>
    /// Constructor. Initializes fields.
    /// </summary>
    /// <param name="newAmplitudes">new amplitudes</param>
    public FFTEventArgs(int[] newAmplitudes)
    {
      _amplitudes = newAmplitudes;
    }

    /// <summary>
    /// Gets the Amplitudes property. This is the array of new amplitude values.
    /// </summary>
    public int[] Amplitudes
    {
      get { return _amplitudes; }
    }
  }

  /// <summary>Used by the <see cref="Media.DESCombine.Completed"/> event.  
  /// Reports the event code that exited the graph.
  /// </summary>
  /// <remarks>Signals that all files have been rendered</remarks>
  public class DESCompletedArgs : System.EventArgs
  {
    /// <summary>The result of the rendering</summary>
    /// <remarks>
    /// This code will be a member of DirectShowLib.EventCode.  Typically it 
    /// will be EventCode.Complete, EventCode.ErrorAbort or EventCode.UserAbort.
    /// </remarks>
    public EventCode Result;

    /// <summary>
    /// Used to construct an instace of the class.
    /// </summary>
    /// <param name="ec"></param>
    public DESCompletedArgs(EventCode ec)
    {
      Result = ec;
    }
  }

  /// <summary>
  /// Used by the <see cref="Media.DESCombine.FileCompleted"/> event.  Reports 
  /// when a file has completed processing.
  /// </summary>
  /// <remarks>
  /// To determine whether you are receiving a notification for an audio
  /// or video file completing, examine the <see cref="FileCompletedArgs.Type"/> property.  Also, when
  /// using the RenderToWindow, dropped frames can result in late or missed notifications.
  /// </remarks>
  public class FileCompletedArgs : System.EventArgs
  {
    /// <summary>
    /// Type of the file that has completed (audio/video)
    /// </summary>
    public enum FileType
    {
      /// <summary>
      /// Type is video
      /// </summary>
      Video,

      /// <summary>
      /// Type is audio
      /// </summary>
      Audio
    }

    /// <summary>
    /// The file name that just completed
    /// </summary>
    /// <remarks>
    /// Use the <see cref="FileCompletedArgs.Type"/> to determine whether this is a video
    /// or audio file.
    /// </remarks>
    public string FileName;

    /// <summary>
    /// The type (audio/video) of file
    /// </summary>
    public FileType Type;

    /// <summary>
    /// Used to construct an instance of the class.
    /// </summary>
    /// <param name="sFilename">Filename that has been completed</param>
    /// <param name="eType">Type of the file (audio/video)</param>
    public FileCompletedArgs(string sFilename, FileType eType)
    {
      FileName = sFilename;
      Type = eType;
    }
  }

}
