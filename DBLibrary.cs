using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Coursework
{
    class DBLibrary
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source = LAPTOP-U6ADBGE\SQLEXPRESS; Initial Catalog = Library; Integrated Security = True"); 
       
        public void OpenConnection()
        {
            if(sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }
        public string GetConnectionString()
        {
            string connectionString = @"Data Source = LAPTOP-U6ADBGE\\SQLEXPRESS; Initial Catalog = Library; Integrated Security = True\";
            return connectionString;
        }
        public void CloseConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        public SqlConnection getConnection()
        {
            return sqlConnection;
        }
    }
}
