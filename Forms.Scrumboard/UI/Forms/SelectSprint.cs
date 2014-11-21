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
    public partial class SelectSprint : Form
    {
        ScrumboardSoapClient client = ServiceConn.getClient();

        public SelectSprint()
        {
            InitializeComponent();
            try
            {
                Team[] teams = client.TeamSelectAll();
                cmbTeam.Items.Clear();
                cmbTeam.DisplayMember = "Name";
                cmbTeam.ValueMember = "Id";
                cmbTeam.DataSource = teams;
                if (teams.Length > 0)
                {
                    if (Config.MyTeam == -1) Config.MyTeam = teams[0].Id;
                    cmbTeam.SelectedValue = Config.MyTeam;
                }
                else
                {
                    Sprint[] list = null;
                    try
                    {
                        list = client.SprintSelectAll();
                    }
                    catch
                    {
                    }
                    if (list != null)
                    {
                        SortedList<String, Sprint> sprints = new SortedList<string, Sprint>();

                        foreach (Sprint sp in list)
                        {
                            String key = sp.Name;
                            while (sprints.ContainsKey(key))
                            {
                                key += "`";
                            }
                            sprints.Add(key, sp);
                        }
                        foreach (Sprint sp in sprints.Values)
                        {
                            ListViewItem lvi = new ListViewItem(sp.Name);
                            lvi.Tag = sp.Id;
                            lvwSprints.Items.Add(lvi);
                        }
                    }
                }
            }
            catch { }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (lvwSprints.SelectedItems.Count == 1)
            {
                Config.MyTeam = (int)cmbTeam.SelectedValue;
                Config.ActiveSprint = Int32.Parse(lvwSprints.SelectedItems[0].Tag.ToString());
            }
            this.Close();
        }

        private void lvwSprints_ItemChecked(object sender, ItemCheckedEventArgs e)
        {

        }

        private void lvwSprints_DoubleClick(object sender, EventArgs e)
        {
            if (lvwSprints.SelectedItems.Count == 1)
            {
                Config.MyTeam = (int)cmbTeam.SelectedValue;
                Config.ActiveSprint = Int32.Parse(lvwSprints.SelectedItems[0].Tag.ToString());
            }
            this.Close();
        }

        private void cmbTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTeam.SelectedValue != null)
            {
                Sprint[] list = null;
                lvwSprints.Items.Clear();
                try
                {
                    list = client.SprintSelectByTeam((int)cmbTeam.SelectedValue);
                }
                catch
                {
                }
                if (list != null)
                {
                    SortedList<String, Sprint> sprints = new SortedList<string, Sprint>();

                    foreach (Sprint sp in list)
                    {
                        String key = sp.Name;
                        while (sprints.ContainsKey(key))
                        {
                            key += "`";
                        }
                        sprints.Add(key, sp);
                    }
                    foreach (Sprint sp in sprints.Values)
                    {
                        ListViewItem lvi = new ListViewItem(sp.Name);
                        lvi.Tag = sp.Id;
                        lvwSprints.Items.Add(lvi);
                    }
                }
            }
        }
    }
}
