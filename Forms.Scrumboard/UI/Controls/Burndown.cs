using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScrumBoard.ScrumboardService;
using ScrumBoard.Common;
using System.Windows.Forms.DataVisualization.Charting;

namespace ScrumBoard.UI.Controls
{
    public partial class BurndownPanel : UserControl
    {
        private ScrumBoard.Business.Sprint sprint;
        private ScrumboardService.Panel panel;
        private ScrumboardService.Layout layout;
        private SortedList<int, Story> stories;

        public BurndownPanel()
        {
            InitializeComponent();
        }
        public BurndownPanel(ScrumboardService.Layout layout, ScrumboardService.Panel panel)
        {
            InitializeComponent();
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AutoScroll = true;
            this.panel = panel;
            this.layout = layout;

        }
        public void AutoResize()
        {
            int height = (Parent.Height / layout.TotalRows);
            int width = (Parent.Width / layout.TotalColumns);
            Top = height * panel.Row;
            Left = width * panel.Column;
            Height = height * panel.Heigth;
            Width = width * panel.Width;
        }


        private decimal CalculatePlanned(DateTime dt, decimal storyPoints)
        {
            decimal points = storyPoints;
            if (sprint != null)
            {
                points -= sprint.BurndownRateOfDay(dt);
            }
            if (points < 0)
                points = 0;
            return points;
        }

        private decimal CalculateExpected(DateTime dt, decimal storyPoints, SortedList<int, Story> stories, bool withBurnup)
        {
            if (dt <= DateTime.Now)
                return CalculateRealized(dt, storyPoints, stories, withBurnup);
            else
            {
                return CalculatePlanned(dt, storyPoints);
            }
        }

        private decimal CalculateRealized(DateTime dt, decimal storyPoints, SortedList<int, Story> stories, bool withBurnup)
        {
            decimal points = storyPoints;
            foreach (Story s in stories.Values)
            {
                if (s.IsBurndownEnabled)
                {
                    // Burnup
                    if (withBurnup)
                    {
                        if (s.Created.Date.Equals(dt.Date) && (s.ClosedDate == DateTime.MinValue || s.ClosedDate.Date > dt.Date) && !s.IsRemoved)
                        {
                            points += s.Estimate;
                        }
                    }
                    // Burndown
                    if (s.ClosedDate != null && s.ClosedDate.Date.Equals(dt.Date) && !s.IsRemoved)
                    {
                        points -= s.Estimate;
                    }
                }
            }
            if (points < 0)
                points = 0;
            return points;
        }

        /// <summary>
        /// Returns the total amount of story points for all stories
        /// </summary>
        private decimal TotalPlannedStoryPoints(DateTime startDate)
        {
            decimal points = 0;
            decimal totalStories = 0;
            foreach (Story story in stories.Values)
            {
                // Burn up based on creation date of the story
                if (story.IsBurndownEnabled && !story.IsRemoved && story.Created.Date <= startDate.Date)
                {
                    points += story.Estimate;
                    totalStories++;
                }
            }
            Console.WriteLine("Total planned stories: #" + totalStories + " total estimate: " + points);
            return points;
        }

        private decimal TotalStoryPoints()
        {
            decimal points = 0;
            foreach (Story story in stories.Values)
            {
                if (story.IsBurndownEnabled && !story.IsRemoved)
                {
                    points += story.Estimate;
                }
            }
            return points;
        }

        private struct Summary
        {
            public decimal Planned { get; set; }
            public decimal Expected { get; set; }
            public decimal Realized { get; set; }
            public DateTime ExpectedDone { get; set; }

        }

