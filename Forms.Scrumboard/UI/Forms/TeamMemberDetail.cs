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
using System.Globalization;

namespace ScrumBoard.UI.Forms
{
    public partial class TeamMemberDetail : Form
    {
        ScrumboardService.ScrumboardSoapClient client = ServiceConn.getClient();
        TeamMember teamMember;

        public TeamMemberDetail()
        {
            InitializeComponent();
        }

        public TeamMember TeamMember
        {
            set
            {
                teamMember = value;
                txtName.Text = value.Name;
                txtUserName.Text = value.UserName;
                txtFocus.Text = DecimalToString(value.FocusFactor);
                txtAvailability.Text = DecimalToString(value.AvailabilityFactor);
                txtNormalHours.Text = DecimalToString(value.NormalWorkingHours);
            }
            get
            {

                return teamMember;
            }
        }

        

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (teamMember != null)
            {
                teamMember.Name = txtName.Text;
                teamMember.UserName = txtUserName.Text;
                teamMember.FocusFactor = StringToDecimal(txtFocus.Text);
                teamMember.AvailabilityFactor = StringToDecimal(txtAvailability.Text);
                teamMember.NormalWorkingHours = StringToDecimal(txtNormalHours.Text);
                client.TeamMemberUpdate(teamMember.Id, teamMember.Name, teamMember.UserName, txtFocus.Text, txtAvailability.Text, txtNormalHours.Text);
            }
            else
            {
                client.TeamMemberInsert(txtName.Text, txtUserName.Text, txtFocus.Text, txtAvailability.Text, txtNormalHours.Text);
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private String DecimalToString(Decimal value)
        {
            return value.ToString(new CultureInfo("en-US"));
        }

        private Decimal StringToDecimal(String value)
        {
            return Decimal.Parse(value, new CultureInfo("en-US"));
        }

    }
}
