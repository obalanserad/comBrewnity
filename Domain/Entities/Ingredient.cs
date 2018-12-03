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
        public string Name { get; set; }
        //public DateTime Created { get; set; }
        public string Manufacturer { get; set; }
        public double Amount { get; set; }
        public double? CalculatedProcent { get; set; }
        public int? ContactTimeMinutes { get; set; }
        public int? ContactTimeDays { get; set; }
        public bool DryHop { get; set; } = false;
        public bool InBoil { get; set; } = true;
        public double? AlphaAcid { get; set; }
        public int? EBC { get; set; }
        public virtual IngredientType Type { get; set; }

        public virtual Recipe Recipe { get; set; }
        //public ICollection<IngredientRecipe> IngredientRecipes { get; set; }
    }

    //public class IngredientRecipe
    //{
    //    public int IngredientId { get; set; }
    //    public Ingredient Ingredient { get; set; }

    //    public int RecipeId { get; set; }
    //    public Recipe Recipe { get; set; }
    //}

    public enum IngredientType
    {
        BASE_MALT=0,CRYSTAL_MALT=1,ROASTED_MALT=2,HOP=3,YEAST=4,OTHER=5
    }


}


