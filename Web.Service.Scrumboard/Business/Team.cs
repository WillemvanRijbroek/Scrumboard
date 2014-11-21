using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;

namespace ScrumboardWebService.Business
{
    public class Team : BO
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public List<Team> Select()
        {
            List<Team> lst = new List<Team>();
            String sql = "SELECT id, name FROM TEAM";
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
                    Team s = new Team();

                    s.Id = rdr.GetInt32(0);
                    s.Name = rdr.GetString(1);

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

        public Team Get(int id)
        {
            Team s = null;
            String sql = String.Format("SELECT id, Name FROM TEAM WHERE ID = {0}", id);
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
                    s = new Team();
                    s.Id = rdr.GetInt32(0);
                    s.Name = rdr.GetString(1);
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



        public int Insert(String name)
        {
            String sql = String.Format("INSERT INTO Team (name) VALUES ('{0}')", asSQLStringValue(name));
            Object rt = executeInsert(sql);
            int newId = -1;
            if (rt != null && Int32.TryParse(rt.ToString(), out newId))
            {
                return newId;
            }
            return -1;
        }

        public void Update(int id, String name)
        {
            String sql = String.Format("UPDATE Team SET name = '{1}' WHERE id = {0}"
                , id, asSQLStringValue(name));
            executeScalar(sql);
        }

        public void Remove(int id)
        {
            String sql = String.Format("DELETE FROM Team WHERE ID = {0}", id);
            executeScalar(sql);
        }

        public void AssignTeamMember(int teamId, int teamMemberId)
        {
            String sql = String.Format("INSERT INTO TeamMembers (TeamId, TeamMemberId) VALUES ({0}, {1})", teamId, teamMemberId);
            executeInsert(sql);
        }

        public void DeassignTeamMember(int teamId, int teamMemberId)
        {
            String sql = String.Format("DELETE FROM TeamMembers WHERE TeamId={0} AND TeamMemberId={1}", teamId, teamMemberId);
            executeInsert(sql);
        }
    }
}