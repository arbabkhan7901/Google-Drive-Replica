using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.DAL
{
    internal class DBHelper : IDisposable
    {
        string connString = @"Data Source=HAIER-PC\SQLSERVER;Initial Catalog=Assignment8;Persist Security Info=True;User ID=sa;Password=12345";
        SqlConnection conn = null;

        public DBHelper()
        {
            conn = new SqlConnection(connString);
            conn.Open();
        }

        public int ExecuteQuery(String query)
        {
            int count = 0;
            try
            {
                SqlCommand command = new SqlCommand(query, conn);
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                count = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                conn.Dispose();
            }
            return count;
        }

        public Object ExecuteScalar(String query)
        {
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
            SqlCommand command = new SqlCommand(query, conn);
            return command.ExecuteScalar();
        }

        public SqlDataReader ExecuteReader(String query)
        {
            SqlCommand command = null;
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                command = new SqlCommand(query, conn);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return command.ExecuteReader();
        }

        public void Dispose()
        {
            if (conn != null && conn.State == System.Data.ConnectionState.Open)
                conn.Close();
        }
    }
}
