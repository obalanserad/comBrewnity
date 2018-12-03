using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class Recipe
    {
        [Key]
        public int RecipeId { get; set; }
        public virtual ICollection<MashStep> MashSteps { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; }

        public int BoilTime { get; set; }
        public int? FermentationTemp { get; set; }
        public int FermentationDays { get; set; }

        public virtual Style Style { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }

    public class MashStep
    {
        public int MashStepId { get; set; }
        public int Temp { get; set; }
        public int Time { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
    public class Style
    {
        public int StyleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
