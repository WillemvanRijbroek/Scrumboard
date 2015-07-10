using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScrumBoard.Common;

namespace ScrumBoard.UI.Forms
{
    public partial class Options : Form
    {
        ScrumboardService.ScrumboardSoapClient client = ServiceConn.getClient();

        public Options()
        {
            InitializeComponent();
            numEstimate.Minimum = 0;
            numEstimate.Maximum = Int32.MaxValue;
            numEstimate.Value = Config.DefaultEstimate;
            txtIssueTrackingURL.Text = Config.IssueTrackingSystemURL;
            chkViewModus.Checked = Config.ViewOnly;
            chkEditDetails.Checked = Config.AutoEditDetails;
            btnColor.BackColor = Color.FromArgb(Config.DefaultBackColor);
            btnTodoBackColor.BackColor = Color.FromArgb(Config.DefaultTodoBackColor);

            cmbStoryType.Items.Clear();
            cmbStoryType.DisplayMember = "Name";
            cmbStoryType.ValueMember = "Id";
            cmbStoryType.DataSource = client.StoryTypeSelectAll();
            cmbStoryType.Text = Config.DefaultStoryType;
            cmbTeam.Items.Clear();
            cmbTeam.DisplayMember = "Name";
            cmbTeam.ValueMember = "Id";
            cmbTeam.DataSource = client.TeamSelectAll();
            cmbTeam.SelectedValue = Config.MyTeam;
            refreshLayouts();
            refreshTeams();
            refreshStoryStates();
            refreshStoryTypes();
        }

        #region Events
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                Config.DefaultEstimate = (int)numEstimate.Value;
            }
            catch { }
            Config.DefaultBackColor = btnColor.BackColor.ToArgb();
            Config.DefaultTodoBackColor = btnTodoBackColor.BackColor.ToArgb();
            Config.ViewOnly = chkViewModus.Checked;
            Config.DefaultStoryType = cmbStoryType.Text;
            Config.AutoEditDetails = chkEditDetails.Checked;
            Config.IssueTrackingSystemURL = txtIssueTrackingURL.Text;
            Config.MyTeam = (int)cmbTeam.SelectedValue;
            Close();
        }
        #endregion

        #region Layouts
        private void btnAddLayout_Click(object sender, EventArgs e)
        {
            LayoutDefinition f = new LayoutDefinition();
            f.ShowDialog(this);
            refreshLayouts();
        }

        private void refreshLayouts()
        {
            ScrumboardService.Layout[] layouts = client.LayoutSelectAll();
            lvwLayouts.Items.Clear();
            foreach (ScrumboardService.Layout layout in layouts)
            {
                ListViewItem it = new ListViewItem(layout.Name);
                it.Tag = layout.Id;
                lvwLayouts.Items.Add(it);
            }
        }

        private void btnEditLayout_Click(object sender, EventArgs e)
        {
            if (lvwLayouts.SelectedItems.Count == 1)
            {
                ScrumboardService.Layout l =
                    client.LayoutGet((int)lvwLayouts.SelectedItems[0].Tag);
                if (l != null)
                {
                    LayoutDefinition f = new LayoutDefinition();
                    f.LayoutDef = l;
                    f.ShowDialog(this);
                    refreshLayouts();
                }
            }

        }

        private void btnDelLayout_Click(object sender, EventArgs e)
        {
            refreshLayouts();
        }
        #endregion

        #region Teams
        private void refreshTeams()
        {
            ScrumboardService.Team[] teams = client.TeamSelectAll();
            lvwTeams.Items.Clear();
            foreach (ScrumboardService.Team team in teams)
            {
                ListViewItem it = new ListViewItem(team.Name);
                it.Tag = team.Id;
                lvwTeams.Items.Add(it);
            }
        }

        private void btnEditTeam_Click(object sender, EventArgs e)
        {
            if (lvwTeams.SelectedItems.Count == 1)
            {
                ScrumboardService.Team t =
                    client.TeamGet((int)lvwTeams.SelectedItems[0].Tag);
                if (t != null)
                {
                    TeamDetail f = new TeamDetail();
                    f.Team = t;
                    f.ShowDialog(this);
                    refreshTeams();
                }
            }
        }

        private void btnAddTeam_Click(object sender, EventArgs e)
        {
            TeamDetail f = new TeamDetail();
            f.ShowDialog(this);
            refreshTeams();
        }

        private void btnRemoveTeam_Click(object sender, EventArgs e)
        {
            if (lvwTeams.SelectedItems.Count == 1)
            {
                ScrumboardService.Team t =
                    client.TeamGet((int)lvwTeams.SelectedItems[0].Tag);
                if (t != null)
                {
                    if (MessageBox.Show(this, "Are you sure to delete team " + t.Name + "?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        client.TeamRemove(t.Id);
                        refreshTeams();
                    }
                }
            }
        }
        #endregion

        #region Story States
        private void refreshStoryStates()
        {
            ScrumboardService.State[] states = client.StateSelectAll();
            lvwStates.Items.Clear();
            foreach (ScrumboardService.State state in states)
            {
                ListViewItem it = new ListViewItem(state.Name);
                it.Tag = state.Id;
                lvwStates.Items.Add(it);
            }
        }
        private void btnNewStatus_Click(object sender, EventArgs e)
        {
            StatusDetail f = new StatusDetail();
            f.ShowDialog(this);
            refreshStoryStates();
        }

        private void btnEditStatus_Click(object sender, EventArgs e)
        {
            if (lvwStates.SelectedItems.Count == 1)
            {
                ScrumboardService.State t =
                    client.StateGet((int)lvwStates.SelectedItems[0].Tag);
                if (t != null)
                {
                    StatusDetail f = new StatusDetail();
                    f.State = t;
                    f.ShowDialog(this);
                    refreshStoryStates();
                }
            }
        }
        #endregion

        #region Story Types
        private void refreshStoryTypes()
        {
            ScrumboardService.StoryType[] types = client.StoryTypeSelectAll();
            lvwStoryTypes.Items.Clear();
            foreach (ScrumboardService.StoryType type in types)
            {
                ListViewItem it = new ListViewItem(type.Name);
                it.Tag = type.Id;
                lvwStoryTypes.Items.Add(it);
            }
        }
        private void btnNewStoryType_Click(object sender, EventArgs e)
        {
            StoryTypeDetail f = new StoryTypeDetail();
            f.ShowDialog(this);
            refreshStoryTypes();
        }
        private void btnEditStoryType_Click(object sender, EventArgs e)
        {
            if (lvwStoryTypes.SelectedItems.Count == 1)
            {
                ScrumboardService.StoryType t =
                    client.StoryTypeGet((int)lvwStoryTypes.SelectedItems[0].Tag);
                if (t != null)
                {
                    StoryTypeDetail f = new StoryTypeDetail();
                    f.StoryType = t;
                    f.ShowDialog(this);
                    refreshStoryTypes();
                }
            }
        }
        #endregion

        #region Story
        private void btnColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                btnColor.BackColor = colorDialog1.Color;
            }
        }
        private void btnTodoBackColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                btnTodoBackColor.BackColor = colorDialog1.Color;
            }
        }
        #endregion

    }
}
