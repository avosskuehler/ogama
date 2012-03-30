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

        public ChartType(Chart chart, string xVariable, string yVariable, 
            string groupBy, string agregateFunction)
        {
            this.Chart = chart;

            string[] x = xVariable.Split('.');
            this.XVariable_Table = x[0];
            this.XVariable = x[1];
            
            string[] y = yVariable.Split('.');
            this.YVariable_Table = y[0];
            this.YVariable = y[1];

            if (groupBy != "")
            {
                string[] group = groupBy.Split('.');
                this.GroupBy_Table = group[0];
                this.GroupBy = group[1];
            }

            this.AgregateFunction = agregateFunction;
        }

        public static Dictionary<string, DataType> columnTypes = new Dictionary<string, DataType>()
        {
            {"Subjects.ID",DataType.Ordinal},
            {"Subjects.SubjectName",DataType.Nominal},
            {"Subjects.Category",DataType.Nominal},
            {"Subjects.Age",DataType.Ordinal},
            {"Subjects.Sex",DataType.Nominal},
            {"Subjects.Handedness",DataType.Nominal},
            {"Subjects.Comments",DataType.Nominal},
            {"Trials.ID",DataType.Ordinal},
            {"Trials.SubjectName",DataType.Nominal},
            {"Trials.TrialSequence",DataType.Ordinal},
            {"Trials.TrialID",DataType.Ordinal},
            {"Trials.TrialName",DataType.Nominal},
            {"Trials.Category",DataType.Nominal},
            {"Trials.TrialStartTime",DataType.Ordinal},
            {"Trials.EliminateData",DataType.Nominal},
            {"GazeFixations.ID",DataType.Ordinal},
            {"GazeFixations.SubjectName",DataType.Nominal},
            {"GazeFixations.TrialID",DataType.Ordinal},
            {"GazeFixations.TrialSequence",DataType.Ordinal},
            {"GazeFixations.CountInTrial",DataType.Ordinal},
            {"GazeFixations.StartTime",DataType.Ordinal},
            {"GazeFixations.Length",DataType.Ordinal},
            {"GazeFixations.PosX",DataType.Ordinal},
            {"GazeFixations.PosY",DataType.Ordinal},
            {"MouseFixations.ID",DataType.Ordinal},
            {"MouseFixations.SubjectName",DataType.Nominal},
            {"MouseFixations.TrialID",DataType.Ordinal},
            {"MouseFixations.TrialSequence",DataType.Ordinal},
            {"MouseFixations.CountInTrial",DataType.Ordinal},
            {"MouseFixations.StartTime",DataType.Ordinal},
            {"MouseFixations.Length",DataType.Ordinal},
            {"MouseFixations.PosX",DataType.Ordinal},
            {"MouseFixations.PosY",DataType.Ordinal},
        };
        public Chart Chart = new Chart();
        public string Name;
        public string YVariable;
        public string YVariable_Table;
        public string XVariable;
        public string XVariable_Table;
        public string GroupBy;//variable defines the difference between individual series
        public string GroupBy_Table;
        public string AgregateFunction;
        public Visifire.Charts.RenderAs Type;
        Ogama.DataSet.OgamaDataSet db = Document.ActiveDocument.DocDataSet;
        
        public void ClearContentsAndAddTitle()
        {
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
            PreDrawPrepare();
            if (GroupBy == null)//no group by
            {
                string query = BuildQuery("");
                SqlDataReader result = ExecuteQuery(query);
                BuildChart("", result);
            }
            else
            {
                var seriesNames = GetSeriesNames();
                foreach (var seriesName in seriesNames)
                {
                    string query = BuildQuery(seriesName);
                    SqlDataReader result = ExecuteQuery(query);
                    BuildChart(seriesName, result);
                }
            }
        }

        public void Draw(string rangesString)
        {
            string[] ranges = rangesString.Split(',');
            PreDrawPrepare();
            if (GroupBy == null)
            {
                string query = BuildQuery("");
                SqlDataReader result = ExecuteQuery(query);
                //<-------------------------------------------
                
                //BuildChart("", result);
            }
            else
            {
                var seriesNames = GetSeriesNames();
                foreach (var seriesName in seriesNames)
                {
                    string query = BuildQuery(seriesName);
                    SqlDataReader result = ExecuteQuery(query);
                    //<-------------------------------------------
                    DataSeries dataseries = new DataSeries();
                    dataseries.Name = seriesName.ToString();
                    List<Tuple<string,object>> newResult = new List<Tuple<string,object>>();
                    while (result.Read())
                    {
                        foreach (string range in ranges)
                        {
                            string[] limits = range.Split('-');
                            long min = Convert.ToInt64(limits[0]);
                            long max = Convert.ToInt64(limits[1]);
                            if ((long)result[0] <= max && (long)result[0] >= min)
                            {
                                newResult.Add(Tuple.Create(range,result[1]));
                            }
                            IEnumerable<Tuple<string, object>> finalResult = newResult;
                            if (AgregateFunction == "Max")
                            {
                                finalResult = newResult.GroupBy(v => v.Item1,
                                    (k, g) => new Tuple<string, object>(k, g.Max(v => v.Item2)));//some logi to use other agregates
                            }
                            else if (AgregateFunction=="Min")
                            {
                                finalResult = newResult.GroupBy(v => v.Item1,
                                    (k, g) => new Tuple<string, object>(k, g.Min(v => v.Item2)));
                            }
                            else if (AgregateFunction=="Sum")
                            {
                                finalResult = newResult;
                            }
                            foreach (var pair in finalResult)
                            {
                                DataPoint point = new DataPoint();
                                point.AxisXLabel = pair.Item1;
                                point.YValue = Convert.ToDouble(pair.Item2);
                                dataseries.DataPoints.Add(point);
                            }
                        }
                    }
                    //BuildChart(seriesName, result);
                }
            }
        }

        private SqlDataReader ExecuteQuery(string query)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = db.DatabaseConnection;

            SqlDataReader result = cmd.ExecuteReader();
            return result;
        }

        private void BuildChart(object series, SqlDataReader result)
        {
            DataSeries dataseries = new DataSeries();
            dataseries.Name = series.ToString();
            //dataseries.RenderAs = RenderAs.StackedColumn;
            while (result.Read())
            {
                DataPoint point = new DataPoint();
                point.AxisXLabel = result[XVariable].ToString() != "" ? result[XVariable].ToString() : "<no name>";
                point.YValue = Convert.ToDouble(result[1]);
                dataseries.DataPoints.Add(point);
            }
            Chart.Series.Add(dataseries);

            result.Close();
            //db.DatabaseConnection.Close();
        }

        private string BuildQuery(object seriesName)
        {
            string select = "";
            if (AgregateFunction == "Avg")
            {
                select = string.Format("SELECT {0}.{1}, ROUND({4}(CAST({2}.{3} AS Float)),2)",
                     XVariable_Table, XVariable, YVariable_Table, YVariable, AgregateFunction);
            }
            else if (AgregateFunction == "Count")
            {
                select = string.Format("SELECT {0}.{1}, {4}(DISTINCT {2}.{3})",
                     XVariable_Table, XVariable, YVariable_Table, YVariable, AgregateFunction);
            }
            else
            {
                select = string.Format("SELECT {0}.{1}, {4}({2}.{3})",
                     XVariable_Table, XVariable, YVariable_Table, YVariable, AgregateFunction);
            }
            string from = "FROM Trials,Subjects";
            string join = "WHERE Trials.SubjectName = Subjects.SubjectName";
            string group = string.Format("GROUP BY {0}.{1}", XVariable_Table, XVariable);//what if time?
            string seriesSelector = "";
            if (seriesName != "")
            {
                seriesSelector = string.Format("AND {0}.{1} = \'{2}\'", GroupBy_Table, GroupBy, seriesName);
            }

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
            string query = select + " " + from + " " + join + " " + seriesSelector + " " + group;
            return query;
        }

        private IEnumerable<object> GetSeriesNames()
        {
            var seriesNames = (from c in db.Tables[GroupBy_Table].AsEnumerable()
                              where !c.IsNull(GroupBy)//omit null records
                              select c[GroupBy]).Distinct();
            return seriesNames;
        }

        /// <summary>
        /// Clears chart contents, adds the new title and axis labels
        /// </summary>
        private void PreDrawPrepare()
        {
            ClearContentsAndAddTitle();
            Axis axisX = new Axis();
            axisX.Title = XVariable;
            Chart.AxesX.Add(axisX);
            Axis axisY = new Axis();
            axisY.Title = YVariable;
            Chart.AxesY.Add(axisY);
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
    
    public enum DataType
    {
        Ordinal,
        Nominal
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
