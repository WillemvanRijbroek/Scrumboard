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
    public partial class StoryDetail : Form
    {
        private int state = 1;
        private Story s;

        public StoryDetail()
        {
            InitializeComponent();
            btnColor.BackColor = Color.FromArgb(Config.DefaultBackColor);
            cmbStoryType.Items.Add(Story.PLANNED);
            cmbStoryType.Items.Add(Story.UNPLANNED);
            cmbStoryType.Items.Add(Story.BONUS);
            cmbStoryType.Text = Config.DefaultStoryType;
            txtEstimate.Text = Config.DefaultEstimate.ToString();
            txtId.Enabled = !Config.ViewOnly;
            txtDescription.Enabled = !Config.ViewOnly;
            txtEstimate.Enabled = !Config.ViewOnly;
            txtTag.Enabled = !Config.ViewOnly;
            cmbStoryType.Enabled = !Config.ViewOnly;
            btnColor.Enabled = !Config.ViewOnly;
            btnOk.Enabled = !Config.ViewOnly;
        }


        private int StoryTypeId
        {
            get
            {
                switch (cmbStoryType.Text)
                {
                    case Story.PLANNED:
                    default:
                        return 1;
                    case Story.UNPLANNED:
                        return 2;
                    case Story.BONUS:
                        return 3;
                }
            }
            set
            {
                switch (value)
                {
                    case 1:
                        cmbStoryType.Text = Story.PLANNED; break;
                    case 2:
                        cmbStoryType.Text = Story.UNPLANNED; break;
                    case 3:
                        cmbStoryType.Text = Story.BONUS; break;
                }
            }
        }

        public Story Story
        {
            set
            {
                txtId.Text = value.ExternalId.ToString();
                txtEstimate.Text = value.Estimate.ToString();
                txtDescription.Text = value.Description;
                StoryTypeId = value.StoryTypeId;
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
                    s.StoryTypeId = StoryTypeId;
                    s.Description = txtDescription.Text;
                    s.Estimate = Int32.Parse(txtEstimate.Text);
                    s.Tag = txtTag.Text;
                    s.BackColor = btnColor.BackColor.ToArgb();
                    s.Save();
                }
                else
                {
                    s = new Story(txtId.Text, Config.ActiveSprint, StoryTypeId, txtDescription.Text, Int32.Parse(txtEstimate.Text), state, btnColor.BackColor.ToArgb(), 30, 30, txtTag.Text);
                    s.Save();
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
