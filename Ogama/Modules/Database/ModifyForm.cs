using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ogama.Modules.Database
{
  public partial class ModifyForm : Form
  {
    public DatabaseModule databaseModule;

    public ModifyForm()
    {
      InitializeComponent();
    }

    private void button1_MouseDown(object sender, MouseEventArgs e)
    {
      
      try
      {
        int iValueX = (int)numericDropdownPosx.Value;
        int iValueY = (int)numericUpDownPoxY.Value;

        databaseModule.handleRawDataUpdate(iValueX, iValueY);
        
      }
      catch (Exception error)
      {
        Console.WriteLine(error);
      }
      this.Close();
    }
  }
}
