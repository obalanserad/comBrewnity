using System;

namespace ComBrewnityV2.DTOs
{
    public class TechnicalDataDto
    {
        public int TechDataId { get; set; }
        public double BrewVol { get; set; }
        public double FinalVol { get; set; }
        public DateTime BrewDate { get; set; }
        public DateTime BottleDate { get; set; }
        public double ABV { get; set; }
        public double OG { get; set; }
        public double FG { get; set; }
        public double? Cost { get; set; }
    }
}