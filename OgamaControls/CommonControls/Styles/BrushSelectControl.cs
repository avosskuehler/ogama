using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;

using VectorGraphics.Elements;

namespace OgamaControls
{
  using VectorGraphics.Tools.CustomEventArgs;

  /// <summary>
  /// 
  /// </summary>
  public partial class BrushSelectControl : UserControl
  {
    /// <summary>
    /// The event that is raised when the brush style has changed.
    /// </summary>
    public event EventHandler<BrushChangedEventArgs> BrushStyleChanged;

    /// <summary>
    /// Sets current brush of the control and updates property fields.
    /// </summary>
    public Brush Brush
    {
      get { return bsaPreview.Brush; }
      set
      {
        Brush cloneBrush = (Brush)value.Clone();
        if (cloneBrush is SolidBrush)
        {
          SolidBrush brush = (SolidBrush)cloneBrush;
          tbcBrushType.SelectedTab = tbpSolid;
          clcSolid.SelectedColor = brush.Color;
          clbForeground.CurrentColor = brush.Color;
          bsaPreview.Brush = brush;
        }
        else if (cloneBrush is TextureBrush)
        {
          TextureBrush brush = (TextureBrush)cloneBrush;
          tbcBrushType.SelectedTab = tbpTexture;
          cbbWrapMode.Text = brush.WrapMode.ToString();
          bsaPreview.Brush = brush;
        }
        else if (cloneBrush is HatchBrush)
        {
          HatchBrush brush = (HatchBrush)cloneBrush;
          tbcBrushType.SelectedTab = tbpHatch;
          cbbHatchStyle.Text = brush.HatchStyle.ToString();
          clbForeground.CurrentColor = brush.ForegroundColor;
          clbBackground.CurrentColor = brush.BackgroundColor;
          bsaPreview.Brush = brush;
        }
        Refresh();
      }
    }

    /// <summary>
    /// 
    /// </summary>
    public BrushSelectControl()
    {
      InitializeComponent();
      cbbWrapMode.Items.AddRange(Enum.GetNames(typeof(WrapMode)));
      cbbWrapMode.SelectedIndex = 0;
      cbbHatchStyle.Items.AddRange(Enum.GetNames(typeof(HatchStyle)));
      cbbHatchStyle.SelectedIndex = 0;
      clcSolid.SelectedColor = Color.Red;
      clbForeground.CurrentColor = Color.Red;
      clbBackground.CurrentColor = Color.Black;
      ofdImage.InitialDirectory = Environment.SpecialFolder.MyDocuments.ToString();
    }

    /// <summary>
    /// Raises the <see cref="BrushStyleChanged"/> event by invoking the delegates.
    /// </summary>
    /// <param name="e"><see cref="BrushChangedEventArgs"/> event arguments</param>.
    public void OnBrushStyleChanged(BrushChangedEventArgs e)
    {
      if (this.BrushStyleChanged != null)
      {
        this.BrushStyleChanged(this, e);
      }
    }

    private void clcSolid_ColorChanged(object sender, ColorChangedEventArgs e)
    {
      SetSolidBrush();
    }


    private void tbcBrushType_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (tbcBrushType.SelectedTab.Text == "Solid")
        SetSolidBrush();
      else if (tbcBrushType.SelectedTab.Text == "Texture")
      {
        SetTextureBrush();
      }
      else if (tbcBrushType.SelectedTab.Text == "Hatch")
      {
        SetHatchBrush();
      }
    }

    private void cbbHatchStyle_SelectionChangeCommitted(object sender, EventArgs e)
    {
      SetHatchBrush();
    }

    private void SetSolidBrush()
    {
      bsaPreview.Brush = new SolidBrush(clcSolid.SelectedColor);
      OnBrushStyleChanged(new BrushChangedEventArgs(bsaPreview.Brush, VGStyleGroup.None));
    }

    private void SetTextureBrush()
    {
      if (cbbWrapMode.Text != "")
      {
        WrapMode wrapMode = (WrapMode)Enum.Parse(typeof(WrapMode), cbbWrapMode.Text);
        Bitmap image = null;
        if (File.Exists(txbImageFile.Text))
        {

          using (FileStream fs = File.OpenRead(txbImageFile.Text))
          {
            Image original = Image.FromStream(fs);
            image = new Bitmap(original);
          }
          if (image != null)
          {
            bsaPreview.Brush = new TextureBrush(image, wrapMode);
            OnBrushStyleChanged(new BrushChangedEventArgs(bsaPreview.Brush, VGStyleGroup.None));
          }
        }
      }
    }

    private void SetHatchBrush()
    {
      if (cbbHatchStyle.Text != "")
      {
        bsaPreview.Brush = new HatchBrush(
       (HatchStyle)Enum.Parse(typeof(HatchStyle), cbbHatchStyle.Text),
       clbForeground.CurrentColor, clbBackground.CurrentColor);
        OnBrushStyleChanged(new BrushChangedEventArgs(bsaPreview.Brush, VGStyleGroup.None));
      }
    }

    private void txbImageFile_TextChanged(object sender, EventArgs e)
    {
      SetTextureBrush();
    }

    private void cbbWrapMode_SelectionChangeCommitted(object sender, EventArgs e)
    {
      SetTextureBrush();
    }

    private void clbForeground_ColorChanged(object sender, EventArgs e)
    {
      SetHatchBrush();
    }

    private void clbBackground_ColorChanged(object sender, EventArgs e)
    {
      SetHatchBrush();
    }

    private void btnImageFile_Click(object sender, EventArgs e)
    {
      if (ofdImage.ShowDialog()==DialogResult.OK)
      {
        txbImageFile.Text = ofdImage.FileName;
      }
    }

  }
}
