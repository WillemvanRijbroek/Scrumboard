using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;

namespace ScrumboardWebService.Business
{
    public class StoryType : BO
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int DefaultBackColor { get; set; }
        public Boolean BurnDownEnabled { get; set; }

        public List<StoryType> Select()
        {
            List<StoryType> list = new List<StoryType>();
            String sql = String.Format("SELECT Id, Name, DefaultBackColor, BurnDownEnabled  FROM StoryType");

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
                    StoryType item = new StoryType();

                    item.Id = rdr.GetInt32(0);
                    item.Name = fromSQLStringValue(rdr.GetString(1));
                    item.DefaultBackColor = rdr.GetInt32(2);
                    item.BurnDownEnabled = rdr.GetBoolean(3);

                    list.Add(item);
                }
                rdr.Close();
            }
            catch { throw; }
            finally
            {
                CloseConnection();
            }
            return list;
        }

        public StoryType Get(int id)
        {
            StoryType item = null;

            String sql = String.Format("SELECT Id, Name, DefaultBackColor, BurnDownEnabled  FROM StoryType WHERE Id = {0}", id);
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
                    item = new StoryType();

                    item.Id = rdr.GetInt32(0);
                    item.Name = fromSQLStringValue(rdr.GetString(1));
                    item.DefaultBackColor = rdr.GetInt32(2);
                    item.BurnDownEnabled = rdr.GetBoolean(3);
                }
                rdr.Close();
            }
            catch { throw; }
            finally
            {
                CloseConnection();
            }
            return item;
        }

        public int Insert(String name, int defaultBackColor, Boolean isBurnDownEnabled)
        {
            String sql = "";
            sql = String.Format("INSERT INTO StoryType (Name, DefaultBackColor, BurnDownEnabled) VALUES ('{0}', {1}, {2})", asSQLStringValue(name), defaultBackColor, isBurnDownEnabled?"1":"0");
            Object rt = executeInsert(sql);
            int newId = -1;
            if (rt != null && Int32.TryParse(rt.ToString(), out newId))
                return newId;
            return -1;
        }

        public void Update(int id, String name, int defaultBackColor, Boolean isBurnDownEnabled)
        {
            String sql = String.Format("UPDATE StoryType SET name = '{1}', DefaultBackColor = {2}, BurnDownEnabled = {3} WHERE id = {0}",
                                        id, asSQLStringValue(name), defaultBackColor, isBurnDownEnabled ? "1" : "0");
            executeScalar(sql);
        }

    }

}