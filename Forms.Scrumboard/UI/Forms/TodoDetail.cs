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
        private ScrumBoard.ScrumboardService.Todo todo;

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
                todo = value;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (todo != null)
            {
                todo.StoryId = this.StoryId;
                todo.Description = txtDescription.Text;
                todo.Estimate = Int32.Parse(txtEstimate.Text);
                todo.BackColor = btnColor.BackColor.ToArgb();
                Data.getInstance().updateTodo(todo);
            }
            else
            {
                todo = new ScrumboardService.Todo();
                todo.StoryId = this.StoryId;
                todo.Description = txtDescription.Text;
                todo.Estimate = Int32.Parse(txtEstimate.Text);
                todo.BackColor = btnColor.BackColor.ToArgb();
                todo.X = 30;
                todo.Y = 30;
                Data.getInstance().insertTodo(todo);
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
