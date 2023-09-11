using System.Data;
using System.Data.SqlClient;
namespace RonnieProjects.Storage
{
    public class SqlConnectionFactory
    {
        private readonly string _connectionString;

        public SqlConnectionFactory()
        {
            _connectionString = "server=MAROKO; DataBase=Users; Integrated Security=true; Encrypt=false";
        }

        public IDbConnection CreateConnection()
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            return dbConnection;
        }
    }
}