using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScrumBoard.Common;

namespace ScrumBoard.UI.Forms
{
    public partial class LayoutPanel : Form
    {
        private const String BURNDOWN = "Burndown";
        ScrumboardService.ScrumboardSoapClient client = ServiceConn.getClient();
        private ScrumboardService.Panel panel;
        private int layoutId;

        public LayoutPanel()
        {
            InitializeComponent();
            cmbState.DisplayMember = "Name";
            cmbState.ValueMember = "Id";
            cmbState.DataSource = client.StateSelectAll();

            cmbStoryType.DisplayMember = "Name";
            cmbStoryType.ValueMember = "Id";
            cmbStoryType.DataSource = client.StoryTypeSelectAll();
        }

        public int LayoutId
        {
            get { return layoutId; }
            set { layoutId = value; }
        }

        public ScrumboardService.Panel Panel
        {
            set
            {
                this.panel = value;
                txtName.Text = value.Title;
                chkIsBurndownGraph.Checked = BURNDOWN.Equals(value.Title);
                numHeight.Value = value.Heigth;
                numWidth.Value = value.Width;
                numColumn.Value = value.Column;
                numRow.Value = value.Row;
                LayoutId = value.LayoutId;
                cmbState.SelectedValue = value.StateId;
                cmbStoryType.SelectedValue = value.StoryTypeId;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (panel != null)
            {
                client.LayoutPanelUpdate(panel.Id, (int)cmbState.SelectedValue, txtName.Text, (int)cmbStoryType.SelectedValue, (int)numColumn.Value, (int)numRow.Value, (int)numHeight.Value, (int)numWidth.Value);
            }
            else
            {
                client.LayoutPanelInsert(layoutId, (int)cmbState.SelectedValue, txtName.Text, (int)cmbStoryType.SelectedValue, (int)numColumn.Value, (int)numRow.Value, (int)numHeight.Value, (int)numWidth.Value);
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private String oldTitle;
        private void chkIsBurndownGraph_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsBurndownGraph.Checked)
            {
                oldTitle = txtName.Text;
                txtName.Text = BURNDOWN;
            }
            else
            {
                txtName.Text = oldTitle;
            }
        }
    }
}
