using CarnetNutrition.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarnetNutrition.Core.Models
{
    public class NutritionalInfo
    {
        public NutritionalInfoType Id { get; set; }

        public Unit Unit { get; set; }

        public NutritionalInfoType ParentId { get; set; }
    }
}
