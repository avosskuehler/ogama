using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using VectorGraphics;

namespace OgamaControls
{
  using VectorGraphics.Tools.Trigger;

  /// <summary>
  /// Inherits <see cref="UserControl"/>. Is to customize a trigger signal
  /// </summary>
  public partial class TriggerControl : UserControl
  {
    /// <summary>
    /// Gets or sets the <see cref="Trigger"/> constructed in this control.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public Trigger TriggerSignal
    {
      get { return GetTrigger(); }
      set { PopulateTrigger(value); }
    }

    /// <summary>
    /// Initializes a new instance of the TriggerControl class.	
    /// Fills combo box with valid entries.
    /// </summary>
    public TriggerControl()
    {
      InitializeComponent();
      cbbTriggerDevice.Items.AddRange(Enum.GetNames(typeof(TriggerOutputDevices)));
      nudTriggerSignalTime.Enabled = chbSendTrigger.Checked;
      nudTriggerValue.Enabled = chbSendTrigger.Checked;
      cbbTriggerDevice.Enabled = chbSendTrigger.Checked;
      txbPortAddress.Text = "0378";
      nudTriggerSignalTime.Value = 10;
    }

    private void PopulateTrigger(Trigger triggerToSet)
    {
      if (triggerToSet != null)
      {
        cbbTriggerDevice.SelectedItem = triggerToSet.OutputDevice.ToString();
        nudTriggerValue.Value = triggerToSet.Value;
        nudTriggerSignalTime.Value = triggerToSet.SignalingTime;
        switch (triggerToSet.Signaling)
        {
          case TriggerSignaling.None:
            chbSendTrigger.Checked = false;
            break;
          case TriggerSignaling.Enabled:
          case TriggerSignaling.Override:
            chbSendTrigger.Checked = true;
            break;
        }
				txbPortAddress.Text = triggerToSet.PortAddress.ToString("X");
      }
    }

    private Trigger GetTrigger()
    {
      Trigger returnTrigger = new Trigger();
      if (cbbTriggerDevice.SelectedItem != null)
      {
        returnTrigger.OutputDevice = (TriggerOutputDevices)Enum.Parse(typeof(TriggerOutputDevices), cbbTriggerDevice.SelectedItem.ToString());
      }
      else
      {
        returnTrigger.OutputDevice = TriggerOutputDevices.LPT;
      }
      TriggerSignaling signaling = TriggerSignaling.None;
      if (chbSendTrigger.Checked)
      {
        signaling = TriggerSignaling.Enabled;
      }
      returnTrigger.Signaling = signaling;
      returnTrigger.SignalingTime = (int)nudTriggerSignalTime.Value;
      returnTrigger.Value = (int)nudTriggerValue.Value;
			returnTrigger.PortAddress = Convert.ToInt32(txbPortAddress.Text,16);
      return returnTrigger;
    }

    private void chbSendTrigger_CheckedChanged(object sender, EventArgs e)
    {
      nudTriggerSignalTime.Enabled = chbSendTrigger.Checked;
      nudTriggerValue.Enabled = chbSendTrigger.Checked;
      cbbTriggerDevice.Enabled = chbSendTrigger.Checked;
			txbPortAddress.Enabled = chbSendTrigger.Checked;
    }

  }
}
