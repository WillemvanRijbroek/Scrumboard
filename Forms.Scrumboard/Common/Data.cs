using ScrumBoard.ScrumboardService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrumBoard.Common
{

    public class Data
    {
        public event ScrumBoard.Common.StoryCache.StoryChangedEventHandler StoryChanged;

        private static Data data = new Data();


        public static Data getInstance()
        {
            return data;
        }


        private DateTime lastRefresh = DateTime.MinValue;
        private StoryCache storyCache;
        private State[] cacheStates = null;
        private StoryType[] cacheStoryTypes = null;
        ScrumboardSoapClient client = ServiceConn.getClient();

        private Data()
        {
            storyCache = new StoryCache();
            storyCache.StoryChanged += storyCache_StoryChanged;
        }

        void storyCache_StoryChanged(Story story)
        {
            if (StoryChanged != null)
            {
                StoryChanged(story);
            }
        }

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
            storyCache.clear();
            cacheStates = null;
            cacheStoryTypes = null;
        }

        public void refreshStories()
        {
            storyCache.update();
        }

        public void insertStory(int sprintId, string externalId, int storyTypeId, int statusId, string description, int estimate, int backcolor, int x, int y, string tag)
        {
            storyCache.insertStory(sprintId, externalId, storyTypeId, statusId, description, estimate, backcolor, x, y, tag);
        }

        public SortedList<int, Story> getSprintStories()
        {
            return storyCache.getSprintStories();
        }

        public void updateStory(Story story)
        {
            storyCache.updateStory(story);
        }

        public void removeStory(Story story)
        {
            storyCache.removeStory(story);
        }

        public void insertTodo(Todo todo)
        {
            storyCache.insertTodo(todo);
        }

        public void updateTodo(ScrumBoard.ScrumboardService.Todo todo)
        {
            storyCache.updateTodo(todo);
        }

        public void removeTodo(ScrumBoard.ScrumboardService.Todo todo)
        {
            storyCache.removeTodo(todo);
        }
    }
}
