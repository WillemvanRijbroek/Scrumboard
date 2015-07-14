//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.IO;
//using System.Drawing;
//using ScrumBoard.Common;

//namespace ScrumBoard.Business
//{
//    public class Story : ScrumboardService.Story
//    {
//        private bool isDirty = false;
//        private bool isStatusDirty = false;

//        public const String PLANNED = "Planned";
//        public const String UNPLANNED = "Unplanned";
//        public const String BONUS = "Bonus";

//        public Story(String externalId, int sprintId, int storyTypeId, String description, int estimate, int statusId, int backcolor, int x, int y, String tag, DateTime modified)
//        {
//            Id = -1;
//            ExternalId = externalId;
//            SprintId = sprintId;
//            StoryTypeId = storyTypeId;
//            StoryTypeId = storyTypeId;
//            Description = description;
//            Estimate = estimate;
//            StatusId = statusId;
//            lastStatusId = statusId;
//            X = x;
//            Y = y;
//            BackColor = backcolor;
//            Tag = tag;
//            Modified = modified;
//            isDirty = false;
//            isStatusDirty = false;
//        }

//        public Story(int id, String externalId, int sprintId, int storyTypeId, String description, int estimate, int statusId, int backcolor, int x, int y, String tag, DateTime modified)
//        {
//            Id = id;
//            ExternalId = externalId;
//            SprintId = sprintId;
//            StoryTypeId = storyTypeId;
//            lastStoryTypeId = storyTypeId;
//            Description = description;
//            Estimate = estimate;
//            StatusId = statusId;
//            lastStatusId = statusId;
//            X = x;
//            Y = y;
//            BackColor = backcolor;
//            Tag = tag;
//            Modified = modified;
//            isDirty = false;
//            isStatusDirty = false;
//        }

//        private int lastStoryTypeId;
//        public new int StoryTypeId
//        {
//            get
//            {
//                return base.StoryTypeId;
//            }
//            set
//            {
//                if (base.StoryTypeId != value && !isDirty)
//                {
//                    lastStoryTypeId = StoryTypeId;
//                    isDirty = true;
//                }
//                base.StoryTypeId = value;

//            }
//        }
//        private int lastStatusId;
//        public new int StatusId
//        {
//            get
//            {
//                return base.StatusId;
//            }
//            set
//            {
//                if (base.StatusId != value && !isStatusDirty)
//                {
//                    lastStatusId = StatusId;
//                    isStatusDirty = true;
//                }
//                base.StatusId = value;
//            }
//        }
//        public int LastStoryTypeId
//        {
//            get
//            {
//                return lastStoryTypeId;
//            }
//        }
//        public int LastStatusId
//        {
//            get
//            {
//                return lastStatusId;
//            }
//        }
//        public Boolean IsPlanned
//        {
//            get
//            {
//                return StoryTypeId == 1;
//            }
//        }
//        public Boolean IsBonus
//        {
//            get
//            {
//                return StoryTypeId == 3;
//            }
//        }

//        public void Save()
//        {
//            if (!Config.ViewOnly)
//            {
//                if (Id == -1)
//                {
//                    ScrumboardService.Story s = Data.getInstance().StoryInsert(SprintId, ExternalId, StoryTypeId, StatusId, Description.Replace("'", "''"), Estimate, BackColor, X, Y, Tag.Replace("'", "''"));
//                    this.Modified = s.Modified;
//                    this.IsRemoved = s.IsRemoved;
//                }
//                else
//                {
//                    //(int id, int sprintId, string externalId, int storyTypeId, int statusId, string description, int estimate, int backcolor, int x, int y, string tag
//                    ScrumboardService.Story s = Data.getInstance().StoryUpdateDetails(this);
//                    this.Modified = s.Modified;
//                    this.IsRemoved = s.IsRemoved;
//                }
//                isDirty = false;
//                isStatusDirty = false;
//            }
//        }

//        public bool Remove()
//        {
//            if (!Config.ViewOnly)
//            {
//                if (Id != -1)
//                {
//                    Data.getInstance().StoryRemove(this);
//                    return true;
//                }
//            }
//            return false;
//        }

//        public static Story Get(String externalId)
//        {
//            return Data.getInstance().getStoryByExternalId(externalId);
//        }

//        public static SortedList<int, Story> getStories(int storyTypeId, int statusId)
//        {
//            return Data.getInstance().getStories(storyTypeId, statusId);
//        }

//        public ScrumBoard.ScrumboardService.Todo[] getTodos()
//        {
//            return Data.getInstance().getStoryTodos(this.Id);
//        }
//    }
//}
