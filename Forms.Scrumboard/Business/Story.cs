using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using ScrumBoard.Common;

namespace ScrumBoard.Business
{
    public class Story : ScrumboardService.Story
    {
        ScrumboardService.ScrumboardSoapClient client = ServiceConn.getClient();

        public const String PLANNED = "Planned";
        public const String UNPLANNED = "Unplanned";
        public const String BONUS = "Bonus";

        public Story(String externalId, int sprintId, int storyTypeId, String description, int estimate, int statusId, int backcolor, int x, int y, String tag)
        {
            Id = -1;
            ExternalId = externalId;
            SprintId = sprintId;
            StoryTypeId = storyTypeId;
            Description = description;
            Estimate = estimate;
            StatusId = statusId;
            X = x;
            Y = y;
            BackColor = backcolor;
            Tag = tag;
        }

        public Story(int id, String externalId, int sprintId, int storyTypeId, String description, int estimate, int statusId, int backcolor, int x, int y, String tag)
        {
            Id = id;
            ExternalId = externalId;
            SprintId = sprintId;
            StoryTypeId = storyTypeId;
            Description = description;
            Estimate = estimate;
            StatusId = statusId;
            X = x;
            Y = y;
            BackColor = backcolor;
            Tag = tag;
        }

        public Boolean IsPlanned
        {
            get
            {
                return StoryTypeId == 1;
            }
        }
        public Boolean IsBonus
        {
            get
            {
                return StoryTypeId == 3;
            }
        }

        public void Save()
        {
            if (!Config.ViewOnly)
            {
                if (Id == -1)
                {
                    Id = client.StoryInsert(SprintId, ExternalId, StoryTypeId, StatusId, Description.Replace("'", "''"), Estimate, BackColor, X, Y, Tag.Replace("'", "''"));
                }
                else
                {
                    //(int id, int sprintId, string externalId, int storyTypeId, int statusId, string description, int estimate, int backcolor, int x, int y, string tag
                    client.StoryUpdateDetails(Id, SprintId, ExternalId, StoryTypeId, StatusId, Description.Replace("'", "''"), Estimate, BackColor, X, Y, Tag.Replace("'", "''"));
                }
            }
        }

        public bool Remove()
        {
            if (!Config.ViewOnly)
            {
                if (Id != -1)
                {
                    client.StoryRemove(Id);
                    return true;
                }
            }
            return false;
        }

        public static Story Get(String externalId)
        {
            Story st = null;
            ScrumboardService.ScrumboardSoapClient client = ServiceConn.getClient();
            ScrumboardService.Story s = client.StoryGetByExternalId(Config.ActiveSprint, externalId);
            if (s != null)
            {
                st = new Story(s.Id, s.ExternalId, s.SprintId, s.StoryTypeId, s.Description, s.Estimate, s.StatusId, s.BackColor, s.X, s.Y, s.Tag);
            }
            return st;
        }
        public static SortedList<int, Story> getStories()
        {
            ScrumboardService.ScrumboardSoapClient client = ServiceConn.getClient();
            ScrumboardService.Story[] stories = client.StoryGetSprintStories(Config.ActiveSprint);
            SortedList<int, Story> list = new SortedList<int, Story>();
            foreach (ScrumboardService.Story s in stories)
            {
                list.Add(s.Id, new Story(s.Id, s.ExternalId, s.SprintId, s.StoryTypeId, s.Description, s.Estimate, s.StatusId, s.BackColor, s.X, s.Y, s.Tag));
            }
            return list;
        }

        public static SortedList<int, Story> getStories(int storyTypeId, int statusId)
        {
            ScrumboardService.ScrumboardSoapClient client = ServiceConn.getClient();
            ScrumboardService.Story[] stories = client.StoryGetPanelStories(Config.ActiveSprint, storyTypeId, statusId);
            SortedList<int, Story> list = new SortedList<int, Story>();
            foreach (ScrumboardService.Story s in stories)
            {
                list.Add(s.Id, new Story(s.Id, s.ExternalId, s.SprintId, s.StoryTypeId, s.Description, s.Estimate, s.StatusId, s.BackColor, s.X, s.Y, s.Tag));
            }
            
            return list;
        }
        
        public ScrumBoard.ScrumboardService.Todo[] getTodos()
        {
            return client.TodoSelect(this.Id);
        }
    }
}
