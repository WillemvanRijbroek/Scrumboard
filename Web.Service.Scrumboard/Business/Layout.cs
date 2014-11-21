using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;

namespace ScrumboardWebService.Business
{
    public class Layout : BO
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public String Name { get; set; }
        public int TotalColumns { get; set; }
        public int TotalRows { get; set; }
        public int FontSize { get; set; }
        public int StoryWidth { get; set; }
        public int StoryHeight { get; set; }

        public List<Layout> Select()
        {
            List<Layout> layouts = new List<Layout>();
            String sql = String.Format("SELECT Id, Name, TotalColumns, TotalRows, FontSize, StoryWidth, StoryHeight FROM Layout");

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
                    Layout layout = new Layout();

                    layout.Id = rdr.GetInt32(0);
                    layout.Name = rdr.GetString(1);
                    layout.TotalColumns = rdr.GetInt32(2);
                    layout.TotalRows = rdr.GetInt32(3);
                    layout.FontSize = rdr.GetInt32(4);
                    layout.StoryWidth = rdr.GetInt32(5);
                    layout.StoryHeight = rdr.GetInt32(6);
                    layouts.Add(layout);
                }
                rdr.Close();
            }
            catch { throw; }
            finally
            {
                CloseConnection();
            }
            return layouts;
        }

        public Layout Get(int layoutId)
        {
            Layout layout = null;

            String sql = String.Format("SELECT Id, Name, TotalColumns, TotalRows, FontSize, StoryWidth, StoryHeight FROM Layout WHERE Id = {0}", layoutId);
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
                    layout = new Layout();

                    layout.Id = rdr.GetInt32(0);
                    layout.Name = rdr.GetString(1);
                    layout.TotalColumns = rdr.GetInt32(2);
                    layout.TotalRows = rdr.GetInt32(3);
                    layout.FontSize = rdr.GetInt32(4);
                    layout.StoryWidth = rdr.GetInt32(5);
                    layout.StoryHeight = rdr.GetInt32(6);

                }
                rdr.Close();
            }
            catch { throw; }
            finally
            {
                CloseConnection();
            }
            return layout;
        }

        public int Insert(String name, int totalColumns, int totalRows, int fontSize, int storyWidth, int storyHeight)
        {
            String sql = "";

            sql = String.Format("INSERT INTO Layout (Name, TotalColumns, TotalRows, FontSize, StoryWidth, StoryHeight) VALUES ('{0}', {1}, {2}, {3}, {4}, {5})",
                asSQLStringValue(name), totalColumns, totalRows, fontSize, storyWidth, storyHeight);

            Object rt = executeInsert(sql);
            int newId = -1;
            if (rt != null && Int32.TryParse(rt.ToString(), out newId))
                return newId;

            return -1;
        }

        public void Update(int id, String name, int totalColumns, int totalRows, int fontSize, int storyWidth, int storyHeight)
        {
            String sql = String.Format("UPDATE Layout SET name = '{1}', TotalColumns = {2}, TotalRows = {3}, FontSize = {4}, StoryWidth = {5}, StoryHeight = {6} WHERE id = {0}", id, asSQLStringValue(name), totalColumns, totalRows, fontSize, storyWidth, storyHeight);
            executeScalar(sql);
        }
    }
}