using ScrumBoard.ScrumboardService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrumBoard.Common
{
    public class StoryCache : Cache
    {
        public delegate void StoryChangedEventHandler(Story story);
        public event StoryChangedEventHandler StoryChanged;

        private Dictionary<int, SortedList<int, Story>> cachedSprintStories;
        private DateTime lastRefresh = DateTime.MinValue;

        public StoryCache()
        {
            cachedSprintStories = new Dictionary<int, SortedList<int, Story>>();
        }

        public override void update()
        {
            int sprintId = Config.ActiveSprint;
            Story[] stories;
            if (lastRefresh == DateTime.MinValue || !cachedSprintStories.ContainsKey(sprintId))
            {
                // Get all stories, and update the cache
                stories = ServiceClient.StoryGetSprintStories(sprintId);
            }
            else
            {
                // Check if there are any modifications on the server
                stories = ServiceClient.StoryGetSprintModifiedStories(sprintId, lastRefresh);
            }
            update(sprintId, stories);
        }

        public override void clear()
        {
            cachedSprintStories.Clear();
        }

        /// <summary>
        /// Gets a list of all (active) sprint stories sorted by External id, Id
        /// </summary>
        /// <param name="sprintId"></param>
        /// <returns></returns>
        public SortedList<int, Story> getSprintStories()
        {
            // Make sure all server side modifications are updated in our cache
            update();
            return cachedSprintStories[Config.ActiveSprint];
        }

        /// <summary>
        /// Gets a list of stories sorted by External id, Id
        /// </summary>
        /// <param name="storyTypeId"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        public SortedList<String, Story> getStories(int storyTypeId, int statusId)
        {
            // Make sure all server side modifications are updated in our cache
            update();
            SortedList<int, Story> stories = cachedSprintStories[Config.ActiveSprint];

            SortedList<String, Story> list = new SortedList<String, Story>();
            if (stories != null)
            {
                foreach (Story story in stories.Values)
                {
                    if (!story.IsRemoved && story.StoryTypeId == storyTypeId && story.StatusId == statusId)
                    {
                        String key = story.ExternalId + "_" + story.Id;
                        list.Add(key, story);
                    }
                }
            }
            return list;
        }

        public void insertStory(int sprintId, string externalId, int storyTypeId, int statusId, string description, decimal estimate, int backcolor, int x, int y, string tag)
        {
            // Make sure all server side modifications are updated in our cache
            update();
            Story s = ServiceClient.StoryInsert(sprintId, externalId, storyTypeId, statusId, description, estimate, backcolor, x, y, tag);
            if (s != null)
            {
                update(s.SprintId, new Story[] { s });
            }
        }

        public void updateStory(Story story)
        {
            // Check if it was updated by anyone else
            if (HasPendingChanges(story.Id, story.Modified))
            {
                throw new PendingChangeException();
            }
            Story s = ServiceClient.StoryUpdateDetails(story.Id, story.SprintId, story.ExternalId, story.StoryTypeId, story.StatusId, story.Description, story.Estimate, story.BackColor, story.X, story.Y, story.Tag);
            if (s != null)
            {
                update(s.SprintId, new Story[] { s });
            }
        }

        public void removeStory(Story story)
        {
            if (HasPendingChanges(story.Id, story.Modified))
            {
                throw new PendingChangeException();
            }
            Story s = ServiceClient.StoryRemove(story.Id);
            if (s != null)
            {
                update(s.SprintId, new Story[] { s });
            }
        }
        public void insertTodo(Todo todo)
        {
            ServiceClient.TodoInsert(todo.StoryId, todo.Description, todo.Estimate, todo.BackColor, todo.X, todo.Y);
            // Make sure all server side modifications are updated in our cache
            update();
        }

        public void updateTodo(ScrumBoard.ScrumboardService.Todo todo)
        {
            if (HasPendingChanges(todo.StoryId, todo.Modified))
            {
                throw new PendingChangeException();
            }
            Todo t = ServiceClient.TodoUpdate(todo.Id, todo.StoryId, todo.Description, todo.Estimate, todo.BackColor, todo.X, todo.Y);
            if (t != null)
            {
                // Make sure all server side modifications are updated in our cache
                update();
            }
        }

        public void removeTodo(ScrumBoard.ScrumboardService.Todo todo)
        {
            if (HasPendingChanges(todo.StoryId, todo.Modified))
            {
                throw new PendingChangeException();
            }
            Todo t = ServiceClient.TodoRemove(todo.Id);
            // Make sure all server side modifications are updated in our cache
            update();
        }

        private Boolean HasPendingChanges(int storyId, DateTime modified)
        {
            int sprintId = Config.ActiveSprint;
            Boolean hasPendingChanges = false;
            ScrumboardService.Story[] stories = ServiceClient.StoryGetSprintModifiedStories(sprintId, lastRefresh);
            if (stories != null && stories.Length > 0)
            {
                foreach (Story s in stories)
                {
                    if (s.Id == storyId && s.Modified > modified)
                    {
                        hasPendingChanges = true;
                    }
                }
            }
            update(sprintId, stories);
            return hasPendingChanges;

        }

        /// <summary>
        /// Add/update/delete these stories in our cache and notify our listeners for each story change
        /// </summary>
        /// <param name="stories"></param>
        private void update(int sprintId, Story[] stories)
        {
            SortedList<int, Story> cachedStories = null;
            if (cachedSprintStories.ContainsKey(sprintId))
            {
                cachedStories = cachedSprintStories[sprintId];
            }
            if (cachedStories == null)
            {
                cachedStories = new SortedList<int, Story>();
            }
            foreach (Story s in stories)
            {
                if (s.SprintId == sprintId)
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
                    // Notify listeners
                    if (StoryChanged != null)
                    {
                        StoryChanged(s);
                    }
                }
            }
            cachedSprintStories[sprintId] = cachedStories;
        }



        private SortedList<int, Story> getStories()
        {
            // Make sure all server side modifications are updated in our cache!
            update();
            return cachedSprintStories[Config.ActiveSprint];
        }


    }
}
