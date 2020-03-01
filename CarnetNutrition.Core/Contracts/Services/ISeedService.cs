using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarnetNutrition.Core.Contracts.Services
{
    public interface ISeedService
    {
        Task SeedNutritionalInfoType();
    }
}
