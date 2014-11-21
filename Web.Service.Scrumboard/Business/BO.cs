using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using ScrumboardWebService.Common;

namespace ScrumboardWebService.Business
{
    public abstract class BO
    {
        protected SqlConnection conn;

        protected Boolean IsDatabaseEnabled
        {
            get { return !String.IsNullOrEmpty(Config.ConnectionString); }
        }

        protected Object executeScalar(String sql)
        {
            Object rt = null;
            OpenConnection();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandTimeout = conn.ConnectionTimeout;
                cmd.CommandType = System.Data.CommandType.Text;
                rt = cmd.ExecuteScalar();
            }
            catch { throw; }
            finally
            {
                CloseConnection();
            }
            return rt;
        }

        protected Object executeInsert(String sql)
        {
            Object rt = null;
            OpenConnection();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandTimeout = conn.ConnectionTimeout;
                cmd.CommandType = System.Data.CommandType.Text;
                rt = cmd.ExecuteScalar();
                cmd.CommandText = "SELECT SCOPE_IDENTITY()";
                rt = cmd.ExecuteScalar();
            }
            catch { throw; }
            finally
            {
                CloseConnection();
            }
            return rt;
        }

        protected SqlDataReader executeReader(String sql)
        {
            SqlDataReader rt = null;
            OpenConnection();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandTimeout = conn.ConnectionTimeout;
                cmd.CommandType = System.Data.CommandType.Text;
                rt = cmd.ExecuteReader();
            }
            catch { throw; }
            finally
            {
                CloseConnection();
            }
            return rt;
        }

        protected void OpenConnection()
        {
            if (conn == null)
            {
                conn = new SqlConnection(Config.ConnectionString);
            }
            conn.Open();
        }

        protected void CloseConnection()
        {
            if (conn != null)
            {
                conn.Close();
                conn = null;
            }
        }
        #region Helpers
        protected String asSQLStringValue(String userInput)
        {
            // prevend sql injection here
            return userInput;
        }
        protected String asSQLDateValue(DateTime userInput)
        {
            return userInput.ToString("yyyy-MM-dd HH:mm:ss");
        }
        #endregion
    }
}