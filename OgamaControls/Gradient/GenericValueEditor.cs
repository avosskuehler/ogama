/*
 *  Copyright (C) 2003 by ILOG.
 *  All Rights Reserved.
 *  Author : Emmanuel Tissandier (etissandier@ilog.fr)
 */

using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Collections;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;

namespace OgamaControls
{

    /// <summary>
    /// Represents a Windows control that allows you to edit a value of any type.
    /// </summary>
    /// <remarks>
    /// <p>The <strong>GenericValueEditor</strong> control allows the user to edit
    /// values of a specific type. Use the <see cref="Value"/> property to access
    /// the edited value.</p>
    /// <p>The type of objects to edit is defined by the
    /// <see cref="EditedType"/> property of this control. The
    /// <strong>GenericValueEditor</strong> uses the <see cref="UITypeEditor"/> and
    /// <see cref="TypeConverter"/> installed on that type to edit and validate values.</p>
    /// <p>When the <see cref="UITypeEditor"/> associated with the edited type has the style
    /// <strong>DropDown</strong> (see <see cref="UITypeEditorEditStyle"/>), then
    /// this control will display a down arrow button that drops the custom editor.
    /// When the <see cref="UITypeEditor"/> associated with the edited type has the style
    /// <strong>Modal</strong>, then this control will display a <strong>...</strong> button
    /// that opens the modal dialog.</p>
    /// <p>When no <see cref="UITypeEditor"/> is associated with the edited type or the
    /// associated editor is of style <strong>None</strong>, then the behavior of the
    /// control depends on the edited type. If the type is enumerated, then the control acts
    /// like a combo box of the enumerated values. If the type is not an enumerated type,
    /// then the control acts like a text box.</p>
    /// <p>If the editor associated with the edited type can display a representation of
    /// the edited value (see 
    /// <see cref="UITypeEditor.GetPaintValueSupported()">UITypeEditor.GetPaintValueSupported</see>),
    /// then a small rectangle showing this representation will be displayed in addition to the
    /// textual value.</p> 
    /// </remarks>
    /// <example>
    /// <para lang="C#,Visual Basic">The following code sample shows how to create a <strong>GenericValueEditor</strong> for editing 
    /// a <see cref="System.Drawing.Color"/> stucture.
    /// </para>
    /// <code lang="C#">
    /// private GenericValueEditor GetColorEditor(Color startColor) {
    ///		GenericValueEditor editor = new GenericValueEditor();
    ///		editor.EditedType = typeof(Color);
    ///		editor.Value = startColor;
    ///		return editor;
    /// }
    /// </code>
    /// <code lang="Visual Basic">
    /// Private Funtion GetColorEditor(ByVal startColor As Color) as GenericValueEditor
    ///		Dim editor as GenericValueEditor = New GenericValueEditor()
    ///		editor.EditedType = GetType(Color)
    ///		editor.Value = startColor
    ///		Return editor
    /// End Function
    /// </code>
    /// </example>
    [ToolboxItem(true)]
    [DefaultEvent("ValueChanged")]	
    public class GenericValueEditor : Control {

        #region Instance Members

        /// <summary>
        /// Indicates whether the control is in auto size mode.
        /// </summary>
        private bool autoSize;

        /// <summary>
        /// The border style. Note that initialization must be done here.
        /// </summary>
        private BorderStyle borderStyle = BorderStyle.Fixed3D;

        /// <summary>
        /// Edited type.
        /// </summary>
        private Type editedType;

        /// <summary>
        /// The type converter for the edited type.
        /// </summary>
        private TypeConverter converter;

        /// <summary>
        /// The editor for the currently edited type.
        /// </summary>
        private UITypeEditor editor;

        /// <summary>
        /// Current value of the editor.
        /// </summary>
        private object currentValue;

        /// <summary>
        /// The text box for editing text.
        /// </summary>
        private TextBox textBox;

        /// <summary>
        /// A button used to drop UI type editors, if any.
        /// </summary>
        private EditorButton editorButton;
		
        /// <summary>
        /// A control used to paint the current value.
        /// </summary>
        private PreviewControl previewControl;

        /// <summary>
        /// Indicates whether a button should be displayed to drop a <strong>UITypeEditor</strong>
        /// or the standard value list box.
        /// </summary>
        private bool hasButton;

        /// <summary>
        /// The <strong>IWindowsFormsEditorService</strong> that 
        /// allows you to drop UI type editors.
        /// </summary>
        private EditorService editorService;

        /// <summary>
        /// Indicates if the UITypeEditor can paint the value.
        /// </summary>
        internal bool paintValueSupported;

        /// <summary>
        /// Indicates if the type converter defines standard values for the type.
        /// </summary>
        private bool hasStandardValues;

        /// <summary>
        /// Indicates if we want to hide the textbox and only paint the value.
        /// </summary>
        private bool showPreviewOnly;

        /// <summary>
        /// Default width of the paint value rectangle.
        /// </summary>
        internal const int PAINT_VALUE_WIDTH = 20;

        /// <summary>
        /// UITypeEditor for types with standard values.
        /// </summary>
        private StandardValuesUIEditor standardValuesUIEditor;
		
