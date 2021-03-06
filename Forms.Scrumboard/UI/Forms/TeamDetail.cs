﻿using System;
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

    }
}
