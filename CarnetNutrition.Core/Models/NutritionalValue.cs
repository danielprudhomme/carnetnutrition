using CarnetNutrition.Core.Enums;
using System;

namespace CarnetNutrition.Core.Models
{
    public class NutritionalValue
    {
        public NutritionalInfoType Type { get; set; }
        public Unit Unit { get; set; }
        public decimal Value { get; set; }
    }
}