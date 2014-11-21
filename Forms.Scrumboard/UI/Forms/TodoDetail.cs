using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScrumBoard.Business;
using ScrumBoard.Common;

namespace ScrumBoard.UI.Forms
{
    public partial class TodoDetail : Form
    {
        ScrumboardService.ScrumboardSoapClient client = ServiceConn.getClient();
        private ScrumBoard.ScrumboardService.Todo s;

        public TodoDetail()
        {
            InitializeComponent();
            btnColor.BackColor = Color.FromArgb(Config.DefaultTodoBackColor);
            txtEstimate.Text = Config.DefaultEstimate.ToString();
            txtEstimate.Enabled = !Config.ViewOnly;
            txtDescription.Enabled = !Config.ViewOnly;
            txtEstimate.Enabled = !Config.ViewOnly;
            btnColor.Enabled = !Config.ViewOnly;
            btnOk.Enabled = !Config.ViewOnly;
        }

        public int StoryId { get; set; }

        public ScrumBoard.ScrumboardService.Todo Todo
        {
            set
            {
                this.StoryId = value.StoryId;
                txtEstimate.Text = value.Estimate.ToString();
                txtDescription.Text = value.Description;
                try
                {
                    btnColor.BackColor = Color.FromArgb(value.BackColor);
                }
                catch { }
                s = value;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (s != null)
            {
                s.StoryId = this.StoryId;
                s.Description = txtDescription.Text.Replace("'", "''");
                s.Estimate = Int32.Parse(txtEstimate.Text);
                s.BackColor = btnColor.BackColor.ToArgb();
                client.TodoUpdate(s.Id, s.StoryId, s.Description, s.Estimate, s.BackColor, s.X, s.Y);
            }
            else
            {
                client.TodoInsert(StoryId, txtDescription.Text.Replace("'", "''"), Int32.Parse(txtEstimate.Text), btnColor.BackColor.ToArgb(), 30, 30);
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                btnColor.BackColor = colorDialog1.Color;
            }
        }
    }
}
