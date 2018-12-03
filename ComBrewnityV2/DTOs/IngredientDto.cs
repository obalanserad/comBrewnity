namespace ComBrewnityV2.DTOs
{
    public class IngredientDto
    {
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
        public IngredientType Type { get; set; }
    }

    public enum IngredientType
    {
        BASE_MALT = 0, CRYSTAL_MALT = 1, ROASTED_MALT = 2, HOP = 3, YEAST = 4, OTHER = 5
    }

}