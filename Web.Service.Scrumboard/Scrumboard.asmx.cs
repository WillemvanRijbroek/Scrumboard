using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using ScrumboardWebService.Business;
using System.Threading;
using System.Web.Security;
using System.Globalization;
using System.IO;
using ScrumboardWebService.Common;

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
        /// <summary>
        /// Add a new story
        /// </summary>
        /// <param name="sprintId"></param>
        /// <param name="externalId"></param>
        /// <param name="storyTypeId"></param>
        /// <param name="statusId"></param>
        /// <param name="description"></param>
        /// <param name="estimate"></param>
        /// <param name="backcolor"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        [WebMethod]
        public Story StoryInsert(int sprintId, String externalId, int storyTypeId, int statusId, String description, decimal estimate, int backcolor, int x, int y, String tag)
        {
            try
            {
                return new Story().Insert(sprintId, externalId, storyTypeId, statusId, description, estimate, backcolor, x, y, tag);
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }

        /// <summary>
        /// Update a story
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sprintId"></param>
        /// <param name="externalId"></param>
        /// <param name="storyTypeId"></param>
        /// <param name="statusId"></param>
        /// <param name="description"></param>
        /// <param name="estimate"></param>
        /// <param name="backcolor"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        [WebMethod]
        public Story StoryUpdateDetails(int id, int sprintId, String externalId, int storyTypeId, int statusId, String description, decimal estimate, int backcolor, int x, int y, String tag)
        {
            try
            {
                return new Story().Update(id, sprintId, externalId, storyTypeId, statusId, description, estimate, backcolor, x, y, tag);
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }

        [WebMethod]
        public Story StoryRemove(int id)
        {
            try
            {
                return new Story().Remove(id);
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }

        /// <summary>
        /// Retrieves a list of all sprint stories
        /// </summary>
        /// <param name="sprintId"></param>
        /// <returns></returns>
        [WebMethod]
        public List<Story> StoryGetSprintStories(int sprintId)
        {
            try
            {
                return new Story().Select(sprintId);
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
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
            try
            {
                return new Story().Select(sprintId, modified);
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }

        #endregion

        #region Sprint
        [WebMethod]
        public int SprintInsert(int layoutId, int teamId, String name, DateTime startDate, DateTime target, decimal velocity, int focusFactor)
        {
            try
            {
                return new Sprint().Insert(layoutId, teamId, name, startDate, target, velocity, focusFactor);
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }
        [WebMethod]
        public void SprintUpdate(int id, int layoutId, int teamId, String name, DateTime startDate, DateTime target, decimal velocity, int focusFactor)
        {
            try
            {
                new Sprint().Update(id, layoutId, teamId, name, startDate, target, velocity, focusFactor);
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }
        [WebMethod]
        public Sprint SprintGet(int sprintId)
        {
            try
            {
                return new Sprint().Get(sprintId);
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }

        [WebMethod]
        public List<Sprint> SprintSelectAll()
        {
            try
            {
                return new Sprint().Select();
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }
        [WebMethod]
        public List<Sprint> SprintSelectByTeam(int teamId)
        {
            try
            {
                return new Sprint().Select(teamId);
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }
        #endregion

        #region State
        [WebMethod]
        public List<State> StateSelectAll()
        {
            try
            {
                return new State().Select();
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }
        [WebMethod]
        public State StateGet(int id)
        {
            try
            {
                return new State().Get(id);
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }
        [WebMethod]
        public int StateInsert(String name, Boolean isInitial, Boolean isFinal)
        {
            try
            {
                return new State().Insert(name, isInitial, isFinal);
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }
        [WebMethod]
        public void StateUpdate(int id, String name, Boolean isInitial, Boolean isFinal)
        {
            try
            {
                new State().Update(id, name, isInitial, isFinal);
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }
        #endregion

        #region Story type
        [WebMethod]
        public List<StoryType> StoryTypeSelectAll()
        {
            try
            {
                return new StoryType().Select();
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }
        [WebMethod]
        public StoryType StoryTypeGet(int id)
        {
            try
            {
                return new StoryType().Get(id);
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }
        [WebMethod]
        public int StoryTypeInsert(String name, int defaultBackColor, Boolean isBurnDownEnabled)
        {
            try
            {
                return new StoryType().Insert(name, defaultBackColor, isBurnDownEnabled);
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }
        [WebMethod]
        public void StoryTypeUpdate(int id, String name, int defaultBackColor, Boolean isBurnDownEnabled)
        {
            try
            {
                new StoryType().Update(id, name, defaultBackColor, isBurnDownEnabled);
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }
        #endregion

        #region Layout
        [WebMethod]
        public List<Layout> LayoutSelectAll()
        {
            try
            {
                return new Layout().Select();
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }

        [WebMethod]
        public int LayoutInsert(String name, int totalColumns, int totalRows, int fontSize, int storyWidth, int storyHeight)
        {
            try
            {
                return new Layout().Insert(name, totalColumns, totalRows, fontSize, storyWidth, storyHeight);
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }

        [WebMethod]
        public void LayoutUpdate(int id, String name, int totalColumns, int totalRows, int fontSize, int storyWidth, int storyHeight)
        {
            try
            {
                new Layout().Update(id, name, totalColumns, totalRows, fontSize, storyWidth, storyHeight);
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }

        [WebMethod]
        public Layout LayoutGet(int id)
        {
            try
            {
                return new Layout().Get(id);
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }
        #endregion

        #region Layout Panels
        [WebMethod]
        public List<Panel> LayoutPanelSelectByLayout(int layoutId)
        {
            try
            {
                return new Panel().Select(layoutId);
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }

        [WebMethod]
        public Panel LayoutPanelGet(int id)
        {
            try
            {
                return new Panel().Get(id);
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }

        [WebMethod]
        public int LayoutPanelInsert(int layoutId, int stateId, String title, int storyTypeId, int column, int row, int height, int width)
        {
            try
            {
                return new Panel().Insert(layoutId, stateId, title, storyTypeId, column, row, height, width);
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }
        [WebMethod]
        public void LayoutPanelUpdate(int id, int stateId, String title, int storyTypeId, int column, int row, int height, int width)
        {
            try
            {
                new Panel().Update(id, stateId, title, storyTypeId, column, row, height, width);
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }
        [WebMethod]
        public void LayoutPanelRemove(int id)
        {
            try
            {
                new Panel().Remove(id);
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }
        #endregion

        #region Team
        [WebMethod]
        public List<Team> TeamSelectAll()
        {
            try
            {
                return new Team().Select();
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }
        [WebMethod]
        public Team TeamGet(int id)
        {
            try
            {
                return new Team().Get(id);
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }
        [WebMethod]
        public int TeamInsert(String name)
        {
            try
            {
                return new Team().Insert(name);
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }
        [WebMethod]
        public void TeamUpdate(int id, String name)
        {
            try
            {
                new Team().Update(id, name);
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }
        [WebMethod]
        public void TeamRemove(int id)
        {
            try
            {
                new Team().Remove(id);
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }
        #endregion

        #region Todo's
        [WebMethod]
        public Todo TodoInsert(int storyId, String description, decimal estimate, int backcolor, int x, int y)
        {
            try
            {
                return new Todo().Insert(storyId, description, estimate, backcolor, x, y);
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }
        [WebMethod]
        public Todo TodoUpdate(int id, int storyId, String description, decimal estimate, int backcolor, int x, int y)
        {
            try
            {
                return new Todo().Update(id, storyId, description, estimate, backcolor, x, y);
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }
        [WebMethod]
        public Todo TodoRemove(int id)
        {
            try
            {
                return new Todo().Remove(id);
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }
        [WebMethod]
        public Todo TodoGet(int id)
        {
            try
            {
                return new Todo().Get(id);
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }
        [WebMethod]
        public List<Todo> TodoSelect(int storyId)
        {
            try
            {
                return new Todo().Select(storyId);
            }
            catch (Exception ex)
            {
                Log.logException(ex);
                throw new System.Web.HttpException(500, "Internal error");
            }
        }
        #endregion

        
    }
}
