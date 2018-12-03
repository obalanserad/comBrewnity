using System.Collections.Generic;

namespace ComBrewnityV2.DTOs
{
    public class RecipeDto
    {
        public int RecipeId { get; set; }
        public List<MashStepDto> MashSteps { get; set; }
        public List<IngredientDto> Ingredients { get; set; }

        public int BoilTime { get; set; }
        public int? FermentationTemp { get; set; }
        public int FermentationDays { get; set; }

        public StyleDto Style { get; set; }
    }

    public class MashStepDto
    {
        public int MashStepId { get; set; }
        public int Temp { get; set; }
        public int Time { get; set; }
    }
    public class StyleDto
    {
        public int StyleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}