using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScrumBoard.UI.Forms;
using ScrumBoard.Common;
using ScrumBoard.Business;

namespace ScrumBoard.UI.Controls
{
    public partial class StickyTodo : StickyNote
    {
        private ScrumBoard.ScrumboardService.Todo todo;
        

        public StickyTodo(ScrumboardService.Layout layout, Mover mover)
            : base(layout, mover)
        {
            InitializeComponent();
            initMenus();
            txtEstimate.Font = new Font(txtEstimate.Font.FontFamily, layout.FontSize);
            txtDescription.Font = new Font(txtDescription.Font.FontFamily, layout.FontSize);
        }
        
        protected override void SetMoving(bool moving)
        {
            if (moving)
                BackColor = Parent.BackColor;
            else
                BackColor = txtDescription.BackColor;
            txtDescription.Visible = !moving;
            txtEstimate.Visible = !moving;
        }
        protected void initMenus()
        {
            if (Config.ViewOnly)
            {
                editToolStripMenuItem.Text = "View";
            }
            else
            {
                editToolStripMenuItem.Text = "Edit";
            }
            mnuRemoveStory.Visible = !Config.ViewOnly;
        }

        private void Edit()
        {
            TodoDetail form = new TodoDetail();
            form.Todo = todo;
            form.ShowDialog();
            Todo = todo;
            ((ScrumBoard.UI.Forms.ScrumBoardForm)this.Parent.Parent).ModifiedTodo(this);
        }

        private void Remove()
        {
            if (MessageBox.Show(this, string.Format("Are you sure to remove todo '{0}'?", todo.Description), "Question", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Data.getInstance().TodoRemove(todo);
                ((ScrumBoard.UI.Forms.ScrumBoardForm)this.Parent.Parent).ModifiedTodo(this);
            }
        }

        public ScrumBoard.ScrumboardService.Todo Todo
        {
            set
            {
                txtEstimate.Text = value.Estimate.ToString();
                txtDescription.Text = value.Description;
                this.BackColor = Color.FromArgb(value.BackColor);
                value.BackColor = this.BackColor.ToArgb();
                txtEstimate.BackColor = this.BackColor;
                txtDescription.BackColor = this.BackColor;
                todo = value;
                Location = new Point(todo.X, todo.Y);
                initMenus();
            }
        }

        private void StickyStory_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(this.Left + this.Parent.Left, this.Top + this.Parent.Top);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void txtDescription_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Edit();
        }

        private void mnuRemoveStory_Click(object sender, EventArgs e)
        {
            Remove();
        }

        private void txtDescription_MouseHover(object sender, EventArgs e)
        {
            this.BringToFront();
        }

        private void txtEstimate_MouseHover(object sender, EventArgs e)
        {
            this.BringToFront();
        }

        private void txtDescription_MouseDown(object sender, MouseEventArgs e)
        {
            onMouseDown(e);
        }

        private void txtDescription_MouseMove(object sender, MouseEventArgs e)
        {
            onMouseMove(e);
        }

        private void txtDescription_MouseUp(object sender, MouseEventArgs e)
        {
            onMouseUp(e);
        }

        private void txtEstimate_MouseDown(object sender, MouseEventArgs e)
        {
            onMouseDown(e);
        }

        private void txtEstimate_MouseMove(object sender, MouseEventArgs e)
        {
            onMouseMove(e);
        }

        private void txtEstimate_MouseUp(object sender, MouseEventArgs e)
        {
            onMouseUp(e);
        }

        private void StickyTodo_MouseLeave(object sender, EventArgs e)
        {
            SendToBack();
        }

    }
}
