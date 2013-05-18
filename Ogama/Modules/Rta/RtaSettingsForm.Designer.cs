using Ogama.Modules.Rta.Model;
namespace Ogama.Modules.Rta
{
    using DirectShowLib;
    using GTHardware.Cameras.DirectShow;
    using System;
    using System.Runtime.InteropServices;
    using Ogama.Modules.Rta;
    using System.Collections.Generic;

    partial class RtaSettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.ComboBox cbbAudioDevices;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericBoxFramerate;

        private RtaSettings RtaSettings = null;

        private RtaController rtaController = new RtaController();

        private static List<string> availableVideoFilterNames = null;


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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnVideoCompressorProperties = new System.Windows.Forms.Button();
            this.cbbVideoCompressor = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAudioCompressorProperties = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cbbAudioCompressor = new System.Windows.Forms.ComboBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.cbbAudioDevices = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numericBoxFramerate = new System.Windows.Forms.NumericUpDown();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericBoxFramerate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Ogama.Properties.Resources.video;
            this.pictureBox1.Location = new System.Drawing.Point(18, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // btnVideoCompressorProperties
            // 
            this.btnVideoCompressorProperties.Image = global::Ogama.Properties.Resources.PropertiesHS;
            this.btnVideoCompressorProperties.Location = new System.Drawing.Point(366, 18);
            this.btnVideoCompressorProperties.Name = "btnVideoCompressorProperties";
            this.btnVideoCompressorProperties.Size = new System.Drawing.Size(23, 23);
            this.btnVideoCompressorProperties.TabIndex = 20;
            this.btnVideoCompressorProperties.UseVisualStyleBackColor = true;
            this.btnVideoCompressorProperties.Click += new System.EventHandler(this.btnVideoCompressorProperties_Click);
            // 
            // cbbVideoCompressor
            // 
            this.cbbVideoCompressor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbVideoCompressor.FormattingEnabled = true;
            this.cbbVideoCompressor.Location = new System.Drawing.Point(150, 18);
            this.cbbVideoCompressor.Name = "cbbVideoCompressor";
            this.cbbVideoCompressor.Size = new System.Drawing.Size(210, 21);
            this.cbbVideoCompressor.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Video compressor";
            // 
            // btnAudioCompressorProperties
            // 
            this.btnAudioCompressorProperties.Image = global::Ogama.Properties.Resources.PropertiesHS;
            this.btnAudioCompressorProperties.Location = new System.Drawing.Point(368, 63);
            this.btnAudioCompressorProperties.Name = "btnAudioCompressorProperties";
            this.btnAudioCompressorProperties.Size = new System.Drawing.Size(21, 21);
            this.btnAudioCompressorProperties.TabIndex = 25;
            this.btnAudioCompressorProperties.UseVisualStyleBackColor = true;
            this.btnAudioCompressorProperties.Click += new System.EventHandler(this.btnAudioCompressorProperties_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(58, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Audio compressor";
            // 
            // cbbAudioCompressor
            // 
            this.cbbAudioCompressor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbAudioCompressor.FormattingEnabled = true;
            this.cbbAudioCompressor.Location = new System.Drawing.Point(150, 63);
            this.cbbAudioCompressor.Name = "cbbAudioCompressor";
            this.cbbAudioCompressor.Size = new System.Drawing.Size(208, 21);
            this.cbbAudioCompressor.TabIndex = 23;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Ogama.Properties.Resources.sound;
            this.pictureBox2.Location = new System.Drawing.Point(18, 63);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(30, 30);
            this.pictureBox2.TabIndex = 22;
            this.pictureBox2.TabStop = false;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(229, 197);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 26;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(313, 197);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 27;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Ogama.Properties.Resources.sound;
            this.pictureBox3.Location = new System.Drawing.Point(20, 103);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(30, 30);
            this.pictureBox3.TabIndex = 28;
            this.pictureBox3.TabStop = false;
            // 
            // cbbAudioDevices
            // 
            this.cbbAudioDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbAudioDevices.FormattingEnabled = true;
            this.cbbAudioDevices.Location = new System.Drawing.Point(150, 103);
            this.cbbAudioDevices.Name = "cbbAudioDevices";
            this.cbbAudioDevices.Size = new System.Drawing.Size(208, 21);
            this.cbbAudioDevices.TabIndex = 30;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 34;
            // 
            // numericBoxFramerate
            // 
            this.numericBoxFramerate.Location = new System.Drawing.Point(150, 150);
            this.numericBoxFramerate.Name = "numericBoxFramerate";
            this.numericBoxFramerate.Size = new System.Drawing.Size(120, 20);
            this.numericBoxFramerate.TabIndex = 33;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::Ogama.Properties.Resources.Desktop1;
            this.pictureBox4.Location = new System.Drawing.Point(20, 150);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(30, 30);
            this.pictureBox4.TabIndex = 35;
            this.pictureBox4.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Audio device";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(58, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 36;
            this.label4.Text = "Framerate";
            // 
            // RtaSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 232);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.numericBoxFramerate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbbAudioDevices);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAudioCompressorProperties);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbbAudioCompressor);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnVideoCompressorProperties);
            this.Controls.Add(this.cbbVideoCompressor);
            this.Controls.Add(this.label2);
            this.Name = "RtaSettingsForm";
            this.Text = "RTA Settings";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericBoxFramerate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnVideoCompressorProperties;
        private System.Windows.Forms.ComboBox cbbVideoCompressor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAudioCompressorProperties;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbbAudioCompressor;
        private System.Windows.Forms.PictureBox pictureBox2;




        /// <summary>
        /// 
        /// 
        /// </summary>
        protected void InitializeVideoCombobox(RtaSettings rtaSettings)
        {
            string preselectedVideoCompressor = rtaSettings.VideoCompressorName;
            DsDevice[] deviceList = DsDevice.GetDevicesOfCat(FilterCategory.VideoCompressorCategory);
            if (deviceList.Length == 0)
            {
                this.cbbVideoCompressor.Text = "No video compressors found";
                return;
            }

            string preselectedCompressor = rtaSettings.VideoCompressorName;

             List<string> filterNames = null;
             if (RtaController.hasNotLoadedVideoFilterNames())
             {
                 ProgressDialog dialog = new ProgressDialog();
                 dialog.start();
                 filterNames = dialog.getFilterNames();
             }
             else
             {
                 filterNames = rtaController.getAvailbleVideoFilterNames();
             }

            for (int i = 0; i < filterNames.Count; i++)
            {
                
                string filterName = filterNames[i];

                this.cbbVideoCompressor.Items.Add(filterName);

                if (filterName != null && preselectedCompressor != null)
                {
                    if (filterName.Contains(preselectedCompressor))
                    {
                        this.cbbVideoCompressor.SelectedIndex = i;
                    }
                }
            }

        

        }

        /// <summary>
        /// 
        /// </summary>
        protected void InitializeAudioCompressor(RtaSettings rtaSettings)
        {
            DsDevice[] audioCompressorList = DsDevice.GetDevicesOfCat(FilterCategory.AudioCompressorCategory);
            if (audioCompressorList.Length == 0)
            {
                this.cbbAudioCompressor.Text = "No audio compressors found";
                return;
            }
            
            string preselectedAudioCompressor = rtaSettings.AudioCompressorName;

            for (int i = 0; i < audioCompressorList.Length; i++)
            {
                string audioCompressor = audioCompressorList[i].Name;
                this.cbbAudioCompressor.Items.Add(audioCompressor);

                if (audioCompressor.Equals("PCM"))
                {
                    cbbAudioCompressor.SelectedIndex = i;
                }

                if (audioCompressor.Equals(preselectedAudioCompressor))
                {
                    cbbAudioCompressor.SelectedIndex = i;
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        protected void InitializeAudioDevice(RtaSettings rtaSettings)
        {
            cbbAudioDevices.Items.Add("Disabled");

            DsDevice[] deviceList = DsDevice.GetDevicesOfCat(FilterCategory.AudioInputDevice);
            string preselectedValue = rtaSettings.AudioInputDeviceName;

            for (int i = 0; i < deviceList.Length; i++)
            {
                string deviceName = deviceList[i].Name;
                cbbAudioDevices.Items.Add(deviceName);

                if (i == 0)
                {
                    cbbAudioDevices.SelectedIndex = 1;
                }
            
                if (deviceName.Equals(preselectedValue))
                {
                    cbbAudioDevices.SelectedIndex = i;
                }
                
                
            }

            
            

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rtaSettings"></param>
        protected void InitializeFramerate(RtaSettings rtaSettings)
        {
            numericBoxFramerate.Value = rtaSettings.Framerate;
        }
        

        /// <summary>
        /// </summary>
        /// <param name="sender">Source of the event</param>
        /// <param name="e">An empty <see cref="EventArgs"/></param>
        private void btnVideoCompressorProperties_Click(object sender, EventArgs e)
        {
            // Create the filter for the selected video compressor
            IBaseFilter compressorFilter = DirectShowUtils.CreateFilter(
              FilterCategory.VideoCompressorCategory,
              this.cbbVideoCompressor.Text);

            // Show property page
            if (compressorFilter != null)
            {
                DirectShowUtils.DisplayPropertyPage(this.Handle, compressorFilter);
                Marshal.ReleaseComObject(compressorFilter);
            }
        }


        /// <summary>
        /// </summary>
        /// <param name="sender">Source of the event</param>
        /// <param name="e">An empty <see cref="EventArgs"/></param>
        private void btnAudioCompressorProperties_Click(object sender, EventArgs e)
        {
            // Create the filter for the selected video compressor
            IBaseFilter compressorFilter = DirectShowUtils.CreateFilter(
              FilterCategory.AudioCompressorCategory,
              this.cbbAudioCompressor.Text);

            // Show property page
            if (compressorFilter != null)
            {
                DirectShowUtils.DisplayPropertyPage(this.Handle, compressorFilter);
                Marshal.ReleaseComObject(compressorFilter);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        public void Initialize(RtaSettings settings)
        {
            this.RtaSettings = settings;

            this.renderSettings(this.RtaSettings);
        }

        private void renderSettings(RtaSettings settings)
        {
            this.InitializeAudioDevice(settings);
            this.InitializeAudioCompressor(settings);
            this.InitializeVideoCombobox(settings);
            this.InitializeFramerate(settings);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RtaSettings getRtaSettings()
        {
            if (this.RtaSettings == null)
            {
                this.RtaSettings = new RtaSettings();
            }
            if(this.cbbAudioCompressor.SelectedItem != null)
            {
                string AudioCompressor = this.cbbAudioCompressor.SelectedItem.ToString();
                this.RtaSettings.AudioCompressorName = AudioCompressor;
            }

            if (this.cbbAudioDevices != null)
            {
                string AudioDevice = this.cbbAudioDevices.SelectedItem.ToString();
                this.RtaSettings.AudioInputDeviceName = AudioDevice;
            }

            if (this.cbbVideoCompressor != null)
            {
                string VideoCompressor = this.cbbVideoCompressor.SelectedItem.ToString();
                this.RtaSettings.VideoCompressorName = VideoCompressor;
            }

            if (this.numericBoxFramerate != null)
            {
                int framerate = (int)this.numericBoxFramerate.Value;
                this.RtaSettings.Framerate = framerate;
            }

            return this.RtaSettings;
        }

        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;


    }
}