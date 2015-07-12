using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;

namespace ScrumboardWebService.Business
{
    public class Story : BO
    {
        public int Id { get; set; }
        public String ExternalId { get; set; }
        public int SprintId { get; set; }
        public int StoryTypeId { get; set; }
        public String Description { get; set; }
        public int Estimate { get; set; }
        public int StatusId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int BackColor { get; set; }
        public String Tag { get; set; }
        public Boolean IsRemoved { get; set; }
        public DateTime ClosedDate { get; set; }
        public DateTime Modified { get; set; }
        public Boolean IsBurndownEnabled { get; set; }


        public int Insert(int sprintId, String externalId, int storyTypeId, int statusId, String description, int estimate, int backcolor, int x, int y, String tag)
        {
            String sql = String.Format("INSERT INTO Story (sprintid, externalid, storytypeid, statusid, description, estimate, backcolor, x, y, tag, modified) VALUES ({0}, '{1}',{2},{3},'{4}',{5},{6},{7},{8},'{9}', GetDate())",
                sprintId, asSQLStringValue(externalId), storyTypeId, statusId, asSQLStringValue(description), estimate, backcolor, x, y, asSQLStringValue(tag));
            Object rt = executeInsert(sql);
            int newId = -1;
            if (rt != null && Int32.TryParse(rt.ToString(), out newId))
            {
                InsertStateTransition(newId, statusId);
                return newId;
            }
            return -1;
        }

        public void Update(int id, int sprintId, String externalId, int storyTypeId, int statusId, String description, int estimate, int backcolor, int x, int y, String tag)
        {
            String sql = String.Format("UPDATE Story SET statusid = {1}, externalid = '{2}', storyTypeId = {3}, description = '{4}', estimate = {5}, backcolor = {6}, x = {7}, y = {8}, tag = '{9}', modified = GetDate() WHERE id = {0}", id, statusId, asSQLStringValue(externalId), storyTypeId, asSQLStringValue(description), estimate, backcolor, x, y, asSQLStringValue(tag));
            InsertStateTransition(id, statusId);
            executeScalar(sql);
        }

        public void Update(int id, int statusId)
        {
            String sql = String.Format("UPDATE Story SET statusid = {1}, modified = GetDate() WHERE id = {0}", id, statusId);
            InsertStateTransition(id, statusId);
            executeScalar(sql);
        }

        public void MarkModified(int id)
        {
            String sql = String.Format("UPDATE Story SET modified = GetDate() WHERE id = {0}", id);
            executeScalar(sql);
        }

        public void Remove(int id)
        {
            String sql = String.Format("UPDATE Story SET removed = 1, modified = GetDate() WHERE id = {0}", id);
            executeScalar(sql);
        }

