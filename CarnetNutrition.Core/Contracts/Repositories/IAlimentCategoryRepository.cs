using CarnetNutrition.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarnetNutrition.Core.Contracts.Repositories
{
    public interface IAlimentCategoryRepository
    {
        Task<IEnumerable<AlimentCategory>> GetAll();
        Task Insert(AlimentCategory entity);
    }
}
