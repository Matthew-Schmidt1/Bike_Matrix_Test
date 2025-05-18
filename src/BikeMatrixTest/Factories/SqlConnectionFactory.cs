using BikeMatrixTest.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BikeMatrixTest.Factories
{
    public class SqlConnectionFactory : ISqlConnectionFactory
    {

        public IDbConnection GetSqlConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}