        /// <summary>
        /// Event fired when the <see cref="Value"/> property is changed on the control.
        /// </summary>
        [Category("Property Changed")]
        [Description("Event fired when the Value property is changed on the control.")]
        public event EventHandler ValueChanged;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericValueEditor"/> class.
        /// </summary>
        /// <remarks>The default edited type is <see cref="string"/>.</remarks>
        public GenericValueEditor() : this(typeof(string)) {
            Value = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericValueEditor"/> class using
        /// the specified type.
        /// </summary>
        /// <param name="editedType">The <see cref="Type"/> of object that can be edited by this control.</param>
        public GenericValueEditor(Type editedType) {
            SetStyle(ControlStyles.Selectable, true);
            SetStyle(ControlStyles.FixedHeight, true);
            autoSize = true;

            SuspendLayout();
          
            // Text box Control
            textBox = new TextBox();
            InitTextBox();

            // editor button
            editorButton = new EditorButton();
            editorButton.Click += new EventHandler(ButtonClicked);

            // Paint value box
            previewControl = new PreviewControl(this);
            previewControl.Click += new EventHandler(PreviewControlClicked);

            // Add the sub-controls

            Controls.AddRange(new Control[] {previewControl, textBox, editorButton});

            editorService  = new EditorService(this);

            EditedType = editedType;
            ResumeLayout();
        }

        /// <summary>
        /// Initializes the text box .
        /// </summary>
        private void InitTextBox() {
            textBox.AcceptsReturn = false;
            textBox.AcceptsTab = false;
            textBox.AutoSize = false;
            textBox.CausesValidation = false;
            textBox.BorderStyle = BorderStyle.None;
            textBox.KeyDown += new KeyEventHandler(TextBoxKeyDown);
            textBox.KeyUp += new KeyEventHandler(TextBoxKeyUp);
            textBox.KeyPress += new KeyPressEventHandler(TextBoxKeyPress);
            textBox.TextChanged += new EventHandler(TextBoxTextChanged);
            textBox.Validating += new CancelEventHandler(TextBoxValidating);
            textBox.Validated += new EventHandler(TextBoxValidated);
            textBox.GotFocus += new EventHandler(TextBoxGotFocus);
            textBox.LostFocus += new EventHandler(TextBoxLostFocus);
        }
        #endregion
		
        #region Properties

        /// <summary>
        /// This member overrides <see cref="Control.BackgroundImage">Control.BackgroundImage</see>.
        /// </summary>
        [Browsable(false)]
        public override Image BackgroundImage {
            get {
                return base.BackgroundImage;
            }
            set {	
                base.BackgroundImage = value;
            }
        }


        /// <summary>
        /// Gets or sets the foreground color of the control.
        /// </summary>
        /// <value>A <see cref="System.Drawing.Color"/> that represents the foreground color of the control.
        /// The default value is the value for window text (<see cref="SystemColors.WindowText">SystemColors.WindowText</see>).</value>
        [Description("The foreground color.")]
        [DefaultValue(typeof(Color), "WindowText")]		
        public override Color ForeColor {
            get {
                return textBox.ForeColor;
            }
            set {
                if (ForeColor != value) {
                    textBox.ForeColor = value;
                    OnForeColorChanged(EventArgs.Empty);
                }
            }
        }


        /// <summary>
        /// Resets the <see cref="ForeColor"/> property to its default value.
        /// </summary>
        private new void ResetForeColor() {
            ForeColor = SystemColors.WindowText;
        }


        /// <summary>
        /// Gets or sets the background color of the control.
        /// </summary>
        /// <value>A <see cref="System.Drawing.Color"/> that represents the background color of the control.
        /// The default value is the value for window text (<see cref="SystemColors.Window">SystemColors.Window</see>).</value>
        [Description("The background color.")]
        [DefaultValue(typeof(Color), "Window")]
        public override Color BackColor {
            get {
                return textBox.BackColor;
            }
            set {
                if (BackColor != value) {
                    textBox.BackColor = value;
                    Invalidate(true);
                    OnBackColorChanged(EventArgs.Empty);
                }
            }
        }


        /// <summary>
        /// Resets the <see cref="BackColor"/> property to its default value.
        /// </summary>
        private new void ResetBackColor() {
            BackColor = SystemColors.Window;
        }


        /// <summary>
        /// Gets or sets a value indicating whether the control automatically adjusts its height to the font height.
        /// </summary>
        /// <value><see langword="true"/> if the control adjusts its height to closely fit 
        /// its contents; <see langword="false"/> otherwise. The default value is <see langword="true"/>.</value>
        [Category("Behavior")]
        [DefaultValue(true)]
        [Description("Indicating whether the control automatically adjusts its height to the font height.")]
        public new bool AutoSize {
            get {
                return autoSize;
            }
            set {
                if (value != autoSize) {
                    autoSize = value;
                    AdjustHeight();
                    SetStyle(ControlStyles.FixedHeight, value);
                    OnAutoSizeChanged(EventArgs.Empty);
                }
            }
        }


        ///// <summary>
        ///// Event fired when the <see cref="AutoSize"/> property is changed on the control.
        ///// </summary>
        //[Category("Property Changed")]
        //[Description("Event fired when the AutoSize property is changed on the control.")]
        //public event EventHandler AutoSizeChanged;

        ///// <summary>
        ///// Invoked when the <see cref="AutoSize"/> property is changed on the control.
        ///// </summary>
        ///// <param name="e">An <see cref="EventArgs"/> that contains the event data. </param>
        ///// <remarks>Called when the <strong>AutoSize</strong> property is changed.</remarks>
        //protected override void OnAutoSizeChanged(EventArgs e) {
        //    if (AutoSizeChanged != null)
        //        AutoSizeChanged(this, e);
        //}


        /// <summary>
        /// This member overrides <see cref="Control.CreateParams">Control.CreateParams</see>.
        /// </summary>
        protected override CreateParams CreateParams {
            get {
                int WS_EX_CLIENTEDGE = 0x200;
                int WS_BORDER = 0x800000;

                CreateParams cparams;

                cparams = base.CreateParams;

                switch (borderStyle) {
                    case BorderStyle.Fixed3D:
                        cparams.ExStyle = cparams.ExStyle | WS_EX_CLIENTEDGE;
                        break;
                    case BorderStyle.FixedSingle:
                        cparams.Style = cparams.Style | WS_BORDER;
                        break;
                }
                return cparams;
            }
        }


        /// <summary>
        /// Gets or sets the border style of the control.
        /// </summary>
        /// <value>One of the <see cref="System.Windows.Forms.BorderStyle"/> values. The default value
        /// is <see cref="System.Windows.Forms.BorderStyle.Fixed3D"/>.</value>
        [Category("Appearance")]
        [DefaultValue(BorderStyle.Fixed3D)]
        [Description("The border style of the control.")]
        [Localizable(true)]
        public BorderStyle BorderStyle {
            get {
                return borderStyle;
            }
            set {
                if (borderStyle != value) {
                    borderStyle = value;
                    UpdateStyles();
                    AdjustHeight();
                    LayoutSubControls();
                    Invalidate(true);
                    OnBorderStyleChanged(EventArgs.Empty);
                }
            }
        }


        /// <summary>
        /// Event fired when the <see cref="BorderStyle"/> property is changed on the control.
        /// </summary>
        [Category("Property Changed")]
        [Description("Event fired when the BorderStyle property is changed on the control.")]
        public event EventHandler BorderStyleChanged;

        /// <summary>
        /// Invoked when the <see cref="BorderStyle"/> property is changed on the control.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data. </param>
        /// <remarks>Called when the <strong>BorderStyle</strong> property is changed.</remarks>
        protected virtual void OnBorderStyleChanged(EventArgs e) {
            if (BorderStyleChanged != null)
                BorderStyleChanged(this, e);
        }


        /// <summary>
        /// Gets or sets the way text is aligned in a <see cref="GenericValueEditor"/> control.
        /// </summary>
        /// <value>One of the <see cref="System.Windows.Forms.HorizontalAlignment"/> enumeration values that specifies 
        /// how text is aligned in the control. The default value is <see cref="System.Windows.Forms.HorizontalAlignment.Left"/>.</value>
        [Category("Appearance")]
        [DefaultValue(HorizontalAlignment.Left)]
        [Description("The alignment of text.")]
        [Localizable(true)]
        public HorizontalAlignment TextAlign {
            get {
                return textBox.TextAlign;
            }
            set {
                if (TextAlign != value) {
                    textBox.TextAlign = value;
                    OnTextAlignChanged(EventArgs.Empty);
                }
            }
        }


        /// <summary>
        /// Event fired when the <see cref="TextAlign"/> property is changed on the control.
        /// </summary>
        [Category("Property Changed")]
        [Description("Event fired when the TextAlign property is changed on the control.")]
        public event EventHandler TextAlignChanged;

        /// <summary>
        /// Invoked when the <see cref="TextAlign"/> property is changed on the control.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data. </param>
        /// <remarks>Called when the <strong>TextAlign</strong> property is changed.</remarks>
        protected virtual void OnTextAlignChanged(EventArgs e) {
            if (TextAlignChanged != null)
                TextAlignChanged(this, e);
        }


        /// <summary>
        /// Gets or sets a value indicating whether text in the text box is read-only.
        /// </summary>
        /// <value><see langword="true"/> if the text box is read-only; <see langword="false"/> otherwise. The default value is 
        /// <see langword="false"/>.</value>
        /// <remarks>When this property is set to <see langword="true"/>, the contents of the control cannot be 
        /// changed by the user at runtime. With this property set to <see langword="true"/>, you can still set 
        /// the value of the <see cref="Text"/> property in code. You can use this feature instead of disabling 
        /// the control with the <see cref="Control.Enabled"/> property to allow the contents to be copied.
        /// </remarks>
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Controls whether the text in the control can be changed or not.")]
        public bool ReadOnly {
            get {
                return textBox.ReadOnly;
            }
            set {
                if (ReadOnly != value) {
                    
                    textBox.ReadOnly = value;
                    previewControl.Enabled = !value;
                    editorButton.Enabled = !value;
                    Invalidate(true);
                    OnReadOnlyChanged(EventArgs.Empty);
                }
            }
        }


        /// <summary>
        /// Event fired when the <see cref="ReadOnly"/> property is changed on the control.
        /// </summary>
        [Category("Property Changed")]
        [Description("Event fired when the ReadOnly property is changed on the control.")]
        public event EventHandler ReadOnlyChanged;

        /// <summary>
        /// Invoked when the <see cref="ReadOnly"/> property is changed on the control.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data. </param>
        /// <remarks>Called when the <strong>ReadOnly</strong> property is changed.</remarks>
        protected virtual void OnReadOnlyChanged(EventArgs e) {
            if (ReadOnlyChanged != null)
                ReadOnlyChanged(this, e);
        }


        /// <summary>
        /// Gets or sets a value indicating whether to show only the rectangle 
        /// that displays a representation of the edited value.
        /// </summary>
        /// <value><see langword="true"/> if the control shows only the rectangle that displays 
        /// a representation of the edited value; <see langword="false"/> otherwise. The textual value is then not visible.</value>
        /// <remarks>
        /// When the editor can paint a representation of the value
        /// (see <see cref="UITypeEditor.GetPaintValueSupported()">UITypeEditor.GetPaintValueSupported</see>)
        /// this control will show both a textual value and a rectangle that displays a
        /// representation of the value.
        /// Setting this property to <see langword="true"/> will hide the textual value.
        /// Not all editors can paint a representation of the edited value. If the
        /// editor cannot paint the edited value, then the value 
        /// of this property is meaningless.
        /// </remarks>
        [DefaultValue(false)]
        [Category("Appearance")]
        [Description("Indicates whether the control only displays the rectangle that previews the value and not the text.")]
        public bool ShowPreviewOnly {
            get {
                return showPreviewOnly;
            }
            set {
                showPreviewOnly = value;
                LayoutSubControls();
                Invalidate(true);
            }
        }


        /// <summary>
        /// Gets or sets the value edited by the control.
        /// </summary>
        /// <value>The current value of the editor.</value>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object Value {
            get {
                return currentValue;
            }
            set {
                if (value != null && !EditedType.IsAssignableFrom(value.GetType()))
                    throw new InvalidCastException("GenericValueEditor.Value : Bad value type.");
                currentValue = value;
                UpdateTextBoxWithValue();
                if (paintValueSupported) 
                    Invalidate(true);
                OnValueChanged(EventArgs.Empty);
            }
        }


