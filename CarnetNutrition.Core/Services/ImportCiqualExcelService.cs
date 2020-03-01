using CarnetNutrition.Core.Contracts.Repositories;
using CarnetNutrition.Core.Contracts.Services;
using CarnetNutrition.Core.Enums;
using CarnetNutrition.Core.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarnetNutrition.Core.Services
{
    public class ImportCiqualExcelService : IImportCiqualExcelService
    {
        private readonly IAlimentCategoryRepository _alimentCategoryRepository;
        private readonly IAlimentRepository _alimentRepository;
        private readonly INutritionalValueRepository _nutritionalValueRepository;
        private readonly ISeedService _seedService;

        private const int AlimentCategory1CodeCol = 1;
        private const int AlimentCategory1LabelCol = 4;
        private const int AlimentCategory2LabelCol = 5;
        private const int AlimentCategory3LabelCol = 6;

        private const int AlimentLabelCol = 8;

        private const int EnergyKCalCol = 10;
        private const int EnergyKJCol = 11;
        private const int WaterCol = 13;
        private const int ProteinCol = 14;
        private const int CarbohydrateCol = 16;
        private const int LipidCol = 17;
        private const int SugarCol = 18;
        private const int StarchCol = 19;
        private const int AlimentaryFiberCol = 20;
        private const int AshesCol = 22;
        private const int AlcoholCol = 23;
        private const int SaturatedFattyAcidCol = 25;
        private const int MonoUnsaturatedFattyAcidCol = 26;
        private const int PolyUnsaturatedFattyAcidCol = 27;
        private const int CholesterolCol = 42;
        private const int SaltCol = 43;
        private const int CalciumCol = 44;
        private const int ChlorureCol = 45;
        private const int CopperCol = 46;
        private const int IronCol = 47;
        private const int IodineCol = 48;
        private const int MagnesiumCol = 49;
        private const int ManganeseCol = 50;
        private const int PhosphorusCol = 51;
        private const int PotassiumCol = 52;
        private const int SeleniumCol = 53;
        private const int SodiumCol = 54;
        private const int ZincCol = 55;
        private const int RetinolCol = 56;
        private const int BetaCaroteneCol = 57;
        private const int VitaminDCol = 58;
        private const int VitaminECol = 59;
        private const int VitaminK1Col = 60;
        private const int VitaminCCol = 62;
        private const int VitaminB1Col = 63;
        private const int VitaminB2Col = 64;
        private const int VitaminB3Col = 65;
        private const int VitaminB5Col = 66;
        private const int VitaminB6Col = 67;
        private const int VitaminB9Col = 68;
        private const int VitaminB12Col = 69;

        public ImportCiqualExcelService(
            IAlimentCategoryRepository alimentCategoryRepository,
            IAlimentRepository alimentRepository,
            INutritionalValueRepository nutritionalValueRepository,
            ISeedService seedService)
        {
            _alimentCategoryRepository = alimentCategoryRepository;
            _alimentRepository = alimentRepository;
            _nutritionalValueRepository = nutritionalValueRepository;
            _seedService = seedService;
        }

        private IList<AlimentCategory> Categories { get; set; }
        private IList<Aliment> Aliments { get; set; }

        private ExcelWorksheet Worksheet { get; set; }

        public async Task<ImportResult> Import(MemoryStream stream)
        {
            await _seedService.SeedNutritionalInfoType();

            using (ExcelPackage package = new ExcelPackage(stream))
            {
                Worksheet = package.Workbook.Worksheets.FirstOrDefault();

                var result = new ImportResult()
                {
                    Lines = 0,
                    InsertedLines = 0
                };

                if (Worksheet != null)
                {
                    int rowCount = Worksheet.Dimension.Rows;
                    result.Lines = rowCount - 1;

                    Categories = (await _alimentCategoryRepository.GetAll()).ToList();
                    Aliments = (await _alimentRepository.GetAll()).ToList();

                    for (int row = 2; row <= rowCount; row++)
                    {
                        if (await ProcessRow(row))
                        {
                            result.InsertedLines++;
                        }
                    }
                }

                return result;
            }
        }

        private async Task<bool> ProcessRow(int row)
        {
            string code = GetCellValue(row, AlimentCategory1CodeCol);
            var result = false;

            if (!string.IsNullOrEmpty(code))
            {
                IList<AlimentCategory> categories = new List<AlimentCategory>();
                await ProcessCategory(row, AlimentCategory1LabelCol, categories);
                await ProcessCategory(row, AlimentCategory2LabelCol, categories);
                await ProcessCategory(row, AlimentCategory3LabelCol, categories);

                result = await ProcessAliment(row, categories);
            }

            return result;
        }

        private async Task ProcessCategory(int row, int col, IList<AlimentCategory> categories)
        {
            string categoryLabel = GetCellValue(row, col);

            if (!string.IsNullOrEmpty(categoryLabel))
            {
                AlimentCategory category = Categories.FirstOrDefault(x => x.Label == categoryLabel);
                if (category == null)
                {
                    category = new AlimentCategory
                    {
                        Id = Guid.NewGuid(),
                        Label = categoryLabel
                    };

                    Categories.Add(category);
                    await _alimentCategoryRepository.Insert(category);
                }

                categories.Add(category);
            }
        }

        private async Task<bool> ProcessAliment(int row, IList<AlimentCategory> categories)
        {
            string alimentLabel = GetCellValue(row, AlimentLabelCol);
            var result = false;

            if (!string.IsNullOrEmpty(alimentLabel))
            {
                Aliment aliment = Aliments.FirstOrDefault(x => x.Label == alimentLabel);
                if (aliment == null)
                {
                    aliment = new Aliment
                    {
                        Id = Guid.NewGuid(),
                        Label = alimentLabel
                    };

                    await _alimentRepository.Insert(aliment);
                }

                foreach(var category in categories)
                {
                    await _alimentRepository.InsertCategoryToAliment(aliment.Id, category.Id);
                }

                await ProcessNutritionalValues(row, WaterCol, aliment, NutritionalInfoType.Water);
                await ProcessNutritionalValues(row, EnergyKCalCol, aliment, NutritionalInfoType.EnergyKCal);
                await ProcessNutritionalValues(row, EnergyKJCol, aliment, NutritionalInfoType.EnergyKJ);
                await ProcessNutritionalValues(row, ProteinCol, aliment, NutritionalInfoType.Protein);
                await ProcessNutritionalValues(row, CarbohydrateCol, aliment, NutritionalInfoType.Carbohydrate);
                await ProcessNutritionalValues(row, AlimentaryFiberCol, aliment, NutritionalInfoType.AlimentaryFiber);
                await ProcessNutritionalValues(row, SugarCol, aliment, NutritionalInfoType.Sugar);
                await ProcessNutritionalValues(row, StarchCol, aliment, NutritionalInfoType.Starch);
                await ProcessNutritionalValues(row, LipidCol, aliment, NutritionalInfoType.Lipid);
                await ProcessNutritionalValues(row, SaturatedFattyAcidCol, aliment, NutritionalInfoType.SaturatedFattyAcid);
                await ProcessNutritionalValues(row, PolyUnsaturatedFattyAcidCol, aliment, NutritionalInfoType.PolyUnsaturatedFattyAcid);
                await ProcessNutritionalValues(row, MonoUnsaturatedFattyAcidCol, aliment, NutritionalInfoType.MonoUnsaturatedFattyAcid);
                await ProcessNutritionalValues(row, CholesterolCol, aliment, NutritionalInfoType.Cholesterol);
                await ProcessNutritionalValues(row, AlcoholCol, aliment, NutritionalInfoType.Alcohol);
                await ProcessNutritionalValues(row, AshesCol, aliment, NutritionalInfoType.Ashes);
                await ProcessNutritionalValues(row, SaltCol, aliment, NutritionalInfoType.Salt);
                await ProcessNutritionalValues(row, CalciumCol, aliment, NutritionalInfoType.Calcium);
                await ProcessNutritionalValues(row, ChlorureCol, aliment, NutritionalInfoType.Chlorure);
                await ProcessNutritionalValues(row, CopperCol, aliment, NutritionalInfoType.Copper);
                await ProcessNutritionalValues(row, IronCol, aliment, NutritionalInfoType.Iron);
                await ProcessNutritionalValues(row, IodineCol, aliment, NutritionalInfoType.Iodine);
                await ProcessNutritionalValues(row, MagnesiumCol, aliment, NutritionalInfoType.Magnesium);
                await ProcessNutritionalValues(row, ManganeseCol, aliment, NutritionalInfoType.Manganese);
                await ProcessNutritionalValues(row, PhosphorusCol, aliment, NutritionalInfoType.Phosphorus);
                await ProcessNutritionalValues(row, PotassiumCol, aliment, NutritionalInfoType.Potassium);
                await ProcessNutritionalValues(row, SeleniumCol, aliment, NutritionalInfoType.Selenium);
                await ProcessNutritionalValues(row, SodiumCol, aliment, NutritionalInfoType.Sodium);
                await ProcessNutritionalValues(row, ZincCol, aliment, NutritionalInfoType.Zinc);
                await ProcessNutritionalValues(row, RetinolCol, aliment, NutritionalInfoType.Retinol);
                await ProcessNutritionalValues(row, BetaCaroteneCol, aliment, NutritionalInfoType.BetaCarotene);
                await ProcessNutritionalValues(row, VitaminDCol, aliment, NutritionalInfoType.VitaminD);
                await ProcessNutritionalValues(row, VitaminECol, aliment, NutritionalInfoType.VitaminE);
                await ProcessNutritionalValues(row, VitaminK1Col, aliment, NutritionalInfoType.VitaminK1);
                await ProcessNutritionalValues(row, VitaminCCol, aliment, NutritionalInfoType.VitaminC);
                await ProcessNutritionalValues(row, VitaminB1Col, aliment, NutritionalInfoType.VitaminB1);
                await ProcessNutritionalValues(row, VitaminB2Col, aliment, NutritionalInfoType.VitaminB2);
                await ProcessNutritionalValues(row, VitaminB3Col, aliment, NutritionalInfoType.VitaminB3);
                await ProcessNutritionalValues(row, VitaminB5Col, aliment, NutritionalInfoType.VitaminB5);
                await ProcessNutritionalValues(row, VitaminB6Col, aliment, NutritionalInfoType.VitaminB6);
                await ProcessNutritionalValues(row, VitaminB9Col, aliment, NutritionalInfoType.VitaminB9);
                await ProcessNutritionalValues(row, VitaminB12Col, aliment, NutritionalInfoType.VitaminB12);
            }

            return result;
        }

        private async Task ProcessNutritionalValues(int row, int col, Aliment aliment, NutritionalInfoType type)
        {
            string cellValue = GetCellValue(row, col);

            if (!string.IsNullOrEmpty(cellValue))
            {
                decimal value;
                if (cellValue.StartsWith("> "))
                {
                    cellValue = cellValue.Substring(2, cellValue.Length - 2);
                    decimal.TryParse(cellValue, out value);
                    value /= 2;
                }
                else
                {
                    decimal.TryParse(cellValue, out value);
                }

                await _nutritionalValueRepository.Insert(aliment, type, value);
            }
        }

        private string GetCellValue(int row, int col)
        {
            var value = Worksheet.Cells[row, col].Value;
            string val = UppercaseFirst(value?.ToString().Trim());
            if (val == null || val == "-")
            {
                return null;
            }
            return val.Length > 100 ? val.Substring(0, 100) : val;
        }

        private string UppercaseFirst(string s)
        {
            return string.IsNullOrEmpty(s) ? s : char.ToUpper(s[0]) + s.Substring(1);
        }
    }
}
