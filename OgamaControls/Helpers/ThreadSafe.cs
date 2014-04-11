namespace OgamaControls
{
  using System;
  using System.ComponentModel;
  using System.Drawing;
  using System.Windows.Forms;

  /// <summary>
  /// Static class providing methods for thread safe calls into
  /// windows forms controls
  /// </summary>
  public static class ThreadSafe
  {
    /// <summary>
    /// Delegate for thread safe retreiving of the controls handle.
    /// </summary>
    /// <param name="control">The <see cref="Control"/> to retreive the handle for.</param>
    /// <returns>An <see cref="IntPtr"/> with the handle of the control.</returns>
    private delegate IntPtr GetHandleCallback(Control control);

    /// <summary>
    /// This delegate enables asynchronous calls for getting
    /// a Rectangle property on a Control.
    /// </summary>
    /// <returns>A <see cref="Rectangle"/> property
    /// of the <see cref="Control"/>.</returns>
    private delegate Rectangle GetRectangleInvoker(Control control);

    /// <summary>
    /// This delegate enables asynchronous calls for getting
    /// a string property on a RichTextBox control.
    /// </summary>
    /// <returns>An <see cref="String"/> for the Text or RTF property
    /// of the <see cref="RichTextBox"/>.</returns>
    private delegate string GetStringInvoker(TextBox control);

    /// <summary>
    /// This delegate enables asynchronous calls for setting
    /// a string property on a Textbox control.
    /// </summary>
    /// <param name="control">The Textbox for which to set the text asynchronously.</param>
    /// <param name="stringToSet">A <see cref="String"/> for the Text property
    /// of the <see cref="TextBox"/>.</param>
    private delegate void SetStringInvoker(TextBox control, string stringToSet);

    /// <summary>
    /// This delegate enables asynchronous calls for setting
    /// the enabled property on a Button control.
    /// </summary>
    /// <param name="control">The Button for which to set the enabled property asynchronously.</param>
    /// <param name="enable">A <see cref="Boolean"/> for the Enabled property
    /// of the <see cref="Button"/>.</param>
    private delegate void EnableDisableButtonInvoker(Button control, bool enable);

    /// <summary>
    /// This delegate enables asynchronous calls for setting
    /// the visible property on a SplitContainer.Panel control.
    /// </summary>
    /// <param name="control">The SplitContainer parent for the panel to be asynchronously shown or hidden.</param>
    /// <param name="show">A <see cref="Boolean"/> for the visible property
    /// of the Panel.</param>
    /// <param name="panel1">True if panel1 of the splitcontainer should be modified, for panel2 set this to false.</param>
    private delegate void ShowHidePanelInvoker(SplitContainer control, bool show, bool panel1);

    /// <summary>
    /// A thread safe version to receive the <see cref="Control.Handle"/>
    /// property of the control.
    /// </summary>
    /// <param name="control">The <see cref="Control"/> to retreive the handle for.</param>
    /// <returns>An <see cref="IntPtr"/> with the handle of the control.</returns>
    public static IntPtr GetHandle(Control control)
    {
      if (control.InvokeRequired)
      {
        var d = new GetHandleCallback(GetHandle);
        return (IntPtr)control.Invoke(d, control);
      }

      return control.Handle;
    }

    /// <summary>
    /// Thread safe version to call the <see cref="Form.Close()"/> method
    /// </summary>
    /// <param name="form">A <see cref="Form"/> that should be closed.</param>
    public static void Close(Form form)
    {
      if (form == null)
      {
        throw new ArgumentNullException("form");
      }

      if (form.InvokeRequired)
      {
        form.Invoke(new MethodInvoker(form.Close));
        return;
      }

      form.Close();
    }

    /// <summary>
    /// Thread safe version to call the <see cref="Component.Dispose()"/> method
    /// </summary>
    /// <param name="control">A <see cref="Control"/> that should be disposed.</param>
    public static void Dispose(Control control)
    {
      if (control == null)
      {
        throw new ArgumentNullException("control");
      }

      if (control.InvokeRequired)
      {
        control.Invoke(new MethodInvoker(control.Dispose));
        return;
      }

      control.Dispose();
    }

    /// <summary>
    /// Thread safe version to get the <see cref="Control.ClientRectangle"/> property
    /// </summary>
    /// <returns>The <see cref="Rectangle"/> of the given control.</returns>
    public static Rectangle GetClientRectangle(Control control)
    {
      if (control == null)
      {
        throw new ArgumentNullException("control");
      }

      if (control.InvokeRequired)
      {
        return (Rectangle)control.Invoke(new GetRectangleInvoker(GetClientRectangle), control);
      }

      return control.ClientRectangle;
    }

    /// <summary>
    /// Thread safe version to set the <see cref="TextBox.Text"/> property
    /// </summary>
    /// <param name="control">The TextBox control.</param>
    /// <param name="stringToSet">The string to set.</param>
    public static void ThreadSafeSetText(TextBox control, string stringToSet)
    {
      if (control.InvokeRequired)
      {
        control.Invoke(new SetStringInvoker(ThreadSafeSetText), control, stringToSet);
        return;
      }

      control.Text = stringToSet;
    }

    /// <summary>
    /// Thread safe version to get the <see cref="TextBox.Text"/> property
    /// </summary>
    /// <param name="control">The TextBox control.</param>
    /// <returns>
    /// The <see cref="String"/> with the string to
    /// be set to the RichTextBox.
    /// </returns>
    public static string ThreadSafeGetText(TextBox control)
    {
      if (control.InvokeRequired)
      {
        return (string)control.Invoke(new GetStringInvoker(ThreadSafeGetText), control);
      }

      return control.Text;
    }

    /// <summary>
    /// Thread safe version to enable or disable a button.
    /// </summary>
    /// <param name="button">The <see cref="Button"/> control to be enabled or disabled.</param>
    /// <param name="enable">True, if the button should be enabled, otherwise false.</param>
    public static void EnableDisableButton(Button button, bool enable)
    {
      if (button.InvokeRequired)
      {
        button.Invoke(new EnableDisableButtonInvoker(EnableDisableButton), button, enable);
      }

      button.Enabled = enable;
    }

    /// <summary>
    /// Thread safe version to show or hide a split container panel.
    /// </summary>
    /// <param name="splitContainer"> The <see cref="SplitContainer"/> control. </param>
    /// <param name="show"> True, if the panel should be visible, otherwise false. </param>
    /// <param name="panel1">True if panel1 of the splitcontainer should be modified, for panel2 set this to false.</param>
    public static void ShowHideSplitContainerPanel(SplitContainer splitContainer, bool show, bool panel1)
    {
      if (splitContainer.InvokeRequired)
      {
        splitContainer.Invoke(new ShowHidePanelInvoker(ShowHideSplitContainerPanel), splitContainer, show, panel1);
      }

      if (panel1)
      {
        splitContainer.Panel1Collapsed = !show;
      }
      else
      {
        splitContainer.Panel2Collapsed = !show;
      }
    }
  }
}