        private DataTable createTable(DateTime startDate, DateTime targetDate, DateTime tillDate, out Summary summary)
        {
            summary = new Summary();
            DataTable table = new DataTable("Realized");
            table.Columns.Add("Date", typeof(DateTime));
            table.Columns.Add("Planned", typeof(decimal));
            table.Columns.Add("Realized", typeof(decimal));
            table.Columns.Add("Expected", typeof(decimal));
            table.Columns.Add("Today", typeof(decimal));
            table.Columns.Add("Target", typeof(decimal));

            DateTime dt = startDate;
            decimal totalPoints = TotalStoryPoints();
            summary.Planned = totalPoints;
            decimal planned = TotalPlannedStoryPoints(startDate);
            decimal realized = planned;
            decimal expected = planned;
            decimal today = totalPoints;
            decimal target = planned;
            while (totalPoints > 0 && dt < tillDate)
            {
                //Console.WriteLine(dt.Date.ToString("ddd dd/MM/yy") + ":");
                //Console.Write("Planned " + planned);
                //Console.Write(" Expected " + expected);
                //Console.Write(" Realized " + realized);
                //Console.WriteLine(" Points " + totalPoints);
                if (dt.Date.Equals(DateTime.Now.Date) && dt.Date.Equals(targetDate))
                {
                    summary.Expected = expected;
                    table.Rows.Add(new object[] { dt, planned, realized, expected, today, target });
                }
                else if (dt.Date.Equals(DateTime.Now.Date))
                {
                    summary.Expected = expected;
                    table.Rows.Add(new object[] { dt, planned, realized, expected, today, 0 });
                }
                else if (dt.Date.Equals(targetDate))
                {
                    table.Rows.Add(new object[] { dt, planned, realized, expected, 0, target });
                }
                else
                {
                    table.Rows.Add(new object[] { dt, planned, realized, expected, 0, 0 });
                }
                if (expected <= 0)
                {
                    summary.ExpectedDone = dt;
                }
                dt = dt.AddDays(1);
                planned = CalculatePlanned(dt, planned);
                totalPoints = CalculateExpected(dt, totalPoints, stories, false);
                expected = CalculateExpected(dt, expected, stories, true);
                realized = CalculateRealized(dt, realized, stories, true);
            }
            //Console.WriteLine(dt.Date.ToString("ddd dd/MM/yy") + ":");
            //Console.Write("Planned " + planned);
            //Console.Write(" Expected " + expected);
            //Console.WriteLine(" Realized " + realized);
            if (dt.Date.Equals(DateTime.Now.Date) && dt.Date.Equals(targetDate))
            {
                summary.Expected = expected;
                table.Rows.Add(new object[] { dt, planned, realized, expected, today, target });
            }
            else if (dt.Date.Equals(DateTime.Now.Date))
            {
                summary.Expected = expected;
                table.Rows.Add(new object[] { dt, planned, realized, expected, today, 0 });
            }
            else if (dt.Date.Equals(targetDate))
            {
                table.Rows.Add(new object[] { dt, planned, realized, expected, 0, target });
            }
            else
            {
                table.Rows.Add(new object[] { dt, planned, realized, expected, 0, 0 });
            }
            summary.Realized = summary.Planned - realized;
            if (expected <= 0)
            {
                summary.ExpectedDone = dt;
            }
            return table;
        }

        public void DrawChart()
        {
            sprint = new ScrumBoard.Business.Sprint(Config.ActiveSprint);
            stories = Data.getInstance().getSprintStories();

            burndownChart.Titles.Clear();
            burndownChart.Titles.Add("Burndown " + sprint.Name + " target: " + sprint.TargetDate.ToShortDateString());

            burndownChart.Titles[0].Font = new Font(burndownChart.Titles[0].Font.FontFamily, 14, FontStyle.Bold);
            DateTime dt = sprint.StartDate;

            DataSet ds = new DataSet();
            DateTime endDate = sprint.TargetDate.AddDays(7);
            if (endDate < DateTime.Now)
            {
                endDate = DateTime.Now.Date;
            }
            Summary summary;
            ds.Tables.Add(createTable(dt, sprint.TargetDate, endDate, out summary));
            burndownChart.Titles.Add(String.Format("Planned: {0}, realized: {1}, expected: {2}, velocity: {3}", summary.Planned, summary.Realized, summary.ExpectedDone.ToShortDateString(), sprint.Velocity));
            burndownChart.DataSource = ds.Tables[0].DefaultView;
            // Defaults
            ChartValueType xValueType = ChartValueType.Date;
            SeriesChartType chartType = SeriesChartType.Line;

            burndownChart.Series.Clear();
            Series pln = burndownChart.Series.Add("Planned");
            pln.XValueMember = "Date";
            pln.YValueMembers = "Planned";
            pln.ChartType = chartType;
            pln.ToolTip = "Planned";
            pln.LegendText = "Planned";
            pln.XValueType = xValueType;
            pln.BorderWidth = 2;

            Series exp = burndownChart.Series.Add("Expected");
            exp.XValueMember = "Date";
            exp.YValueMembers = "Expected";
            exp.ChartType = chartType;
            exp.ToolTip = "Expected";
            exp.LegendText = "Expected";
            exp.XValueType = xValueType;
            exp.BorderWidth = 2;

            Series rea = burndownChart.Series.Add("Realized");
            rea.XValueMember = "Date";
            rea.YValueMembers = "Realized";
            rea.ChartType = chartType;
            rea.ToolTip = "Realized";
            rea.LegendText = "Realized";
            rea.XValueType = xValueType;
            rea.BorderWidth = 2;

            Series today = burndownChart.Series.Add("Today");
            today.XValueMember = "Date";
            today.YValueMembers = "Today";
            today.ChartType = SeriesChartType.Column;
            today.BorderWidth = 0;
            today.ToolTip = "Today";
            today.LegendText = "Today";
            today.XValueType = xValueType;
            today.CustomProperties = "PointWidth=0.1";

            //Series target = burndownChart.Series.Add("Target");
            //target.XValueMember = "Date";
            //target.YValueMembers = "Target";
            //target.ChartType = SeriesChartType.Column;
            //target.BorderWidth = 0;
            //target.Color = Color.Green;
            //target.ToolTip = "Target";
            //target.LegendText = "Target";
            //target.XValueType = xValueType;
            //target.CustomProperties = "PointWidth=0.1";

            burndownChart.DataBind();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Print();
        }

        public void Print()
        {
            burndownChart.Printing.Print(true);
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            DrawChart();
            Cursor = Cursors.Default;
        }
    }
}
