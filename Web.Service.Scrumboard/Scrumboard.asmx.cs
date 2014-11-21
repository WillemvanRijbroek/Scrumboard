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
        public int StoryInsert(int sprintId, String externalId, int storyTypeId, int statusId, String description, int estimate, int backcolor, int x, int y, String tag)
        {
            return new Story().Insert(sprintId, externalId, storyTypeId, statusId, description, estimate, backcolor, x, y, tag);
        }
        [WebMethod]
        public void StoryUpdateDetails(int id, int sprintId, String externalId, int storyTypeId, int statusId, String description, int estimate, int backcolor, int x, int y, String tag)
        {
            new Story().Update(id, sprintId, externalId, storyTypeId, statusId, description, estimate, backcolor, x, y, tag);
        }

        [WebMethod]
        public void StoryUpdateStatus(int id, int statusId)
        {
            new Story().Update(id, statusId);
        }

        [WebMethod]
        public void StoryRemove(int id)
        {
            new Story().Remove(id);
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

        [WebMethod]
        public List<Story> StoryGetPanelStories(int sprintId, int storyTypeId, int statusId)
        {
            return new Story().Select(sprintId, storyTypeId, statusId);
        }
        #endregion

        #region Sprint
        [WebMethod]
        public int SprintInsert(int layoutId, int teamId, String name, DateTime startDate, DateTime target)
        {
            return new Sprint().Insert(layoutId, teamId, name, startDate, target);
        }

        [WebMethod]
        public void SprintUpdate(int id, int layoutId, int teamId, String name, DateTime startDate, DateTime target)
        {
            new Sprint().Update(id, layoutId, teamId, name, startDate, target);
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
        [WebMethod]
        public void TeamAssignMember(int teamId, int teamMemberId)
        {
            new Team().AssignTeamMember(teamId, teamMemberId);
        }
        [WebMethod]
        public void TeamDeassignMember(int teamId, int teamMemberId)
        {
            new Team().DeassignTeamMember(teamId, teamMemberId);
        }
        #endregion

        #region TeamMember
        [WebMethod]
        public List<TeamMember> TeamMemberSelectAll()
        {
            return new TeamMember().Select();
        }
        [WebMethod]
        public List<TeamMember> TeamMemberSelectByTeam(int teamId)
        {
            return new TeamMember().Select(teamId);
        }

        [WebMethod]
        public TeamMember TeamMemberGet(int id)
        {
            return new TeamMember().Get(id);
        }
        [WebMethod]
        public String TeamMemberTestInsert(String name, String userName, String focusFactor, String availabilityFactor, String normalWorkingHours)
        {
            CultureInfo ci = new CultureInfo("en-US");
            return Decimal.Parse(focusFactor, ci).ToString(ci) + " " + availabilityFactor + " " + normalWorkingHours;
        }
        [WebMethod]
        public int TeamMemberInsert(String name, String userName, String focusFactor, String availabilityFactor, String normalWorkingHours)
        {
            CultureInfo ci =  new CultureInfo("en-US");
            return new TeamMember().Insert(name, userName, Decimal.Parse(focusFactor, ci), Decimal.Parse(availabilityFactor,ci), Decimal.Parse(normalWorkingHours, ci));
        }
        [WebMethod]
        public void TeamMemberUpdate(int id, String name, String userName, String focusFactor, String availabilityFactor, String normalWorkingHours)
        {
            CultureInfo ci = new CultureInfo("en-US");
            new TeamMember().Update(id, name, userName, Decimal.Parse(focusFactor, ci), Decimal.Parse(availabilityFactor, ci), Decimal.Parse(normalWorkingHours, ci));
        }
        [WebMethod]
        public void TeamMemberRemove(int id)
        {
            new TeamMember().Remove(id);
        }

        [WebMethod]
        public List<NonWorkingHours> TeamGetNonWorkingHours(int teamMemberId)
        {
            return new NonWorkingHours().Select(teamMemberId);
        }

        [WebMethod]
        public List<NonWorkingHours> TeamGetNonWorkingHoursAtDay(int teamMemberId, DateTime day)
        {
            return new NonWorkingHours().Select(teamMemberId, day);
        }

        [WebMethod]
        public int TeamInsertNonWorkingHours(int teamMemberId, DateTime day, String hours)
        {
            return new NonWorkingHours().Insert(teamMemberId, day, Decimal.Parse(hours));
        }

        [WebMethod]
        public void TeamUpdateNonWorkingHours(int id, int teamMemberId, DateTime day, String hours)
        {
            new NonWorkingHours().Update(id, teamMemberId, day, Decimal.Parse(hours));
        }
        [WebMethod]
        public void TeamRemoveNonWorkingHours(int id)
        {
            new NonWorkingHours().Remove(id);
        }
        #endregion

        #region Todo's
        [WebMethod]
        public int TodoInsert(int storyId, String description, int estimate, int backcolor, int x, int y)
        {
            return new Todo().Insert(storyId, description, estimate, backcolor, x, y);
        }
        [WebMethod]
        public void TodoUpdate(int id, int storyId, String description, int estimate, int backcolor, int x, int y)
        {
            new Todo().Update(id, storyId, description, estimate, backcolor, x, y);
        }
        [WebMethod]
        public void TodoRemove(int id)
        {
            new Todo().Remove(id);
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

        #region Authentication
        //[WebMethod]
        //public bool Login(string userName, string password)
        //{
        //    TeamMember user = new TeamMember().Get(userName);

        //    if (user != null)
        //    {
        //        // Issue the authentication key to the client
        //        FormsAuthentication.SetAuthCookie(userName, false);
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //[WebMethod]
        //public void LogOut()
        //{
        //    // Deprive client of the authentication key
        //    FormsAuthentication.SignOut();
        //}

        //[WebMethod]
        //public TeamMember CurrentTeamMember()
        //{
        //if (!string.IsNullOrEmpty(Thread.CurrentPrincipal.Identity.Name))
        //{
        //    return new TeamMember().Get(Thread.CurrentPrincipal.Identity.Name); ;
        //}
        //    return null;
        //}
        //private int teamId = -1;
        //private int CurrentTeamId
        //{
        //    get
        //    {
        //        //if (teamId == -1 && CurrentTeamMember() != null)
        //        //{
        //        //    teamId = CurrentTeamMember().TeamId;
        //        //}
        //        return teamId;
        //    }
        //}
        //[WebMethod]
        //public string WhoAmI()
        //{
        //    if (CurrentTeamMember() != null)
        //    {
        //        return CurrentTeamMember().Name;
        //    }
        //    return null;
        //}
        #endregion
    }
}
