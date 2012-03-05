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
    using Visifire.Charts;

    /// <summary>
    /// Defines a basic type of chart as the name and the correponding method to draw it.
    /// </summary>
    public abstract class ChartType
    {
        public ChartType(Chart chart)
        {
            this.Chart = chart;
        }

        public Chart Chart = new Chart();
        public abstract string Name
        {
            get;
        }
        public virtual void Draw()
        {
            //Chart.View3D = true;
            //Chart.AxesX.Clear();
            //Chart.AxesY.Clear();
            Chart.Titles.Clear();
            Title title = new Title();
            title.Text = Name;
            Chart.Titles.Add(title);
        }
    }

    public class Chart_AverageFixationBySubjectAndTrial : ChartType
    {
        public Chart_AverageFixationBySubjectAndTrial(Chart chart)
            : base(chart)
        {
        }

        public override string Name
        {
            get { return "Average fixation time grouped by subject and trial category"; }
        } 
        /// <summary>
        /// the original draw
        /// </summary>
        //public override void Draw()
        //{
        //    base.Draw();
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
            base.Draw();
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
                          group new {gazeFixations,trials.Category} by trials.Category into res
                             select new
                             {
                                 Category = res.Key,
                                 AvgFixationTime = res.Average(p=>(int)p.gazeFixations["Length"])
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

    public class Chart_AverageFixationOverTime : ChartType
    {
        public Chart_AverageFixationOverTime(Chart chart)
            : base(chart)
        {
        }
        public override string Name
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
        public override string Name
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
        public override string Name
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
        public override string Name
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
        public override string Name
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
        public override string Name
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
        public override string Name
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
        public override string Name
        {
            get { return "Average fixation duration over age by sex"; }
        }
        public override void Draw()
        {
        }
    }
}
