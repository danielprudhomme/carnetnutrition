using CarnetNutrition.Core.Contracts.Repositories;
using CarnetNutrition.Core.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarnetNutrition.DAL.Repositories
{
    public class AlimentRepository : BaseRepository, IAlimentRepository
    {
        public AlimentRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<Aliment>> GetAll()
        {
            return await Query<Aliment>("GetAllAliment");
        }

        public async Task Insert(Aliment entity)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", entity.Id);
            queryParameters.Add("@Label", entity.Label);

            await Execute("InsertAliment", queryParameters);
        }

        public async Task InsertCategoryToAliment(Guid alimentId, Guid categoryId)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@AlimentId", alimentId);
            queryParameters.Add("@AlimentCategoryId", categoryId);

            await Execute("InsertAlimentCategoryToAliment", queryParameters);
        }
    }
}
