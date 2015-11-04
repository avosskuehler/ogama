namespace Ogama.Modules.Recording.GazepointInterface
{
    partial class GazepointSettingsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GazepointSettingsDialog));
            this.rdbGazepointManualCalib = new System.Windows.Forms.RadioButton();
            this.rdbGazepointAutoCalib = new System.Windows.Forms.RadioButton();
            this.rdbGazepointTimeOutCalib = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelSpeedInSec = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txbServerPort = new System.Windows.Forms.TextBox();
            this.txbServerAddress = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txbTimeOut = new System.Windows.Forms.TextBox();
            this.chbHideCalibWindow = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdbGazepointManualCalib
            // 
            this.rdbGazepointManualCalib.AutoSize = true;
            this.rdbGazepointManualCalib.Checked = true;
            this.rdbGazepointManualCalib.Location = new System.Drawing.Point(6, 19);
            this.rdbGazepointManualCalib.Name = "rdbGazepointManualCalib";
            this.rdbGazepointManualCalib.Size = new System.Drawing.Size(60, 17);
            this.rdbGazepointManualCalib.TabIndex = 17;
            this.rdbGazepointManualCalib.TabStop = true;
            this.rdbGazepointManualCalib.Text = "Manual";
            this.rdbGazepointManualCalib.CheckedChanged += new System.EventHandler(this.rdbGazepointCalibStartType_CheckedChanged);
            // 
            // rdbGazepointAutoCalib
            // 
            this.rdbGazepointAutoCalib.AutoSize = true;
            this.rdbGazepointAutoCalib.Cursor = System.Windows.Forms.Cursors.Default;
            this.rdbGazepointAutoCalib.Location = new System.Drawing.Point(6, 42);
            this.rdbGazepointAutoCalib.Name = "rdbGazepointAutoCalib";
            this.rdbGazepointAutoCalib.Size = new System.Drawing.Size(72, 17);
            this.rdbGazepointAutoCalib.TabIndex = 18;
            this.rdbGazepointAutoCalib.Text = "Automatic";
            this.rdbGazepointAutoCalib.CheckedChanged += new System.EventHandler(this.rdbGazepointCalibStartType_CheckedChanged);
            // 
            // rdbGazepointTimeOutCalib
            // 
            this.rdbGazepointTimeOutCalib.AutoSize = true;
            this.rdbGazepointTimeOutCalib.Checked = true;
            this.rdbGazepointTimeOutCalib.Cursor = System.Windows.Forms.Cursors.Default;
            this.rdbGazepointTimeOutCalib.Location = new System.Drawing.Point(6, 19);
            this.rdbGazepointTimeOutCalib.Name = "rdbGazepointTimeOutCalib";
            this.rdbGazepointTimeOutCalib.Size = new System.Drawing.Size(66, 17);
            this.rdbGazepointTimeOutCalib.TabIndex = 18;
            this.rdbGazepointTimeOutCalib.TabStop = true;
            this.rdbGazepointTimeOutCalib.Text = "Time out";
            this.rdbGazepointTimeOutCalib.CheckedChanged += new System.EventHandler(this.rdbGazepointCalibSpeedType_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 114);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 63;
            this.label5.Text = "Tracker IP:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(236, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 62;
            this.label4.Text = "Port:";
            // 
            // labelSpeedInSec
            // 
            this.labelSpeedInSec.AutoSize = true;
            this.labelSpeedInSec.Location = new System.Drawing.Point(121, 21);
            this.labelSpeedInSec.Name = "labelSpeedInSec";
            this.labelSpeedInSec.Size = new System.Drawing.Size(47, 13);
            this.labelSpeedInSec.TabIndex = 72;
            this.labelSpeedInSec.Text = "seconds";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdbGazepointManualCalib);
            this.groupBox2.Controls.Add(this.rdbGazepointAutoCalib);
            this.groupBox2.Location = new System.Drawing.Point(15, 153);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(142, 70);
            this.groupBox2.TabIndex = 53;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Calibration start";
            // 
            // txbServerPort
            // 
            this.txbServerPort.Location = new System.Drawing.Point(271, 111);
            this.txbServerPort.Name = "txbServerPort";
            this.txbServerPort.Size = new System.Drawing.Size(52, 20);
            this.txbServerPort.TabIndex = 60;
            this.txbServerPort.Text = "4242";
            this.txbServerPort.TextChanged += new System.EventHandler(this.txbServerPort_TextChanged);
            // 
            // txbServerAddress
            // 
            this.txbServerAddress.Location = new System.Drawing.Point(78, 111);
            this.txbServerAddress.Name = "txbServerAddress";
            this.txbServerAddress.Size = new System.Drawing.Size(133, 20);
            this.txbServerAddress.TabIndex = 61;
            this.txbServerAddress.Text = "127.0.0.1";
            this.txbServerAddress.TextChanged += new System.EventHandler(this.txbServerAddress_TextChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(275, 308);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 59;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(194, 308);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 58;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(86, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(286, 91);
            this.label1.TabIndex = 56;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Ogama.Properties.Resources.GazepointFoto64;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 55;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.labelSpeedInSec);
            this.groupBox3.Controls.Add(this.txbTimeOut);
            this.groupBox3.Controls.Add(this.rdbGazepointTimeOutCalib);
            this.groupBox3.Location = new System.Drawing.Point(176, 153);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(174, 70);
            this.groupBox3.TabIndex = 54;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Calibration Speed";
            // 
            // txbTimeOut
            // 
            this.txbTimeOut.Location = new System.Drawing.Point(82, 19);
            this.txbTimeOut.Name = "txbTimeOut";
            this.txbTimeOut.Size = new System.Drawing.Size(33, 20);
            this.txbTimeOut.TabIndex = 72;
            this.txbTimeOut.Text = "5";
            this.txbTimeOut.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txbTimeOut.TextChanged += new System.EventHandler(this.txbTimeOut_TextChanged);
            // 
            // chbHideCalibWindow
            // 
            this.chbHideCalibWindow.AutoSize = true;
            this.chbHideCalibWindow.Location = new System.Drawing.Point(21, 247);
            this.chbHideCalibWindow.Name = "chbHideCalibWindow";
            this.chbHideCalibWindow.Size = new System.Drawing.Size(244, 17);
            this.chbHideCalibWindow.TabIndex = 64;
            this.chbHideCalibWindow.Text = "Hide calibration window when calibration ends";
            this.chbHideCalibWindow.UseVisualStyleBackColor = true;
            this.chbHideCalibWindow.CheckedChanged += new System.EventHandler(this.chbHideCalibWindow_CheckedChanged);
            // 
            // GazepointSettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 372);
            this.Controls.Add(this.chbHideCalibWindow);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txbServerPort);
            this.Controls.Add(this.txbServerAddress);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(400, 400);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "GazepointSettingsDialog";
            this.Text = "Specify Gazepoint GP3 calibration settings ...";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdbGazepointAutoCalib;
        private System.Windows.Forms.RadioButton rdbGazepointTimeOutCalib;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txbServerPort;
        private System.Windows.Forms.TextBox txbServerAddress;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label labelSpeedInSec;
        private System.Windows.Forms.TextBox txbTimeOut;
        private System.Windows.Forms.RadioButton rdbGazepointManualCalib;
        private System.Windows.Forms.CheckBox chbHideCalibWindow;

    }
}