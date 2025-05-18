using System.Data;

namespace BikeMatrixTest.Interfaces
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetSqlConnection(string connectionString);
    }
}