        /// <summary>
        /// Invoked when the <see cref="Value"/> property is changed on the control.
        /// </summary>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// <remarks>Called when the <strong>Value</strong> property is changed.</remarks>
        protected virtual void OnValueChanged(EventArgs e) {
            if (ValueChanged != null)
                ValueChanged(this, e);
        }


        /// <summary>
        /// Gets or sets the starting point of text selected in the control.
        /// </summary>
        /// <value>The starting position of text selected in the control.</value>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionStart {
            get {
                return textBox.SelectionStart;
            }
            set {
                textBox.SelectionStart = value;
            }
        }


        /// <summary>
        /// Gets or sets the number of characters selected in the control.
        /// </summary>
        /// <value>The number of characters selected in the control.</value>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionLength {
            get {
                return textBox.SelectionLength;
            }
            set {
                textBox.SelectionLength = value;
            }
        }


        /// <summary>
        /// This member overrides <see cref="Control.Text">Control.Text</see>.
        /// </summary>
        public override string Text {
            get {
                return textBox.Text;
            }
            set {
                if (Text != value)
                    ValidateText(value);
            }
        }


        /// <summary>
        /// Gets or sets the <see cref="Type"/> this control can edit.
        /// </summary>
        /// <value>A <see cref="Type"/> instance that represents the type of object that can be edited 
        /// by the editor.</value>
        /// <exception cref="ArgumentNullException">The property value is
        /// <see langword="null"/>.</exception>
        /// <remarks>Changing this property also changes the <see cref="Value"/>,
        /// <see cref="Converter"/>, and <see cref="Editor"/> properties.</remarks>
        [Browsable(false)]
        [DefaultValue(typeof(string))]
        public Type EditedType {
            get {
                return editedType;
            }			
            set {
                if (value == null)
                    throw new ArgumentNullException("type should not be null");
                if (editedType != value) {
                    editedType = value;
                    converter = TypeDescriptor.GetConverter(editedType);
                    editor = (UITypeEditor)TypeDescriptor.GetEditor(editedType, typeof(UITypeEditor));
 
                    OnConverterOrEditorChanged();
                }
            }
        }


        /// <summary>
        /// Gets or sets the type converter used by the editor.
        /// </summary>
        /// <value>A <see cref="TypeConverter"/> instance that is used to convert the edited value from and to text.</value>
        [Browsable(false)]
        [AmbientValue(null)]
        public TypeConverter Converter {
            get {
                return converter;
            }
            set {
                if (converter != value) {
                    converter = value;
                    OnConverterOrEditorChanged();
                }
            }
        }


        private bool ShouldSerializeConverter() {
            return converter != null;
        }
		

        /// <summary>
        /// Gets or sets the type editor for this control.
        /// </summary>
        /// <value>A <see cref="UITypeEditor"/> instance that defines the way this control will edit the value.</value>
        /// <remarks>
        /// <p>When the editor has the style <strong>DropDown</strong>
        /// (see <see cref="UITypeEditorEditStyle"/>), then this control will display a
        /// down-arrow button that drops the custom editor. When the editor has the style
        /// <strong>Modal</strong>, then this control will display a <strong>...</strong>
        /// button that opens the modal dialog.</p>
        /// <p>When no editor is set or the editor is of style <strong>None</strong>, then
        /// the behavior of the control depends on the edited type. If the type is enumerated
        /// then the control acts like a combo box of the enumerated values. If the type is
        /// not an enumerated type, then the control acts like a text box.</p>
        /// <p>If the editor can display a representation of the edited value
        /// (see <see cref="UITypeEditor.GetPaintValueSupported()">UITypeEditor.GetPaintValueSupported</see>),
        /// then a small rectangle showing this representation will be displayed in addition
        /// to the textual value.</p>
        /// </remarks>
        [Browsable(false)]
        [AmbientValue(null)]
        public UITypeEditor Editor {
            get {
                return editor;
            }
            set {		
                if (editor != value) {
                    editor = value;
                    OnConverterOrEditorChanged();
                }
            }
        }

        private bool ShouldSerializeEditor() {
            return editor != null;
        }


        private void OnConverterOrEditorChanged() {
            paintValueSupported = editor != null && editor.GetPaintValueSupported();
            hasStandardValues = converter != null && 
                converter.GetStandardValuesSupported() && 
                converter.GetStandardValues().Count != 0;
            hasButton = (editor != null && 
                editor.GetEditStyle() != UITypeEditorEditStyle.None) 
                || hasStandardValues;

            editorButton.IsDialog = editor != null && editor.GetEditStyle() == UITypeEditorEditStyle.Modal;
            LayoutSubControls();
            UpdateTextBoxWithValue();
        }

    
        #endregion

        #region Protected Methods...

        /// <summary>
        /// This members overrides <see cref="Control.OnSystemColorsChanged">Control.OnSystemColorsChanged</see>.
        /// </summary>
        protected override void OnSystemColorsChanged(EventArgs e) {
            base.OnSystemColorsChanged(e);
            // Must delegate to the editors....
            if (editorService != null)
                editorService.SystemColorsChanged();

        }

        /// <summary>
        /// This member overrides <see cref="Control.DefaultSize">Control.DefaultSize</see>.
        /// </summary>
        protected override Size DefaultSize {
            get {
                return new Size(100, PreferredHeight);
            }
        }

        /// <summary>
        /// This member overrides <see cref="Control.OnCursorChanged">Control.OnCursorChanged</see>.
        /// </summary>
        /// <param name="args">An <see cref="EventArgs"/> that contains the event data.</param>
        protected override void OnCursorChanged(EventArgs args) {
            base.OnCursorChanged(args);
            textBox.Cursor = Cursor;
        }

        /// <summary>
        /// This member overrides <see cref="Control.SetBoundsCore">Control.SetBoundsCore</see>.
        /// </summary>
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified) {
            if (autoSize && height != Height)
                height = PreferredHeight;
            base.SetBoundsCore(x, y, width, height, specified);
            LayoutSubControls();			
        }

        /// <summary>
        /// This member overrides <see cref="Control.OnFontChanged">Control.OnFontChanged</see>.
        /// </summary>
        protected override void OnFontChanged(EventArgs e) {
            base.OnFontChanged(e);
            AdjustHeight();
        }

        /// <summary>
        /// This member overrides <see cref="Control.OnHandleCreated">Control.OnHandleCreated</see>.
        /// </summary>
        protected override void OnHandleCreated(EventArgs args) {
            base.OnHandleCreated(args);
            AdjustHeight();
            LayoutSubControls();
        }

        /// <summary>
        /// This member overrides <see cref="Control.OnValidating">Control.OnValidating</see>.
        /// </summary>
        protected override void OnValidating(CancelEventArgs e) {
            editorService.HideForm();
            base.OnValidating(e);
            if (!ValidateText())
                e.Cancel = true;
        }

        /// <summary>
        /// This member overrides <see cref="Control.OnLeave">Control.OnLeave</see>.
        /// </summary>
        protected override void OnLeave(EventArgs e) {
            editorService.HideForm();
            base.OnLeave(e);
        }

        /// <summary>
        /// This member overrides <see cref="Control.OnGotFocus">Control.OnGotFocus</see>.
        /// </summary>
        protected override void OnGotFocus(EventArgs e) {
            base.OnGotFocus(e);
            textBox.Focus();
            Invalidate(true);
        }

        /// <summary>
        /// This member overrides <see cref="Control.Focused">Control.Focused</see>.
        /// </summary>
        public override bool Focused {
            get {
                return textBox.Focused;
            }
        }

        /// <summary>
        /// This member overrides <see cref="Control.OnMouseDown">Control.OnMouseDown</see>.
        /// </summary>
        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);
            Focus();
            if (!ReadOnly && !IsTextEditable())
                DropEditor();
        }
		
        /// <summary>
        /// This member overrides <see cref="Control.OnEnabledChanged">Control.OnEnabledChanged</see>.
        /// </summary>
        protected override void OnEnabledChanged(EventArgs args) {
            base.OnEnabledChanged(args);
            textBox.Enabled = Enabled;
        }

       
        #endregion

        #region Misc

  
      /// <summary>
      /// Override.Fills background with solid brush
      /// </summary>
      /// <param name="pevent"></param>
        protected override void OnPaintBackground(PaintEventArgs pevent) {
            Brush brush = new SolidBrush(Enabled? BackColor : SystemColors.Control);
            pevent.Graphics.FillRectangle(brush, this.ClientRectangle);
            brush.Dispose();
        }

        private void AdjustHeight() {
            if (autoSize)
                Height = PreferredHeight;
        }

        private int BorderSize {
            get {
                switch (borderStyle) {
                    case BorderStyle.FixedSingle : return 1;
                    case BorderStyle.Fixed3D : return 2;
                    default : return 0;
                }
            }
        }

        private int PreferredHeight {
            get {
                int preferred = Font.Height;
                if (borderStyle != BorderStyle.None) {
                    Size size = SystemInformation.BorderSize;
                    preferred += size.Height * 4 + 3;
                }
                return preferred;
            }
        }


        /// <summary>
        /// Gets the picture box of the control.
        /// </summary>
        internal PreviewControl PreviewControl {
            get {
                return previewControl;
            }
        }


        /// <summary>
        /// Invoked when clicking the picture box.
        /// </summary>
        private void PreviewControlClicked(object sender, EventArgs args) {
            Focus();
            if (!IsTextEditable())
                DropEditor();
        }

        /// <summary>
        /// Invoked when clicking the drop button.
        /// </summary>
        private void ButtonClicked(object sender, EventArgs args) {
            DropEditor();
        }
		
        private void LayoutSubControls() {
            Rectangle cRect = ClientRectangle;
            int buttonWidth = hasButton ? SystemInformation.VerticalScrollBarWidth : 0;				
   
            previewControl.Visible = paintValueSupported;
            editorButton.Visible = hasButton;

            if (paintValueSupported) 
                previewControl.SetBounds(cRect.X + 1, cRect.Y + 1,
                    ShowPreviewOnly ? Math.Max(0, cRect.Width-buttonWidth-2) :
                    Math.Min(PAINT_VALUE_WIDTH, 
                    Math.Max(0, cRect.Width-buttonWidth-2)),
                    Math.Max(0, cRect.Height - 2));

            if (hasButton) 
                editorButton.SetBounds(cRect.Right - buttonWidth, 
                    cRect.Y, buttonWidth, cRect.Height);

            if (!(ShowPreviewOnly && paintValueSupported)) {
                int leftMargin = paintValueSupported ? PAINT_VALUE_WIDTH + 5 : 1;
                int topMargin = 0;
                switch (BorderStyle) {
                    case BorderStyle.Fixed3D : topMargin =  1; break;
                    case BorderStyle.FixedSingle : topMargin = 2;break;
                }
                textBox.SetBounds(cRect.X + leftMargin , 
                    cRect.Y + topMargin, 
                    Math.Max(0, cRect.Width-buttonWidth-leftMargin), 
                    Math.Max(0, cRect.Height));  
            } else 
                textBox.Width = 0;
        }


        internal string GetValueAsText(object value) {
            if (value == null)
                return string.Empty;
            if (value is string)
                return (string)value;
            try {
                if (converter != null && converter.CanConvertTo(typeof(string)))
                    return converter.ConvertToString(value);
            } 
            catch (Exception) {}
            return value != null
                ? value.ToString()
                : string.Empty;
        }

        /// <summary>
        /// Gets the list of standard values from the converter.
        /// </summary>
        /// <returns></returns>
        internal object[] GetStandardValues() {
            object[] values = null;
		
            TypeConverter converter = Converter;
            if (converter.GetStandardValuesSupported()) {
                ICollection standard = converter.GetStandardValues();
                values = new Object[standard.Count];
                standard.CopyTo(values, 0);
            }
            return values;
        }

        /// <summary>
        /// Drops the <see cref="UITypeEditor"/> associated with the edited value.
        /// </summary>
        /// <remarks>The method may also drop a list box if the edited value does not 
        /// have any editor and the type proposes standard values.
        /// </remarks>
        protected virtual void DropEditor() {
            UITypeEditor editor = Editor;
			
            if ((editor == null || 
                editor.GetEditStyle() == UITypeEditorEditStyle.None) && 
                hasStandardValues) {
                if (standardValuesUIEditor == null)
                    standardValuesUIEditor = new StandardValuesUIEditor(this);
                editor = standardValuesUIEditor;
            } 
          
            if (editor != null) {
  
                try {
                    object result = editor.EditValue(editorService, currentValue);	
                    Value = result;
                } 
                catch (Exception) {}
              
            }
        }

        private void SelectTextBox() {
            textBox.SelectAll();
            textBox.SelectionStart = 0;
            textBox.SelectionLength = 0;
        }

  
        internal bool IsTextEditable() {
            if (showPreviewOnly && paintValueSupported)
                return false;
            TypeConverter converter = Converter;
            if (converter != null) {
                if (converter.GetStandardValuesSupported() && 
                    converter.GetStandardValuesExclusive()) {
                    return false;
                } 
                else 
                    return true;
            }
            return false;
        }

        private void TextBoxValidating(object sender, CancelEventArgs e) {
            OnValidating(e);
        }

        private void TextBoxValidated(object sender, EventArgs e) {
            OnValidated(e);
        }

        private void TextBoxTextChanged(object sender, EventArgs e) {
            OnTextChanged(e);
        }

        private void TextBoxKeyPress(object sender, KeyPressEventArgs ke) {
            OnKeyPress(ke);
        }

        private void TextBoxKeyDown(object sender, KeyEventArgs ke) {
            OnKeyDown(ke);
        }

        private void TextBoxKeyUp(object sender, KeyEventArgs ke) {
            OnKeyUp(ke);
        }

        private void TextBoxLostFocus(object sender, EventArgs e) {
            Invalidate(true);
        }

        private void TextBoxGotFocus(object sender, EventArgs e) {
            Invalidate(true);
        }

        /// <summary>
        /// This member overrides <see cref="Control.OnKeyPress">Control.OnKeyPress</see>.
        /// </summary>
        protected override void OnKeyPress(KeyPressEventArgs ke) {
            if (!(IsTextEditable()))
                ke.Handled = true;
            else if (ke.KeyChar == (char)13 || ke.KeyChar == (char)27) {
                ke.Handled = true; // avoid beep done by TextBox when
                // multiline is not allowed
            }
            base.OnKeyPress(ke);
        }

        private void SelectStandardValue(bool next) {
            if (!hasStandardValues)
                return;
            object[] values = GetStandardValues();
            int validation = next
                ? validation = values.Length - 1
                : 0;
            for (int i = 0; i < values.Length; i++) {
                if (values[i].Equals(currentValue)) {
                    if (next) {
                        if (i == 0) 
                            return;
                        validation = i - 1;
                    } 
                    else {
                        if (i == values.Length - 1)
                            return;
                        validation = i + 1;
                    }
                    break;
                }
            }
            ValidateValue(values[validation]);
            SelectTextBox();
        }

        /// <summary>
        /// This member overrides <see cref="Control.OnMouseWheel">Control.OnMouseWheel</see>.
        /// </summary>
        /// <param name="e">A <see cref="MouseEventArgs"/> that contains the data.</param>
        /// <remarks>The default implementation iterates on the standard values proposed by
        /// the edited type, if any.</remarks>
        protected override void OnMouseWheel(MouseEventArgs e) {
            if (textBox.Focused && !ReadOnly) {
                if (hasStandardValues)
                    SelectStandardValue(e.Delta > 0);
            }
            base.OnMouseWheel(e);
        }

        /// <summary>
        /// This member overrides <see cref="Control.OnKeyDown">Control.OnKeyDown</see>.
        /// </summary>
        protected override void OnKeyDown(KeyEventArgs ke) {
            if (!ReadOnly) {
                bool alt = ke.Alt;
                if (!(alt) && ke.KeyCode == Keys.Down || ke.KeyCode == Keys.Up) {
                    if (hasStandardValues) {
                        SelectStandardValue(ke.KeyCode == Keys.Down);
                        SelectTextBox();
                    }
                }
                if (alt && ke.KeyCode == Keys.Down && hasButton) {
                    ke.Handled = true;
                    DropEditor();
                } 
                else if (ke.KeyCode == Keys.Enter) {
                    ke.Handled = true;
                    ValidateText();
                } 
                else if (ke.KeyCode == Keys.Escape) {
                    // ??
                }
            }
            base.OnKeyDown(ke);
        }

        private bool ValidateValue(object value) {
            try {
                editorService.CloseDropDown();
                Value = value;
            } 
            catch (Exception) {
                return false;
            }
            return true;
        }

        private void UpdateTextBoxWithValue() {
            textBox.Text = GetValueAsText(currentValue);  
        }


        /// <summary>
        /// Is called to validate the text that is currently edited by the control.
        /// </summary>
        /// <returns><see langword="true"/> if the string has been successfully converted into 
        /// the type defined by the property <see cref="EditedType"/>; <see langword="false"/> otherwise.
        /// </returns>
        protected virtual bool ValidateText() {
            if (!ValidateText(textBox.Text)) {
                UpdateTextBoxWithValue();
                return false;
            } 
            else
                return true;
        }

        private bool ValidateText(string text) {
            object value = null;
            try {
                if (converter != null && converter.CanConvertFrom(typeof(string)))
                    value = converter.ConvertFromString(null, CultureInfo.CurrentCulture, text);
            } 
            catch (Exception) {}

            return value != null
                ? ValidateValue(value)
                : false;
        }

       
        #endregion

        #region Editor Service
        /// <summary>
        /// The <strong>IWindowsFormsEditorService</strong> that allows you to
        /// drop dialog and UI type editors for a <see cref="GenericValueEditor"/>.
        /// </summary>
        class EditorService : IServiceProvider, IWindowsFormsEditorService {

            /// <summary>
            /// The control that uses this service.
            /// </summary>
            private GenericValueEditor editor;

            /// <summary>
            /// A control that holds the dropped editors.
            /// </summary>
            private DropDownForm dropDownForm;

            /// <summary>
            /// Indicates whether we are currently closing the drop-down form.
            /// </summary>
            private bool closingDropDown;

            /// <summary>
            /// Creates the editor service.
            /// </summary>
            /// <param name="editor">The cell editor.</param>
            public EditorService(GenericValueEditor editor) {
                this.editor = editor;
            }

            /// <summary>
            /// Gets the service object of the specified type.
            /// </summary>
            /// <param name="serviceType">An object that specifies the type of service object to get.</param>
            /// <returns>A service object of type <paramref name="serviceType"/>.</returns>
            public object GetService(Type serviceType) {
                if (serviceType == typeof(IWindowsFormsEditorService)) 
                    return this;
                return null;
            }

            /// <summary>
            /// Drops the editor control.
            /// </summary>
            /// <param name="ctl">The control to drop.</param>
            public void DropDownControl(Control ctl) {

                if (dropDownForm == null)
                    dropDownForm = new DropDownForm(this);

                dropDownForm.Visible = false;
                dropDownForm.Component = ctl;

                Rectangle editorBounds = editor.Bounds;
				
                Size size = dropDownForm.Size;

                // location of the form
                Point location 
                    = new Point(editorBounds.Right - size.Width, 
                              editorBounds.Bottom+1);
                // location in screen coordinate
                location = editor.Parent.PointToScreen(location);
                    
	            // check the form is in the screen working area
                Rectangle screenWorkingArea = Screen.FromControl(editor).WorkingArea;
			
                location.X = Math.Min(screenWorkingArea.Right - size.Width, 
                                      Math.Max(screenWorkingArea.X, location.X));
               
                if (size.Height + location.Y + editor.textBox.Height > screenWorkingArea.Bottom)
                    location.Y = location.Y - size.Height - editorBounds.Height -1;
			
                dropDownForm.SetBounds(location.X, location.Y, size.Width, size.Height);
                dropDownForm.Visible = true;	
                ctl.Focus();

                editor.SelectTextBox();
                // wait for the end of the editing
                
                while (dropDownForm.Visible) {
                    Application.DoEvents();
                    User32.MsgWaitForMultipleObjects(0, 0, true, 250, 255);
                }
               
                // editing is done or aborted
				
            }

            /// <summary>
            /// Hides the drop-down editor.
            /// </summary>
            public void HideForm() {	
                if (dropDownForm != null && dropDownForm.Visible)	
                    dropDownForm.Visible = false;
            }

            /// <summary>
            /// Closes the dropped editor.
            /// </summary>
            public virtual void CloseDropDown() {
                if (closingDropDown)
                    return;
                try {
                    closingDropDown =  true;
                    if (dropDownForm != null && dropDownForm.Visible) {
                        dropDownForm.Component = null;
                        dropDownForm.Visible = false;

                        if (editor.textBox.Visible)
                            editor.textBox.Focus();			
                    }
                } 
                finally {
                    closingDropDown = false;
                }
            }

            /// <summary>
            /// Opens a dialog editor.
            /// </summary>
            /// <param name="dialog">The dialog to open.</param>
            public DialogResult ShowDialog(Form dialog) {
                dialog.ShowDialog(editor);
                return dialog.DialogResult;  
            }

            /// <summary>
            /// Is Called when the SystemColorsChanged event is received
            /// by the GenericValueEditor.
            /// </summary>
            public void SystemColorsChanged() {
                if (dropDownForm != null)
                    dropDownForm.SystemColorChanged();
            }

        }
        #endregion
    }

    #region Internal classes...
    /// <summary>
    /// The small rectangle that paints the current edited value.
    /// </summary>
    class PreviewControl : Button {
        private GenericValueEditor editor;

        public PreviewControl(GenericValueEditor editor) {
            this.editor = editor;
            Cursor = Cursors.Default;
        }

        protected override void OnPaint(PaintEventArgs pe) {
            Rectangle rect = ClientRectangle;
            Brush b = new SolidBrush(editor.BackColor);
            pe.Graphics.FillRectangle(b, rect);
            b.Dispose();
            editor.Editor.PaintValue(editor.Value, pe.Graphics, rect);
            pe.Graphics.DrawRectangle(SystemPens.WindowText, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
        }
    }

    /// <summary>
    /// The button that opens <see cref="UITypeEditor"/> controls.
    /// </summary>
    class EditorButton : Button {

        /// <summary>
        /// Indicates whether the button should be displayed as a 
        /// drop-down arrow or as a dialog button.
        /// </summary>
        private bool dialog;

        private bool pushed;

        /// <summary>
        /// Creates a <strong>EditorButton</strong>.
        /// </summary>
        public EditorButton() {
            SetStyle(ControlStyles.Selectable, true);
            BackColor = SystemColors.Control;
            ForeColor = SystemColors.ControlText;
            TabStop = false;
            IsDefault = false;
            dialog = false;
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Gets or sets a value indicating if the button should be 
        /// drawn as a drop dialog button or as a drop button.
        /// </summary>
        /// <value><see langword="true"/> if the button should be 
        /// drawn as a drop dialog button; <see langword="false"/> otherwise.</value>
        public bool IsDialog {
            get {
                return dialog;
            }
            set {
                dialog = value;
                Invalidate();
            }		
        }

        protected override void OnMouseDown(MouseEventArgs arg) {
            base.OnMouseDown(arg);
            if (arg.Button == MouseButtons.Left) {
                pushed = true;
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs arg) {
            base.OnMouseUp(arg);
            if (arg.Button == MouseButtons.Left) {
                pushed = false;
                Invalidate();
            }
        }
		
        /// <summary>
        /// This member overrides <see cref="Control.OnPaint">Control.OnPaint</see>.
        /// </summary>
        protected override void OnPaint(PaintEventArgs pe) {
            Graphics g = pe.Graphics;
            Rectangle r = ClientRectangle;

            if (dialog) {
                base.OnPaint(pe);
                // draws dot dot dot.
                int x = r.X+r.Width/2-5;
                int y = r.Bottom-5;
                Brush brush = new SolidBrush(Enabled ? SystemColors.ControlText : SystemColors.GrayText);
                g.FillRectangle(brush, x,y,2,2);
                g.FillRectangle(brush, x+4,y,2,2);
                g.FillRectangle(brush, x+8,y,2,2);
                brush.Dispose();
            } 
            else
                ControlPaint.DrawComboButton(g, ClientRectangle, !Enabled ? ButtonState.Inactive : (pushed? ButtonState.Pushed : ButtonState.Flat));
        }		
    }


    class StandardValuesUIEditor : UITypeEditor {
        
        GenericValueEditor editor;
        StandardValuesListBox listbox;
        IWindowsFormsEditorService edSvc;

        public StandardValuesUIEditor(GenericValueEditor editor) {
            this.editor = editor;
        }

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context) {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value) {            
   
            // Uses the IWindowsFormsEditorService to display a  drop-down UI
            if (edSvc == null)
              edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if( edSvc != null ) {
                if (listbox == null) {
                    listbox = new StandardValuesListBox(editor);
                    listbox.SelectedIndexChanged += new EventHandler(OnListBoxChanged);
                }
                object[] values = editor.GetStandardValues();
                listbox.Items.Clear();

                int width = 0;
                Font font = listbox.Font;

                // Add the standard values in the list box and
                // measure the text at the same time.

                using (Graphics g = listbox.CreateGraphics()) {
                    foreach (object item in values) {
                        if (!listbox.Items.Contains(item)) {
                            string valueString = editor.GetValueAsText(item);
                            if (!editor.ShowPreviewOnly)
                                width = (int)Math.Max(width, g.MeasureString(valueString, font).Width);
                            listbox.Items.Add(item);
                        }
                    }
                }

                if (editor.paintValueSupported) 
                    width += GenericValueEditor.PAINT_VALUE_WIDTH + 4;

                Rectangle bounds = editor.Bounds;
                listbox.SelectedItem = value;
                listbox.Height = 
                    Math.Max(font.Height + 2, Math.Min(200, listbox.PreferredHeight));
                listbox.Width = Math.Max(width, bounds.Width);
                
                edSvc.DropDownControl( listbox );

                if (listbox.SelectedItem != null)
                    return listbox.SelectedItem;
                else return value;
               
            }
            return value;
        }

        private void OnListBoxChanged(object sender, EventArgs e) {
            edSvc.CloseDropDown();
        }
    }

    /// <summary>
    /// <strong>ListBox</strong> which is dropped when the type contains standard values.
    /// </summary>
    /// 
    class StandardValuesListBox : ListBox {
        GenericValueEditor editor;
		
        /// <summary>
        /// Creates a <strong>DropListBox</strong>.
        /// </summary>
        public StandardValuesListBox(GenericValueEditor control) {
            editor = control;
            BorderStyle = BorderStyle.None;
            IntegralHeight = false;
            DrawMode = DrawMode.OwnerDrawVariable;
        }

        /// <summary>
        /// This member overrides <see cref="ListBox.OnDrawItem">ListBox.OnDrawItem</see>.
        /// </summary>
        protected override void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e) {			
            e.DrawBackground();

            if (e.Index < 0 || e.Index >= Items.Count)
                return;

            object value = Items[e.Index];
            Rectangle bounds = e.Bounds;

            if (editor.paintValueSupported) {
                Pen pen = new Pen(ForeColor);
                try {
                    Rectangle r = e.Bounds;
                    r.Height -= 1;
                    if (editor.ShowPreviewOnly) {
                        r.X += 2;
                        r.Width -= 5;
                    } 
                    else {
                        r.Width = GenericValueEditor.PAINT_VALUE_WIDTH;
                        r.X += 2;
                        bounds.X += GenericValueEditor.PAINT_VALUE_WIDTH + 2;
                        bounds.Width -= GenericValueEditor.PAINT_VALUE_WIDTH + 2;		
                    }
                    editor.Editor.PaintValue(value, e.Graphics, r);
                    e.Graphics.DrawRectangle(pen, r);
                } 
                finally {
                    pen.Dispose();
					
                }
            }
            if (!editor.ShowPreviewOnly || !editor.paintValueSupported) {
                Brush brush = new SolidBrush(e.ForeColor);
                StringFormat format = new StringFormat();
               
                try {					
                    e.Graphics.DrawString(editor.GetValueAsText(value), Font,  brush, bounds, format);
                } 
                finally {
                    brush.Dispose();
                    format.Dispose();
                }
            }
        }

        /// <summary>
        /// This member overrides <see cref="ListBox.OnMeasureItem">ListBox.OnMeasureItem</see>.
        /// </summary>
        protected override void OnMeasureItem(System.Windows.Forms.MeasureItemEventArgs e) {
            e.ItemHeight += 1;
        }
    }

    /// <summary>
    /// The form that contains the dropped down editor.
    /// </summary>
    class DropDownForm : Form {

        /// <summary>
        /// Currently dropped control.
        /// </summary>
        private Control currentControl;

        /// <summary>
        /// The service that dropped this form.
        /// </summary>
        private IWindowsFormsEditorService service;

        /// <summary>
        /// Creates a <strong>DropDownForm</strong>.
        /// </summary>
        /// <param name="service">The service that drops this form.</param>
        public DropDownForm(IWindowsFormsEditorService service) {
            StartPosition = FormStartPosition.Manual;
            currentControl = null;
            ShowInTaskbar = false;
            ControlBox = false;
            MinimizeBox = false;
            MaximizeBox = false;
            Text = "";
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Visible = false;
            this.service = service;
        }

        internal void SystemColorChanged() {
            OnSystemColorsChanged(EventArgs.Empty);
        }

        /// <summary>
        /// This member overrides <see cref="Control.OnMouseDown">Control.OnMouseDown</see>.
        /// </summary>
        /// <remarks>
        /// Closes the form when the left button is clicked.
        /// </remarks>
        protected override void OnMouseDown(MouseEventArgs me) {
            if (me.Button == MouseButtons.Left)
                service.CloseDropDown();
            base.OnMouseDown(me);
        }

        /// <summary>
        /// This member overrides <see cref="Form.OnClosed">Form.OnClosed</see>.
        /// </summary>
        protected override void OnClosed(EventArgs args) {
            if (Visible) 
                service.CloseDropDown();
            base.OnClosed(args);
        }

        /// <summary>
        /// This member overrides <see cref="Form.OnDeactivate">Form.OnDeactivate</see>.
        /// </summary>
        protected override void OnDeactivate(EventArgs args) {
            if (Visible) 
                service.CloseDropDown();
            base.OnDeactivate(args);
        }

        /// <summary>
        /// Gets or sets the control displayed by the form.
        /// </summary>
        /// <value>A <see cref="Control"/> instance.</value>
        public Control Component {
            get {	
                return currentControl;
            }
            set {			
                if (currentControl != null) {
                    Controls.Remove(currentControl);
                    currentControl = null;
                }
                if (value != null) {
                    currentControl = value;
                    Controls.Add(currentControl);
                    Size = new Size(2 + currentControl.Width, 2 + currentControl.Height);
                    currentControl.Location = new Point(0, 0);
                    currentControl.Visible = true;
                    currentControl.Resize += new EventHandler(OnCurrentControlResize);
                }
                Enabled = currentControl != null;
            }
        }

        /// <summary>
        /// Invoked when the dropped control is resized.
        /// This resizes the form and realigns it.
        /// </summary>
        private void OnCurrentControlResize(object o, EventArgs e) {
            int width;
            if (currentControl != null) {
                width = Width;
                Size = new Size(2 + currentControl.Width, 2 + currentControl.Height);
                Left -=  Width - width;
            }
        }
		
        /// <summary>
        /// Invoked when the form is resized.
        /// </summary>
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified) {
            if (currentControl != null) {
                currentControl.SetBounds(0, 0, width - 2, height - 2);
                width = currentControl.Width;
                height = currentControl.Height;
                if (height == 0 && currentControl is ListBox ) {
                    height = ((ListBox)currentControl).ItemHeight;
                    currentControl.Height = height;
                }
                width = width + 2;
                height = height + 2;
            }
            base.SetBoundsCore(x, y, width, height, specified);
        }

    }
    #endregion
}