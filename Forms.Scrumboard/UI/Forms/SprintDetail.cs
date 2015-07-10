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
    public partial class SprintDetail : Form
    {
        ScrumboardService.ScrumboardSoapClient client = ServiceConn.getClient();
        Sprint s;

        public SprintDetail()
        {
            InitializeComponent();
            dtTarget.MinDate = DateTime.MinValue;
            dtTarget.MaxDate = DateTime.MaxValue;
            dtStartdate.MinDate = DateTime.MinValue;
            dtStartdate.MaxDate = DateTime.MaxValue;
            cmbLayout.Items.Clear();
            cmbLayout.DisplayMember = "Name";
            cmbLayout.ValueMember = "Id";
            cmbLayout.DataSource = client.LayoutSelectAll();

            cmbTeam.Items.Clear();
            cmbTeam.DisplayMember = "Name";
            cmbTeam.ValueMember = "Id";
            cmbTeam.DataSource = client.TeamSelectAll();

            numFocusFactor.Value = 0;
            numFocusFactor.Minimum = 0;
            numFocusFactor.Maximum = 100;

            numVelocity.Minimum = 0;
            numVelocity.Maximum = Int32.MaxValue;
            numVelocity.Value = 0;
        }

        public Sprint Sprint
        {
            set
            {
                s = value;
                txtName.Text = value.Name;
                try { dtStartdate.Value = value.StartDate; }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                try { dtTarget.Value = value.TargetDate; }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                cmbLayout.SelectedValue = value.LayoutId;
                cmbTeam.SelectedValue = value.TeamId;
                numVelocity.Value = value.Velocity;
                numFocusFactor.Value = value.FocusFactor;
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
                s.StartDate = dtStartdate.Value.Date;
                s.TargetDate = dtTarget.Value.Date;
                s.LayoutId = (int)cmbLayout.SelectedValue;
                s.TeamId = (int)cmbTeam.SelectedValue;
                s.Velocity = (int)numVelocity.Value;
                s.FocusFactor = (int)numFocusFactor.Value;
                client.SprintUpdate(s.Id, s.LayoutId, s.TeamId, s.Name, s.StartDate, s.TargetDate, s.Velocity,s.FocusFactor);
            }
            else
            {
                Config.ActiveSprint = client.SprintInsert((int)cmbLayout.SelectedValue, (int)cmbTeam.SelectedValue, txtName.Text, dtStartdate.Value.Date, dtTarget.Value.Date, (int)numVelocity.Value, (int)numFocusFactor.Value);
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
