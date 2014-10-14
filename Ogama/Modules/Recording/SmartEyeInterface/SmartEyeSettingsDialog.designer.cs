namespace Ogama.Modules.Recording.SmartEyeInterface
{
    partial class SmartEyeSettingsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SmartEyeSettingsDialog));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.clbSmartEyeBackColor = new OgamaControls.ColorButton(this.components);
            this.clbSmartEyePointColor = new OgamaControls.ColorButton(this.components);
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txbSmartEyeAddress = new System.Windows.Forms.TextBox();
            this.txbSmartEyePort = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txbOGAMAPort = new System.Windows.Forms.TextBox();
            this.txbOGAMAAddress = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdbSmartEyeSpeedFast = new System.Windows.Forms.RadioButton();
            this.rdbSmartEyeSpeedSlow = new System.Windows.Forms.RadioButton();
            this.rdbSmartEyeSpeedMedium = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdbSmartEyeSizeSmall = new System.Windows.Forms.RadioButton();
            this.rdbSmartEyeSizeLarge = new System.Windows.Forms.RadioButton();
            this.rdbSmartEyeSizeMedium = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbSmartEye3PtsCalib = new System.Windows.Forms.RadioButton();
            this.rdbSmartEye9PtsCalib = new System.Windows.Forms.RadioButton();
            this.rdbSmartEye5PtsCalib = new System.Windows.Forms.RadioButton();
            this.chbRandomizePointOrder = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Ogama.Properties.Resources.SmartEyeAuroraFoto64;
            this.pictureBox1.Location = new System.Drawing.Point(15, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.TabIndex = 23;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(99, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 68);
            this.label1.TabIndex = 24;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.clbSmartEyeBackColor);
            this.groupBox4.Controls.Add(this.clbSmartEyePointColor);
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
            // clbSmartEyeBackColor
            // 
            this.clbSmartEyeBackColor.AutoButtonString = "Automatic";
            this.clbSmartEyeBackColor.CurrentColor = System.Drawing.SystemColors.ControlLight;
            this.clbSmartEyeBackColor.Location = new System.Drawing.Point(154, 32);
            this.clbSmartEyeBackColor.Name = "clbSmartEyeBackColor";
            this.clbSmartEyeBackColor.Size = new System.Drawing.Size(75, 23);
            this.clbSmartEyeBackColor.TabIndex = 0;
            this.clbSmartEyeBackColor.UseVisualStyleBackColor = true;
            // 
            // clbSmartEyePointColor
            // 
            this.clbSmartEyePointColor.AutoButtonString = "Automatic";
            this.clbSmartEyePointColor.CurrentColor = System.Drawing.Color.Red;
            this.clbSmartEyePointColor.Location = new System.Drawing.Point(12, 32);
            this.clbSmartEyePointColor.Name = "clbSmartEyePointColor";
            this.clbSmartEyePointColor.Size = new System.Drawing.Size(75, 23);
            this.clbSmartEyePointColor.TabIndex = 0;
            this.clbSmartEyePointColor.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(182, 342);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 26;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(264, 342);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 26;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // txbSmartEyeAddress
            // 
            this.txbSmartEyeAddress.Location = new System.Drawing.Point(113, 92);
            this.txbSmartEyeAddress.Name = "txbSmartEyeAddress";
            this.txbSmartEyeAddress.Size = new System.Drawing.Size(120, 20);
            this.txbSmartEyeAddress.TabIndex = 27;
            this.txbSmartEyeAddress.Validating += new System.ComponentModel.CancelEventHandler(this.TxbSmartEyeAddress_Validating);
            // 
            // txbSmartEyePort
            // 
            this.txbSmartEyePort.Location = new System.Drawing.Point(273, 92);
            this.txbSmartEyePort.Name = "txbSmartEyePort";
            this.txbSmartEyePort.Size = new System.Drawing.Size(52, 20);
            this.txbSmartEyePort.TabIndex = 27;
            this.toolTip1.SetToolTip(this.txbSmartEyePort, "Port of the Remote Procedure Call (RPC)-Server of Smart Eye");
            this.txbSmartEyePort.Validating += new System.ComponentModel.CancelEventHandler(this.TxbSmartEyePort_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(238, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "Port:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "Smart Eye address:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 13);
            this.label6.TabIndex = 33;
            this.label6.Text = "OGAMA address:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(238, 121);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 32;
            this.label7.Text = "Port:";
            // 
            // txbOGAMAPort
            // 
            this.txbOGAMAPort.Location = new System.Drawing.Point(273, 118);
            this.txbOGAMAPort.Name = "txbOGAMAPort";
            this.txbOGAMAPort.Size = new System.Drawing.Size(52, 20);
            this.txbOGAMAPort.TabIndex = 30;
            this.txbOGAMAPort.Validating += new System.ComponentModel.CancelEventHandler(this.TxbOGAMAPort_Validating);
            // 
            // txbOGAMAAddress
            // 
            this.txbOGAMAAddress.Location = new System.Drawing.Point(113, 118);
            this.txbOGAMAAddress.Name = "txbOGAMAAddress";
            this.txbOGAMAAddress.ReadOnly = true;
            this.txbOGAMAAddress.Size = new System.Drawing.Size(120, 20);
            this.txbOGAMAAddress.TabIndex = 31;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdbSmartEyeSpeedFast);
            this.groupBox3.Controls.Add(this.rdbSmartEyeSpeedSlow);
            this.groupBox3.Controls.Add(this.rdbSmartEyeSpeedMedium);
            this.groupBox3.Location = new System.Drawing.Point(225, 148);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(100, 72);
            this.groupBox3.TabIndex = 36;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Speed";
            // 
            // rdbSmartEyeSpeedFast
            // 
            this.rdbSmartEyeSpeedFast.AutoSize = true;
            this.rdbSmartEyeSpeedFast.Location = new System.Drawing.Point(6, 19);
            this.rdbSmartEyeSpeedFast.Name = "rdbSmartEyeSpeedFast";
            this.rdbSmartEyeSpeedFast.Size = new System.Drawing.Size(45, 17);
            this.rdbSmartEyeSpeedFast.TabIndex = 17;
            this.rdbSmartEyeSpeedFast.Text = "&Fast";
            // 
            // rdbSmartEyeSpeedSlow
            // 
            this.rdbSmartEyeSpeedSlow.AutoSize = true;
            this.rdbSmartEyeSpeedSlow.Location = new System.Drawing.Point(6, 53);
            this.rdbSmartEyeSpeedSlow.Name = "rdbSmartEyeSpeedSlow";
            this.rdbSmartEyeSpeedSlow.Size = new System.Drawing.Size(48, 17);
            this.rdbSmartEyeSpeedSlow.TabIndex = 19;
            this.rdbSmartEyeSpeedSlow.Text = "&Slow";
            // 
            // rdbSmartEyeSpeedMedium
            // 
            this.rdbSmartEyeSpeedMedium.AutoSize = true;
            this.rdbSmartEyeSpeedMedium.Checked = true;
            this.rdbSmartEyeSpeedMedium.Location = new System.Drawing.Point(6, 36);
            this.rdbSmartEyeSpeedMedium.Name = "rdbSmartEyeSpeedMedium";
            this.rdbSmartEyeSpeedMedium.Size = new System.Drawing.Size(62, 17);
            this.rdbSmartEyeSpeedMedium.TabIndex = 19;
            this.rdbSmartEyeSpeedMedium.TabStop = true;
            this.rdbSmartEyeSpeedMedium.Text = "&Medium";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdbSmartEyeSizeSmall);
            this.groupBox2.Controls.Add(this.rdbSmartEyeSizeLarge);
            this.groupBox2.Controls.Add(this.rdbSmartEyeSizeMedium);
            this.groupBox2.Location = new System.Drawing.Point(118, 148);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(100, 72);
            this.groupBox2.TabIndex = 35;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Size";
            // 
            // rdbSmartEyeSizeSmall
            // 
            this.rdbSmartEyeSizeSmall.AutoSize = true;
            this.rdbSmartEyeSizeSmall.Location = new System.Drawing.Point(6, 53);
            this.rdbSmartEyeSizeSmall.Name = "rdbSmartEyeSizeSmall";
            this.rdbSmartEyeSizeSmall.Size = new System.Drawing.Size(50, 17);
            this.rdbSmartEyeSizeSmall.TabIndex = 19;
            this.rdbSmartEyeSizeSmall.Text = "&Small";
            // 
            // rdbSmartEyeSizeLarge
            // 
            this.rdbSmartEyeSizeLarge.AutoSize = true;
            this.rdbSmartEyeSizeLarge.Location = new System.Drawing.Point(6, 19);
            this.rdbSmartEyeSizeLarge.Name = "rdbSmartEyeSizeLarge";
            this.rdbSmartEyeSizeLarge.Size = new System.Drawing.Size(52, 17);
            this.rdbSmartEyeSizeLarge.TabIndex = 17;
            this.rdbSmartEyeSizeLarge.Text = "&Large";
            // 
            // rdbSmartEyeSizeMedium
            // 
            this.rdbSmartEyeSizeMedium.AutoSize = true;
            this.rdbSmartEyeSizeMedium.Checked = true;
            this.rdbSmartEyeSizeMedium.Cursor = System.Windows.Forms.Cursors.Default;
            this.rdbSmartEyeSizeMedium.Location = new System.Drawing.Point(6, 36);
            this.rdbSmartEyeSizeMedium.Name = "rdbSmartEyeSizeMedium";
            this.rdbSmartEyeSizeMedium.Size = new System.Drawing.Size(62, 17);
            this.rdbSmartEyeSizeMedium.TabIndex = 18;
            this.rdbSmartEyeSizeMedium.TabStop = true;
            this.rdbSmartEyeSizeMedium.Text = "&Medium";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbSmartEye3PtsCalib);
            this.groupBox1.Controls.Add(this.rdbSmartEye9PtsCalib);
            this.groupBox1.Controls.Add(this.rdbSmartEye5PtsCalib);
            this.groupBox1.Location = new System.Drawing.Point(12, 148);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(100, 72);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Number";
            // 
            // rdbSmartEye3PtsCalib
            // 
            this.rdbSmartEye3PtsCalib.AutoSize = true;
            this.rdbSmartEye3PtsCalib.Location = new System.Drawing.Point(6, 53);
            this.rdbSmartEye3PtsCalib.Name = "rdbSmartEye3PtsCalib";
            this.rdbSmartEye3PtsCalib.Size = new System.Drawing.Size(62, 17);
            this.rdbSmartEye3PtsCalib.TabIndex = 19;
            this.rdbSmartEye3PtsCalib.Text = "&3 points";
            // 
            // rdbSmartEye9PtsCalib
            // 
            this.rdbSmartEye9PtsCalib.AutoSize = true;
            this.rdbSmartEye9PtsCalib.Location = new System.Drawing.Point(6, 19);
            this.rdbSmartEye9PtsCalib.Name = "rdbSmartEye9PtsCalib";
            this.rdbSmartEye9PtsCalib.Size = new System.Drawing.Size(62, 17);
            this.rdbSmartEye9PtsCalib.TabIndex = 17;
            this.rdbSmartEye9PtsCalib.Text = "&9 points";
            // 
            // rdbSmartEye5PtsCalib
            // 
            this.rdbSmartEye5PtsCalib.AutoSize = true;
            this.rdbSmartEye5PtsCalib.Checked = true;
            this.rdbSmartEye5PtsCalib.Cursor = System.Windows.Forms.Cursors.Default;
            this.rdbSmartEye5PtsCalib.Location = new System.Drawing.Point(6, 36);
            this.rdbSmartEye5PtsCalib.Name = "rdbSmartEye5PtsCalib";
            this.rdbSmartEye5PtsCalib.Size = new System.Drawing.Size(62, 17);
            this.rdbSmartEye5PtsCalib.TabIndex = 18;
            this.rdbSmartEye5PtsCalib.TabStop = true;
            this.rdbSmartEye5PtsCalib.Text = "&5 points";
            // 
            // chbRandomizePointOrder
            // 
            this.chbRandomizePointOrder.AutoSize = true;
            this.chbRandomizePointOrder.Checked = true;
            this.chbRandomizePointOrder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbRandomizePointOrder.Location = new System.Drawing.Point(18, 235);
            this.chbRandomizePointOrder.Name = "chbRandomizePointOrder";
            this.chbRandomizePointOrder.Size = new System.Drawing.Size(132, 17);
            this.chbRandomizePointOrder.TabIndex = 37;
            this.chbRandomizePointOrder.Text = "Randomize point order";
            this.chbRandomizePointOrder.UseVisualStyleBackColor = true;
            // 
            // SmartEyeSettingsDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(351, 377);
            this.Controls.Add(this.chbRandomizePointOrder);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txbOGAMAPort);
            this.Controls.Add(this.txbOGAMAAddress);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txbSmartEyePort);
            this.Controls.Add(this.txbSmartEyeAddress);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SmartEyeSettingsDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Specify Smart Eye settings ...";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.GroupBox groupBox4;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private OgamaControls.ColorButton clbSmartEyeBackColor;
    private OgamaControls.ColorButton clbSmartEyePointColor;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.TextBox txbSmartEyeAddress;
    private System.Windows.Forms.TextBox txbSmartEyePort;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox txbOGAMAPort;
    private System.Windows.Forms.TextBox txbOGAMAAddress;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.RadioButton rdbSmartEyeSpeedFast;
    private System.Windows.Forms.RadioButton rdbSmartEyeSpeedSlow;
    private System.Windows.Forms.RadioButton rdbSmartEyeSpeedMedium;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.RadioButton rdbSmartEyeSizeSmall;
    private System.Windows.Forms.RadioButton rdbSmartEyeSizeLarge;
    private System.Windows.Forms.RadioButton rdbSmartEyeSizeMedium;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.RadioButton rdbSmartEye3PtsCalib;
    private System.Windows.Forms.RadioButton rdbSmartEye9PtsCalib;
    private System.Windows.Forms.RadioButton rdbSmartEye5PtsCalib;
    private System.Windows.Forms.CheckBox chbRandomizePointOrder;
    private System.Windows.Forms.ToolTip toolTip1;
  }
}