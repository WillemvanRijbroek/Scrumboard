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
using ScrumBoard.ScrumboardService;

namespace ScrumBoard.UI.Forms
{
    public partial class StoryDetail : Form
    {
        private int state = 1;
        private Story s;
        ScrumboardService.ScrumboardSoapClient client = ServiceConn.getClient();

        public StoryDetail()
        {
            InitializeComponent();
            btnColor.BackColor = Color.FromArgb(Config.DefaultBackColor);
            
            cmbStoryType.ValueMember = "Id";
            cmbStoryType.DisplayMember = "Name";
            cmbStoryType.DataSource= client.StoryTypeSelectAll();
            cmbStoryType.SelectedText = Config.DefaultStoryType;
            txtEstimate.Text = Config.DefaultEstimate.ToString();
            txtId.Enabled = !Config.ViewOnly;
            txtDescription.Enabled = !Config.ViewOnly;
            txtEstimate.Enabled = !Config.ViewOnly;
            txtTag.Enabled = !Config.ViewOnly;
            cmbStoryType.Enabled = !Config.ViewOnly;
            btnColor.Enabled = !Config.ViewOnly;
            btnOk.Enabled = !Config.ViewOnly;
        }


       

        public Story Story
        {
            set
            {
                txtId.Text = value.ExternalId.ToString();
                txtEstimate.Text = value.Estimate.ToString();
                txtDescription.Text = value.Description;
                cmbStoryType.SelectedValue = value.StoryTypeId;
                state = value.StatusId;
                try
                {
                    btnColor.BackColor = Color.FromArgb(value.BackColor);
                }
                catch { }
                txtTag.Text = value.Tag;
                s = value;
            }
            get
            {
                return s;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (s != null)
                {
                    s.SprintId = Config.ActiveSprint;
                    s.ExternalId = txtId.Text;
                    s.StoryTypeId = Int32.Parse(cmbStoryType.SelectedValue.ToString());
                    s.Description = txtDescription.Text;
                    s.Estimate = (txtEstimate.Value);
                    s.Tag = txtTag.Text;
                    s.BackColor = btnColor.BackColor.ToArgb();
                    Data.getInstance().updateStory(s);
                }
                else
                {
                    Data.getInstance().insertStory(Config.ActiveSprint, txtId.Text, Int32.Parse(cmbStoryType.SelectedValue.ToString()), state, txtDescription.Text, Decimal.ToInt32(txtEstimate.Value), btnColor.BackColor.ToArgb(), 30, 30, txtTag.Text);
                }
                this.Close();
            }
            catch (PendingChangeException ex)
            {
                MessageBox.Show(this, "Story is already changed by someone else, please retry");
            }
            
            
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
