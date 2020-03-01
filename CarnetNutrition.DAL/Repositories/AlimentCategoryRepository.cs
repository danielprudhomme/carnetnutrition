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
    public class AlimentCategoryRepository : BaseRepository, IAlimentCategoryRepository
    {
        public AlimentCategoryRepository(IConfiguration configuration) : base(configuration)
        {
        } 

        public async Task<IEnumerable<AlimentCategory>> GetAll()
        {
            return await Query<AlimentCategory>("GetAllAlimentCategory");
        }

        public async Task Insert(AlimentCategory entity)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", entity.Id);
            queryParameters.Add("@Label", entity.Label);

            await Execute("InsertAlimentCategory", queryParameters);
        }
    }
}
