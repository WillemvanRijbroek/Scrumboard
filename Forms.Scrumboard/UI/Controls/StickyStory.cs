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
using ScrumBoard.ScrumboardService;

namespace ScrumBoard.UI.Controls
{
    public partial class StickyStory : StickyNote
    {
        private List<StickyTodo> todos;
        private Story s;
        public struct MoveToOtherPanelEventArgs
        {
            public Control Parent;
            public Story Story;
            public int X;
            public int Y;

            public MoveToOtherPanelEventArgs(Control parent, Story story, int x, int y)
            {
                Parent = parent;
                Story = story;
                X = x;
                Y = y;
            }
        }

        public StickyStory(ScrumboardService.Layout layout, Mover mover)
            : base(layout, mover)
        {
            InitializeComponent();
            initMenus();
            // Console.WriteLine("Fontsize: " + layout.FontSize);
            lblId.Font = new Font(lblId.Font.FontFamily, layout.FontSize);
            txtEstimate.Font = new Font(txtEstimate.Font.FontFamily, layout.FontSize);
            txtDescription.Font = new Font(txtDescription.Font.FontFamily, layout.FontSize);
            todos = new List<StickyTodo>();
        }

        private void initMenus()
        {
            if (Config.ViewOnly)
            {
                editToolStripMenuItem.Text = "View";
            }
            else
            {
                editToolStripMenuItem.Text = "Edit";
            }
            mnuAddTodo.Visible = !Config.ViewOnly;
            mnuRemoveStory.Visible = !Config.ViewOnly;
        }

        public Story Story
        {
            set
            {
                lblId.Text = value.ExternalId.ToString();
                txtEstimate.Text = value.Estimate.ToString();
                txtDescription.Text = value.Description;
                lblTag.Text = value.Tag ;
                this.BackColor = Color.FromArgb(value.BackColor);
                value.BackColor = this.BackColor.ToArgb();
                txtEstimate.BackColor = this.BackColor;
                txtDescription.BackColor = this.BackColor;
                lblTag.BackColor = this.BackColor;
                s = value;
                Location = new Point(s.X, s.Y);
                initMenus();
            }
            get { return s; }
        }

        private void StickyStory_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(this.Left + this.Parent.Left, this.Top + this.Parent.Top);
        }

        private void lblId_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            String url = String.Format(Config.IssueTrackingSystemURL, ((LinkLabel)sender).Text);
            System.Diagnostics.Process.Start(url);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StoryDetail form = new StoryDetail();
            form.Story = s;
            form.ShowDialog();
        }

        protected override void SetMoving(bool moving)
        {
            if (moving)
                BackColor = Parent.BackColor;
            else
                BackColor = txtDescription.BackColor;
            lblId.Visible = !moving;
            txtDescription.Visible = !moving;
            lblTag.Visible = !moving;
            txtEstimate.Visible = !moving;
        }
        protected override void MovedTo(StatePanel movedToPanel)
        {
            base.MovedTo(movedToPanel);
            s.StatusId = movedToPanel.StateId;
            s.StoryTypeId = movedToPanel.StoryTypeId;
            s.X = Mover.Left - movedToPanel.Left;
            s.Y = Mover.Top - movedToPanel.Top;
            if (s.X < 0) s.X = 0;
            if (s.Y < 0) s.Y = 0;
            try
            {
                Data.getInstance().updateStory(s);
            }
            catch (PendingChangeException e)
            {
                MessageBox.Show(this, "Story is already changed by someone else, please retry");
            }
            if (Config.AutoEditDetails)
            {
                StoryDetail form = new StoryDetail();
                form.Story = s;
                form.ShowDialog();
            }
        }

        protected override void NoteLocationChanged(int x, int y, Boolean save)
        {
            base.NoteLocationChanged(x, y, save);
            int tx = Config.TODO_SPACING;
            int ty = Config.TODO_SPACING;

            foreach (StickyTodo std in todos)
            {
                Point loc = new Point(x + tx, y + ty);
                std.Location = loc;
                std.Visible = true;
                tx += Config.TODO_SPACING;
                ty += Config.TODO_SPACING;
                std.Show();
                std.Refresh();
                std.BringToFront();
            }
            BringToFront();
            if (save)
            {
                s.X = x;
                s.Y = y;
                try
                {
                    Data.getInstance().updateStory(s);
                }
                catch (PendingChangeException e)
                {
                    MessageBox.Show(this, "Story is already changed by someone else, please retry");
                }
            }
        }

        private void StickyStory_MouseDown(object sender, MouseEventArgs e)
        {
            onMouseDown(e);
        }

        private void StickyStory_MouseUp(object sender, MouseEventArgs e)
        {
            onMouseUp(e);
            BringToFront();
        }

        private void StickyStory_MouseMove(object sender, MouseEventArgs e)
        {
            onMouseMove(e);
        }

        public void SavePosition()
        {
            NoteLocationChanged(this.Left, this.Top, true);
        }

        private void txtDescription_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            StoryDetail form = new StoryDetail();
            form.Story = s;
            form.ShowDialog();
        }

        private void mnuRemoveStory_Click(object sender, EventArgs e)
        {
            if (!Config.ViewOnly)
            {
                if (MessageBox.Show(this, string.Format("Are you sure to remove story '{0}'?", s.Description), "Question", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        Data.getInstance().removeStory(s);
                    }
                    catch (PendingChangeException ex)
                    {
                        MessageBox.Show(this, "Story is already changed by someone else, please retry");
                    }
                }
            }
        }

        private void mnuAddTodo_Click(object sender, EventArgs e)
        {
            TodoDetail form = new TodoDetail();
            form.StoryId = s.Id;
            form.ShowDialog();
        }

        private void lblId_MouseHover(object sender, EventArgs e)
        {
            BringToFront();
        }

        private void txtEstimate_MouseHover(object sender, EventArgs e)
        {
            BringToFront();
        }

        private void txtDescription_MouseHover(object sender, EventArgs e)
        {
            BringToFront();
        }

        public void AddTodo(StickyTodo todo)
        {
            todos.Add(todo);
        }
        public void RemoveTodo(StickyTodo todo)
        {
            todos.Remove(todo);
        }

        public List<StickyTodo> StickyTodos { get { return todos; } }

        public void BringToFront()
        {
            foreach (StickyTodo std in todos)
            {
                std.BringToFront();
            }
            base.BringToFront();
        }

        private void lblTag_MouseHover(object sender, EventArgs e)
        {
            BringToFront();
        }
        
        private void txtDescription_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            onLinkClicked(e.LinkText);
        }
    }
}
