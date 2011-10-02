namespace Ogama.Modules.Recording.Gazegroup
{
  partial class GazetrackerIPClientSettingsDialog
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
      this.label5 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.txbServerPort = new System.Windows.Forms.TextBox();
      this.txbServerAddress = new System.Windows.Forms.TextBox();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOK = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.aleaToolTip = new System.Windows.Forms.ToolTip(this.components);
      this.label6 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.txbClientAddress = new System.Windows.Forms.TextBox();
      this.txbClientPort = new System.Windows.Forms.TextBox();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(9, 98);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(128, 13);
      this.label5.TabIndex = 42;
      this.label5.Text = "Gazedata server address:";
      this.aleaToolTip.SetToolTip(this.label5, "IP address of the eye tracker server.");
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(290, 98);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(29, 13);
      this.label4.TabIndex = 41;
      this.label4.Text = "Port:";
      this.aleaToolTip.SetToolTip(this.label4, "Target port of the server socket link.");
      // 
      // txbServerPort
      // 
      this.txbServerPort.Location = new System.Drawing.Point(325, 95);
      this.txbServerPort.Name = "txbServerPort";
      this.txbServerPort.Size = new System.Drawing.Size(52, 20);
      this.txbServerPort.TabIndex = 39;
      this.txbServerPort.TextChanged += new System.EventHandler(this.TxbServerPortTextChanged);
      // 
      // txbServerAddress
      // 
      this.txbServerAddress.Location = new System.Drawing.Point(145, 95);
      this.txbServerAddress.Name = "txbServerAddress";
      this.txbServerAddress.Size = new System.Drawing.Size(133, 20);
      this.txbServerAddress.TabIndex = 40;
      this.txbServerAddress.TextChanged += new System.EventHandler(this.TxbServerAddressTextChanged);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(301, 151);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 38;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(220, 151);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(75, 23);
      this.btnOK.TabIndex = 37;
      this.btnOK.Text = "&OK";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(90, 17);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(235, 72);
      this.label1.TabIndex = 35;
      this.label1.Text = "Please specify the network settings for the gazetracker IP connection.\r\nAll other" +
          " settings are available in the gazetracker settings of the gazetracker GUI appli" +
          "cation.";
      // 
      // pictureBox1
      // 
      this.pictureBox1.Image = global::Ogama.Properties.Resources.ITU_61_61;
      this.pictureBox1.Location = new System.Drawing.Point(16, 12);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(64, 59);
      this.pictureBox1.TabIndex = 34;
      this.pictureBox1.TabStop = false;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(9, 124);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(129, 13);
      this.label6.TabIndex = 48;
      this.label6.Text = "Command server address:";
      this.aleaToolTip.SetToolTip(this.label6, "IP address of the client application.");
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(290, 124);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(29, 13);
      this.label7.TabIndex = 50;
      this.label7.Text = "Port:";
      this.aleaToolTip.SetToolTip(this.label7, "Listen port of the client socket link.");
      // 
      // txbClientAddress
      // 
      this.txbClientAddress.Location = new System.Drawing.Point(145, 121);
      this.txbClientAddress.Name = "txbClientAddress";
      this.txbClientAddress.Size = new System.Drawing.Size(133, 20);
      this.txbClientAddress.TabIndex = 49;
      this.txbClientAddress.TextChanged += new System.EventHandler(this.TxbClientAddressTextChanged);
      // 
      // txbClientPort
      // 
      this.txbClientPort.Location = new System.Drawing.Point(325, 121);
      this.txbClientPort.Name = "txbClientPort";
      this.txbClientPort.Size = new System.Drawing.Size(52, 20);
      this.txbClientPort.TabIndex = 51;
      this.txbClientPort.TextChanged += new System.EventHandler(this.TxbClientPortTextChanged);
      // 
      // GazetrackerIPSettingsDialog
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(388, 180);
      this.Controls.Add(this.txbClientPort);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.txbClientAddress);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.txbServerPort);
      this.Controls.Add(this.txbServerAddress);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.pictureBox1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "GazetrackerIPSettingsDialog";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Specify gazetracker client settings ...";
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txbServerPort;
    private System.Windows.Forms.TextBox txbServerAddress;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.ToolTip aleaToolTip;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox txbClientAddress;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox txbClientPort;
  }
}