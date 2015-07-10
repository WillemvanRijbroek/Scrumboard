using ScrumBoard.ScrumboardService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrumBoard.Common
{

    public class Data
    {
        private static Data data = new Data();
        private DateTime lastRefresh = DateTime.MinValue;
        private Boolean forceComplete = false;
        public static Data getInstance()
        {
            return data;
        }


        private State[] cacheStates = null;
        private StoryType[] cacheStoryTypes = null;
        ScrumboardSoapClient client = ServiceConn.getClient();

        private Data() { }

        public State[] StateSelectAll()
        {
            if (cacheStates == null) { cacheStates = client.StateSelectAll(); }
            return cacheStates;
        }

        public StoryType[] StoryTypeSelectAll()
        {
            if (cacheStoryTypes == null) { cacheStoryTypes = client.StoryTypeSelectAll(); }
            return cacheStoryTypes;
        }

        public int LayoutPanelInsert(int layoutId, int stateId, string title, int storyTypeId, int column, int row, int height, int width)
        {
            return client.LayoutPanelInsert(layoutId, stateId, title, storyTypeId, column, row, height, width);
        }

        public void LayoutPanelUpdate(int id, int stateId, string title, int storyTypeId, int column, int row, int height, int width)
        {
            client.LayoutPanelUpdate(id, stateId, title, storyTypeId, column, row, height, width);
        }

        public ScrumBoard.Business.Story getStoryByExternalId(String externalId)
        {
            ScrumBoard.Business.Story st = null;
            //ScrumboardService.ScrumboardSoapClient client = ServiceConn.getClient();
            ScrumboardService.Story s = client.StoryGetByExternalId(Config.ActiveSprint, externalId);
            if (s != null)
            {
                st = new ScrumBoard.Business.Story(s.Id, s.ExternalId, s.SprintId, s.StoryTypeId, s.Description, s.Estimate, s.StatusId, s.BackColor, s.X, s.Y, s.Tag);
            }
            return st;
        }

        public void clearCaches()
        {
            cachedSprintStories = null;
            cachedTodos = null;
            cacheStates = null;
            cacheStoryTypes = null;
        }



        private Dictionary<int, Todo[]> cachedTodos;

        public Todo[] getStoryTodos(int storyId)
        {
            if (cachedTodos == null)
            {
                cachedTodos = new Dictionary<int, Todo[]>();
            }
            if (!cachedTodos.ContainsKey(storyId))
            {
                cachedTodos.Add(storyId, client.TodoSelect(storyId));
            }
            return cachedTodos[storyId];
        }

        public int TodoInsert(int storyId, string description, int estimate, int backcolor, int x, int y)
        {
            if (cachedTodos != null && cachedTodos.ContainsKey(storyId))
            {
                cachedTodos.Remove(storyId);
            }
            forceComplete = true;
            return client.TodoInsert(storyId, description, estimate, backcolor, x, y);
        }

        public void TodoUpdate(ScrumBoard.ScrumboardService.Todo todo)
        {
            if (cachedTodos != null && cachedTodos.ContainsKey(todo.StoryId))
            {
                cachedTodos.Remove(todo.StoryId);
            }
            client.TodoUpdate(todo.Id, todo.StoryId, todo.Description, todo.Estimate, todo.BackColor, todo.X, todo.Y);
            forceComplete = true;
        }

        public void TodoRemove(ScrumBoard.ScrumboardService.Todo todo)
        {
            if (cachedTodos != null && cachedTodos.ContainsKey(todo.StoryId))
            {
                cachedTodos.Remove(todo.StoryId);
            }
            client.TodoRemove(todo.Id);
            forceComplete = true;
        }


        private Dictionary<int, SortedList<int, ScrumBoard.Business.Story>> cachedSprintStories;

        public Boolean HasPendingChanges()
        {
            if (forceComplete)
            {
                return true;
            }
            else
            {
                ScrumboardService.Story[] stories = client.StoryGetSprintModifiedStories(Config.ActiveSprint, lastRefresh);
                return (stories != null && stories.Length > 0);
            }
        }

        public Boolean HasPendingChanges(ScrumBoard.Business.Story story)
        {
            ScrumboardService.Story[] stories = client.StoryGetSprintModifiedStories(Config.ActiveSprint, lastRefresh);
            if (stories != null && stories.Length > 0)
            {
                foreach (ScrumboardService.Story s in stories)
                {
                    if (s.Id == story.Id)
                    {
                        return true;
                    }
                }
            }
            return false;

        }

        private SortedList<int, ScrumBoard.Business.Story> getStories()
        {
            int sprintId = Config.ActiveSprint;
            if (cachedSprintStories == null)
            {
                cachedSprintStories = new Dictionary<int, SortedList<int, Business.Story>>();
            }
            if (!cachedSprintStories.ContainsKey(sprintId))
            {
                ScrumboardService.Story[] stories = client.StoryGetSprintStories(Config.ActiveSprint);
                lastRefresh = DateTime.Now;
                SortedList<int, ScrumBoard.Business.Story> list = new SortedList<int, ScrumBoard.Business.Story>();
                foreach (ScrumboardService.Story s in stories)
                {
                    list.Add(s.Id, new ScrumBoard.Business.Story(s.Id, s.ExternalId, s.SprintId, s.StoryTypeId, s.Description, s.Estimate, s.StatusId, s.BackColor, s.X, s.Y, s.Tag));
                }
                cachedSprintStories.Add(sprintId, list);

            }
            else
            {
                // Check if there are any modifications on the server
                ScrumboardService.Story[] stories = null;
                if (!forceComplete)
                {
                    stories = client.StoryGetSprintModifiedStories(Config.ActiveSprint, lastRefresh);
                }
                else
                {
                    stories = client.StoryGetSprintStories(Config.ActiveSprint);
                    forceComplete = false;
                }
                lastRefresh = DateTime.Now;
                if (stories.Length > 0)
                {
                    SortedList<int, ScrumBoard.Business.Story> cachedStories = cachedSprintStories[sprintId];
                    foreach (Story s in stories)
                    {
                        if (s.IsRemoved && cachedStories.ContainsKey(s.Id))
                        {
                            cachedStories.Remove(s.Id);
                        }
                        else if (cachedStories.ContainsKey(s.Id))
                        {
                            cachedStories.Remove(s.Id);
                            cachedStories.Add(s.Id, new Business.Story(s.Id, s.ExternalId, s.SprintId, s.StoryTypeId, s.Description, s.Estimate, s.StatusId, s.BackColor, s.X, s.Y, s.Tag));
                        }
                        else
                        {
                            cachedStories.Add(s.Id, new Business.Story(s.Id, s.ExternalId, s.SprintId, s.StoryTypeId, s.Description, s.Estimate, s.StatusId, s.BackColor, s.X, s.Y, s.Tag));
                        }
                    }
                    cachedSprintStories[sprintId] = cachedStories;
                }
            }
            return cachedSprintStories[sprintId];
        }

        public SortedList<int, ScrumBoard.Business.Story> getStories(int storyTypeId, int statusId)
        {
            SortedList<int, ScrumBoard.Business.Story> stories = getStories();
            SortedList<int, ScrumBoard.Business.Story> list = new SortedList<int, Business.Story>();
            foreach (ScrumBoard.Business.Story story in stories.Values)
            {
                if (story.StoryTypeId == storyTypeId && story.StatusId == statusId)
                {
                    list.Add(story.Id, story);
                }
            }
            return list;
        }

        public int StoryInsert(int sprintId, string externalId, int storyTypeId, int statusId, string description, int estimate, int backcolor, int x, int y, string tag)
        {
            return client.StoryInsert(sprintId, externalId, storyTypeId, statusId, description, estimate, backcolor, x, y, tag);
        }

        public void StoryUpdateDetails(ScrumBoard.Business.Story story)
        {
            // Check if it was updated by anyone else
            if (HasPendingChanges(story))
            {
                throw new PendingChangeException();
            }
            client.StoryUpdateDetails(story.Id, story.SprintId, story.ExternalId, story.StoryTypeId, story.StatusId, story.Description.Replace("'", "''"), story.Estimate, story.BackColor, story.X, story.Y, story.Tag.Replace("'", "''"));
        }

        public void StoryRemove(ScrumBoard.Business.Story story)
        {
            if (HasPendingChanges(story))
            {
                throw new PendingChangeException();
            }
            client.StoryRemove(story.Id);
        }
    }
}
