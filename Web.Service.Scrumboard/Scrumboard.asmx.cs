using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using ScrumboardWebService.Business;
using System.Threading;
using System.Web.Security;
using System.Globalization;

namespace ScrumboardWebService
{
    /// <summary>
    /// Summary description for Scrumboard
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Scrumboard : System.Web.Services.WebService
    {
        #region Story
        [WebMethod]
        public Story StoryInsert(int sprintId, String externalId, int storyTypeId, int statusId, String description, int estimate, int backcolor, int x, int y, String tag)
        {
            return new Story().Insert(sprintId, externalId, storyTypeId, statusId, description, estimate, backcolor, x, y, tag);
        }
        [WebMethod]
        public Story StoryUpdateDetails(int id, int sprintId, String externalId, int storyTypeId, int statusId, String description, int estimate, int backcolor, int x, int y, String tag)
        {
            return new Story().Update(id, sprintId, externalId, storyTypeId, statusId, description, estimate, backcolor, x, y, tag);
        }

        [WebMethod]
        public Story StoryUpdateStatus(int id, int statusId)
        {
            return new Story().Update(id, statusId);
        }

        [WebMethod]
        public Story StoryRemove(int id)
        {
            return new Story().Remove(id);
        }

        [WebMethod]
        public Story StoryGetByExternalId(int sprintId, String externalId)
        {
            return new Story().Get(sprintId, externalId);
        }

        [WebMethod]
        public List<Story> StoryGetSprintStories(int sprintId)
        {
            return new Story().Select(sprintId);
        }

        /// <summary>
        /// Retrieves a list of sprint stories that are changed after the modified date
        /// </summary>
        /// <param name="sprintId"></param>
        /// <param name="modified"></param>
        /// <returns></returns>
        [WebMethod]
        public List<Story> StoryGetSprintModifiedStories(int sprintId, DateTime modified)
        {
            return new Story().Select(sprintId, modified);
        }

        [WebMethod]
        public List<Story> StoryGetPanelStories(int sprintId, int storyTypeId, int statusId)
        {
            return new Story().Select(sprintId, storyTypeId, statusId);
        }
        #endregion

        #region Sprint
        [WebMethod]
        public int SprintInsert(int layoutId, int teamId, String name, DateTime startDate, DateTime target, int velocity, int focusFactor)
        {
            return new Sprint().Insert(layoutId, teamId, name, startDate, target, velocity, focusFactor);
        }
        [WebMethod]
        public void SprintUpdate(int id, int layoutId, int teamId, String name, DateTime startDate, DateTime target, int velocity, int focusFactor)
        {
            new Sprint().Update(id, layoutId, teamId, name, startDate, target, velocity, focusFactor);
        }
        [WebMethod]
        public Sprint SprintGet(int sprintId)
        {
            return new Sprint().Get(sprintId);
        }

        [WebMethod]
        public List<Sprint> SprintSelectAll()
        {
            //TeamMember user = new TeamMember().Get(Thread.CurrentPrincipal.Identity.Name);
            //if (user != null)
            //{
            //     return new Sprint().Select(user.TeamId);
            // }
            //  else
            // {
            return new Sprint().Select();
            // }
        }
        [WebMethod]
        public List<Sprint> SprintSelectByTeam(int teamId)
        {
            return new Sprint().Select(teamId);
        }
        #endregion

        #region State
        [WebMethod]
        public List<State> StateSelectAll()
        {
            return new State().Select();
        }
        [WebMethod]
        public State StateGet(int id)
        {
            return new State().Get(id);
        }
        [WebMethod]
        public int StateInsert(String name, Boolean isInitial, Boolean isFinal)
        {
            return new State().Insert(name, isInitial, isFinal);
        }
        [WebMethod]
        public void StateUpdate(int id, String name, Boolean isInitial, Boolean isFinal)
        {
            new State().Update(id, name, isInitial, isFinal);
        }
        #endregion

        #region Story type
        [WebMethod]
        public List<StoryType> StoryTypeSelectAll()
        {
            return new StoryType().Select();
        }
        [WebMethod]
        public StoryType StoryTypeGet(int id)
        {
            return new StoryType().Get(id);
        }
        [WebMethod]
        public int StoryTypeInsert(String name, int defaultBackColor, Boolean isBurnDownEnabled)
        {
            return new StoryType().Insert(name, defaultBackColor, isBurnDownEnabled);
        }
        [WebMethod]
        public void StoryTypeUpdate(int id, String name, int defaultBackColor, Boolean isBurnDownEnabled)
        {
            new StoryType().Update(id, name, defaultBackColor, isBurnDownEnabled);
        }
        #endregion

        #region Layout
        [WebMethod]
        public List<Layout> LayoutSelectAll()
        {
            return new Layout().Select();
        }

        [WebMethod]
        public int LayoutInsert(String name, int totalColumns, int totalRows, int fontSize, int storyWidth, int storyHeight)
        {
            return new Layout().Insert(name, totalColumns, totalRows, fontSize, storyWidth, storyHeight);
        }

        [WebMethod]
        public void LayoutUpdate(int id, String name, int totalColumns, int totalRows, int fontSize, int storyWidth, int storyHeight)
        {
            new Layout().Update(id, name, totalColumns, totalRows, fontSize, storyWidth, storyHeight);
        }

        [WebMethod]
        public Layout LayoutGet(int id)
        {
            return new Layout().Get(id);
        }
        #endregion

        #region Layout Panels
        [WebMethod]
        public List<Panel> LayoutPanelSelectByLayout(int layoutId)
        {
            return new Panel().Select(layoutId);
        }

        [WebMethod]
        public Panel LayoutPanelGet(int id)
        {
            return new Panel().Get(id);
        }

        [WebMethod]
        public int LayoutPanelInsert(int layoutId, int stateId, String title, int storyTypeId, int column, int row, int height, int width)
        {
            return new Panel().Insert(layoutId, stateId, title, storyTypeId, column, row, height, width);
        }
        [WebMethod]
        public void LayoutPanelUpdate(int id, int stateId, String title, int storyTypeId, int column, int row, int height, int width)
        {
            new Panel().Update(id, stateId, title, storyTypeId, column, row, height, width);
        }
        [WebMethod]
        public void LayoutPanelRemove(int id)
        {
            new Panel().Remove(id);
        }
        #endregion

        #region Team
        [WebMethod]
        public List<Team> TeamSelectAll()
        {
            return new Team().Select();
        }
        [WebMethod]
        public Team TeamGet(int id)
        {
            return new Team().Get(id);
        }
        [WebMethod]
        public int TeamInsert(String name)
        {
            return new Team().Insert(name);
        }
        [WebMethod]
        public void TeamUpdate(int id, String name)
        {
            new Team().Update(id, name);
        }
        [WebMethod]
        public void TeamRemove(int id)
        {
            new Team().Remove(id);
        }
        #endregion

        #region Todo's
        [WebMethod]
        public Todo TodoInsert(int storyId, String description, int estimate, int backcolor, int x, int y)
        {
            return new Todo().Insert(storyId, description, estimate, backcolor, x, y);
        }
        [WebMethod]
        public Todo TodoUpdate(int id, int storyId, String description, int estimate, int backcolor, int x, int y)
        {
            return new Todo().Update(id, storyId, description, estimate, backcolor, x, y);
        }
        [WebMethod]
        public Todo TodoRemove(int id)
        {
            return new Todo().Remove(id);
        }
        [WebMethod]
        public Todo TodoGet(int id)
        {
            return new Todo().Get(id);
        }
        [WebMethod]
        public List<Todo> TodoSelect(int storyId)
        {
            return new Todo().Select(storyId);
        }
        #endregion

    }
}
