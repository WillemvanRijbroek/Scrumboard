﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScrumBoard.Business;
using ScrumBoard.Common;
using ScrumBoard.ScrumboardService;

namespace ScrumBoard.UI.Controls
{
    public partial class StatePanel : System.Windows.Forms.Panel
    {
        
        private Dictionary<int, StickyStory> stories;
        private ScrumboardService.Panel panel;
        private ScrumboardService.Layout layout;
        Mover mover;
        Mover placeHolder;
        Boolean hasTitle = false;

        public StatePanel()
        {
            InitializeComponent();
        }

        public StatePanel(Mover mover, ScrumboardService.Layout layout, ScrumboardService.Panel panel)
        {
            InitializeComponent();
            stories = new Dictionary<int, StickyStory>();
            this.mover = mover;
            placeHolder = new Mover();
            BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            AutoScroll = true;
            SetAutoScrollMargin(5, 5);
            this.panel = panel;
            this.layout = layout;
            if (!String.IsNullOrEmpty(panel.Title))
            {
                // add label
                hasTitle = true;
                Label lblTitle = new Label();
                lblTitle.Text = panel.Title;
                lblTitle.Dock = DockStyle.Top;
                lblTitle.Top = 0;
                lblTitle.Left = 0;
                lblTitle.Font = new System.Drawing.Font(FontFamily.GenericSansSerif, 14, FontStyle.Bold);
                lblTitle.AutoSize = true;
                Controls.Add(lblTitle);
            }
            Data.getInstance().StoryChanged += StatePanel_StoryChanged;
        }



        public int StateId
        {
            get
            {
                return panel.StateId;
            }
        }
        public int StoryTypeId
        {
            get
            {
                return panel.StoryTypeId;
            }
        }

        public void AutoResize()
        {
            int height = (Parent.Height / layout.TotalRows) * panel.Heigth;
            int width = (Parent.Width / layout.TotalColumns) * panel.Width;
            Top = height * panel.Row;
            Left = width * panel.Column;
            Height = height;
            Width = width;
        }

        void StatePanel_StoryChanged(Story story)
        {
            Boolean exists = stories.ContainsKey(story.Id);
            Boolean storyMatchesPanel = (story.StatusId == StateId) && (story.StoryTypeId == StoryTypeId);
            if (exists)
            {
                // Remove the StickyStory
                StickyStory st = stories[story.Id];
                if (!storyMatchesPanel || story.IsRemoved)
                {
                    Controls.Remove(st);
                    foreach (StickyTodo std in st.StickyTodos)
                    {
                        Controls.Remove(std);
                    }
                    stories.Remove(story.Id);
                }
                else
                {
                    UpdateStickyStory(st, story);
                }
            }
            else if (storyMatchesPanel)
            {
                AddStickyStory(story);
            }
        }

        private void AddStickyStory(Story story)
        {
            StickyStory st = new StickyStory(layout, mover);
            st.Story = story;
            st.Width = layout.StoryWidth;
            st.Height = layout.StoryHeight;
            Controls.Add(st);
            UpdateStickyTodos(st, story);
            stories.Add(story.Id, st);
            st.BringToFront();
        }

