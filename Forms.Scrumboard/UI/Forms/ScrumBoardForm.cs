using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ScrumBoard.UI.Controls;
using ScrumBoard.Common;
using ScrumBoard.Business;
using ScrumBoard.ScrumboardService;

namespace ScrumBoard.UI.Forms
{
    public partial class ScrumBoardForm : Form
    {
        private Boolean autoRefresh;
        private Timer refreshTimer = new Timer();
        private Mover mover = new Mover();


        Business.Sprint currentSprint = null;
        bool init = false;
        ScrumboardService.ScrumboardSoapClient client = ServiceConn.getClient();

        public ScrumBoardForm()
        {
            InitializeComponent();
            mover.Visible = false;
            this.Controls.Add(mover);
            autoAlignStoriesToolStripMenuItem.Enabled = !Config.ViewOnly;
            addStoryToolStripMenuItem.Enabled = !Config.ViewOnly;
            importSprintToolStripMenuItem1.Enabled = !Config.ViewOnly;
            newSprintToolStripMenuItem.Enabled = !Config.ViewOnly;
            editSprintToolStripMenuItem.Enabled = !Config.ViewOnly;
            refreshTimer.Tick += new EventHandler(refreshTimer_Tick);
        }



        private void ScrumBoard_Load(object sender, EventArgs e)
        {
            init = true;
            this.WindowState = Config.MainWindowState;
            Left = Config.MainWindowLeft;
            Top = Config.MainWindowTop;
            Width = Config.MainWindowWidth > 0 ? Config.MainWindowWidth : Width;
            Height = Config.MainWindowHeight > 0 ? Config.MainWindowHeight : Height;
            init = false;
        }

        private void showSprint()
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                for (int i = Controls.Count - 1; i >= 0; i--)
                {
                    if (Controls[i] is StatePanel || Controls[i] is BurndownPanel)
                    {
                        Controls.RemoveAt(i);
                    }
                }
                currentSprint = new Business.Sprint(Config.ActiveSprint);
                Text = "Scrumboard v3.0.1            Sprint: " + currentSprint.Name + " Target: " + currentSprint.TargetDate.ToShortDateString();
                if (currentSprint.Panels != null)
                {
                    foreach (ScrumboardService.Panel pnl in currentSprint.Panels)
                    {
                        if (!"Burndown".Equals(pnl.Title))
                        {
                            this.Controls.Add(new StatePanel(mover, currentSprint.Layout, pnl));
                        }
                        else
                        {
                            this.Controls.Add(new BurndownPanel(currentSprint.Layout, pnl));
                        }
                    }
                    RefreshSprint();
                    autoResize();
                }
                checkRefresh(true);
            }
            catch (System.Exception ex)
            {

                MessageBox.Show(this, ex.Message + "\n\r" + ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Cursor = Cursors.Default;
        }

        public StatePanel getPanel(int X, int Y)
        {
            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                if (Controls[i] is StatePanel)
                {
                    StatePanel p = ((StatePanel)Controls[i]);
                    if ((p.Top < Y) && ((p.Top + p.Height) >= Y)
                        && (p.Left < X) && ((p.Left + p.Width) >= X))
                    {
                        return p;
                    }
                }
            }
            return null;
        }

        private void SwitchSprint(bool up)
        {
            if (Config.MyTeam == -1)
            {
                ScrumboardService.Team[] teams = client.TeamSelectAll();
                if (teams.Length > 0)
                {
                    Config.MyTeam = teams[0].Id;
                }
            }
            if (Config.MyTeam != -1)
            {
                ScrumboardService.Sprint[] sprints = client.SprintSelectByTeam(Config.MyTeam);
                for (int i = 0; i < sprints.Length; i++)
                {
                    ScrumboardService.Sprint s = sprints[i];
                    if (s.Id == Config.ActiveSprint)
                    {
                        if (up)
                        {
                            int n = i + 1;
                            if (n >= sprints.Length) n = 0;
                            Config.ActiveSprint = sprints[n].Id;
                        }
                        else
                        {
                            int n = i - 1;
                            if (n < 0) n = sprints.Length - 1;
                            Config.ActiveSprint = sprints[n].Id;
                        }
                        showSprint();
                        break;
                    }
                }
            }
        }


