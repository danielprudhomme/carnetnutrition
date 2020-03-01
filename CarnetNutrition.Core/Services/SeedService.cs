using CarnetNutrition.Core.Contracts.Services;
using CarnetNutrition.Core.Enums;
using CarnetNutrition.Core.Models;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CarnetNutrition.Core.Services
{
    public class SeedService : ISeedService
    {
        private readonly IConfiguration _configuration;
        public SeedService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SeedNutritionalInfoType()
        {
            var infos = new List<NutritionalInfo>
            {
                new NutritionalInfo { Id = NutritionalInfoType.Water, Unit = Unit.Gram },

                new NutritionalInfo { Id = NutritionalInfoType.EnergyKCal, Unit = Unit.KCal },
                new NutritionalInfo { Id = NutritionalInfoType.EnergyKJ, Unit = Unit.KJ },

                new NutritionalInfo { Id = NutritionalInfoType.Protein, Unit = Unit.Gram },

                new NutritionalInfo { Id = NutritionalInfoType.Carbohydrate, Unit = Unit.Gram },
                new NutritionalInfo
                {
                    Id = NutritionalInfoType.Sugar,
                    Unit = Unit.Gram,
                    ParentId = NutritionalInfoType.Carbohydrate
                },
                new NutritionalInfo
                {
                    Id = NutritionalInfoType.Starch,
                    Unit = Unit.Gram,
                    ParentId = NutritionalInfoType.Carbohydrate
                },

                new NutritionalInfo { Id = NutritionalInfoType.AlimentaryFiber, Unit = Unit.Gram },

                new NutritionalInfo { Id = NutritionalInfoType.Lipid, Unit = Unit.Gram },
                new NutritionalInfo
                {
                    Id = NutritionalInfoType.SaturatedFattyAcid,
                    Unit = Unit.Gram,
                    ParentId = NutritionalInfoType.Lipid
                },
                new NutritionalInfo
                {
                    Id = NutritionalInfoType.PolyUnsaturatedFattyAcid,
                    Unit = Unit.Gram,
                    ParentId = NutritionalInfoType.Lipid
                },
                new NutritionalInfo
                {
                    Id = NutritionalInfoType.MonoUnsaturatedFattyAcid,
                    Unit = Unit.Gram,
                    ParentId = NutritionalInfoType.Lipid
                },
                new NutritionalInfo
                {
                    Id = NutritionalInfoType.Cholesterol,
                    Unit = Unit.Gram,
                    ParentId = NutritionalInfoType.Lipid
                },

                new NutritionalInfo { Id = NutritionalInfoType.Alcohol, Unit = Unit.Gram },
                new NutritionalInfo { Id = NutritionalInfoType.Ashes, Unit = Unit.Gram },
                new NutritionalInfo { Id = NutritionalInfoType.Salt, Unit = Unit.Gram },

                new NutritionalInfo { Id = NutritionalInfoType.Minerals, Unit = Unit.None },
                new NutritionalInfo
                {
                    Id = NutritionalInfoType.Calcium,
                    Unit = Unit.Milligram,
                    ParentId = NutritionalInfoType.Minerals
                },
                new NutritionalInfo
                {
                    Id = NutritionalInfoType.Chlorure,
                    Unit = Unit.Milligram,
                    ParentId = NutritionalInfoType.Minerals
                },
                new NutritionalInfo
                {
                    Id = NutritionalInfoType.Copper,
                    Unit = Unit.Milligram,
                    ParentId = NutritionalInfoType.Minerals
                },
                new NutritionalInfo
                {
                    Id = NutritionalInfoType.Iron,
                    Unit = Unit.Milligram,
                    ParentId = NutritionalInfoType.Minerals
                },
                new NutritionalInfo
                {
                    Id = NutritionalInfoType.Iodine,
                    Unit = Unit.Milligram,
                    ParentId = NutritionalInfoType.Minerals
                },
                new NutritionalInfo
                {
                    Id = NutritionalInfoType.Magnesium,
                    Unit = Unit.Milligram,
                    ParentId = NutritionalInfoType.Minerals
                },
                new NutritionalInfo
                {
                    Id = NutritionalInfoType.Manganese,
                    Unit = Unit.Milligram,
                    ParentId = NutritionalInfoType.Minerals
                },
                new NutritionalInfo
                {
                    Id = NutritionalInfoType.Phosphorus,
                    Unit = Unit.Milligram,
                    ParentId = NutritionalInfoType.Minerals
                },
                new NutritionalInfo
                {
                    Id = NutritionalInfoType.Potassium,
                    Unit = Unit.Milligram,
                    ParentId = NutritionalInfoType.Minerals
                },
                new NutritionalInfo
                {
                    Id = NutritionalInfoType.Selenium,
                    Unit = Unit.Microgram,
                    ParentId = NutritionalInfoType.Minerals
                },
                new NutritionalInfo
                {
                    Id = NutritionalInfoType.Sodium,
                    Unit = Unit.Milligram,
                    ParentId = NutritionalInfoType.Minerals
                },
                new NutritionalInfo
                {
                    Id = NutritionalInfoType.Zinc,
                    Unit = Unit.Milligram,
                    ParentId = NutritionalInfoType.Minerals
                },

                new NutritionalInfo { Id = NutritionalInfoType.Vitamins, Unit = Unit.None },
                new NutritionalInfo
                {
                    Id = NutritionalInfoType.Retinol,
                    Unit = Unit.Microgram,
                    ParentId = NutritionalInfoType.Vitamins
                },
                new NutritionalInfo
                {
                    Id = NutritionalInfoType.BetaCarotene,
                    Unit = Unit.Microgram,
                    ParentId = NutritionalInfoType.Vitamins
                },
                new NutritionalInfo
                {
                    Id = NutritionalInfoType.VitaminD,
                    Unit = Unit.Microgram,
                    ParentId = NutritionalInfoType.Vitamins
                },
                new NutritionalInfo
                {
                    Id = NutritionalInfoType.VitaminE,
                    Unit = Unit.Milligram,
                    ParentId = NutritionalInfoType.Vitamins
                },
                new NutritionalInfo
                {
                    Id = NutritionalInfoType.VitaminK1,
                    Unit = Unit.Microgram,
                    ParentId = NutritionalInfoType.Vitamins
                },
                new NutritionalInfo
                {
                    Id = NutritionalInfoType.VitaminC,
                    Unit = Unit.Milligram,
                    ParentId = NutritionalInfoType.Vitamins
                },
                new NutritionalInfo
                {
                    Id = NutritionalInfoType.VitaminB1,
                    Unit = Unit.Milligram,
                    ParentId = NutritionalInfoType.Vitamins
                },
                new NutritionalInfo
                {
                    Id = NutritionalInfoType.VitaminB2,
                    Unit = Unit.Milligram,
                    ParentId = NutritionalInfoType.Vitamins
                },
                new NutritionalInfo
                {
                    Id = NutritionalInfoType.VitaminB3,
                    Unit = Unit.Milligram,
                    ParentId = NutritionalInfoType.Vitamins
                },
                new NutritionalInfo
                {
                    Id = NutritionalInfoType.VitaminB5,
                    Unit = Unit.Milligram,
                    ParentId = NutritionalInfoType.Vitamins
                },
                new NutritionalInfo
                {
                    Id = NutritionalInfoType.VitaminB6,
                    Unit = Unit.Milligram,
                    ParentId = NutritionalInfoType.Vitamins
                },
                new NutritionalInfo
                {
                    Id = NutritionalInfoType.VitaminB9,
                    Unit = Unit.Microgram,
                    ParentId = NutritionalInfoType.Vitamins
                },
                new NutritionalInfo
                {
                    Id = NutritionalInfoType.VitaminB12,
                    Unit = Unit.Microgram,
                    ParentId = NutritionalInfoType.Vitamins
                }
            };

            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync("DELETE FROM NutritionalInfo");

                string sql = "INSERT INTO NutritionalInfo VALUES (@Id, @Unit, @ParentId)";
                foreach (var info in infos)
                {
                    await connection.ExecuteAsync(sql, new { Id = info.Id, Unit = info.Unit, ParentId = info.ParentId });
                }
            }
        }
    }
}
