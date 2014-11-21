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
                    item.Name = rdr.GetString(1);
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

            String sql = String.Format("SELECT Id, Name, DefaultBackColor, BurnDownEnabled  FROM StoryType WHERE Id = {0})", id);
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
                    item.Name = rdr.GetString(1);
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

        //public int Insert(int teamId, String name, int totalColumns, int totalRows, int fontSize, int storyWidth, int storyHeight)
        //{
        //    String sql = "";
        //    if (teamId >= 0)
        //    {
        //        sql = String.Format("INSERT INTO Layout (TeamId, Name, TotalColumns, TotalRows, FontSize, StoryWidth, StoryHeight) VALUES ({6}, '{0}', {1}, {2}, {3}, {4}, {5})",
        //            asSQLStringValue(name), totalColumns, totalRows, fontSize, storyWidth, storyHeight, teamId);
        //    }
        //    else
        //    {
        //        sql = String.Format("INSERT INTO Layout (Name, TotalColumns, TotalRows, FontSize, StoryWidth, StoryHeight) VALUES ('{0}', {1}, {2}, {3}, {4}, {5})",
        //            asSQLStringValue(name), totalColumns, totalRows, fontSize, storyWidth, storyHeight);
        //    }
        //    Object rt = executeInsert(sql);
        //    int newId = -1;
        //    if (rt != null && Int32.TryParse(rt.ToString(), out newId))
        //        return newId;

        //    return -1;
        //}

        //public void Update(int id, String name, int totalColumns, int totalRows, int fontSize, int storyWidth, int storyHeight)
        //{
        //    String sql = String.Format("UPDATE Layout SET name = '{1}', TotalColumns = {2}, TotalRows = {3}, FontSize = {4}, StoryWidth = {5}, StoryHeight = {6} WHERE id = {0}", id, asSQLStringValue(name), totalColumns, totalRows, fontSize, storyWidth, storyHeight);
        //    executeScalar(sql);
        //}

    }

}