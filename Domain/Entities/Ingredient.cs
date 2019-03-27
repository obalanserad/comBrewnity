using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


    namespace Domain.Entities
    {

    public class Ingredient
    {
        [Key]
        public int IngredientId { get; set; }
        //ärver från BaseType
        public string Name { get; set; }
        //ärver från BaseType
        public string Manufacturer { get; set; }
        //user anger
        public double Amount { get; set; }
        //räknas ut innan den sparas
        public double? CalculatedProcent { get; set; }
        //user anger
        public int? ContactTimeMinutes { get; set; }
        //user anger
        public int? ContactTimeDays { get; set; }
        //user anger
        public bool DryHop { get; set; } = false;
        //user anger
        public bool InBoil { get; set; } = true;
        //user anger
        public double? AlphaAcid { get; set; }
        //user anger
        public int? EBC { get; set; }
        //user anger
        public int? IBU { get; set; }
        //fältet man fyller i anger detta
        public virtual IngredientType Type { get; set; }

        public virtual Recipe Recipe { get; set; }

    }

    public enum IngredientType
    {
        BASE_MALT=0,CRYSTAL_MALT=1,ROASTED_MALT=2,HOP=3,YEAST=4,OTHER=5
    }


}


