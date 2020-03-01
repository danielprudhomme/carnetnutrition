using CarnetNutrition.Core.Contracts.Repositories;
using CarnetNutrition.Core.Enums;
using CarnetNutrition.Core.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarnetNutrition.DAL.Repositories
{
    public class NutritionalValueRepository : BaseRepository, INutritionalValueRepository
    {
        public NutritionalValueRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task Insert(Aliment aliment, NutritionalInfoType type, decimal value)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@AlimentId", aliment.Id);
            queryParameters.Add("@NutritionalInfoId", type);
            queryParameters.Add("@Value", value);

            await Execute("InsertNutritionalValue", queryParameters);
        }
    }
}
