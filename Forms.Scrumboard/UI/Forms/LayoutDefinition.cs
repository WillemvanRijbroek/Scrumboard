using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScrumBoard.Common;
using ScrumBoard.UI.Controls;

namespace ScrumBoard.UI.Forms
{
    public partial class LayoutDefinition : Form
    {
        ScrumboardService.ScrumboardSoapClient client = ServiceConn.getClient();
        private ScrumboardService.Layout layout;

        public LayoutDefinition()
        {
            InitializeComponent();
            lvwPanels.Columns.Add("Title");
            lvwPanels.Columns.Add("Story type");
            lvwPanels.Columns.Add("Story status");
            lvwPanels.Columns.Add("Column");
            lvwPanels.Columns.Add("Row");

        }

        public ScrumboardService.Layout LayoutDef
        {
            set
            {
                layout = value;
                txtName.Text = value.Name;
                numColumns.Value = value.TotalColumns;
                numRows.Value = value.TotalRows;
                numFontSize.Value = value.FontSize;
                numStoryHeight.Value = value.StoryHeight;
                numStoryWidth.Value = value.StoryWidth;
                RefreshPanels();
            }
        }

        private void RefreshPanels()
        {
            lvwPanels.Items.Clear();
            pnlExample.Controls.Clear();
            if (layout != null)
            {
                ScrumboardService.Panel[] panels = client.LayoutPanelSelectByLayout(layout.Id);
                foreach (ScrumboardService.Panel panel in panels)
                {
                    ListViewItem item = new ListViewItem(panel.Title);
                    item.Tag = panel.Id;
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, panel.StoryTypeName));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, panel.StateName));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, panel.Column.ToString()));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, panel.Row.ToString()));

                    lvwPanels.Items.Add(item);

                    if (!"Burndown".Equals(panel.Title))
                    {
                        pnlExample.Controls.Add(new StatePanel(null, layout, panel));
                    }
                    else
                    {
                        pnlExample.Controls.Add(new Burndown(layout, panel));
                    }
                }
                autoResize();
            }
        }
        private void autoResize()
        {
            for (int i = pnlExample.Controls.Count - 1; i >= 0; i--)
            {
                if (pnlExample.Controls[i] is StatePanel)
                {
                    ((StatePanel)pnlExample.Controls[i]).AutoResize();
                }
                else if (pnlExample.Controls[i] is Burndown)
                {
                    ((Burndown)pnlExample.Controls[i]).AutoResize();
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (layout != null)
            {
                client.LayoutUpdate(layout.Id, txtName.Text, (int)numColumns.Value, (int)numRows.Value, (int)numFontSize.Value, (int)numStoryWidth.Value, (int)numStoryHeight.Value);
            }
            else
            {
                client.LayoutInsert(txtName.Text, (int)numColumns.Value, (int)numRows.Value, (int)numFontSize.Value, (int)numStoryWidth.Value, (int)numStoryHeight.Value);
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (layout != null)
            {
                LayoutPanel form = new LayoutPanel();
                form.LayoutId = layout.Id;
                form.ShowDialog(this);
                RefreshPanels();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (layout != null && lvwPanels.SelectedItems.Count == 1)
            {
                LayoutPanel form = new LayoutPanel();
                ScrumboardService.Panel panel = client.LayoutPanelGet((int)lvwPanels.SelectedItems[0].Tag);
                form.Panel = panel;
                form.ShowDialog(this);
                RefreshPanels();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (layout != null && lvwPanels.SelectedItems.Count == 1)
            {
                if (MessageBox.Show("Are you sure?", "Remove Panel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    client.LayoutPanelRemove((int)lvwPanels.SelectedItems[0].Tag);
                    RefreshPanels();
                }
            }
        }

        private void LayoutDefinition_ResizeEnd(object sender, EventArgs e)
        {
            RefreshPanels();
        }

    }
}
