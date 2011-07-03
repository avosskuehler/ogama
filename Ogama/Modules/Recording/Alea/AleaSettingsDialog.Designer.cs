namespace Ogama.Modules.Recording.Alea
{
    partial class AleaSettingsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AleaSettingsDialog));
            this.chbRandomizePointOrder = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txbServerPort = new System.Windows.Forms.TextBox();
            this.txbServerAddress = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.clbAleaBackColor = new OgamaControls.ColorButton(this.components);
            this.clbAleaPointColor = new OgamaControls.ColorButton(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdbAleaEyeBoth = new System.Windows.Forms.RadioButton();
            this.rdbAleaEyeLeft = new System.Windows.Forms.RadioButton();
            this.rdbAleaEyeRight = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdbAleaAreaBottom = new System.Windows.Forms.RadioButton();
            this.rdbAleaAreaFull = new System.Windows.Forms.RadioButton();
            this.rdbAleaAreaCenter = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbAlea1PtsCalib = new System.Windows.Forms.RadioButton();
            this.rdbAlea5PtsCalib = new System.Windows.Forms.RadioButton();
            this.rdbAlea16PtsCalib = new System.Windows.Forms.RadioButton();
            this.rdbAlea9PtsCalib = new System.Windows.Forms.RadioButton();
            this.chbSkipBadPoints = new System.Windows.Forms.CheckBox();
            this.chbPlayAudioFeedback = new System.Windows.Forms.CheckBox();
            this.chbSlowMode = new System.Windows.Forms.CheckBox();
            this.aleaToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txbClientAddress = new System.Windows.Forms.TextBox();
            this.txbClientPort = new System.Windows.Forms.TextBox();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chbRandomizePointOrder
            // 
            this.chbRandomizePointOrder.AutoSize = true;
            this.chbRandomizePointOrder.Checked = true;
            this.chbRandomizePointOrder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbRandomizePointOrder.Location = new System.Drawing.Point(12, 264);
            this.chbRandomizePointOrder.Name = "chbRandomizePointOrder";
            this.chbRandomizePointOrder.Size = new System.Drawing.Size(132, 17);
            this.chbRandomizePointOrder.TabIndex = 43;
            this.chbRandomizePointOrder.Text = "Randomize point order";
            this.chbRandomizePointOrder.UseVisualStyleBackColor = true;
            this.chbRandomizePointOrder.CheckedChanged += new System.EventHandler(this.chbRandomizePointOrder_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 42;
            this.label5.Text = "Serveraddress:";
            this.aleaToolTip.SetToolTip(this.label5, "IP address of the eye tracker server.");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(238, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 41;
            this.label4.Text = "Port:";
            this.aleaToolTip.SetToolTip(this.label4, "Target port of the server socket link.");
            // 
            // txbServerPort
            // 
            this.txbServerPort.Location = new System.Drawing.Point(273, 95);
            this.txbServerPort.Name = "txbServerPort";
            this.txbServerPort.Size = new System.Drawing.Size(52, 20);
            this.txbServerPort.TabIndex = 39;
            this.txbServerPort.TextChanged += new System.EventHandler(this.txbServerPort_TextChanged);
            // 
            // txbServerAddress
            // 
            this.txbServerAddress.Location = new System.Drawing.Point(93, 95);
            this.txbServerAddress.Name = "txbServerAddress";
            this.txbServerAddress.Size = new System.Drawing.Size(133, 20);
            this.txbServerAddress.TabIndex = 40;
            this.txbServerAddress.TextChanged += new System.EventHandler(this.txbServerAddress_TextChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(248, 386);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 38;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(167, 386);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 37;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.clbAleaBackColor);
            this.groupBox4.Controls.Add(this.clbAleaPointColor);
            this.groupBox4.Location = new System.Drawing.Point(10, 315);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(313, 63);
            this.groupBox4.TabIndex = 36;
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
            // clbAleaBackColor
            // 
            this.clbAleaBackColor.AutoButtonString = "Automatic";
            this.clbAleaBackColor.CurrentColor = System.Drawing.SystemColors.ControlLight;
            this.clbAleaBackColor.Location = new System.Drawing.Point(154, 32);
            this.clbAleaBackColor.Name = "clbAleaBackColor";
            this.clbAleaBackColor.Size = new System.Drawing.Size(75, 23);
            this.clbAleaBackColor.TabIndex = 0;
            this.clbAleaBackColor.UseVisualStyleBackColor = true;
            this.clbAleaBackColor.ColorChanged += new System.EventHandler(this.clbAleaBackColor_ColorChanged);
            // 
            // clbAleaPointColor
            // 
            this.clbAleaPointColor.AutoButtonString = "Automatic";
            this.clbAleaPointColor.CurrentColor = System.Drawing.Color.Red;
            this.clbAleaPointColor.Location = new System.Drawing.Point(6, 32);
            this.clbAleaPointColor.Name = "clbAleaPointColor";
            this.clbAleaPointColor.Size = new System.Drawing.Size(75, 23);
            this.clbAleaPointColor.TabIndex = 0;
            this.clbAleaPointColor.UseVisualStyleBackColor = true;
            this.clbAleaPointColor.ColorChanged += new System.EventHandler(this.clbAleaPointColor_ColorChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(90, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(235, 72);
            this.label1.TabIndex = 35;
            this.label1.Text = "Please specify the calibration settings for the Alea Tracker.\r\nThat is the number" +
                " of calibration points, the calibration area, the eye which is used for calibrat" +
                "ion and the calibration colors.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(16, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(54, 75);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 34;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdbAleaEyeBoth);
            this.groupBox3.Controls.Add(this.rdbAleaEyeLeft);
            this.groupBox3.Controls.Add(this.rdbAleaEyeRight);
            this.groupBox3.Location = new System.Drawing.Point(225, 153);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(100, 82);
            this.groupBox3.TabIndex = 33;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Eye";
            // 
            // rdbAleaEyeBoth
            // 
            this.rdbAleaEyeBoth.AutoSize = true;
            this.rdbAleaEyeBoth.Checked = true;
            this.rdbAleaEyeBoth.Location = new System.Drawing.Point(6, 19);
            this.rdbAleaEyeBoth.Name = "rdbAleaEyeBoth";
            this.rdbAleaEyeBoth.Size = new System.Drawing.Size(47, 17);
            this.rdbAleaEyeBoth.TabIndex = 17;
            this.rdbAleaEyeBoth.TabStop = true;
            this.rdbAleaEyeBoth.Text = "Both";
            this.aleaToolTip.SetToolTip(this.rdbAleaEyeBoth, "Calibrate both eyes.");
            this.rdbAleaEyeBoth.CheckedChanged += new System.EventHandler(this.rdbAleaEye_CheckedChanged);
            // 
            // rdbAleaEyeLeft
            // 
            this.rdbAleaEyeLeft.AutoSize = true;
            this.rdbAleaEyeLeft.Cursor = System.Windows.Forms.Cursors.Default;
            this.rdbAleaEyeLeft.Location = new System.Drawing.Point(6, 37);
            this.rdbAleaEyeLeft.Name = "rdbAleaEyeLeft";
            this.rdbAleaEyeLeft.Size = new System.Drawing.Size(43, 17);
            this.rdbAleaEyeLeft.TabIndex = 18;
            this.rdbAleaEyeLeft.Text = "Left";
            this.aleaToolTip.SetToolTip(this.rdbAleaEyeLeft, "Calibrate left, track both eyes");
            this.rdbAleaEyeLeft.CheckedChanged += new System.EventHandler(this.rdbAleaEye_CheckedChanged);
            // 
            // rdbAleaEyeRight
            // 
            this.rdbAleaEyeRight.AutoSize = true;
            this.rdbAleaEyeRight.Location = new System.Drawing.Point(6, 55);
            this.rdbAleaEyeRight.Name = "rdbAleaEyeRight";
            this.rdbAleaEyeRight.Size = new System.Drawing.Size(50, 17);
            this.rdbAleaEyeRight.TabIndex = 19;
            this.rdbAleaEyeRight.Text = "Right";
            this.aleaToolTip.SetToolTip(this.rdbAleaEyeRight, "Calibrate right, track both eyes.");
            this.rdbAleaEyeRight.CheckedChanged += new System.EventHandler(this.rdbAleaEye_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdbAleaAreaBottom);
            this.groupBox2.Controls.Add(this.rdbAleaAreaFull);
            this.groupBox2.Controls.Add(this.rdbAleaAreaCenter);
            this.groupBox2.Location = new System.Drawing.Point(119, 153);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(100, 82);
            this.groupBox2.TabIndex = 32;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Calibration Area";
            // 
            // rdbAleaAreaBottom
            // 
            this.rdbAleaAreaBottom.AutoSize = true;
            this.rdbAleaAreaBottom.Location = new System.Drawing.Point(6, 53);
            this.rdbAleaAreaBottom.Name = "rdbAleaAreaBottom";
            this.rdbAleaAreaBottom.Size = new System.Drawing.Size(58, 17);
            this.rdbAleaAreaBottom.TabIndex = 19;
            this.rdbAleaAreaBottom.Text = "&Bottom";
            this.aleaToolTip.SetToolTip(this.rdbAleaAreaBottom, "Points are located in the lower half of the monitor.");
            this.rdbAleaAreaBottom.CheckedChanged += new System.EventHandler(this.rdbAleaArea_CheckedChanged);
            // 
            // rdbAleaAreaFull
            // 
            this.rdbAleaAreaFull.AutoSize = true;
            this.rdbAleaAreaFull.Checked = true;
            this.rdbAleaAreaFull.Location = new System.Drawing.Point(6, 19);
            this.rdbAleaAreaFull.Name = "rdbAleaAreaFull";
            this.rdbAleaAreaFull.Size = new System.Drawing.Size(41, 17);
            this.rdbAleaAreaFull.TabIndex = 17;
            this.rdbAleaAreaFull.TabStop = true;
            this.rdbAleaAreaFull.Text = "&Full";
            this.aleaToolTip.SetToolTip(this.rdbAleaAreaFull, "Outer points are 5% off the monitor border.");
            this.rdbAleaAreaFull.CheckedChanged += new System.EventHandler(this.rdbAleaArea_CheckedChanged);
            // 
            // rdbAleaAreaCenter
            // 
            this.rdbAleaAreaCenter.AutoSize = true;
            this.rdbAleaAreaCenter.Cursor = System.Windows.Forms.Cursors.Default;
            this.rdbAleaAreaCenter.Location = new System.Drawing.Point(6, 36);
            this.rdbAleaAreaCenter.Name = "rdbAleaAreaCenter";
            this.rdbAleaAreaCenter.Size = new System.Drawing.Size(56, 17);
            this.rdbAleaAreaCenter.TabIndex = 18;
            this.rdbAleaAreaCenter.Text = "&Center";
            this.aleaToolTip.SetToolTip(this.rdbAleaAreaCenter, "Outer points are 20% off the monitor border.");
            this.rdbAleaAreaCenter.CheckedChanged += new System.EventHandler(this.rdbAleaArea_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbAlea1PtsCalib);
            this.groupBox1.Controls.Add(this.rdbAlea5PtsCalib);
            this.groupBox1.Controls.Add(this.rdbAlea16PtsCalib);
            this.groupBox1.Controls.Add(this.rdbAlea9PtsCalib);
            this.groupBox1.Location = new System.Drawing.Point(12, 153);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(100, 97);
            this.groupBox1.TabIndex = 31;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Number";
            // 
            // rdbAlea1PtsCalib
            // 
            this.rdbAlea1PtsCalib.AutoSize = true;
            this.rdbAlea1PtsCalib.Location = new System.Drawing.Point(6, 70);
            this.rdbAlea1PtsCalib.Name = "rdbAlea1PtsCalib";
            this.rdbAlea1PtsCalib.Size = new System.Drawing.Size(57, 17);
            this.rdbAlea1PtsCalib.TabIndex = 20;
            this.rdbAlea1PtsCalib.Text = "1 point";
            this.rdbAlea1PtsCalib.CheckedChanged += new System.EventHandler(this.rdbAleaNumPtsCalib_CheckedChanged);
            // 
            // rdbAlea5PtsCalib
            // 
            this.rdbAlea5PtsCalib.AutoSize = true;
            this.rdbAlea5PtsCalib.Location = new System.Drawing.Point(6, 53);
            this.rdbAlea5PtsCalib.Name = "rdbAlea5PtsCalib";
            this.rdbAlea5PtsCalib.Size = new System.Drawing.Size(62, 17);
            this.rdbAlea5PtsCalib.TabIndex = 19;
            this.rdbAlea5PtsCalib.Text = "5 points";
            this.rdbAlea5PtsCalib.CheckedChanged += new System.EventHandler(this.rdbAleaNumPtsCalib_CheckedChanged);
            // 
            // rdbAlea16PtsCalib
            // 
            this.rdbAlea16PtsCalib.AutoSize = true;
            this.rdbAlea16PtsCalib.Location = new System.Drawing.Point(6, 19);
            this.rdbAlea16PtsCalib.Name = "rdbAlea16PtsCalib";
            this.rdbAlea16PtsCalib.Size = new System.Drawing.Size(68, 17);
            this.rdbAlea16PtsCalib.TabIndex = 17;
            this.rdbAlea16PtsCalib.Text = "16 points";
            this.rdbAlea16PtsCalib.CheckedChanged += new System.EventHandler(this.rdbAleaNumPtsCalib_CheckedChanged);
            // 
            // rdbAlea9PtsCalib
            // 
            this.rdbAlea9PtsCalib.AutoSize = true;
            this.rdbAlea9PtsCalib.Checked = true;
            this.rdbAlea9PtsCalib.Cursor = System.Windows.Forms.Cursors.Default;
            this.rdbAlea9PtsCalib.Location = new System.Drawing.Point(6, 36);
            this.rdbAlea9PtsCalib.Name = "rdbAlea9PtsCalib";
            this.rdbAlea9PtsCalib.Size = new System.Drawing.Size(62, 17);
            this.rdbAlea9PtsCalib.TabIndex = 18;
            this.rdbAlea9PtsCalib.TabStop = true;
            this.rdbAlea9PtsCalib.Text = "9 points";
            this.rdbAlea9PtsCalib.CheckedChanged += new System.EventHandler(this.rdbAleaNumPtsCalib_CheckedChanged);
            // 
            // chbSkipBadPoints
            // 
            this.chbSkipBadPoints.AutoSize = true;
            this.chbSkipBadPoints.Checked = true;
            this.chbSkipBadPoints.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbSkipBadPoints.Location = new System.Drawing.Point(12, 287);
            this.chbSkipBadPoints.Name = "chbSkipBadPoints";
            this.chbSkipBadPoints.Size = new System.Drawing.Size(99, 17);
            this.chbSkipBadPoints.TabIndex = 44;
            this.chbSkipBadPoints.Text = "Skip bad points";
            this.chbSkipBadPoints.UseVisualStyleBackColor = true;
            this.chbSkipBadPoints.CheckedChanged += new System.EventHandler(this.chbSkipBadPoints_CheckedChanged);
            // 
            // chbPlayAudioFeedback
            // 
            this.chbPlayAudioFeedback.AutoSize = true;
            this.chbPlayAudioFeedback.Checked = true;
            this.chbPlayAudioFeedback.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbPlayAudioFeedback.Location = new System.Drawing.Point(198, 264);
            this.chbPlayAudioFeedback.Name = "chbPlayAudioFeedback";
            this.chbPlayAudioFeedback.Size = new System.Drawing.Size(127, 17);
            this.chbPlayAudioFeedback.TabIndex = 45;
            this.chbPlayAudioFeedback.Text = "Play Audio Feedback";
            this.chbPlayAudioFeedback.UseVisualStyleBackColor = true;
            this.chbPlayAudioFeedback.CheckedChanged += new System.EventHandler(this.chbPlayAudioFeedback_CheckedChanged);
            // 
            // chbSlowMode
            // 
            this.chbSlowMode.AutoSize = true;
            this.chbSlowMode.Location = new System.Drawing.Point(198, 287);
            this.chbSlowMode.Name = "chbSlowMode";
            this.chbSlowMode.Size = new System.Drawing.Size(79, 17);
            this.chbSlowMode.TabIndex = 46;
            this.chbSlowMode.Text = "Slow Mode";
            this.chbSlowMode.UseVisualStyleBackColor = true;
            this.chbSlowMode.CheckedChanged += new System.EventHandler(this.chbSlowMode_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 48;
            this.label6.Text = "Clientaddress:";
            this.aleaToolTip.SetToolTip(this.label6, "IP address of the client application.");
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(238, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 50;
            this.label7.Text = "Port:";
            this.aleaToolTip.SetToolTip(this.label7, "Listen port of the client socket link.");
            // 
            // txbClientAddress
            // 
            this.txbClientAddress.Location = new System.Drawing.Point(93, 121);
            this.txbClientAddress.Name = "txbClientAddress";
            this.txbClientAddress.Size = new System.Drawing.Size(133, 20);
            this.txbClientAddress.TabIndex = 49;
            this.txbClientAddress.TextChanged += new System.EventHandler(this.txbClientAddress_TextChanged);
            // 
            // txbClientPort
            // 
            this.txbClientPort.Location = new System.Drawing.Point(273, 121);
            this.txbClientPort.Name = "txbClientPort";
            this.txbClientPort.Size = new System.Drawing.Size(52, 20);
            this.txbClientPort.TabIndex = 51;
            this.txbClientPort.TextChanged += new System.EventHandler(this.txbClientPort_TextChanged);
            // 
            // AleaSettingsDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(335, 415);
            this.Controls.Add(this.txbClientPort);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txbClientAddress);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.chbSlowMode);
            this.Controls.Add(this.chbPlayAudioFeedback);
            this.Controls.Add(this.chbSkipBadPoints);
            this.Controls.Add(this.chbRandomizePointOrder);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txbServerPort);
            this.Controls.Add(this.txbServerAddress);
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
            this.Name = "AleaSettingsDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Specify Alea calibration settings ...";
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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

        private System.Windows.Forms.CheckBox chbRandomizePointOrder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txbServerPort;
        private System.Windows.Forms.TextBox txbServerAddress;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private OgamaControls.ColorButton clbAleaBackColor;
        private OgamaControls.ColorButton clbAleaPointColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdbAleaEyeBoth;
        private System.Windows.Forms.RadioButton rdbAleaEyeLeft;
        private System.Windows.Forms.RadioButton rdbAleaEyeRight;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdbAleaAreaBottom;
        private System.Windows.Forms.RadioButton rdbAleaAreaFull;
        private System.Windows.Forms.RadioButton rdbAleaAreaCenter;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbAlea5PtsCalib;
        private System.Windows.Forms.RadioButton rdbAlea16PtsCalib;
        private System.Windows.Forms.RadioButton rdbAlea9PtsCalib;
        private System.Windows.Forms.RadioButton rdbAlea1PtsCalib;
        private System.Windows.Forms.CheckBox chbSkipBadPoints;
        private System.Windows.Forms.CheckBox chbPlayAudioFeedback;
        private System.Windows.Forms.CheckBox chbSlowMode;
        private System.Windows.Forms.ToolTip aleaToolTip;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txbClientAddress;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txbClientPort;
    }
}