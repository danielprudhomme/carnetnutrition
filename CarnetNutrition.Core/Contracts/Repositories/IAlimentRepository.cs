using CarnetNutrition.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarnetNutrition.Core.Contracts.Repositories
{
    public interface IAlimentRepository
    {
        Task<IEnumerable<Aliment>> GetAll();
        Task Insert(Aliment entity);
        Task InsertCategoryToAliment(Guid alimentId, Guid categoryId);
    }
}
