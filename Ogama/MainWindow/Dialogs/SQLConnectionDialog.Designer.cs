namespace Ogama.MainWindow.Dialogs
{
  partial class SQLConnectionDialog
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLConnectionDialog));
      this.sqlConnectionUIControl = new Microsoft.Data.ConnectionUI.SqlConnectionUIControl();
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnAdvanced = new System.Windows.Forms.Button();
      this.btnTestConnection = new System.Windows.Forms.Button();
      this.btnDefaultConnection = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // sqlConnectionUIControl
      // 
      this.sqlConnectionUIControl.Location = new System.Drawing.Point(9, 9);
      this.sqlConnectionUIControl.Margin = new System.Windows.Forms.Padding(0);
      this.sqlConnectionUIControl.MinimumSize = new System.Drawing.Size(350, 360);
      this.sqlConnectionUIControl.Name = "sqlConnectionUIControl";
      this.sqlConnectionUIControl.Size = new System.Drawing.Size(350, 360);
      this.sqlConnectionUIControl.TabIndex = 1;
      // 
      // btnOK
      // 
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(203, 404);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(75, 23);
      this.btnOK.TabIndex = 2;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(284, 404);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // btnAdvanced
      // 
      this.btnAdvanced.Location = new System.Drawing.Point(9, 404);
      this.btnAdvanced.Name = "btnAdvanced";
      this.btnAdvanced.Size = new System.Drawing.Size(75, 23);
      this.btnAdvanced.TabIndex = 3;
      this.btnAdvanced.Text = "Advanced ...";
      this.btnAdvanced.UseVisualStyleBackColor = true;
      this.btnAdvanced.Click += new System.EventHandler(this.btnAdvanced_Click);
      // 
      // btnTestConnection
      // 
      this.btnTestConnection.Location = new System.Drawing.Point(90, 404);
      this.btnTestConnection.Name = "btnTestConnection";
      this.btnTestConnection.Size = new System.Drawing.Size(104, 23);
      this.btnTestConnection.TabIndex = 4;
      this.btnTestConnection.Text = "Test Connection";
      this.btnTestConnection.UseVisualStyleBackColor = true;
      this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
      // 
      // btnDefaultConnection
      // 
      this.btnDefaultConnection.Location = new System.Drawing.Point(9, 375);
      this.btnDefaultConnection.Name = "btnDefaultConnection";
      this.btnDefaultConnection.Size = new System.Drawing.Size(185, 23);
      this.btnDefaultConnection.TabIndex = 4;
      this.btnDefaultConnection.Text = "Use OGAMAs default connection.";
      this.btnDefaultConnection.UseVisualStyleBackColor = true;
      this.btnDefaultConnection.Click += new System.EventHandler(this.btnDefaultConnection_Click);
      // 
      // SQLConnectionDialog
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(370, 438);
      this.Controls.Add(this.btnDefaultConnection);
      this.Controls.Add(this.btnTestConnection);
      this.Controls.Add(this.btnAdvanced);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.sqlConnectionUIControl);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "SQLConnectionDialog";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Specify custom SQL connection ...";
      this.ResumeLayout(false);

    }

    #endregion

    private Microsoft.Data.ConnectionUI.SqlConnectionUIControl sqlConnectionUIControl;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnAdvanced;
    private System.Windows.Forms.Button btnTestConnection;
    private System.Windows.Forms.Button btnDefaultConnection;

  }
}