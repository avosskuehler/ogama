using System;
using System.Threading;

namespace OgamaControls.Media
{
  /// <summary>
  /// 
  /// </summary>
  public class DESAudioPlayer
  {
    private DESCombine ds;

    /// <summary>
    /// Milliseconds
    /// </summary>
    public int TimelineLength
    {
      get { return (int)(ds.AudioLength / 10000f); }
    }

    /// <summary>
    /// Sets the current playback rate of the audio player.
    /// </summary>
    /// <value>A <see cref="double"/> with the playback rate. Must not be zero.</value>
    public double PlaybackRate
    {
      set { ds.PlaybackRate = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    public DESAudioPlayer()
    {
      // FPS, BPP, Width, Height
      ds = new DESCombine(25, 24, 10, 10, true, false);

      // Wire events
      ds.Completed += new EventHandler(Completed);
      ds.FileCompleted += new EventHandler(FileCompleted);
    }

    private void Completed(object sender, System.EventArgs e)
    {
      DESCompletedArgs ca = e as DESCompletedArgs;
      Console.WriteLine(ca.Result.ToString());
      ds.Seek(0);
      //ds.Dispose();
      //ds = null;
      //GC.Collect(); 
      //GC.WaitForPendingFinalizers();
    }

    private void FileCompleted(object sender, System.EventArgs e)
    {
      FileCompletedArgs ca = e as FileCompletedArgs;
      Console.WriteLine("Finished File" + ca.FileName);
    }

    /// <summary>
    /// 
    /// </summary>
    public void Initialize()
    {
      if (ds.MediaLength > 0)
      {
        ds.RenderAudioToOutput(null);
        this.Play();
        Thread.Sleep(100);
        ds.StopWhenReady();
        this.Seek(0);
      }
    }

    /// <summary>
    /// This method adds the given sound file to the timeline at the
    /// position given in millisecónds in the second parameter.
    /// </summary>
    /// <param name="filename"></param>
    /// <param name="position">The initial time in milliseconds that this sound should be started.</param>
    public void AddSoundAtPosition(string filename, long position)
    {
      ds.AddAudioFile(filename, position * 10000);
    }

    /// <summary>
    /// This method adds the given sound file to the timeline at the
    /// position given in millisecónds in the second parameter.
    /// </summary>
    /// <param name="filename"></param>
    /// <param name="startPosition">The initial time in milliseconds that this sound should be started.</param>
    /// <param name="endPosition">The time in milliseconds that this sound should be stopped.</param>
    public void AddSoundAtRange(string filename, long startPosition, long endPosition)
    {
      ds.AddAudioFile(filename, startPosition * 10000, endPosition * 10000);
    }

    /// <summary>
    /// 
    /// </summary>
    public void Play()
    {
      if (ds.MediaLength > 0)
      {
        ds.StartRendering();
      }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"></param>
    public void Seek(long position)
    {
      if (ds.MediaLength > 0)
      {
        ds.Seek(position);
      }
    }
    /// <summary>
    /// 
    /// </summary>
    public void Pause()
    {
      ds.PauseRendering();
    }
    /// <summary>
    /// 
    /// </summary>
    public void Stop()
    {
      ds.StopRendering();
    }

    /// <summary>
    /// 
    /// </summary>
    public void Reset()
    {
      ds.RemoveAudioFiles();
    }

    /// <summary>
    /// 
    /// </summary>
    public void Close()
    {
      ds.Cancel();
      ds.Completed -= new EventHandler(Completed);
      ds.FileCompleted -= new EventHandler(FileCompleted);
      ds.Dispose();
    }

  }
}
