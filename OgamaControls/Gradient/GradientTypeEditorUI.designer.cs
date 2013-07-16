namespace OgamaControls
{
  /// <summary>
  /// Control for creating custom controls
  /// </summary>
    partial class GradientTypeEditorUI
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
          System.Windows.Forms.Label label1;
          System.Windows.Forms.Label label2;
          System.Windows.Forms.Label label3;
          System.Windows.Forms.Label label4;
          System.Windows.Forms.Label label5;
          Gradient gradient1 = new Gradient();
          System.Drawing.Drawing2D.ColorBlend colorBlend1 = new System.Drawing.Drawing2D.ColorBlend();
          System.ComponentModel.StringConverter stringConverter1 = new System.ComponentModel.StringConverter();
          this.gradientBuilder = new GradientBuilder();
          this.grpEditor = new System.Windows.Forms.GroupBox();
          this.opacityBox = new System.Windows.Forms.NumericUpDown();
          this.positionBox = new System.Windows.Forms.NumericUpDown();
          this.colorEditor = new GenericValueEditor();
          label1 = new System.Windows.Forms.Label();
          label2 = new System.Windows.Forms.Label();
          label3 = new System.Windows.Forms.Label();
          label4 = new System.Windows.Forms.Label();
          label5 = new System.Windows.Forms.Label();
          this.grpEditor.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.opacityBox)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.positionBox)).BeginInit();
          this.SuspendLayout();
          // 
          // label1
          // 
          label1.AutoSize = true;
          label1.Location = new System.Drawing.Point(6, 22);
          label1.Name = "label1";
          label1.Size = new System.Drawing.Size(31, 13);
          label1.TabIndex = 1;
          label1.Text = "Color";
          // 
          // label2
          // 
          label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
          label2.AutoSize = true;
          label2.Location = new System.Drawing.Point(115, 49);
          label2.Name = "label2";
          label2.Size = new System.Drawing.Size(44, 13);
          label2.TabIndex = 1;
          label2.Text = "Position";
          // 
          // label3
          // 
          label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
          label3.AutoSize = true;
          label3.Location = new System.Drawing.Point(204, 49);
          label3.Name = "label3";
          label3.Size = new System.Drawing.Size(15, 13);
          label3.TabIndex = 1;
          label3.Text = "%";
          // 
          // label4
          // 
          label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
          label4.AutoSize = true;
          label4.Location = new System.Drawing.Point(7, 49);
          label4.Name = "label4";
          label4.Size = new System.Drawing.Size(43, 13);
          label4.TabIndex = 1;
          label4.Text = "Opacity";
          // 
          // label5
          // 
          label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
          label5.AutoSize = true;
          label5.Location = new System.Drawing.Point(95, 49);
          label5.Name = "label5";
          label5.Size = new System.Drawing.Size(15, 13);
          label5.TabIndex = 1;
          label5.Text = "%";
          // 
          // gradientBuilder
          // 
          this.gradientBuilder.Dock = System.Windows.Forms.DockStyle.Fill;
          colorBlend1.Colors = new System.Drawing.Color[] {
        System.Drawing.Color.White,
        System.Drawing.Color.White};
          colorBlend1.Positions = new float[] {
        0F,
        1F};
          gradient1.ColorBlend = colorBlend1;
          gradient1.GradientDirection = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
          this.gradientBuilder.Gradient = gradient1;
          this.gradientBuilder.Location = new System.Drawing.Point(224, 0);
          this.gradientBuilder.Name = "gradientBuilder";
          this.gradientBuilder.Size = new System.Drawing.Size(341, 78);
          this.gradientBuilder.TabIndex = 0;
          this.gradientBuilder.Text = "gradientBuilder1";
          this.gradientBuilder.MarkerSelected += new System.EventHandler(this.gradientBuilder_MarkerSelected);
          this.gradientBuilder.GradientChanged += new System.EventHandler(this.gradientBuilder_GradientChanged);
          // 
          // grpEditor
          // 
          this.grpEditor.Controls.Add(this.opacityBox);
          this.grpEditor.Controls.Add(this.positionBox);
          this.grpEditor.Controls.Add(label5);
          this.grpEditor.Controls.Add(label3);
          this.grpEditor.Controls.Add(label2);
          this.grpEditor.Controls.Add(label4);
          this.grpEditor.Controls.Add(label1);
          this.grpEditor.Controls.Add(this.colorEditor);
          this.grpEditor.Dock = System.Windows.Forms.DockStyle.Left;
          this.grpEditor.Enabled = false;
          this.grpEditor.Location = new System.Drawing.Point(0, 0);
          this.grpEditor.Name = "grpEditor";
          this.grpEditor.Size = new System.Drawing.Size(224, 78);
          this.grpEditor.TabIndex = 1;
          this.grpEditor.TabStop = false;
          this.grpEditor.Text = "Color Stops";
          // 
          // opacityBox
          // 
          this.opacityBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
          this.opacityBox.Location = new System.Drawing.Point(56, 47);
          this.opacityBox.Name = "opacityBox";
          this.opacityBox.Size = new System.Drawing.Size(41, 20);
          this.opacityBox.TabIndex = 3;
          this.opacityBox.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
          this.opacityBox.ValueChanged += new System.EventHandler(this.colorMixerFieldValueChanged);
          // 
          // positionBox
          // 
          this.positionBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
          this.positionBox.Location = new System.Drawing.Point(165, 47);
          this.positionBox.Name = "positionBox";
          this.positionBox.Size = new System.Drawing.Size(41, 20);
          this.positionBox.TabIndex = 2;
          this.positionBox.ValueChanged += new System.EventHandler(this.colorMixerFieldValueChanged);
          // 
          // colorEditor
          // 
          this.colorEditor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.colorEditor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
          this.colorEditor.Converter = stringConverter1;
          this.colorEditor.Location = new System.Drawing.Point(56, 19);
          this.colorEditor.Name = "colorEditor";
          this.colorEditor.Size = new System.Drawing.Size(162, 20);
          this.colorEditor.TabIndex = 0;
          this.colorEditor.ValueChanged += new System.EventHandler(this.colorMixerFieldValueChanged);
          // 
          // GradientTypeEditorUI
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.Controls.Add(this.gradientBuilder);
          this.Controls.Add(this.grpEditor);
          this.Name = "GradientTypeEditorUI";
          this.Size = new System.Drawing.Size(565, 78);
          this.grpEditor.ResumeLayout(false);
          this.grpEditor.PerformLayout();
          ((System.ComponentModel.ISupportInitialize)(this.opacityBox)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.positionBox)).EndInit();
          this.ResumeLayout(false);

        }

        #endregion

        private GenericValueEditor colorEditor;
        private System.Windows.Forms.NumericUpDown opacityBox;
        private System.Windows.Forms.NumericUpDown positionBox;
        private System.Windows.Forms.GroupBox grpEditor;
        private GradientBuilder gradientBuilder;


    }
}
