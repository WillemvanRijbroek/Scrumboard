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
using System.Drawing.Drawing2D;

namespace ScrumBoard.UI.Controls
{
    public partial class StickyNote : UserControl
    {
        protected ScrumboardService.Layout layout;
        public StickyNote()
        {
            InitializeComponent();
        }
        public StickyNote(ScrumboardService.Layout layout, Mover mover)
        {
            InitializeComponent();
            this.layout = layout;
            Mover = mover;
            Width = layout.StoryWidth;
            Height = layout.StoryHeight;
        }

        protected virtual void MovedTo(StatePanel movedToPanel)
        {
            if (Parent is StatePanel)
            {
                ((StatePanel)Parent).MovedToOtherPanel(this);
            }
        }

        protected virtual void NoteLocationChanged(int x, int y, Boolean save)
        {
            Location = new Point(x, y);
        }

        public Mover Mover { get; set; }
        public Mover PlaceHolder { get; set; }
        int moving = 0;
        int mX = 0; int mY = 0;
        int startTop = 0;
        int startY = 0;

        protected virtual void SetMoving(bool moving) { }

        protected void onMouseDown(MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && !Config.ViewOnly)
            {
                SetMoving(true);
                startTop = Top;
                startY = ((Panel)Parent).AutoScrollPosition.Y;
                moving = 1;
                Mover.Visible = true;
                Mover.Location = new Point(Location.X, Location.Y);
                Mover.Width = Width;
                Mover.Height = Height;
                Mover.BringToFront();
            }
        }
        protected void onMouseMove(MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && (moving > 0) && !Config.ViewOnly)
            {
                if (moving == 1)
                {
                    int offsetY = (startY - ((Panel)Parent).AutoScrollPosition.Y);
                    int offsetTop = Top - startTop;
                    Point loc = new Point(Left + Parent.Left, Top + Parent.Top + offsetTop);
                    Mover.SetMoverLocation(loc);
                    moving++;
                    mX = e.X;
                    mY = e.Y;
                }
                else if (moving > 1)
                {
                    int nX = (e.X - mX) + Mover.Left;
                    int nY = (e.Y - mY) + Mover.Top;
                    mX = e.X;
                    mY = e.Y;

                    Point loc = new Point(nX, nY);
                    Mover.SetMoverLocation(loc);
                    Mover.Top = nY;
                    Mover.Left = nX;
                }
            }
        }
        protected void onMouseUp(MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && !Config.ViewOnly)
            {
                Mover.Visible = false;
                moving = 0;
                SetMoving(false);
                StatePanel myPanel = (StatePanel)Parent;
                if (myPanel != null)
                {
                    StatePanel movedToPanel = ((ScrumBoard.UI.Forms.ScrumBoardForm)Parent.Parent).getPanel(Mover.Left, Mover.Top);
                    if (movedToPanel != null && myPanel != movedToPanel)
                    {
                        MovedTo(movedToPanel);
                        BringToFront();
                    }
                    else if (movedToPanel != null && myPanel == movedToPanel)
                    {
                        NoteLocationChanged(Mover.Left - Parent.Left, Mover.Top - Parent.Top, true);
                    }
                }
            }
        }

        private void StickyNote_MouseDown(object sender, MouseEventArgs e)
        {
            onMouseDown(e);
        }

        private void StickyNote_MouseUp(object sender, MouseEventArgs e)
        {
            onMouseUp(e);
        }

        private void StickyNote_MouseMove(object sender, MouseEventArgs e)
        {
            onMouseMove(e);
        }

        private void StickyNote_LocationChanged(object sender, EventArgs e)
        {
            if (BackColor != Color.Gray)
            {
                NoteLocationChanged(Location.X, Location.Y, false);
            }
        }

        private void StickyNote_MouseHover(object sender, EventArgs e)
        {
            this.BringToFront();
        }
    }
}
