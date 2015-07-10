using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;

namespace ScrumboardWebService.Business
{
    public class Todo : BO
    {
        public int Id { get; set; }
        public int StoryId { get; set; }
        public String Description { get; set; }
        public int Estimate { get; set; }
        public int BackColor { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public int Insert(int storyId, String description, int estimate, int backcolor, int x, int y)
        {
            String sql = String.Format("INSERT INTO Todo (StoryID, Description, Estimate, Backcolor, X, Y) VALUES ({0}, '{1}',{2},{3},{4},{5})",
                storyId, asSQLStringValue(description), estimate, backcolor, x, y);
            Object rt = executeInsert(sql);
            int newId = -1;
            if (rt != null && Int32.TryParse(rt.ToString(), out newId))
            {
                return newId;
            }
            return -1;
        }

        public void Update(int id, int storyId, String description, int estimate, int backcolor, int x, int y)
        {
            String sql = String.Format("UPDATE Todo SET storyId = {1}, description = '{2}', estimate = {3}, backcolor = {4}, x = {5}, y = {6} WHERE id = {0}", id, storyId, asSQLStringValue(description), estimate, backcolor, x, y);
            executeScalar(sql);
        }

        public void Remove(int id)
        {
            String sql = String.Format("UPDATE Todo SET removed = 1 WHERE id = {0}", id);
            executeScalar(sql);
        }

        public List<Todo> Select(int storyId)
        {
            List<Todo> lst = new List<Todo>();
            String sql = String.Format("SELECT id, storyid, description, estimate, backcolor, x, y FROM TODO WHERE storyid = {0} and removed <> 1", storyId);
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
                    Todo s = new Todo();

                    s.Id = rdr.GetInt32(0);
                    s.StoryId = rdr.GetInt32(1);
                    s.Description = HttpUtility.HtmlDecode(rdr.GetString(2));
                    s.Estimate = rdr.GetInt32(3);
                    s.BackColor = rdr.GetInt32(4);
                    s.X = rdr.GetInt32(5);
                    s.Y = rdr.GetInt32(6);
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

        public Todo Get(int id)
        {
            Todo s = null;
            String sql = String.Format("SELECT id, storyid, description, estimate, backcolor, x, y FROM TODO WHERE id = {0} and removed <> 1", id);
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
                    s = new Todo();

                    s.Id = rdr.GetInt32(0);
                    s.StoryId = rdr.GetInt32(1);
                    s.Description = HttpUtility.HtmlDecode(rdr.GetString(2));
                    s.Estimate = rdr.GetInt32(3);
                    s.BackColor = rdr.GetInt32(4);
                    s.X = rdr.GetInt32(5);
                    s.Y = rdr.GetInt32(6);
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

    }
}