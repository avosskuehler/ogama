namespace Ogama.ExceptionHandling
{
  using Ogama.Modules.Common.Controls;

  partial class ExceptionDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExceptionDialog));
      this.pnlDescription = new System.Windows.Forms.Panel();
      this.dialogTop1 = new Ogama.Modules.Common.Controls.DialogTop();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.spcMessageDetails = new System.Windows.Forms.SplitContainer();
      this.lblExceptionMessage = new System.Windows.Forms.Label();
      this.txbExceptionDetails = new System.Windows.Forms.TextBox();
      this.pnlButtons = new System.Windows.Forms.Panel();
      this.chbShowDetails = new System.Windows.Forms.CheckBox();
      this.btnViewLog = new System.Windows.Forms.Button();
      this.btnAbort = new System.Windows.Forms.Button();
      this.btnOK = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.pnlDescription.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.spcMessageDetails)).BeginInit();
      this.spcMessageDetails.Panel1.SuspendLayout();
      this.spcMessageDetails.Panel2.SuspendLayout();
      this.spcMessageDetails.SuspendLayout();
      this.pnlButtons.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlDescription
      // 
      this.pnlDescription.Controls.Add(this.dialogTop1);
      this.pnlDescription.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlDescription.Location = new System.Drawing.Point(0, 0);
      this.pnlDescription.Margin = new System.Windows.Forms.Padding(0);
      this.pnlDescription.Name = "pnlDescription";
      this.pnlDescription.Size = new System.Drawing.Size(440, 60);
      this.pnlDescription.TabIndex = 7;
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "An exception occured during processing your request with the following message:";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = null;
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(440, 60);
      this.dialogTop1.TabIndex = 0;
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this.spcMessageDetails, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.pnlDescription, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.pnlButtons, 0, 2);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(440, 431);
      this.tableLayoutPanel1.TabIndex = 8;
      // 
      // spcMessageDetails
      // 
      this.spcMessageDetails.Dock = System.Windows.Forms.DockStyle.Fill;
      this.spcMessageDetails.Location = new System.Drawing.Point(3, 63);
      this.spcMessageDetails.Name = "spcMessageDetails";
      this.spcMessageDetails.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // spcMessageDetails.Panel1
      // 
      this.spcMessageDetails.Panel1.Controls.Add(this.lblExceptionMessage);
      // 
      // spcMessageDetails.Panel2
      // 
      this.spcMessageDetails.Panel2.Controls.Add(this.txbExceptionDetails);
      this.spcMessageDetails.Size = new System.Drawing.Size(434, 215);
      this.spcMessageDetails.SplitterDistance = 99;
      this.spcMessageDetails.TabIndex = 8;
      // 
      // lblExceptionMessage
      // 
      this.lblExceptionMessage.BackColor = System.Drawing.SystemColors.Window;
      this.lblExceptionMessage.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblExceptionMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblExceptionMessage.Location = new System.Drawing.Point(0, 0);
      this.lblExceptionMessage.Name = "lblExceptionMessage";
      this.lblExceptionMessage.Size = new System.Drawing.Size(434, 99);
      this.lblExceptionMessage.TabIndex = 1;
      // 
      // txbExceptionDetails
      // 
      this.txbExceptionDetails.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txbExceptionDetails.Location = new System.Drawing.Point(0, 0);
      this.txbExceptionDetails.Multiline = true;
      this.txbExceptionDetails.Name = "txbExceptionDetails";
      this.txbExceptionDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txbExceptionDetails.Size = new System.Drawing.Size(434, 112);
      this.txbExceptionDetails.TabIndex = 2;
      // 
      // pnlButtons
      // 
      this.pnlButtons.Controls.Add(this.chbShowDetails);
      this.pnlButtons.Controls.Add(this.btnViewLog);
      this.pnlButtons.Controls.Add(this.btnAbort);
      this.pnlButtons.Controls.Add(this.btnOK);
      this.pnlButtons.Controls.Add(this.label2);
      this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlButtons.Location = new System.Drawing.Point(3, 284);
      this.pnlButtons.Name = "pnlButtons";
      this.pnlButtons.Size = new System.Drawing.Size(434, 144);
      this.pnlButtons.TabIndex = 6;
      // 
      // chbShowDetails
      // 
      this.chbShowDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.chbShowDetails.AutoSize = true;
      this.chbShowDetails.Location = new System.Drawing.Point(345, 7);
      this.chbShowDetails.Name = "chbShowDetails";
      this.chbShowDetails.Size = new System.Drawing.Size(86, 17);
      this.chbShowDetails.TabIndex = 5;
      this.chbShowDetails.Text = "Show details";
      this.chbShowDetails.UseVisualStyleBackColor = true;
      this.chbShowDetails.CheckedChanged += new System.EventHandler(this.chbShowDetails_CheckedChanged);
      // 
      // btnViewLog
      // 
      this.btnViewLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnViewLog.AutoSize = true;
      this.btnViewLog.Location = new System.Drawing.Point(6, 115);
      this.btnViewLog.Name = "btnViewLog";
      this.btnViewLog.Size = new System.Drawing.Size(118, 26);
      this.btnViewLog.TabIndex = 3;
      this.btnViewLog.Text = "View exception log ...";
      this.btnViewLog.UseVisualStyleBackColor = true;
      this.btnViewLog.Click += new System.EventHandler(this.btnViewLog_Click);
      // 
      // btnAbort
      // 
      this.btnAbort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnAbort.AutoSize = true;
      this.btnAbort.DialogResult = System.Windows.Forms.DialogResult.Abort;
      this.btnAbort.Location = new System.Drawing.Point(338, 86);
      this.btnAbort.Name = "btnAbort";
      this.btnAbort.Size = new System.Drawing.Size(93, 26);
      this.btnAbort.TabIndex = 3;
      this.btnAbort.Text = "Abort OGAMA";
      this.btnAbort.UseVisualStyleBackColor = true;
      this.btnAbort.Visible = false;
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.AutoSize = true;
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(338, 115);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(93, 26);
      this.btnOK.TabIndex = 3;
      this.btnOK.Text = "Try to continue";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(0, 7);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(359, 102);
      this.label2.TabIndex = 4;
      this.label2.Text = resources.GetString("label2.Text");
      // 
      // ExceptionDialog
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(440, 431);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "ExceptionDialog";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Exception occured ...";
      this.pnlDescription.ResumeLayout(false);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.spcMessageDetails.Panel1.ResumeLayout(false);
      this.spcMessageDetails.Panel2.ResumeLayout(false);
      this.spcMessageDetails.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.spcMessageDetails)).EndInit();
      this.spcMessageDetails.ResumeLayout(false);
      this.pnlButtons.ResumeLayout(false);
      this.pnlButtons.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel pnlDescription;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.SplitContainer spcMessageDetails;
    private System.Windows.Forms.TextBox txbExceptionDetails;
    private System.Windows.Forms.Panel pnlButtons;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button btnViewLog;
    private System.Windows.Forms.Button btnAbort;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Label lblExceptionMessage;
    private System.Windows.Forms.CheckBox chbShowDetails;
    private DialogTop dialogTop1;
  }
}