using ScrumBoard.ScrumboardService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrumBoard.Common
{

    public class Data
    {
        public delegate void StoryChangedEventHandler(Story story);
        public event StoryChangedEventHandler StoryChanged;
        public delegate void TodoChangedEventHandler(Todo todo);
        public event TodoChangedEventHandler TodoChanged;

        private static Data data = new Data();
        private DateTime lastRefresh = DateTime.MinValue;


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

        public void TodoInsert(Todo todo)
        {
            Todo t = client.TodoInsert(todo.StoryId, todo.Description, todo.Estimate, todo.BackColor, todo.X, todo.Y);
            if (t != null)
            {
                if (t.Modified > lastRefresh)
                {
                    lastRefresh = t.Modified;
                }
                if (cachedTodos != null && cachedTodos.ContainsKey(todo.StoryId))
                {
                    Todo[] todos = cachedTodos[t.StoryId];
                    Todo[] newTodos = null;
                    if (todos != null)
                    {
                        newTodos = new Todo[todos.Length];
                        for (int i = 0; i < todos.Length; i++)
                        {
                            newTodos[i] = todos[i];
                        }
                        newTodos[todos.Length] = t;
                    }
                    else
                    {
                        newTodos = new Todo[1];
                        newTodos[0] = t;
                    }
                    cachedTodos.Add(t.StoryId, newTodos);
                }
                if (TodoChanged != null)
                {
                    TodoChanged(t);
                }
            }
        }

        public void TodoUpdate(ScrumBoard.ScrumboardService.Todo todo)
        {
            if (cachedTodos != null && cachedTodos.ContainsKey(todo.StoryId))
            {
                cachedTodos.Remove(todo.StoryId);
            }
            Todo t = client.TodoUpdate(todo.Id, todo.StoryId, todo.Description, todo.Estimate, todo.BackColor, todo.X, todo.Y);
            // forceComplete = true;
            if (t != null && t.Modified > lastRefresh)
            {
                lastRefresh = t.Modified;
            }
        }

        public void TodoRemove(ScrumBoard.ScrumboardService.Todo todo)
        {
            if (cachedTodos != null && cachedTodos.ContainsKey(todo.StoryId))
            {
                cachedTodos.Remove(todo.StoryId);
            }
            Todo t = client.TodoRemove(todo.Id);
            if (t != null && t.Modified > lastRefresh)
            {
                lastRefresh = t.Modified;
            }
        }


        private Dictionary<int, SortedList<int, Story>> cachedSprintStories;

        public Boolean HasPendingChanges()
        {
            DateTime lastModified = lastRefresh > DateTime.MinValue ? lastRefresh.AddMilliseconds(1000) : lastRefresh;
            Story[] stories = client.StoryGetSprintModifiedStories(Config.ActiveSprint, lastModified);
            return (stories != null && stories.Length > 0);
        }

        public Boolean HasPendingChanges(Story story)
        {
            DateTime lastModified = lastRefresh > DateTime.MinValue ? lastRefresh.AddMilliseconds(1000) : lastRefresh;
            ScrumboardService.Story[] stories = client.StoryGetSprintModifiedStories(Config.ActiveSprint, lastModified);
            if (stories != null && stories.Length > 0)
            {
                foreach (Story s in stories)
                {
                    if (s.Id == story.Id && s.Modified > story.Modified)
                    {
                        return true;
                    }
                }
            }
            return false;

        }

        private SortedList<int, Story> getStories()
        {
            int sprintId = Config.ActiveSprint;
            if (cachedSprintStories == null)
            {
                cachedSprintStories = new Dictionary<int, SortedList<int, Story>>();
            }
            if (!cachedSprintStories.ContainsKey(sprintId))
            {
                Story[] stories = client.StoryGetSprintStories(Config.ActiveSprint);
                SortedList<int, Story> list = new SortedList<int, Story>();
                foreach (Story s in stories)
                {
                    if (s.Modified > lastRefresh)
                    {
                        lastRefresh = s.Modified;
                    }
                    list.Add(s.Id, s);
                }
                cachedSprintStories.Add(sprintId, list);
            }
            else
            {
                // Check if there are any modifications on the server
                ScrumboardService.Story[] stories = null;
                DateTime lastModified = lastRefresh > DateTime.MinValue ? lastRefresh : lastRefresh;
                stories = client.StoryGetSprintModifiedStories(Config.ActiveSprint, lastModified);
                if (stories.Length > 0)
                {
                    SortedList<int, Story> cachedStories = cachedSprintStories[sprintId];
                    foreach (Story s in stories)
                    {
                        if (s.Modified > lastRefresh)
                        {
                            lastRefresh = s.Modified;
                        }
                        if (s.IsRemoved && cachedStories.ContainsKey(s.Id))
                        {
                            cachedStories.Remove(s.Id);
                        }
                        else if (cachedStories.ContainsKey(s.Id))
                        {
                            cachedStories.Remove(s.Id);
                            cachedStories.Add(s.Id, s);
                        }
                        else
                        {
                            cachedStories.Add(s.Id, s);
                        }
                    }
                    cachedSprintStories[sprintId] = cachedStories;
                }
            }
            return cachedSprintStories[sprintId];
        }


        public SortedList<int, Story> getStories(int storyTypeId, int statusId)
        {
            SortedList<int, Story> stories = getStories();
            SortedList<int, Story> list = new SortedList<int, Story>();
            foreach (Story story in stories.Values)
            {
                if (story.StoryTypeId == storyTypeId && story.StatusId == statusId)
                {
                    if (story.Modified > lastRefresh)
                    {
                        lastRefresh = story.Modified;
                    }
                    list.Add(story.Id, story);
                }
            }
            return list;
        }
        public Story StoryInsert(int sprintId, string externalId, int storyTypeId, int statusId, string description, int estimate, int backcolor, int x, int y, string tag)
        {
            Story s = client.StoryInsert(sprintId, externalId, storyTypeId, statusId, description, estimate, backcolor, x, y, tag);
            if (s != null)
            {
                // refresh data with all modifications
                getStories();
            }
            return s;
        }

        public Story StoryUpdateDetails(Story story)
        {
            // Check if it was updated by anyone else
            if (HasPendingChanges(story))
            {
                throw new PendingChangeException();
            }
            Story s = client.StoryUpdateDetails(story.Id, story.SprintId, story.ExternalId, story.StoryTypeId, story.StatusId, story.Description, story.Estimate, story.BackColor, story.X, story.Y, story.Tag);
            if (s != null)
            {
                // refresh data with all modifications
                getStories();
            }
            return s;
        }

        public void StoryRemove(Story story)
        {
            if (HasPendingChanges(story))
            {
                throw new PendingChangeException();
            }
            Story s = client.StoryRemove(story.Id);
            if (s != null)
            {
                // refresh data with all modifications
                getStories();
            }
        }
    }
}
