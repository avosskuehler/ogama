using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ogama.Modules.Diagrams
{
    public partial class ChartProperties : Form
    {
        public Visifire.Charts.Chart Chart;
        public ChartProperties(Visifire.Charts.Chart chart)
        {
            InitializeComponent();
            this.Chart = chart;
            textBoxTitle.Text = Chart.Titles[0].Text;
            textBoxAxisXTitle.Text = Chart.AxesX[0].Name;
            textBoxAxisYTitle.Text = Chart.AxesY[0].Name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Chart.Titles.Clear();
            Visifire.Charts.Title title = new Visifire.Charts.Title();
            title.Text = textBoxTitle.Text;
            Chart.Titles.Add(title);
            
            Chart.AxesX.Clear();
            Visifire.Charts.Axis axisX = new Visifire.Charts.Axis();
            axisX.Title = textBoxAxisXTitle.Text;
            Chart.AxesX.Add(axisX);

            Chart.AxesY.Clear();
            Visifire.Charts.Axis axisY = new Visifire.Charts.Axis();
            axisY.Title = textBoxAxisYTitle.Text;
            Chart.AxesY.Add(axisY);
        }
    }
}
