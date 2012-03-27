using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

//using System.Linq.Expressions;
//using System.Data.Linq;
//using System.Data.Linq.Mapping;
//using System.Data;
using Visifire.Charts;

namespace Ogama.Modules.Diagrams
{
    /// <summary>
    /// Interaction logic for DiagramsControl.xaml
    /// </summary>
    public partial class DiagramsControl : UserControl
    {
        List<ChartType> defaultChartTypes = new List<ChartType>();

        public DiagramsControl()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string name = (sender as ComboBox).SelectedItem as string;
            defaultChartTypes.Find((type) => { return type.Name == name; }).Draw();
        }

        private void comboChartTypes_Loaded(object sender, RoutedEventArgs e)
        {
            ChartType custom = new ChartType(theChart);
            custom.Name = "test custom chart";
            custom.YVariable = "";
            custom.XVariable="";
            custom.GroupBy = "";
            defaultChartTypes.Add(custom);
            //ChartType AverageFixationBySubjectAndTrial =
            //    new Chart_AverageFixationBySubjectAndTrial(theChart);
            //ChartType AverageFixationOverTime =
            //    new Chart_AverageFixationOverTime(theChart);
            //ChartType PupilDiameterOverTime = new Chart_PupilDiameterOverTime(theChart);
            //ChartType XOverYGazePosition = new Chart_XOverYGazePosition(theChart);
            //ChartType SubjectCountOverAgeBySex = new Chart_SubjectCountOverAgeBySex(theChart);
            //ChartType subjectCountOver_____ByOccupation =
            //    new Chart_subjectCountOver_____ByOccupation(theChart);
            //ChartType SaccadeDistanceOverSaccadeNumber =
            //    new Chart_SaccadeDistanceOverSaccadeNumber(theChart);
            //ChartType AverageFixationDurationOverSex =
            //    new Chart_AverageFixationDurationOverSex(theChart);
            //ChartType AverageFixationDurationOverAgeBySex =
            //    new Chart_AverageFixationDurationOverAgeBySex(theChart);

            //defaultChartTypes.Add(AverageFixationBySubjectAndTrial);
            //defaultChartTypes.Add(AverageFixationOverTime);
            //defaultChartTypes.Add(PupilDiameterOverTime);
            //defaultChartTypes.Add(XOverYGazePosition);
            //defaultChartTypes.Add(SubjectCountOverAgeBySex);
            //defaultChartTypes.Add(subjectCountOver_____ByOccupation);
            //defaultChartTypes.Add(SaccadeDistanceOverSaccadeNumber);
            //defaultChartTypes.Add(AverageFixationDurationOverSex);
            //defaultChartTypes.Add(AverageFixationDurationOverAgeBySex);

            foreach (var chart in defaultChartTypes)
            {
                comboChartTypes.Items.Add(chart.Name);
            }
            comboChartTypes.SelectedIndex = 0;
        }
    }
}
