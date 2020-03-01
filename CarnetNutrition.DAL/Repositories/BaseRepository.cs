using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace CarnetNutrition.DAL.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly IConfiguration _configuration;

        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected async Task<IEnumerable<T>> Query<T>(string storedProcedureName, DynamicParameters queryParameters = null)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return await connection.QueryAsync<T>(storedProcedureName, queryParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        protected async Task Execute(string storedProcedureName, DynamicParameters queryParameters = null)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(storedProcedureName, queryParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
