namespace Ogama.Modules.Common.Controls
{
  partial class TrialTimeLine
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (this.components != null))
      {
        this.components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrialTimeLine));
      this.imlEvents = new System.Windows.Forms.ImageList(this.components);
      // 
      // imlEvents
      // 
      this.imlEvents.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlEvents.ImageStream")));
      this.imlEvents.TransparentColor = System.Drawing.Color.Transparent;
      this.imlEvents.Images.SetKeyName(0, "Key");
      this.imlEvents.Images.SetKeyName(1, "Mouse");
      this.imlEvents.Images.SetKeyName(2, "MouseLeft");
      this.imlEvents.Images.SetKeyName(3, "MouseRight");
      this.imlEvents.Images.SetKeyName(4, "Slide");
      this.imlEvents.Images.SetKeyName(5, "Flash");
      this.imlEvents.Images.SetKeyName(6, "Sound");
      this.imlEvents.Images.SetKeyName(7, "Marker");
      this.imlEvents.Images.SetKeyName(8, "MouseDown");
      this.imlEvents.Images.SetKeyName(9, "MouseUp");
      this.imlEvents.Images.SetKeyName(10, "Scroll");
      // 
      // TrialTimeLine
      // 
      this.EventImages = this.imlEvents;
      this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TrialTimeLine_MouseDown);

    }

    #endregion

    private System.Windows.Forms.ImageList imlEvents;
  }
}
