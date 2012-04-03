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
            checkBoxGroupBy.IsChecked = true;
            theChart.MouseDoubleClick += new MouseButtonEventHandler(theChart_MouseDoubleClick);

            FillComboAgregate();
            FillComboGroupBy();
            FillComboY();
            FillComboX(); 
            
            ChartType AverageFixationBySubjectAndTrial =
                new Chart_AverageFixationBySubjectAndTrial(theChart);
            ChartType AverageFixationOverTime =
                new Chart_AverageFixationOverTime(theChart);
            ChartType PupilDiameterOverTime = new Chart_PupilDiameterOverTime(theChart);
            ChartType XOverYGazePosition = new Chart_XOverYGazePosition(theChart);
            ChartType SubjectCountOverAgeBySex = new Chart_SubjectCountOverAgeBySex(theChart);
            ChartType subjectCountOver_____ByOccupation =
                new Chart_subjectCountOver_____ByOccupation(theChart);
            ChartType SaccadeDistanceOverSaccadeNumber =
                new Chart_SaccadeDistanceOverSaccadeNumber(theChart);
            ChartType AverageFixationDurationOverSex =
                new Chart_AverageFixationDurationOverSex(theChart);
            ChartType AverageFixationDurationOverAgeBySex =
                new Chart_AverageFixationDurationOverAgeBySex(theChart);

            defaultChartTypes.Add(AverageFixationBySubjectAndTrial);
            defaultChartTypes.Add(AverageFixationOverTime);
            defaultChartTypes.Add(PupilDiameterOverTime);
            defaultChartTypes.Add(XOverYGazePosition);
            defaultChartTypes.Add(SubjectCountOverAgeBySex);
            defaultChartTypes.Add(subjectCountOver_____ByOccupation);
            defaultChartTypes.Add(SaccadeDistanceOverSaccadeNumber);
            defaultChartTypes.Add(AverageFixationDurationOverSex);
            defaultChartTypes.Add(AverageFixationDurationOverAgeBySex);

            foreach (var chart in defaultChartTypes)
            {
                comboChartTypes.Items.Add(chart.Name);
            }
            comboChartTypes.SelectedIndex = 0;
        }

        void theChart_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ChartProperties properties = new ChartProperties(theChart);
            properties.ShowDialog();
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string name = (sender as ComboBox).SelectedItem as string;
            defaultChartTypes.Find((type) => { return type.Name == name; }).Draw();
        }

        private void comboChartTypes_Loaded(object sender, RoutedEventArgs e)
        {
            //FillComboAgregate();
            //FillComboGroupBy();
            //FillComboY();
            //FillComboX();

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

            //foreach (var chart in defaultChartTypes)
            //{
            //    comboChartTypes.Items.Add(chart.Name);
            //}
            //comboChartTypes.SelectedIndex = 0;
        }

        private void FillComboY()
        {
            foreach (string table in new[]{"Subjects","Trials","MouseFixations","GazeFixations"})
            {
                foreach (var column in Document.ActiveDocument.DocDataSet.Tables[table].Columns)
                {
                    comboY.Items.Add(table + "." + column);
                }
            }
            comboY.SelectedIndex = 0;
        }

        private void FillComboX()
        {
            foreach (string table in new[] { "Subjects", "Trials", "MouseFixations", "GazeFixations" })
            {
                foreach (var column in Document.ActiveDocument.DocDataSet.Tables[table].Columns)
                {
                    comboX.Items.Add(table + "." + column);
                }
            }
            comboX.SelectedIndex = 0;
        }

        private void FillComboGroupBy()
        {
            comboGroupBy.Items.Add("Subjects.SubjectName");
            comboGroupBy.Items.Add("Subjects.Category");
            comboGroupBy.Items.Add("Subjects.Age");
            comboGroupBy.Items.Add("Subjects.Sex");
            comboGroupBy.Items.Add("Subjects.Handedness");
            comboGroupBy.Items.Add("Subjects.Comments");//Inventive minds will find this useful
            comboGroupBy.Items.Add("Trials.TrialName");
            comboGroupBy.Items.Add("Trials.Category");
            comboGroupBy.Items.Add("Trials.EliminateData");
            
            comboGroupBy.SelectedIndex = 0;
        }

        private void FillComboAgregate()
        {
            string[] agregateFunctions = Enum.GetNames(typeof(AgregateFunction));
            foreach (string f in agregateFunctions)
            {
                comboAgregateFunction.Items.Add(f);
            }
            comboAgregateFunction.SelectedIndex = 0;
        }

        private void buttonDrawIt_Click(object sender, RoutedEventArgs e)
        {
            //ChartType custom = new ChartType(theChart, "Trials.Category", "GazeFixations.Length",
            //    "Subjects.Category", "Avg");
            //custom.Name = "test custom chart";
            //defaultChartTypes.Add(custom);

            //TODO: some checkings
            string groupBy = "";
            if (comboGroupBy.IsEnabled == true)
            {
                groupBy = comboGroupBy.Text;
            }
            ChartType customChart = new ChartType(theChart, comboX.Text, comboY.Text,
                    groupBy, comboAgregateFunction.Text);
            //customChart.Type = RenderAs.Column;
            customChart.Type = RenderAs.Pie;
            customChart.Type = GetChartTypeSelected();
            customChart.Draw();
        }

        private RenderAs GetChartTypeSelected()
        {
            if (radioColumn.IsChecked == true)
            {
                return RenderAs.Column;
            }
            else if (radioStacked.IsChecked == true)
            {
                return RenderAs.StackedColumn;
            }
            else if (radioPie.IsChecked == true)
            {
                return RenderAs.Pie;
            }
            return RenderAs.Column;//ideally should never reach here
        }

        private void comboX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ChartType.columnTypes[(string)e.AddedItems[0]] == DataType.Ordinal)
            {
                textBoxRanges.IsEnabled = true;
            }
            else
            {
                textBoxRanges.IsEnabled = false;
            }
        }

        private void checkBoxGroupBy_Checked(object sender, RoutedEventArgs e)
        {
            comboGroupBy.IsEnabled = true;
        }

        private void checkBoxGroupBy_Unchecked(object sender, RoutedEventArgs e)
        {
            comboGroupBy.IsEnabled = false;
        }
    }
}
