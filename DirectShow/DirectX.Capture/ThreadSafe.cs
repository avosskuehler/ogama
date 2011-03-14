using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DirectX.Capture
{
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
    /// A thread safe version to receive the <see cref="Control.Handle"/>
    /// property of the control.
    /// </summary>
    /// <param name="control">The <see cref="Control"/> to retreive the handle for.</param>
    /// <returns>An <see cref="IntPtr"/> with the handle of the control.</returns>
    public static IntPtr GetHandle(Control control)
    {
      if (control.InvokeRequired)
      {
        GetHandleCallback d = new GetHandleCallback(ThreadSafe.GetHandle);
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
        throw new ArgumentNullException("Form is null");
      }

      if (form.InvokeRequired)
      {
        form.Invoke(new MethodInvoker(form.Close));
        return;
      }

      form.Close();
    }

    /// <summary>
    /// Thread safe version to get the <see cref="Control.ClientRectangle"/> property
    /// </summary>
    /// <returns>The <see cref="Rectangle"/> of the given control.</returns>
    public static Rectangle GetClientRectangle(Control control)
    {
      if (control == null)
      {
        throw new ArgumentNullException("Control is null");
      }

      if (control.InvokeRequired)
      {
        return (Rectangle)control.Invoke(new GetRectangleInvoker(GetClientRectangle), control);
      }

      return control.ClientRectangle;
    }

  }
}
