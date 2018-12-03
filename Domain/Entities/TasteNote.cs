using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class TasteNote
    {
        [Key]
        public int TasteNoteId { get; set; }
        public string Apperance { get; set; }
        public string Aroma { get; set; }
        public string Mouthfeel { get; set; }
        public string Taste { get; set; }
        public string Conclusion { get; set; }
        public string Improvments { get; set; }

        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
