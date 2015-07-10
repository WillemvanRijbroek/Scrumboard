using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;

namespace ScrumboardWebService.Business
{
    public class Panel : BO
    {
        public int Id { get; set; }
        public int LayoutId { get; set; }
        public int StateId { get; set; }
        public String Title { get; set; }
        public int StoryTypeId { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
        public int Heigth { get; set; }
        public int Width { get; set; }
        public String StateName { get; set; }
        public String StoryTypeName { get; set; }

        public List<Panel> Select(int layoutId)
        {
            List<Panel> list = new List<Panel>();
            String sql = String.Format("SELECT p.ID, p.LayoutID, p.StateID, s.Name AS StateName, p.Title, p.StoryTypeID, t.Name AS StoryTypeName, p.[Column], p.Row, p.Height, p.Width FROM Panel p right join State s on s.ID = p.StateID right join StoryType t on t.ID = p.StoryTypeID WHERE p.layoutid = {0} order by p.stateid, p.storytypeid", layoutId);
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
                    Panel item = new Panel();

                    item.Id = rdr.GetInt32(0);
                    item.LayoutId = rdr.GetInt32(1);
                    item.StateId = rdr.GetInt32(2);
                    item.StateName = rdr.GetString(3);
                    item.Title = rdr.GetString(4);
                    item.StoryTypeId = rdr.GetInt32(5);
                    item.StoryTypeName = rdr.GetString(6);
                    item.Column = rdr.GetInt32(7);
                    item.Row = rdr.GetInt32(8);
                    item.Heigth = rdr.GetInt32(9);
                    item.Width = rdr.GetInt32(10);

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
        public Panel Get(int id)
        {
            Panel item = null;
            String sql = String.Format("SELECT p.ID, p.LayoutID, p.StateID, s.Name AS StateName, p.Title, p.StoryTypeID, t.Name AS StoryTypeName, p.[Column], p.Row, p.Height, p.Width FROM Panel p right join State s on s.ID = p.StateID right join StoryType t on t.ID = p.StoryTypeID WHERE p.id = {0}", id);
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
                    item = new Panel();

                    item.Id = rdr.GetInt32(0);
                    item.LayoutId = rdr.GetInt32(1);
                    item.StateId = rdr.GetInt32(2);
                    item.StateName = rdr.GetString(3);
                    item.Title = rdr.GetString(4);
                    item.StoryTypeId = rdr.GetInt32(5);
                    item.StoryTypeName = rdr.GetString(6);
                    item.Column = rdr.GetInt32(7);
                    item.Row = rdr.GetInt32(8);
                    item.Heigth = rdr.GetInt32(9);
                    item.Width = rdr.GetInt32(10);

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
        public int Insert(int layoutId, int stateId, String title, int storyTypeId, int column, int row, int height, int width)
        {
            String sql = String.Format("INSERT INTO Panel (LayoutID, stateId, title, storyTypeId, [column], row, height, width) VALUES ({0}, {1}, '{2}', {3}, {4}, {5}, {6}, {7})",
                layoutId, stateId, asSQLStringValue(title), storyTypeId, column, row, height, width);
            Object rt = executeInsert(sql);
            int newId = -1;
            if (rt != null && Int32.TryParse(rt.ToString(), out newId))
                return newId;

            return -1;
        }

        public void Update(int id, int stateId, String title, int storyTypeId, int column, int row, int height, int width)
        {
            String sql = String.Format("UPDATE Panel SET title = '{1}', storyTypeId = {2}, [column] = {3}, row = {4}, height = {5}, width = {6}, stateID={7} WHERE id = {0}"
                , id, asSQLStringValue(title), storyTypeId, column, row, height, width, stateId);
            executeScalar(sql);
        }

        public void Remove(int id)
        {
            String sql = String.Format("DELETE FROM Panel WHERE id = {0}", id);
            executeScalar(sql);
        }
    }
}