        public List<Story> Select(int sprintId)
        {
            return Select(sprintId, DateTime.MinValue);
        }
        public List<Story> Select(int sprintId, DateTime modifiedSince)
        {
            List<Story> lst = new List<Story>();

            String sql = String.Format("SELECT id,sprintid, externalid, storytypeid, statusid, description, estimate, backcolor, x, y, tag, " +
                "StateHistory.TransitionDate AS ClosedDate, " +
                "(select 1 from StoryType st WHERE BurnDownEnabled = 1 and ID = StoryTypeId) as IsBurndownEnabled, Modified, Removed " +
                "FROM STORY LEFT OUTER JOIN StateHistory on StateHistory.StoryId = Story.ID " +
                "and StateHistory.StateId = Story.StatusID " +
                "and TransitionDate = (SELECT MAX(TransitionDate) FROM StateHistory WHERE StateHistory.StoryId = Story.ID and StateHistory.StateId = Story.StatusID) " +
                "and StatusID in (SELECT ID from State where IsFinal=1) " +
                "WHERE sprintid = {0}", sprintId);
            if (modifiedSince > DateTime.MinValue)
            {
                sql += " and modified >= '" + asSQLDateValue(modifiedSince)+"'";
            }
            else
            {
                sql += " and removed <> 1";
            }
            OpenConnection();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandTimeout = conn.ConnectionTimeout;
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Story s = new Story();

                    s.Id = rdr.GetInt32(0);
                    s.SprintId = rdr.GetInt32(1);
                    s.ExternalId = fromSQLStringValue(rdr.GetString(2));
                    s.StoryTypeId = rdr.GetInt32(3);
                    s.StatusId = rdr.GetInt32(4);
                    s.Description = fromSQLStringValue(rdr.GetString(5));
                    s.Estimate = rdr.GetInt32(6);
                    s.BackColor = rdr.GetInt32(7);
                    s.X = rdr.GetInt32(8);
                    s.Y = rdr.GetInt32(9);
                    s.Tag = fromSQLStringValue(rdr.GetString(10));
                    if (!rdr.IsDBNull(11))
                        s.ClosedDate = rdr.GetDateTime(11);
                    //if (!rdr.IsDBNull(12))
                    //    s.IsBurndownEnabled = rdr.GetBoolean(12);
                    if (!rdr.IsDBNull(12))
                    {
                        int v = rdr.GetInt32(12);
                        s.IsBurndownEnabled = (v == 1);
                    }
                    if (!rdr.IsDBNull(13))
                    {
                        s.Modified = rdr.GetDateTime(13);
                    }
                    if (!rdr.IsDBNull(14))
                    {
                        String v = rdr.GetString(14);
                        s.IsRemoved = (v != "0");
                    }
                    lst.Add(s);
                }
                rdr.Close();
            }
            catch { throw; }
            finally
            {
                CloseConnection();
            }
            return lst;
        }

        public Story Get(int sprintId, String externalId)
        {
            Story s = null;
            String sql = String.Format("SELECT id,sprintid, externalid, storytypeid, statusid, description, estimate, backcolor, x, y, tag, StateHistory.TransitionDate AS ClosedDate, " +
                "(select 1 from StoryType st WHERE BurnDownEnabled = 1 and ID = StoryTypeId) as IsBurndownEnabled, Modified " +
                "FROM STORY LEFT OUTER JOIN StateHistory on StateHistory.StoryId = Story.ID " +
                "and StateHistory.StateId = Story.StatusID " +
                "and TransitionDate = (SELECT MAX(TransitionDate) FROM StateHistory WHERE StateHistory.StoryId = Story.ID and StateHistory.StateId = Story.StatusID) " +
                "and StatusID in (SELECT ID from State where IsFinal=1) " +
                "WHERE sprintid = {0} and ExternalId = '{1}' and removed <> 1", sprintId, asSQLStringValue(externalId));
            OpenConnection();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandTimeout = conn.ConnectionTimeout;
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    s = new Story();

                    s.Id = rdr.GetInt32(0);
                    s.SprintId = rdr.GetInt32(1);
                    s.ExternalId = fromSQLStringValue(rdr.GetString(2));
                    s.StoryTypeId = rdr.GetInt32(3);
                    s.StatusId = rdr.GetInt32(4);
                    s.Description = fromSQLStringValue(rdr.GetString(5));
                    s.Estimate = rdr.GetInt32(6);
                    s.BackColor = rdr.GetInt32(7);
                    s.X = rdr.GetInt32(8);
                    s.Y = rdr.GetInt32(9);
                    s.Tag = fromSQLStringValue(rdr.GetString(10));
                    if (!rdr.IsDBNull(11))
                        s.ClosedDate = rdr.GetDateTime(11);
                    //if (!rdr.IsDBNull(12))
                    //    s.IsBurndownEnabled = rdr.GetBoolean(12);
                    if (!rdr.IsDBNull(12))
                    {
                        int v = rdr.GetInt32(12);
                        s.IsBurndownEnabled = (v == 1);
                    }
                    if (!rdr.IsDBNull(13))
                    {
                        s.Modified = rdr.GetDateTime(13);
                    }
                }
                rdr.Close();
            }
            catch { throw; }
            finally
            {
                CloseConnection();
            }
            return s;
        }

        public List<Story> Select(int sprintId, int storyTypeId, int statusId)
        {
            List<Story> lst = new List<Story>();
            String sql = String.Format("SELECT id,sprintid, externalid, storytypeid, statusid, description, estimate, backcolor, x, y, tag, StateHistory.TransitionDate AS ClosedDate," +
                "(select 1 from StoryType st WHERE BurnDownEnabled = 1 and ID = StoryTypeId) as IsBurndownEnabled, Modified " +
                "FROM STORY LEFT OUTER JOIN StateHistory on StateHistory.StoryId = Story.ID " +
                "and StateHistory.StateId = Story.StatusID " +
                "and TransitionDate = (SELECT MAX(TransitionDate) FROM StateHistory WHERE StateHistory.StoryId = Story.ID and StateHistory.StateId = Story.StatusID) " +
                "and StatusID in (SELECT ID from State where IsFinal=1) " +
                "WHERE sprintid = {0} and statusid = {1} and storytypeid = {2} and removed <> 1", sprintId, statusId, storyTypeId);
            OpenConnection();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandTimeout = conn.ConnectionTimeout;
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Story s = new Story();

                    s.Id = rdr.GetInt32(0);
                    s.SprintId = rdr.GetInt32(1);
                    s.ExternalId = fromSQLStringValue(rdr.GetString(2));
                    s.StoryTypeId = rdr.GetInt32(3);
                    s.StatusId = rdr.GetInt32(4);
                    s.Description = fromSQLStringValue(rdr.GetString(5));
                    s.Estimate = rdr.GetInt32(6);
                    s.BackColor = rdr.GetInt32(7);
                    s.X = rdr.GetInt32(8);
                    s.Y = rdr.GetInt32(9);
                    s.Tag = fromSQLStringValue(rdr.GetString(10));
                    if (!rdr.IsDBNull(11))
                        s.ClosedDate = rdr.GetDateTime(11);
                    if (!rdr.IsDBNull(12))
                    {
                        int v = rdr.GetInt32(12);
                        s.IsBurndownEnabled = (v == 1);
                    }
                    if (!rdr.IsDBNull(13))
                    {
                        s.Modified = rdr.GetDateTime(13);
                    }
                    lst.Add(s);
                }
                rdr.Close();
            }
            catch { throw; }
            finally
            {
                CloseConnection();
            }
            return lst;
        }

        #region State history
        public void InsertStateTransition(int storyId, int statusId)
        {
            String sql = String.Format("if (not exists (select StateId from StateHistory where StateId = {1} and TransitionDate = (select MAX(transitiondate) FROM StateHistory where StoryId={0}))) INSERT INTO StateHistory (storyId, stateid, TransitionDate) VALUES ({0}, {1}, GetDate())",
                storyId, statusId);
            executeInsert(sql);
        }
        #endregion
    }
}