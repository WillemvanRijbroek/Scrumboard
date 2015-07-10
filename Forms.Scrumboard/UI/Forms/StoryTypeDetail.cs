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
    public partial class StoryTypeDetail : Form
    {
        ScrumboardService.ScrumboardSoapClient client = ServiceConn.getClient();
        StoryType s;

        public StoryTypeDetail()
        {
            InitializeComponent();

        }

        public StoryType StoryType
        {
            set
            {
                s = value;
                if (s != null)
                {
                    txtName.Text = value.Name;
                    chkBurndownEnabled.Checked = value.BurnDownEnabled;
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
                s.BurnDownEnabled = chkBurndownEnabled.Checked;
                client.StoryTypeUpdate(s.Id, s.Name, -256, s.BurnDownEnabled);
            }
            else
            {
                client.StoryTypeInsert(txtName.Text, -256, chkBurndownEnabled.Checked);
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