        public void RefreshSprint()
        {
            Cursor = Cursors.WaitCursor;
            currentSprint = new Business.Sprint(Config.ActiveSprint);
            Data.getInstance().clearCaches();
            Data.getInstance().refreshStories();
            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                if (Controls[i] is BurndownPanel)
                {
                    ((BurndownPanel)Controls[i]).DrawChart();
                }
            }
            Cursor = Cursors.Default;
        }

        public void RefreshBurndown()
        {
            Cursor = Cursors.WaitCursor;
            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                if (Controls[i] is BurndownPanel)
                {
                    ((BurndownPanel)Controls[i]).DrawChart();
                }
            }
            Cursor = Cursors.Default;
        }

        private bool screenUpdates = false;
        private void autoResize()
        {
            screenUpdates = true;
            Cursor = Cursors.WaitCursor;
            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                if (Controls[i] is StatePanel)
                {
                    ((StatePanel)Controls[i]).AutoResize();
                }
                else if (Controls[i] is BurndownPanel)
                {
                    ((BurndownPanel)Controls[i]).AutoResize();
                }
            }
            Cursor = Cursors.Default;
            screenUpdates = false;
        }

        private void autoAlignControls()
        {
            screenUpdates = true;
            Cursor = Cursors.WaitCursor;
            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                if (Controls[i] is StatePanel)
                {
                    ((StatePanel)Controls[i]).AutoAlignStories();

                }
            }
            Cursor = Cursors.Default;
            screenUpdates = false;
        }

        private void ScrumBoard_SizeChanged(object sender, EventArgs e)
        {
            autoResize();
        }

        private void addStoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StoryDetail form = new StoryDetail();
            DialogResult r = form.ShowDialog(this);
            //if (r == System.Windows.Forms.DialogResult.OK)
            //{
            //    foreach (Control c in Controls)
            //    {
            //        if (c is StatePanel)
            //        {
            //            StatePanel sp = ((StatePanel)c);
            //            sp.AutoAlignStories();
            //        }
            //    }
            //}
        }

        private void openSprintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = currentSprint != null ? currentSprint.Id : -1;
            SelectSprint f = new SelectSprint();
            f.ShowDialog();
            currentSprint = new Business.Sprint(Config.ActiveSprint);
            if (id != currentSprint.Id)
            {
                showSprint();
            }
        }

        private void autoAlignStoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            autoAlignControls();
        }

        private void mnuOptions_Click(object sender, EventArgs e)
        {
            Options f = new Options();
            DialogResult r = f.ShowDialog(this);
            if (r == System.Windows.Forms.DialogResult.OK)
            {
                autoAlignStoriesToolStripMenuItem.Enabled = !Config.ViewOnly;
                addStoryToolStripMenuItem.Enabled = !Config.ViewOnly;
                importSprintToolStripMenuItem1.Enabled = !Config.ViewOnly;
                newSprintToolStripMenuItem.Enabled = !Config.ViewOnly;
                editSprintToolStripMenuItem.Enabled = !Config.ViewOnly;
                showSprint();
            }
        }

        private void importSprintToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            dlgOpenFile.DefaultExt = "*.spr";
            dlgOpenFile.Title = "Select Sprint definition";
            dlgOpenFile.Filter = "Sprint definition|*.spr";
            dlgOpenFile.InitialDirectory = Config.StoragePath.FullName;
            dlgOpenFile.Multiselect = false;
            dlgOpenFile.CheckFileExists = true;
            if (dlgOpenFile.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;
                FileInfo sprintFile = new FileInfo(dlgOpenFile.FileName);
                Config.StoragePath = sprintFile.Directory;
                currentSprint.ImportStories(sprintFile);
                Cursor = Cursors.Default;
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshSprint();
        }

        private void autoRefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkRefresh(!autoRefresh);
        }
        void refreshTimer_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Data.getInstance().refreshStories();
            Cursor = Cursors.Default;
        }
        private void checkRefresh(Boolean refresh)
        {
            if (refresh != autoRefresh)
            {
                autoRefresh = refresh;
                if (refresh)
                {
                    autoRefreshToolStripMenuItem.Text = "Auto Refresh Off";
                    refreshTimer.Interval = 5000;
                    refreshTimer.Start();
                }
                else
                {
                    autoRefreshToolStripMenuItem.Text = "Auto Refresh On";
                    refreshTimer.Stop();
                }
            }
        }

        private void editSprintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SprintDetail f = new SprintDetail();
            f.Sprint = currentSprint;
            f.ShowDialog();
            showSprint();
        }

        private void newSprintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SprintDetail f = new SprintDetail();
            f.ShowDialog();
            showSprint();
        }

        private void burndownChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            BurndownGraph burndownForm = new BurndownGraph();
            Cursor = Cursors.Default;
            burndownForm.Show(this);
        }

        private void ScrumBoardForm_ResizeEnd(object sender, EventArgs e)
        {
            if (!init)
            {
                Config.MainWindowState = this.WindowState;
                Config.MainWindowLeft = this.Left;
                Config.MainWindowTop = this.Top;
                Config.MainWindowWidth = this.Width;
                Config.MainWindowHeight = this.Height;
            }
        }

        private void ScrumBoardForm_Shown(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            currentSprint = new Business.Sprint(Config.ActiveSprint);
            if (currentSprint != null && currentSprint.Id >= 0)
            {
                showSprint();
            }
            Cursor = Cursors.Default;
        }

        private void exportStoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dlgOpenFile.DefaultExt = "*.str";
            dlgOpenFile.Title = "Select Sprint stories";
            dlgOpenFile.Filter = "Sprint Stories File|*.str";
            dlgOpenFile.InitialDirectory = Config.StoragePath.FullName;
            dlgOpenFile.FileName = "sprint.str";
            dlgOpenFile.Multiselect = false;
            dlgOpenFile.CheckFileExists = false;
            if (dlgOpenFile.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;
                FileInfo sprintFile = new FileInfo(dlgOpenFile.FileName);
                Config.StoragePath = sprintFile.Directory;

                currentSprint.ExportStories(sprintFile);
                Cursor = Cursors.Default;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool ret = base.ProcessCmdKey(ref msg, keyData);
            if (keyData == (Keys.Control | Keys.F5))
            {
                autoAlignControls();
                Activate();
                Focus();
            }
            else if (keyData == (Keys.F5))
            {
                RefreshSprint();
                Activate();
                Focus();
            }
            else if (keyData == (Keys.Left))
            {
                SwitchSprint(false);
                Activate();
                Focus();
            }
            else if (keyData == (Keys.Right))
            {
                SwitchSprint(true);
                Activate();
                Focus();
            }
            return ret;
        }
    }
}