        private void UpdateStickyTodos(StickyStory st, Story story)
        {
            foreach (StickyTodo std in st.StickyTodos)
            {
                Controls.Remove(std);
            }
            if (story.Todos != null)
            {
                int x = Config.TODO_SPACING;
                int y = Config.TODO_SPACING;
                foreach (ScrumBoard.ScrumboardService.Todo t in story.Todos)
                {
                    StickyTodo std = new StickyTodo(layout, mover);
                    t.X = story.X + x;
                    t.Y = story.Y + y;
                    std.Todo = t;
                    x += Config.TODO_SPACING;
                    y += Config.TODO_SPACING;
                    std.BringToFront();
                    std.Width = layout.StoryWidth;
                    std.Height = layout.StoryHeight;
                    Controls.Add(std);
                    st.AddTodo(std);
                }
            }
        }
        private void UpdateStickyStory(StickyStory st, Story story)
        {
            st.Story = story;
            UpdateStickyTodos(st, story);
            st.BringToFront();

        }
        //public void RefreshStories()
        //{
        //    ClearControls();
        //    this.stories.Clear();
        //    SortedList<int, Story> stories = Data.getInstance().getStories(panel.StoryTypeId, panel.StateId);
        //    IEnumerator<KeyValuePair<int, Story>> enm = stories.GetEnumerator();
        //    enm.MoveNext();
        //    Story s = enm.Current.Value;
        //    while (s != null)
        //    {
        //        AddOrUpdateStory(s);
        //        enm.MoveNext();
        //        s = enm.Current.Value;
        //    }
        //    // See if we need to remove controls
        //    for (int i = Controls.Count - 1; i >= 0; i--)
        //    {
        //        if (Controls[i] is StickyStory)
        //        {
        //            StickyStory st = ((StickyStory)Controls[i]);
        //            if (!stories.ContainsKey(st.Story.Id))
        //            {
        //                Controls.RemoveAt(i);
        //                foreach (StickyTodo td in st.StickyTodos)
        //                {
        //                    Controls.Remove(td);
        //                }
        //            }
        //        }
        //    }

        //}


        public void MovedToOtherPanel(StickyNote note)
        {
            if (Controls.Contains(note))
            {
                if (note is StickyStory)
                {
                    foreach (StickyTodo todo in ((StickyStory)note).StickyTodos)
                    {
                        Controls.Remove(todo);
                    }
                    stories.Remove(((StickyStory)note).Story.Id);
                }
                Controls.Remove(note);
            }

        }
        public void AutoAlignStories()
        {
            if (Controls.Count == 2)
            {
                Console.WriteLine();
            }
            int i = 0;
            bool first = true;
            int lastLeft = 2;
            int lastTop = hasTitle ? 30 : 2;
            SortedList<String, StickyStory> sortedStories = new SortedList<string, StickyStory>();
            foreach (Control control in Controls)
            {
                if (control is StickyStory)
                {
                    StickyStory st = (StickyStory)control;
                    sortedStories.Add(st.Story.ExternalId + "_" + st.Story.Id, st);
                }
            }

            IEnumerator<KeyValuePair<String, StickyStory>> enuma = sortedStories.GetEnumerator();
            enuma.MoveNext();
            StickyStory stickyStory = enuma.Current.Value;

            while (stickyStory != null)
            {

                if (stickyStory != null)
                {
                    Story story = stickyStory.Story;

                    if ((lastLeft + 2 + (2 * stickyStory.Width)) < Width)
                    {
                        lastLeft = lastLeft + 2 + stickyStory.Width;
                        stickyStory.Top = lastTop + this.AutoScrollPosition.Y;
                        stickyStory.Left = lastLeft + this.AutoScrollPosition.X;
                        if (first)
                        {
                            stickyStory.Left = 2 + this.AutoScrollPosition.X;
                            first = false;
                            lastLeft = 2;
                        }
                    }
                    else
                    {
                        lastLeft = 2;
                        i++;
                        if (hasTitle)
                            lastTop = 30 + (i * stickyStory.Height);
                        else
                            lastTop = 2 + (i * stickyStory.Height);
                        stickyStory.Top = lastTop + this.AutoScrollPosition.Y;
                        stickyStory.Left = lastLeft + this.AutoScrollPosition.X;

                    }
                    //   Console.WriteLine(story.ExternalId + " (" + stickyStory.Top + ":" + stickyStory.Left + ")");
                    stickyStory.SavePosition();
                }
                enuma.MoveNext();
                stickyStory = enuma.Current.Value;
            }
        }

        private void ClearControls()
        {
            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                if (Controls[i] is StickyNote)
                {
                    Controls.RemoveAt(i);
                }
            }
        }

        private void StatePanel_Move(object sender, EventArgs e)
        {

        }
    }
}
