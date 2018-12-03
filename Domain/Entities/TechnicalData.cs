using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class TechnicalData
    {
        [Key]
        public int TechDataId { get; set; }
        public double BrewVol { get; set; }
        public double FinalVol { get; set; }
        public DateTime BrewDate { get; set; }
        public DateTime BottleDate { get; set; }
        public double ABV { get; set; }
        public double OG { get; set; }
        public double FG { get; set; }
        public double? Cost { get; set; }

        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
