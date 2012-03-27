// -----------------------------------------------------------------------
// <copyright file="ChartTypes.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Ogama.Modules.Diagrams
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data;
    using Visifire.Charts;
    using System.Data.SqlClient;

    /// <summary>
    /// Defines a basic type of chart as the name and the correponding method to draw it.
    /// </summary>
    public class ChartType
    {
        public ChartType(Chart chart)
        {
            this.Chart = chart;
        }

        public Chart Chart = new Chart();
        public string Name;
        public string YVariable;
        public string YVariable_Table;
        public string XVariable;
        public string XVariable_Table;
        public string GroupBy;//variable defines the difference between individual series
        public string GroupBy_Table;
        public AgregateFunction AgregateFunction;
        public Visifire.Charts.RenderAs Type;
        Ogama.DataSet.OgamaDataSet db = Document.ActiveDocument.DocDataSet;
        
        public void ClearContentsAndAddTitle()
        {
            //Chart.View3D = true;
            Chart.AxesX.Clear();
            Chart.AxesY.Clear();
            Chart.Series.Clear();
            Chart.Titles.Clear();
            Title title = new Title();
            title.Text = Name;
            Chart.Titles.Add(title);
        }
        private List<string> FindTableOfColumn(string column)
        {
            List<string> tables = new List<string>();//List<DataTable> tables = new List<DataTable>();
            //if (db.Rawdata.Columns.Contains(column))
            //{
            //    tables.Add(db.Rawdata);
            //}
            if (db.GazeFixations.Columns.Contains(column))
            {
                tables.Add(db.GazeFixations.TableName);
            }
            if (db.MouseFixations.Columns.Contains(column))
            {
                tables.Add(db.MouseFixations.TableName);
            }
            return tables;
        }
        public virtual void Draw()
        {
            //to be deleted - just for debug
            AgregateFunction = AgregateFunction.Avg;//AgregateFunction.Count;
            YVariable = "Length";//"ID";
            YVariable_Table = "GazeFixations";
            XVariable = "Category";
            XVariable_Table = "Trials";
            GroupBy = "Category";
            GroupBy_Table = "Subjects";
            //------------------------------

            ClearContentsAndAddTitle();
            Axis axisX = new Axis();
            axisX.Title = XVariable;
            Chart.AxesX.Add(axisX);
            Axis axisY = new Axis();
            axisY.Title = YVariable;
            Chart.AxesY.Add(axisY);
            
            var categories = (from c in db.Tables[GroupBy_Table].AsEnumerable()
                              where !c.IsNull(GroupBy)//omit null records
                              select c[GroupBy]).Distinct();
            foreach (var series in categories)
            {
                string select = string.Format("SELECT {0}.{1}, ROUND({4}(CAST({2}.{3} AS Float)),2)",
                        XVariable_Table, XVariable, YVariable_Table, YVariable, AgregateFunction);
                string from = "FROM Trials,Subjects";
                string join = "WHERE Trials.SubjectName = Subjects.SubjectName";
                string group = string.Format("GROUP BY {0}.{1}", XVariable_Table, XVariable);//what if time?
                string seriesSelector = string.Format("AND {0}.{1} = \'{2}\'", GroupBy_Table, GroupBy, series);

                if (XVariable_Table == db.GazeFixations.TableName ||
                    YVariable_Table == db.GazeFixations.TableName ||
                    GroupBy_Table == db.GazeFixations.TableName)
                {
                    from += ",GazeFixations";
                    join += " AND GazeFixations.TrialID = Trials.TrialID"
                    + " AND GazeFixations.TrialSequence = Trials.TrialSequence"
                    + " AND GazeFixations.SubjectName = Trials.SubjectName";
                }
                else if (XVariable_Table == db.MouseFixations.TableName ||
                    YVariable_Table == db.MouseFixations.TableName ||
                    GroupBy_Table == db.MouseFixations.TableName)
                {
                    from += ",MouseFixations";
                    join += " AND MouseFixations.TrialID = Trials.TrialID"
                    + " AND MouseFixations.TrialSequence = Trials.TrialSequence"
                    + " AND MouseFixations.SubjectName = Trials.SubjectName";
                }

                
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = select + " " + from + " " + join + " " + seriesSelector + " " + group;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = db.DatabaseConnection;

                SqlDataReader result = cmd.ExecuteReader();//(System.Data.CommandBehavior.CloseConnection);

                DataSeries dataseries = new DataSeries();
                dataseries.Name = series.ToString();
                while (result.Read())
                {
                    DataPoint point = new DataPoint();
                    point.AxisXLabel = result[XVariable].ToString();//point.XValue = result[XVariable];
                    point.YValue = Convert.ToDouble(result[1]);
                    dataseries.DataPoints.Add(point);
                }
                Chart.Series.Add(dataseries);

                result.Close();
                //db.DatabaseConnection.Close();
            }
        }
    }

    public class Chart_AverageFixationBySubjectAndTrial : ChartType
    {
        public Chart_AverageFixationBySubjectAndTrial(Chart chart)
            : base(chart)
        {
        }

        public string Name
        {
            get { return "Average fixation time grouped by subject and trial category"; }
        } 
        /// <summary>
        /// the original draw
        /// </summary>
        //public override void Draw()
        //{
        //    ClearContentsAndAddTitle();//base.Draw();
        //    Axis axisX = new Axis();
        //    axisX.Title = "Trial category";
        //    axisX.Prefix = "Category:";
        //    Chart.AxesX.Add(axisX);
        //    Axis axisY = new Axis();
        //    axisY.Title = "Average fixation duration";
        //    axisY.Suffix = "ms";
        //    Chart.AxesY.Add(axisY);
        //    var db = Document.ActiveDocument.DocDataSet;//alias
        //    var subjectCategories = (from subjects in db.Subjects select subjects.Category).Distinct();
        //    foreach (var subjectCategory in subjectCategories)
        //    {
        //        var result = from tr in db.Trials
        //                     join gf in db.GazeFixations
        //                        on new { tr.TrialID, tr.SubjectName, tr.TrialSequence } equals
        //                            new { gf.TrialID, gf.SubjectName, gf.TrialSequence }
        //                     join sb in db.Subjects
        //                        on tr.SubjectName equals sb.SubjectName
        //                     where sb.Category == subjectCategory
        //                     group new { gf, tr.Category } by tr.Category into res
        //                     select new
        //                     {
        //                         Category = res.Key,
        //                         AvgFixationTime = res.Average(p => p.gf.Length)
        //                     };

        //        DataSeries series = new DataSeries();
        //        //series.RenderAs = RenderAs.Column;
        //        series.Name = subjectCategory;
        //        foreach (var p in result)
        //        {
        //            DataPoint point = new DataPoint();
        //            point.XValue = p.Category;
        //            point.YValue = p.AvgFixationTime;
        //            series.DataPoints.Add(point);
        //        }
        //        Chart.Series.Add(series);
        //    }
        //}
        /// <summary>
        /// The MegaJOIN
        /// </summary>
        public override void Draw()
        {
            ClearContentsAndAddTitle();//base.Draw();
            Axis axisX = new Axis();
            axisX.Title = "Trial category";
            axisX.Prefix = "Category:";
            Chart.AxesX.Add(axisX);
            Axis axisY = new Axis();
            axisY.Title = "Average fixation duration";
            axisY.Suffix = "ms";
            Chart.AxesY.Add(axisY);
            var db = Document.ActiveDocument.DocDataSet;//alias
            var subjectCategories = (from subjects in db.Subjects select subjects.Category).Distinct();
            foreach (var subjectCategory in subjectCategories)
            {
                //var result = from tr in db.Trials
                //             join gf in db.GazeFixations
                //                on new { tr.TrialID, tr.SubjectName, tr.TrialSequence } equals
                //                    new { gf.TrialID, gf.SubjectName, gf.TrialSequence }
                //             join sb in db.Subjects
                //                on tr.SubjectName equals sb.SubjectName
                //             where sb.Category == subjectCategory
                //             group new { gf, tr.Category } by tr.Category into res
                //             select new
                //             {
                //                 Category = res.Key,
                //                 AvgFixationTime = res.Average(p => p.gf.Length)
                //             };
                var result = from trials in db.Trials
                             join gazeFixations in db.GazeFixations
                                             on new { trials.TrialID, trials.SubjectName, trials.TrialSequence } equals
                                 new { gazeFixations.TrialID, gazeFixations.SubjectName, gazeFixations.TrialSequence }
                             join mouseFixations in db.MouseFixations
                                             on new { trials.TrialID, trials.SubjectName, trials.TrialSequence } equals
                                 new { mouseFixations.TrialID, mouseFixations.SubjectName, mouseFixations.TrialSequence }
                             join trialEvents in db.TrialEvents
                                             on new { trials.SubjectName, trials.TrialSequence } equals
                                 new { trialEvents.SubjectName, trialEvents.TrialSequence }
                             join subjects in db.Subjects
                                on trials.SubjectName equals subjects.SubjectName
                             join subjectParameters in db.SubjectParameters
                                 on subjects.SubjectName equals subjectParameters.SubjectName
                             join Params in db.Params
                                 on subjectParameters.Param equals Params.Param
                             join AOIs in db.AOIs
                                 on trials.TrialID equals AOIs.TrialID
                             join shapeGroups in db.ShapeGroups
                                 on AOIs.ShapeGroup equals shapeGroups.ShapeGroup
                             where subjects.Category == subjectCategory
                             group new { gazeFixations, trials.Category } by trials.Category into res
                             select new
                             {
                                 Category = res.Key,
                                 AvgFixationTime = res.Average(p => (int)p.gazeFixations["Length"])
                             };

                DataSeries series = new DataSeries();
                //series.RenderAs = RenderAs.Column;
                series.Name = subjectCategory;
                foreach (var p in result)
                {
                    DataPoint point = new DataPoint();
                    point.XValue = p.Category;
                    point.YValue = p.AvgFixationTime;
                    series.DataPoints.Add(point);
                }
                Chart.Series.Add(series);
            }
            
        }
    }

    public enum AgregateFunction
    {
        Sum,
        Avg,
        Count,
        Min,
        Max
    }

    public class Chart_AverageFixationOverTime : ChartType
    {
        public Chart_AverageFixationOverTime(Chart chart)
            : base(chart)
        {
        }
        public string Name
        {
            get { return "Average fixation duration over trial time"; }
        }
        public override void Draw()
        {
        }
    }

    public class Chart_PupilDiameterOverTime : ChartType
    {
        public Chart_PupilDiameterOverTime(Chart chart)
            : base(chart)
        {
        }
        public string Name
        {
            get { return "Pupil diameter over time"; }
        }
        public override void Draw()
        {
        }
    }

    public class Chart_XOverYGazePosition : ChartType
    {
        public Chart_XOverYGazePosition(Chart chart)
            : base(chart)
        {
        }
        public string Name
        {
            get { return "X over Y gaze position raw data"; }
        }
        public override void Draw()
        {
        }
    }

    public class Chart_SubjectCountOverAgeBySex : ChartType
    {
        public Chart_SubjectCountOverAgeBySex(Chart chart)
            : base(chart)
        {
        }
        public string Name
        {
            get { return "Subject count over time by sex"; }
        }
        public override void Draw()
        {
        }
    }

    public class Chart_subjectCountOver_____ByOccupation : ChartType
    {
        public Chart_subjectCountOver_____ByOccupation(Chart chart)
            : base(chart)
        {
        }
        public string Name
        {
            get { return "Subject count over ___ by occupation"; }
        }
        public override void Draw()
        {
        }
    }

    public class Chart_SaccadeDistanceOverSaccadeNumber : ChartType
    {
        public Chart_SaccadeDistanceOverSaccadeNumber(Chart chart)
            : base(chart)
        {
        }
        public string Name
        {
            get { return "Saccade distance over saccade number"; }
        }
        public override void Draw()
        {
        }
    }

    public class Chart_AverageFixationDurationOverSex : ChartType
    {
        public Chart_AverageFixationDurationOverSex(Chart chart)
            : base(chart)
        {
        }
        public string Name
        {
            get { return "Average fixation duration over sex"; }
        }
        public override void Draw()
        {
        }
    }

    public class Chart_AverageFixationDurationOverAgeBySex : ChartType
    {
        public Chart_AverageFixationDurationOverAgeBySex(Chart chart)
            : base(chart)
        {
        }
        public string Name
        {
            get { return "Average fixation duration over age by sex"; }
        }
        public override void Draw()
        {
        }
    }
}
