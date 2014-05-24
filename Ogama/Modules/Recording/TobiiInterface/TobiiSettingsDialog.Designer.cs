
namespace Ogama.Modules.Recording.TobiiInterface
{
  partial class TobiiSettingsDialog
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
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.rdbTobii2PtsCalib = new System.Windows.Forms.RadioButton();
      this.rdbTobii5PtsCalib = new System.Windows.Forms.RadioButton();
      this.rdbTobii9PtsCalib = new System.Windows.Forms.RadioButton();
      this.rdbTobiiSizeLarge = new System.Windows.Forms.RadioButton();
      this.rdbTobiiSizeMedium = new System.Windows.Forms.RadioButton();
      this.rdbTobiiSizeSmall = new System.Windows.Forms.RadioButton();
      this.rdbTobiiSpeedFast = new System.Windows.Forms.RadioButton();
      this.rdbTobiiSpeedMediumFast = new System.Windows.Forms.RadioButton();
      this.rdbTobiiSpeedMedium = new System.Windows.Forms.RadioButton();
      this.rdbTobiiSpeedMediumSlow = new System.Windows.Forms.RadioButton();
      this.rdbTobiiSpeedSlow = new System.Windows.Forms.RadioButton();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.label1 = new System.Windows.Forms.Label();
      this.groupBox4 = new System.Windows.Forms.GroupBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.clbTobiiBackColor = new OgamaControls.ColorButton(this.components);
      this.clbTobiiPointColor = new OgamaControls.ColorButton(this.components);
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.txbDevice = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.chbRandomizePointOrder = new System.Windows.Forms.CheckBox();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.groupBox4.SuspendLayout();
      this.SuspendLayout();
      // 
      // rdbTobii2PtsCalib
      // 
      this.rdbTobii2PtsCalib.AutoSize = true;
      this.rdbTobii2PtsCalib.Location = new System.Drawing.Point(6, 53);
      this.rdbTobii2PtsCalib.Name = "rdbTobii2PtsCalib";
      this.rdbTobii2PtsCalib.Size = new System.Drawing.Size(62, 17);
      this.rdbTobii2PtsCalib.TabIndex = 19;
      this.rdbTobii2PtsCalib.Text = "&2 points";
      this.rdbTobii2PtsCalib.CheckedChanged += new System.EventHandler(this.rdbTobiiNumPtsCalib_CheckedChanged);
      // 
      // rdbTobii5PtsCalib
      // 
      this.rdbTobii5PtsCalib.AutoSize = true;
      this.rdbTobii5PtsCalib.Checked = true;
      this.rdbTobii5PtsCalib.Cursor = System.Windows.Forms.Cursors.Default;
      this.rdbTobii5PtsCalib.Location = new System.Drawing.Point(6, 36);
      this.rdbTobii5PtsCalib.Name = "rdbTobii5PtsCalib";
      this.rdbTobii5PtsCalib.Size = new System.Drawing.Size(62, 17);
      this.rdbTobii5PtsCalib.TabIndex = 18;
      this.rdbTobii5PtsCalib.TabStop = true;
      this.rdbTobii5PtsCalib.Text = "&5 points";
      this.rdbTobii5PtsCalib.CheckedChanged += new System.EventHandler(this.rdbTobiiNumPtsCalib_CheckedChanged);
      // 
      // rdbTobii9PtsCalib
      // 
      this.rdbTobii9PtsCalib.AutoSize = true;
      this.rdbTobii9PtsCalib.Location = new System.Drawing.Point(6, 19);
      this.rdbTobii9PtsCalib.Name = "rdbTobii9PtsCalib";
      this.rdbTobii9PtsCalib.Size = new System.Drawing.Size(62, 17);
      this.rdbTobii9PtsCalib.TabIndex = 17;
      this.rdbTobii9PtsCalib.Text = "&9 points";
      this.rdbTobii9PtsCalib.CheckedChanged += new System.EventHandler(this.rdbTobiiNumPtsCalib_CheckedChanged);
      // 
      // rdbTobiiSizeLarge
      // 
      this.rdbTobiiSizeLarge.AutoSize = true;
      this.rdbTobiiSizeLarge.Location = new System.Drawing.Point(6, 19);
      this.rdbTobiiSizeLarge.Name = "rdbTobiiSizeLarge";
      this.rdbTobiiSizeLarge.Size = new System.Drawing.Size(52, 17);
      this.rdbTobiiSizeLarge.TabIndex = 17;
      this.rdbTobiiSizeLarge.Text = "&Large";
      this.rdbTobiiSizeLarge.CheckedChanged += new System.EventHandler(this.rdbTobiiSize_CheckedChanged);
      // 
      // rdbTobiiSizeMedium
      // 
      this.rdbTobiiSizeMedium.AutoSize = true;
      this.rdbTobiiSizeMedium.Checked = true;
      this.rdbTobiiSizeMedium.Cursor = System.Windows.Forms.Cursors.Default;
      this.rdbTobiiSizeMedium.Location = new System.Drawing.Point(6, 36);
      this.rdbTobiiSizeMedium.Name = "rdbTobiiSizeMedium";
      this.rdbTobiiSizeMedium.Size = new System.Drawing.Size(62, 17);
      this.rdbTobiiSizeMedium.TabIndex = 18;
      this.rdbTobiiSizeMedium.TabStop = true;
      this.rdbTobiiSizeMedium.Text = "&Medium";
      this.rdbTobiiSizeMedium.CheckedChanged += new System.EventHandler(this.rdbTobiiSize_CheckedChanged);
      // 
      // rdbTobiiSizeSmall
      // 
      this.rdbTobiiSizeSmall.AutoSize = true;
      this.rdbTobiiSizeSmall.Location = new System.Drawing.Point(6, 53);
      this.rdbTobiiSizeSmall.Name = "rdbTobiiSizeSmall";
      this.rdbTobiiSizeSmall.Size = new System.Drawing.Size(50, 17);
      this.rdbTobiiSizeSmall.TabIndex = 19;
      this.rdbTobiiSizeSmall.Text = "&Small";
      this.rdbTobiiSizeSmall.CheckedChanged += new System.EventHandler(this.rdbTobiiSize_CheckedChanged);
      // 
      // rdbTobiiSpeedFast
      // 
      this.rdbTobiiSpeedFast.AutoSize = true;
      this.rdbTobiiSpeedFast.Location = new System.Drawing.Point(6, 19);
      this.rdbTobiiSpeedFast.Name = "rdbTobiiSpeedFast";
      this.rdbTobiiSpeedFast.Size = new System.Drawing.Size(45, 17);
      this.rdbTobiiSpeedFast.TabIndex = 17;
      this.rdbTobiiSpeedFast.Text = "Fast";
      this.rdbTobiiSpeedFast.CheckedChanged += new System.EventHandler(this.rdbTobiiSpeed_CheckedChanged);
      // 
      // rdbTobiiSpeedMediumFast
      // 
      this.rdbTobiiSpeedMediumFast.AutoSize = true;
      this.rdbTobiiSpeedMediumFast.Cursor = System.Windows.Forms.Cursors.Default;
      this.rdbTobiiSpeedMediumFast.Location = new System.Drawing.Point(6, 37);
      this.rdbTobiiSpeedMediumFast.Name = "rdbTobiiSpeedMediumFast";
      this.rdbTobiiSpeedMediumFast.Size = new System.Drawing.Size(82, 17);
      this.rdbTobiiSpeedMediumFast.TabIndex = 18;
      this.rdbTobiiSpeedMediumFast.Text = "MediumFast";
      this.rdbTobiiSpeedMediumFast.CheckedChanged += new System.EventHandler(this.rdbTobiiSpeed_CheckedChanged);
      // 
      // rdbTobiiSpeedMedium
      // 
      this.rdbTobiiSpeedMedium.AutoSize = true;
      this.rdbTobiiSpeedMedium.Location = new System.Drawing.Point(6, 55);
      this.rdbTobiiSpeedMedium.Name = "rdbTobiiSpeedMedium";
      this.rdbTobiiSpeedMedium.Size = new System.Drawing.Size(62, 17);
      this.rdbTobiiSpeedMedium.TabIndex = 19;
      this.rdbTobiiSpeedMedium.Text = "Medium";
      this.rdbTobiiSpeedMedium.CheckedChanged += new System.EventHandler(this.rdbTobiiSpeed_CheckedChanged);
      // 
      // rdbTobiiSpeedMediumSlow
      // 
      this.rdbTobiiSpeedMediumSlow.AutoSize = true;
      this.rdbTobiiSpeedMediumSlow.Checked = true;
      this.rdbTobiiSpeedMediumSlow.Cursor = System.Windows.Forms.Cursors.Default;
      this.rdbTobiiSpeedMediumSlow.Location = new System.Drawing.Point(6, 73);
      this.rdbTobiiSpeedMediumSlow.Name = "rdbTobiiSpeedMediumSlow";
      this.rdbTobiiSpeedMediumSlow.Size = new System.Drawing.Size(85, 17);
      this.rdbTobiiSpeedMediumSlow.TabIndex = 18;
      this.rdbTobiiSpeedMediumSlow.TabStop = true;
      this.rdbTobiiSpeedMediumSlow.Text = "MediumSlow";
      this.rdbTobiiSpeedMediumSlow.CheckedChanged += new System.EventHandler(this.rdbTobiiSpeed_CheckedChanged);
      // 
      // rdbTobiiSpeedSlow
      // 
      this.rdbTobiiSpeedSlow.AutoSize = true;
      this.rdbTobiiSpeedSlow.Location = new System.Drawing.Point(6, 91);
      this.rdbTobiiSpeedSlow.Name = "rdbTobiiSpeedSlow";
      this.rdbTobiiSpeedSlow.Size = new System.Drawing.Size(48, 17);
      this.rdbTobiiSpeedSlow.TabIndex = 19;
      this.rdbTobiiSpeedSlow.Text = "Slow";
      this.rdbTobiiSpeedSlow.CheckedChanged += new System.EventHandler(this.rdbTobiiSpeed_CheckedChanged);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.rdbTobii2PtsCalib);
      this.groupBox1.Controls.Add(this.rdbTobii9PtsCalib);
      this.groupBox1.Controls.Add(this.rdbTobii5PtsCalib);
      this.groupBox1.Location = new System.Drawing.Point(12, 139);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(100, 72);
      this.groupBox1.TabIndex = 20;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Number";
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.rdbTobiiSizeSmall);
      this.groupBox2.Controls.Add(this.rdbTobiiSizeLarge);
      this.groupBox2.Controls.Add(this.rdbTobiiSizeMedium);
      this.groupBox2.Location = new System.Drawing.Point(119, 139);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(100, 72);
      this.groupBox2.TabIndex = 21;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Size";
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.rdbTobiiSpeedMediumSlow);
      this.groupBox3.Controls.Add(this.rdbTobiiSpeedFast);
      this.groupBox3.Controls.Add(this.rdbTobiiSpeedMediumFast);
      this.groupBox3.Controls.Add(this.rdbTobiiSpeedSlow);
      this.groupBox3.Controls.Add(this.rdbTobiiSpeedMedium);
      this.groupBox3.Location = new System.Drawing.Point(225, 139);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(100, 120);
      this.groupBox3.TabIndex = 22;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Speed";
      // 
      // pictureBox1
      // 
      this.pictureBox1.Image = global::Ogama.Properties.Resources.TobiiPhoto64;
      this.pictureBox1.Location = new System.Drawing.Point(12, 12);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(64, 64);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pictureBox1.TabIndex = 23;
      this.pictureBox1.TabStop = false;
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(90, 17);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(235, 72);
      this.label1.TabIndex = 24;
      this.label1.Text = "Please specify the calibration settings for the Tobii.\r\nThat is the number of cal" +
    "ibration points, their size, the speed of display, their color and the backgroun" +
    "d color.";
      // 
      // groupBox4
      // 
      this.groupBox4.Controls.Add(this.label3);
      this.groupBox4.Controls.Add(this.label2);
      this.groupBox4.Controls.Add(this.clbTobiiBackColor);
      this.groupBox4.Controls.Add(this.clbTobiiPointColor);
      this.groupBox4.Location = new System.Drawing.Point(12, 265);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new System.Drawing.Size(313, 63);
      this.groupBox4.TabIndex = 25;
      this.groupBox4.TabStop = false;
      this.groupBox4.Text = "Colors";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(151, 16);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(94, 13);
      this.label3.TabIndex = 1;
      this.label3.Text = "Background color:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(9, 16);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(111, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "Calibration point color:";
      // 
      // clbTobiiBackColor
      // 
      this.clbTobiiBackColor.AutoButtonString = "Automatic";
      this.clbTobiiBackColor.CurrentColor = System.Drawing.SystemColors.ControlLight;
      this.clbTobiiBackColor.Location = new System.Drawing.Point(154, 32);
      this.clbTobiiBackColor.Name = "clbTobiiBackColor";
      this.clbTobiiBackColor.Size = new System.Drawing.Size(75, 23);
      this.clbTobiiBackColor.TabIndex = 0;
      this.clbTobiiBackColor.UseVisualStyleBackColor = true;
      this.clbTobiiBackColor.ColorChanged += new System.EventHandler(this.clbTobiiBackColor_ColorChanged);
      // 
      // clbTobiiPointColor
      // 
      this.clbTobiiPointColor.AutoButtonString = "Automatic";
      this.clbTobiiPointColor.CurrentColor = System.Drawing.Color.Red;
      this.clbTobiiPointColor.Location = new System.Drawing.Point(12, 32);
      this.clbTobiiPointColor.Name = "clbTobiiPointColor";
      this.clbTobiiPointColor.Size = new System.Drawing.Size(75, 23);
      this.clbTobiiPointColor.TabIndex = 0;
      this.clbTobiiPointColor.UseVisualStyleBackColor = true;
      this.clbTobiiPointColor.ColorChanged += new System.EventHandler(this.clbTobiiPointColor_ColorChanged);
      // 
      // btnOK
      // 
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(169, 334);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(75, 23);
      this.btnOK.TabIndex = 26;
      this.btnOK.Text = "&OK";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(250, 334);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 26;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // txbDevice
      // 
      this.txbDevice.Location = new System.Drawing.Point(109, 95);
      this.txbDevice.Name = "txbDevice";
      this.txbDevice.ReadOnly = true;
      this.txbDevice.Size = new System.Drawing.Size(216, 20);
      this.txbDevice.TabIndex = 27;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(9, 98);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(96, 13);
      this.label5.TabIndex = 29;
      this.label5.Text = "Connected Device";
      // 
      // chbRandomizePointOrder
      // 
      this.chbRandomizePointOrder.AutoSize = true;
      this.chbRandomizePointOrder.Checked = true;
      this.chbRandomizePointOrder.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chbRandomizePointOrder.Location = new System.Drawing.Point(24, 230);
      this.chbRandomizePointOrder.Name = "chbRandomizePointOrder";
      this.chbRandomizePointOrder.Size = new System.Drawing.Size(132, 17);
      this.chbRandomizePointOrder.TabIndex = 30;
      this.chbRandomizePointOrder.Text = "Randomize point order";
      this.chbRandomizePointOrder.UseVisualStyleBackColor = true;
      this.chbRandomizePointOrder.CheckedChanged += new System.EventHandler(this.chbRandomizePointOrder_CheckedChanged);
      // 
      // TobiiSettingsDialog
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(335, 365);
      this.Controls.Add(this.chbRandomizePointOrder);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.txbDevice);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.groupBox4);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.pictureBox1);
      this.Controls.Add(this.groupBox3);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "TobiiSettingsDialog";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Specify Tobii calibration settings ...";
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.groupBox4.ResumeLayout(false);
      this.groupBox4.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.RadioButton rdbTobii2PtsCalib;
    private System.Windows.Forms.RadioButton rdbTobii5PtsCalib;
    private System.Windows.Forms.RadioButton rdbTobii9PtsCalib;
    private System.Windows.Forms.RadioButton rdbTobiiSizeLarge;
    private System.Windows.Forms.RadioButton rdbTobiiSizeMedium;
    private System.Windows.Forms.RadioButton rdbTobiiSizeSmall;
    private System.Windows.Forms.RadioButton rdbTobiiSpeedFast;
    private System.Windows.Forms.RadioButton rdbTobiiSpeedMediumFast;
    private System.Windows.Forms.RadioButton rdbTobiiSpeedMedium;
    private System.Windows.Forms.RadioButton rdbTobiiSpeedMediumSlow;
    private System.Windows.Forms.RadioButton rdbTobiiSpeedSlow;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.GroupBox groupBox4;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private OgamaControls.ColorButton clbTobiiBackColor;
    private OgamaControls.ColorButton clbTobiiPointColor;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.TextBox txbDevice;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.CheckBox chbRandomizePointOrder;
  }
}

