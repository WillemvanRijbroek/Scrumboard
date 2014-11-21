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
    public partial class TeamMemberSelect : Form
    {
        ScrumboardService.ScrumboardSoapClient client = ServiceConn.getClient();
        
        public TeamMemberSelect()
        {
            InitializeComponent();
            refreshTeamMembers();
        }

        public int SelectedId { get; set; }
        
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (lvwTeamMembers.SelectedItems.Count == 1)
            {
                SelectedId = (int)lvwTeamMembers.SelectedItems[0].Tag;
            }
            else
            {
                SelectedId = -1;
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SelectedId = -1;
            this.Close();
        }

        private void refreshTeamMembers()
        {
            this.lvwTeamMembers.Items.Clear();
            ScrumboardService.TeamMember[] members = client.TeamMemberSelectAll();
            foreach (ScrumboardService.TeamMember member in members)
            {
                ListViewItem it = new ListViewItem(member.Name);
                it.Tag = member.Id;
                lvwTeamMembers.Items.Add(it);
            }
        }

    }
}
