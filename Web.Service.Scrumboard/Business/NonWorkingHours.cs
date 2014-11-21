using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;

namespace ScrumboardWebService.Business
{
    public class NonWorkingHours : BO
    {
        public int Id { get; set; }
        public int TeamMemberId { get; set; }
        public DateTime Day { get; set; }
        public Decimal Hours { get; set; }

        public List<NonWorkingHours> Select(int teamMemberId)
        {
            List<NonWorkingHours> hours = new List<NonWorkingHours>();
            String sql = String.Format("SELECT ID, TeamMemberId, Day, Hours FROM NonWorkingHours WHERE (TeamMemberId = {0}) ORDER BY Day", teamMemberId);

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
                    NonWorkingHours nwh = new NonWorkingHours();

                    nwh.Id = rdr.GetInt32(0);
                    nwh.TeamMemberId = rdr.GetInt32(1);
                    nwh.Day = rdr.GetDateTime(2);
                    nwh.Hours = rdr.GetDecimal(3);

                    hours.Add(nwh);
                }
                rdr.Close();
            }
            catch { throw; }
            finally
            {
                CloseConnection();
            }
            return hours;
        }

        public List<NonWorkingHours> Select(int teamMemberId, DateTime day)
        {
            List<NonWorkingHours> hours = new List<NonWorkingHours>();
            String sql = String.Format("SELECT ID, TeamMemberId, Day, Hours FROM NonWorkingHours WHERE (TeamMemberId = {0} and Day = '{1}') ORDER BY Day", teamMemberId, asSQLDateValue(day));

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
                    NonWorkingHours nwh = new NonWorkingHours();

                    nwh.Id = rdr.GetInt32(0);
                    nwh.TeamMemberId = rdr.GetInt32(1);
                    nwh.Day = rdr.GetDateTime(2);
                    nwh.Hours = rdr.GetDecimal(3);

                    hours.Add(nwh);
                }
                rdr.Close();
            }
            catch { throw; }
            finally
            {
                CloseConnection();
            }
            return hours;
        }

        public int Insert(int teamMemberId, DateTime day, Decimal hours)
        {
            String sql = String.Format("INSERT INTO NonWorkingHours (TeamMemberId, Day, Hours) VALUES ({0}, '{1}', {2})",
                teamMemberId, asSQLDateValue(day), hours);
            Object rt = executeInsert(sql);
            int newId = -1;
            if (rt != null && Int32.TryParse(rt.ToString(), out newId))
            {
                return newId;
            }
            return -1;
        }

        public void Update(int id, int teamMemberId, DateTime day, Decimal hours)
        {
            String sql = String.Format("UPDATE NonWorkingHours SET TeamMemberId = {1}, Day = '{2}', Hours = {3} WHERE id = {0}"
                , id, teamMemberId, asSQLDateValue(day), hours);
            executeScalar(sql);
        }

        public void Remove(int id)
        {
            String sql = String.Format("DELETE FROM NonWorkingHours WHERE ID = {0}", id);
            executeScalar(sql);
        }
    }
}