using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;

namespace ScrumboardWebService.Business
{
    public class Sprint : BO
    {
        public int Id { get; set; }
        public int LayoutId { get; set; }
        public int TeamId { get; set; }
        public String Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime TargetDate { get; set; }

        public int Insert(int layoutId, int teamId, String name, DateTime startDate, DateTime target)
        {
            String sql = String.Format("INSERT INTO Sprint (LayoutID, TeamId, Name, StartDate, Target) VALUES ({0}, {1}, '{2}','{3}','{4}')",
                layoutId, teamId, asSQLStringValue(name), asSQLDateValue(startDate), asSQLDateValue(target));
            Object rt = executeInsert(sql);
            int newId = -1;
            if (rt != null && Int32.TryParse(rt.ToString(), out newId))
                return newId;

            return -1;
        }

        public void Update(int id, int layoutId, int teamId, String name, DateTime startDate, DateTime target)
        {
            String sql = String.Format("UPDATE Sprint SET layoutId = {1}, name = '{2}', target = '{3}', teamid = {4}, StartDate = '{5}' WHERE id = {0}", id, layoutId, asSQLStringValue(name), asSQLDateValue(target), teamId, asSQLDateValue(startDate));
            executeScalar(sql);
        }

        public Sprint Get(int sprintId)
        {
            Sprint sprint = null;
            String sql = "SELECT id,layoutid,teamid,name,target,StartDate FROM SPRINT WHERE Id = " + sprintId;
            OpenConnection();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandTimeout = conn.ConnectionTimeout;
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    sprint = new Sprint();

                    sprint.Id = rdr.GetInt32(0);
                    sprint.LayoutId = rdr.GetInt32(1);
                    sprint.TeamId = rdr.GetInt32(2);
                    sprint.Name = rdr.GetString(3);
                    sprint.TargetDate = rdr.GetDateTime(4);
                    sprint.StartDate = rdr.GetDateTime(5);
                }
                rdr.Close();
            }
            catch { throw; }
            finally
            {
                CloseConnection();
            }
            return sprint;
        }

        public List<Sprint> Select(int teamId)
        {
            List<Sprint> lst = new List<Sprint>();
            String sql = "SELECT id,layoutid,teamid,name,target,startdate FROM SPRINT WHERE teamid = " + teamId;
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
                    Sprint s = new Sprint();

                    s.Id = rdr.GetInt32(0);
                    s.LayoutId = rdr.GetInt32(1);
                    s.TeamId = rdr.GetInt32(2);
                    s.Name = rdr.GetString(3);
                    s.TargetDate = rdr.GetDateTime(4);
                    s.StartDate = rdr.GetDateTime(5);
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

        public List<Sprint> Select()
        {
            List<Sprint> lst = new List<Sprint>();
            String sql = "SELECT id,layoutid,teamid,name,target,startdate FROM SPRINT";
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
                    Sprint s = new Sprint();
                    s.Id = rdr.GetInt32(0);
                    s.LayoutId = rdr.GetInt32(1);
                    s.TeamId = rdr.GetInt32(2);
                    s.Name = rdr.GetString(3);
                    s.TargetDate = rdr.GetDateTime(4);
                    s.StartDate = rdr.GetDateTime(5);
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
    }
}