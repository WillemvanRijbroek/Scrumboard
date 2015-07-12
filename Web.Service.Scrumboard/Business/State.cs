using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;

namespace ScrumboardWebService.Business
{
    public class State :BO
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public Boolean IsInitial { get; set; }
        public Boolean IsFinal { get; set; }

        public int Insert(String name, Boolean isInitial, Boolean isFinal)
        {
            String sql = String.Format("INSERT INTO State (Name, IsInitial, IsFinal) VALUES ('{0}', {1}, {2})",
                asSQLStringValue(name), isInitial ? "1" : "0", isFinal ? "1" : "0");
            Object rt = executeInsert(sql);
            int newId = -1;
            if (rt != null && Int32.TryParse(rt.ToString(), out newId))
                return newId;

            return -1;
        }

        public void Update(int id, String name, Boolean isInitial, Boolean isFinal)
        {
            String sql = String.Format("UPDATE State SET Name = '{1}', IsInitial = {2}, IsFinal = {3} WHERE id = {0}", id, asSQLStringValue(name), isInitial ? "1" : "0", isFinal ? "1" : "0");
            executeScalar(sql);
        }

        public List<State> Select()
        {
            List<State> list = new List<State>();
            String sql = "SELECT id,name,isinitial,isfinal FROM STATE";
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
                    State item = new State();

                    item.Id = rdr.GetInt32(0);
                    item.Name = fromSQLStringValue( rdr.GetString(1));
                    item.IsInitial = rdr.GetBoolean(2);
                    item.IsFinal = rdr.GetBoolean(3);

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

        public State Get(int id)
        {
            State item = null;

            String sql = String.Format("SELECT id,name,isinitial,isfinal FROM STATE WHERE Id = {0}", id);
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
                    item = new State();

                    item.Id = rdr.GetInt32(0);
                    item.Name = fromSQLStringValue(rdr.GetString(1));
                    item.IsInitial = rdr.GetBoolean(2);
                    item.IsFinal = rdr.GetBoolean(3);
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
    }
}