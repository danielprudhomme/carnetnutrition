using CarnetNutrition.Core.Enums;
using CarnetNutrition.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarnetNutrition.Core.Contracts.Repositories
{
    public interface INutritionalValueRepository
    {
        Task Insert(Aliment aliment, NutritionalInfoType type, decimal value);
    }
}
