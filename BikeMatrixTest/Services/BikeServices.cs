using BikeMatrixModels;
using BikeMatrixTest.Exceptions;
using BikeMatrixTest.Interfaces;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace BikeMatrixTest.Services
{
    public class BikeServices : IBikeServices
    {
       private readonly string _connectionString;
        private readonly ILogger _logger;
        private readonly ISqlConnectionFactory _connectionFactory;
        public BikeServices(IConfiguration configuration,ILogger<BikeServices> Logger, ISqlConnectionFactory sqlConnectionFactory)
        {
            _logger = Logger;
            _connectionString = configuration.GetConnectionString("LocalDbConnection");
            _connectionFactory = sqlConnectionFactory;
        }

        /// <summary>
        /// Creates a Bike in the dAtabase
        /// </summary>
        /// <param name="bike"></param>
        /// <returns></returns>
        public async Task createBikeAsync(Bikes bike)
        {
            _logger.LogTrace($"Starting Method {nameof(createBikeAsync)}");
            if (!bike.ValidateObject(out var bikeResponse))
            {
                throw new BikeMatirxValidationExceptions() { Errors = bikeResponse };
            }
            using (var connection = _connectionFactory.GetSqlConnection(_connectionString))
            {
                await connection.ExecuteScalarAsync<int>("CreateBike", new
                {
                    EmailAddress = bike.EmailAddress,
                    Brand = bike.Brand,
                    Model = bike.Model,
                    YearOfManufactor = bike.YearOfManufactor
                }, commandType: CommandType.StoredProcedure);
                _logger.LogTrace($"Ending Method {nameof(createBikeAsync)}");
                return;
            }
        }
        /// <summary>
        /// Gets a Bike Record by its BikeID
        /// </summary>
        /// <param name="BikeId"></param>
        /// <returns></returns>
        public async Task<Bikes> GetBikeAsync(int BikeId)
        {
            _logger.LogTrace($"Starting Method {nameof(GetBikeAsync)}");
            using (var connection = _connectionFactory.GetSqlConnection(_connectionString))
            {
                return (await connection.QueryAsync<Bikes>("GetBike", new { BikeID = BikeId }, commandType: CommandType.StoredProcedure)).FirstOrDefault();
            }
        }

        public async Task<Bikes> UpdateBikeAsync(Bikes newBike)
        {

            _logger.LogTrace($"Starting Method {nameof(UpdateBikeAsync)}");
            if (!newBike.ValidateObject(out var bikeResponse))
            {
                throw new BikeMatirxValidationExceptions() { Errors = bikeResponse};
            }

            using (var connection = _connectionFactory.GetSqlConnection(_connectionString))
            {
                await connection.ExecuteScalarAsync<int>("UpdateBike", new
                {
                    EmailAddress = newBike.EmailAddress,
                    Brand = newBike.Brand,
                    Model = newBike.Model,
                    YearOfManufactor = newBike.YearOfManufactor
                }, commandType: CommandType.StoredProcedure);
            }
            _logger.LogTrace($"Ending Method {nameof(UpdateBikeAsync)}");
            return await GetBikeAsync(newBike.id);
        }
        /// <summary>
        /// Deletes the Record with the Id
        /// </summary>
        /// <param name="bikes"></param>
        /// <returns></returns>
        public async Task<bool> DeleteBikeAsync(int bikesID)
        {
            _logger.LogTrace($"Starting Method {nameof(DeleteBikeAsync)}");
            using (var connection = _connectionFactory.GetSqlConnection(_connectionString))
            {
                var result = await connection.ExecuteScalarAsync<int>(
                    "DeleteBike",
                    new { Id = bikesID },
                    commandType: CommandType.StoredProcedure
                );
                _logger.LogTrace($"Ending Method {nameof(DeleteBikeAsync)}");
                if ( result == 1)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task<IEnumerable<Bikes>> getAllBikesAsync()
        {
            _logger.LogTrace($"Starting Method {nameof(getAllBikesAsync)}");
            using (var connection = _connectionFactory.GetSqlConnection(_connectionString))
            {
                return await connection.QueryAsync<Bikes>("GetAllBikes", commandType: CommandType.StoredProcedure);
            }
        }
    }
}
