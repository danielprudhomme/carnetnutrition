using System;
using System.Collections.Generic;

namespace CarnetNutrition.Core.Models
{
    public class Aliment
    {
        public Aliment()
        {
            Categories = new List<AlimentCategory>();
            NutritionalValues = new List<NutritionalValue>();
        }

        public Guid Id { get; set; }
        public string Label { get; set; }
        public IList<AlimentCategory> Categories { get; set; }
        public IList<NutritionalValue> NutritionalValues { get; set; }
    }
}
