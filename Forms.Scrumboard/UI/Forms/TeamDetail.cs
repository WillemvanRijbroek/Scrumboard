using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScrumBoard.ScrumboardService;
using ScrumBoard.Common;

namespace ScrumBoard.UI.Forms
{
    public partial class TeamDetail : Form
    {
        ScrumboardService.ScrumboardSoapClient client = ServiceConn.getClient();
        Team s;

        public TeamDetail()
        {
            InitializeComponent();

        }

        public Team Team
        {
            set
            {
                s = value;
                if (s != null)
                    txtName.Text = value.Name;
                refreshTeamMembers();
            }
            get
            {
                return s;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (s != null)
            {
                s.Name = txtName.Text;
                client.TeamUpdate(s.Id, s.Name);
            }
            else
            {
                client.TeamInsert(txtName.Text);
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void refreshTeamMembers()
        {
            lvwTeamMembers.Items.Clear();
            if (s != null)
            {
                ScrumboardService.TeamMember[] members = client.TeamMemberSelectByTeam(s.Id);
                foreach (ScrumboardService.TeamMember member in members)
                {
                    ListViewItem it = new ListViewItem(member.Name);
                    it.Tag = member.Id;
                    lvwTeamMembers.Items.Add(it);
                }
            }
        }

        private void btnAssignMember_Click(object sender, EventArgs e)
        {
            if (s != null)
            {
                TeamMemberSelect f = new TeamMemberSelect();
                f.ShowDialog();
                if (f.SelectedId >= 0)
                {
                    client.TeamAssignMember(s.Id, f.SelectedId);
                    refreshTeamMembers();
                }
            }
        }

        private void btnDeassignMember_Click(object sender, EventArgs e)
        {
            if (s != null)
            {
                if (lvwTeamMembers.SelectedItems.Count == 1)
                {
                    client.TeamDeassignMember(s.Id, (int)lvwTeamMembers.SelectedItems[0].Tag);
                    refreshTeamMembers();
                }
            }
        }

        private void btnEditMember_Click(object sender, EventArgs e)
        {
            if (s != null)
            {
                if (lvwTeamMembers.SelectedItems.Count == 1)
                {
                    TeamMember m = client.TeamMemberGet((int)lvwTeamMembers.SelectedItems[0].Tag);
                    if (m != null)
                    {
                        TeamMemberDetail f = new TeamMemberDetail();
                        f.TeamMember = m;
                        f.ShowDialog();
                        refreshTeamMembers();
                    }
                }
            }
        }

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            if (s != null)
            {
                TeamMemberDetail f = new TeamMemberDetail();
                f.ShowDialog();
                refreshTeamMembers();
            }
        }

        private void btnDeleteMember_Click(object sender, EventArgs e)
        {
            if (s != null)
            {
                if (lvwTeamMembers.SelectedItems.Count == 1)
                {
                    TeamMember m = client.TeamMemberGet((int)lvwTeamMembers.SelectedItems[0].Tag);
                    if (m != null)
                    {
                        try
                        {
                            client.TeamDeassignMember(s.Id, m.Id);
                            client.TeamMemberRemove(m.Id);
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        refreshTeamMembers();
                    }
                }
            }
        }

    }
}
