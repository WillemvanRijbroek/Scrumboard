using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Globalization;

namespace ScrumboardWebService.Business
{
    public class TeamMember : BO
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String UserName { get; set; }
        public Decimal FocusFactor { get; set; }
        public Decimal AvailabilityFactor { get; set; }
        public Decimal NormalWorkingHours { get; set; }

        public List<TeamMember> Select()
        {
            List<TeamMember> lst = new List<TeamMember>();
            String sql = "SELECT id, Name, UserName, FocusFactor, AvailabilityFactor, NormalWorkingHours FROM TEAMMEMBER";
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
                    TeamMember s = new TeamMember();

                    s.Id = rdr.GetInt32(0);
                    s.Name = rdr.GetString(1);
                    s.UserName = rdr.GetString(2);
                    if (!rdr.IsDBNull(3))
                        s.FocusFactor = rdr.GetDecimal(3);

                    if (!rdr.IsDBNull(4))
                        s.AvailabilityFactor = rdr.GetDecimal(4);
                    if (!rdr.IsDBNull(5))
                        s.NormalWorkingHours = rdr.GetDecimal(5);

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

        public List<TeamMember> Select(int teamId)
        {
            List<TeamMember> lst = new List<TeamMember>();
            String sql = String.Format("SELECT id, Name, UserName, FocusFactor, AvailabilityFactor, NormalWorkingHours FROM TEAMMEMBER WHERE id in (SELECT TeamMemberId FROM TEAMMEMBERS WHERE TeamId = {0})", teamId);
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
                    TeamMember s = new TeamMember();

                    s.Id = rdr.GetInt32(0);
                    s.Name = rdr.GetString(1);
                    s.UserName = rdr.GetString(2);
                    if (!rdr.IsDBNull(3))
                        s.FocusFactor = rdr.GetDecimal(3);
                    if (!rdr.IsDBNull(4))
                        s.AvailabilityFactor = rdr.GetDecimal(4);
                    if (!rdr.IsDBNull(5))
                        s.NormalWorkingHours = rdr.GetDecimal(5);

                    lst.Add(s);
                }
                rdr.Close();;
            }
            catch { throw; }
            finally
            {
                CloseConnection();
            }
            return lst;
        }
        public TeamMember Get(String userName)
        {
            TeamMember s = null;
            String sql = String.Format("SELECT id, Name, UserName, FocusFactor, AvailabilityFactor, NormalWorkingHours FROM TEAMMEMBER WHERE UserName = '{0}'", asSQLStringValue(userName));
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
                    s = new TeamMember();
                    s.Id = rdr.GetInt32(0);
                    s.Name = rdr.GetString(1);
                    s.UserName = rdr.GetString(2);
                    if (!rdr.IsDBNull(3))
                        s.FocusFactor = rdr.GetDecimal(3);
                    if (!rdr.IsDBNull(4))
                        s.AvailabilityFactor = rdr.GetDecimal(4);
                    if (!rdr.IsDBNull(5))
                        s.NormalWorkingHours = rdr.GetDecimal(5);
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

        public TeamMember Get(int id)
        {
            TeamMember s = null;
            String sql = String.Format("SELECT id, Name, UserName, FocusFactor, AvailabilityFactor, NormalWorkingHours FROM TEAMMEMBER WHERE ID = {0}", id);
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
                    s = new TeamMember();
                    s.Id = rdr.GetInt32(0);
                    s.Name = rdr.GetString(1);
                    s.UserName = rdr.GetString(2);
                    if (!rdr.IsDBNull(3))
                        s.FocusFactor = rdr.GetDecimal(3);
                    if (!rdr.IsDBNull(4))
                        s.AvailabilityFactor = rdr.GetDecimal(4);
                    if (!rdr.IsDBNull(5))
                        s.NormalWorkingHours = rdr.GetDecimal(5);
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



        public int Insert(String name, String userName, Decimal focusFactor, Decimal availabilityFactor, Decimal normalWorkingHours)
        {
            CultureInfo ci = new CultureInfo("en-US");
            String sql = String.Format("INSERT INTO TeamMember (name, userName, focusFactor, availabilityFactor, normalWorkingHours) VALUES ('{0}','{1}',{2},{3},{4})",
                asSQLStringValue(name), asSQLStringValue(userName), focusFactor.ToString(ci), availabilityFactor.ToString(ci), normalWorkingHours.ToString(ci));
            Object rt = executeInsert(sql);
            int newId = -1;
            if (rt != null && Int32.TryParse(rt.ToString(), out newId))
            {
                return newId;
            }
            return -1;
        }

        public void Update(int id, String name, String userName, Decimal focusFactor, Decimal availabilityFactor, Decimal normalWorkingHours)
        {
            CultureInfo ci = new CultureInfo("en-US");
            String sql = String.Format("UPDATE TeamMember SET name = '{1}', userName= '{2}', focusFactor = {3}, availabilityFactor = {4}, normalWorkingHours = {5} WHERE id = {0}"
                , id, asSQLStringValue(name), asSQLStringValue(userName), focusFactor.ToString(ci), availabilityFactor.ToString(ci), normalWorkingHours.ToString(ci));
            executeScalar(sql);
        }

        public void Remove(int id)
        {
            String sql = String.Format("DELETE FROM TeamMember WHERE ID = {0}", id);
            executeScalar(sql);
        }


    }
}