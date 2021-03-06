﻿using System;
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
    public partial class Burndown : UserControl
    {
        private ScrumboardSoapClient client;
        private ScrumBoard.Business.Sprint sprint;
        private ScrumboardService.Panel panel;
        private ScrumboardService.Layout layout;

        public Burndown()
        {
            InitializeComponent();
        }
        public Burndown(ScrumboardService.Layout layout, ScrumboardService.Panel panel)
        {
            InitializeComponent();
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AutoScroll = true;
            this.panel = panel;
            this.layout = layout;

        }

        public void AutoResize()
        {
            int height = (Parent.Height / layout.TotalRows) * panel.Heigth;
            int width = (Parent.Width / layout.TotalColumns) * panel.Width;
            Top = height * panel.Row;
            Left = width * panel.Column;
            Height = height;
            Width = width;
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

        private decimal CalculateExpected(DateTime dt, decimal storyPoints, Story[] stories)
        {
            if (dt <= DateTime.Now)
                return CalculateRealized(dt, storyPoints, stories);
            else
            {
                return CalculatePlanned(dt, storyPoints);
            }
        }

        private decimal CalculateRealized(DateTime dt, decimal storyPoints, Story[] stories)
        {
            decimal points = storyPoints;
            foreach (Story s in stories)
            {
                if (s.IsBurndownEnabled)
                {
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
        private DataTable CreateTable(DateTime startDate, DateTime tillDate)
        {
            if (client == null)
            {
                client = ServiceConn.getClient();
            }
            Story[] stories = client.StoryGetSprintStories(Config.ActiveSprint);
            DataTable table = new DataTable("Realized");
            table.Columns.Add("Date");
            table.Columns.Add("Planned");
            table.Columns.Add("Realized");
            table.Columns.Add("Expected");
            table.Columns.Add("Today");
            DateTime dt = startDate;
            decimal pln = 0;
            foreach (Story s in stories)
            {
                if (s.IsBurndownEnabled)
                    pln += s.Estimate;
            }
            decimal rel = pln;
            decimal exp = pln;
            decimal today = pln;
            while (exp > 0 && dt < tillDate)
            {
                if (dt.Date.Equals(DateTime.Now.Date))
                    table.Rows.Add(new object[] { dt.ToShortDateString(), pln, rel, exp, today });
                else
                    table.Rows.Add(new object[] { dt.ToShortDateString(), pln, rel, exp, 0 });
                Console.WriteLine("{0} pln: {1} rel: {2} exp {3}", dt.ToShortDateString(), pln, rel, exp);
                dt = dt.AddDays(1);
                if (pln > 0)
                    pln = CalculatePlanned(dt, pln);
                if (exp > 0)
                    exp = CalculateExpected(dt, exp, stories);
                if (rel > 0)
                    rel = CalculateRealized(dt, rel, stories);
            }
            if (dt.Date.Equals(DateTime.Now.Date))
                table.Rows.Add(new object[] { dt.ToShortDateString(), pln, rel, exp, today });
            else
                table.Rows.Add(new object[] { dt.ToShortDateString(), pln, rel, exp, 0 });
            //Console.WriteLine("{0} pln: {1} rel: {2} exp {3}", dt.ToShortDateString(), pln, rel, exp);
            return table;
        }

        public void DrawChart()
        {

            sprint = new ScrumBoard.Business.Sprint(Config.ActiveSprint);
            burndownChart.Titles.Clear();
            burndownChart.Titles.Add("Burndown " + sprint.Name + " Target: " + sprint.TargetDate.ToShortDateString());
            burndownChart.Titles[0].Font = new Font(burndownChart.Titles[0].Font.FontFamily, 14, FontStyle.Bold);
            DateTime dt = sprint.StartDate;

            DataSet ds = new DataSet();
            DateTime endDate = sprint.TargetDate.AddDays(7);
            ds.Tables.Add(CreateTable(dt, endDate));

            burndownChart.DataSource = ds.Tables[0].DefaultView;

            burndownChart.Series.Clear();
            Series pln = burndownChart.Series.Add("Planned");
            pln.XValueMember = "Date";
            pln.YValueMembers = "Planned";
            pln.ChartType = SeriesChartType.Line;
            pln.ToolTip = "Planned";
            pln.LegendText = "Planned";
            pln.XValueType = ChartValueType.Date;
            //pln.BorderWidth = 2;

            Series exp = burndownChart.Series.Add("Expected");
            exp.XValueMember = "Date";
            exp.YValueMembers = "Expected";
            exp.ChartType = SeriesChartType.Line;
            exp.ToolTip = "Expected";
            exp.LegendText = "Expected";
            exp.XValueType = ChartValueType.Date;
            //exp.BorderWidth = 2;

            Series rea = burndownChart.Series.Add("Realized");
            rea.XValueMember = "Date";
            rea.YValueMembers = "Realized";
            rea.ChartType = SeriesChartType.Line;
            rea.ToolTip = "Realized";
            rea.LegendText = "Realized";
            rea.XValueType = ChartValueType.Date;
            //rea.BorderWidth = 2;

            Series today = burndownChart.Series.Add("Today");
            today.XValueMember = "Date";
            today.YValueMembers = "Today";
            today.ChartType = SeriesChartType.Column;
            today.BorderWidth = 0;
            today.ToolTip = "Today";
            today.LegendText = "Today";
            today.XValueType = ChartValueType.Date;
            today.CustomProperties = "PointWidth=0.1";

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
