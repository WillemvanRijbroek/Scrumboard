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
            Console.WriteLine("Fontsize: " + layout.FontSize);
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
            mnuRemoveStory.Visible = !Config.ViewOnly;
        }

        public Story Story
        {
            set
            {
                lblId.Text = value.ExternalId.ToString();
                txtEstimate.Text = value.Estimate.ToString();
                txtDescription.Text = value.Description + (value.Tag != null ? "\r\n" + value.Tag : "");
                this.BackColor = Color.FromArgb(value.BackColor);
                value.BackColor = this.BackColor.ToArgb();
                txtEstimate.BackColor = this.BackColor;
                txtDescription.BackColor = this.BackColor;
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
            Story = s;
        }

        protected override void SetMoving(bool moving)
        {
            if (moving)
                BackColor = Parent.BackColor;
            else
                BackColor = txtDescription.BackColor;
            lblId.Visible = !moving;
            txtDescription.Visible = !moving;
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
            s.Save();
            if (Config.AutoEditDetails)
            {
                StoryDetail form = new StoryDetail();
                form.Story = s;
                form.ShowDialog();
                Story = s;
                movedToPanel.AddOrUpdateStory(s);
            }
        }

        protected override void NoteLocationChanged(int x, int y, Boolean save)
        {
            base.NoteLocationChanged(x, y, save);
            int tx = 10;
            int ty = 10;

            foreach (StickyTodo std in todos)
            {
                Point loc = new Point(x + tx, y + ty);
                std.Location = loc;
                std.Visible = true;
                tx += 10;
                ty += 10;
                std.Show();
                std.Refresh();
            }
            if (save)
            {
                s.X = x;
                s.Y = y;
                s.Save();
            }
        }

        private void StickyStory_MouseDown(object sender, MouseEventArgs e)
        {
            onMouseDown(e);
        }

        private void StickyStory_MouseUp(object sender, MouseEventArgs e)
        {
            onMouseUp(e);

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
            Story = s;
        }

        private void mnuRemoveStory_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, string.Format("Are you sure to remove story '{0}'?", s.Description), "Question", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (s.Remove())
                    ((ScrumBoard.UI.Forms.ScrumBoardForm)this.Parent.Parent).RemovedStory(this);
            }
        }

        private void mnuAddTodo_Click(object sender, EventArgs e)
        {
            TodoDetail form = new TodoDetail();
            form.StoryId = s.Id;
            form.ShowDialog();
            ((ScrumBoard.UI.Forms.ScrumBoardForm)this.Parent.Parent).RefreshStory(this);
        }

        private void lblId_MouseHover(object sender, EventArgs e)
        {
            this.BringToFront();
        }

        private void txtEstimate_MouseHover(object sender, EventArgs e)
        {
            this.BringToFront();
        }

        private void txtDescription_MouseHover(object sender, EventArgs e)
        {
            this.BringToFront();
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
    }
}
