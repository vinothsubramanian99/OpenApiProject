using System;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace OpenApiProject1.DBCon
{
    public class DBConnection : IDisposable
    {
       
        private readonly string _connectionString = "Server=10.1.15.40;Database=AdventureWorks2019;User Id=Traininguser;Password=Traininguser;Trusted_Connection=False;TrustServerCertificate=True;";
        private SqlConnection _connection;
        DataSet dt =new DataSet();
        public DBConnection()
        {
            _connection = new SqlConnection(_connectionString);
        }

        public DataSet GetConnection(string query)
        {
            if (_connection.State == System.Data.ConnectionState.Closed)
            {
                _connection.Open();
            }
            
            SqlCommand command=new SqlCommand(query,_connection);
            DataAdapter ad=new SqlDataAdapter(command);
            ad.Fill(dt);
            return dt;
        }

        public void CloseConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
            }
        }
    }
}
