using CarnetNutrition.Core.Models;
using System.IO;
using System.Threading.Tasks;

namespace CarnetNutrition.Core.Contracts.Services
{
    public interface IImportCiqualExcelService
    {
        Task<ImportResult> Import(MemoryStream stream);
    }
}
