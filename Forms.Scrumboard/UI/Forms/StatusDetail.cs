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
    public partial class StatusDetail : Form
    {
        ScrumboardService.ScrumboardSoapClient client = ServiceConn.getClient();
        State s;

        public StatusDetail()
        {
            InitializeComponent();

        }

        public State State
        {
            set
            {
                s = value;
                if (s != null)
                {
                    txtName.Text = value.Name;
                    chkInitialState.Checked = value.IsInitial;
                    chkFinalState.Checked = value.IsFinal;
                }
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
                s.IsInitial = chkInitialState.Checked;
                s.IsFinal = chkFinalState.Checked;
                client.StateUpdate(s.Id, s.Name, s.IsInitial, s.IsFinal);
            }
            else
            {
                client.StateInsert(txtName.Text, chkInitialState.Checked, chkFinalState.Checked);
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
