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
            txtEstimate.Text = Config.DefaultEstimate.ToString();
            txtIssueTrackingURL.Text = Config.IssueTrackingSystemURL;
            chkViewModus.Checked = Config.ViewOnly;
            chkEditDetails.Checked = Config.AutoEditDetails;
            btnColor.BackColor = Color.FromArgb(Config.DefaultBackColor);
            btnTodoBackColor.BackColor = Color.FromArgb(Config.DefaultTodoBackColor);
            cmbStoryType.Items.Add("Planned");
            cmbStoryType.Items.Add("Unplanned");
            cmbStoryType.Items.Add("Bonus");
            cmbStoryType.Text = Config.DefaultStoryType;
            cmbTeam.Items.Clear();
            cmbTeam.DisplayMember = "Name";
            cmbTeam.ValueMember = "Id";
            cmbTeam.DataSource = client.TeamSelectAll();
            cmbTeam.SelectedValue = Config.MyTeam;
            refreshLayouts();
            refreshTeams();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                Config.DefaultEstimate = Int32.Parse(txtEstimate.Text);
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

        private void btnColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                btnColor.BackColor = colorDialog1.Color;
            }
        }

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

        private void btnTodoBackColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                btnTodoBackColor.BackColor = colorDialog1.Color;
            }
        }
    }
}
