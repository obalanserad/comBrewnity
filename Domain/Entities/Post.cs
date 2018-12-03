using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
     public class Post
    {
        [Key]
        public int PostId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public string Title { get; set; }
        public string Introduction { get; set; }
      
        public virtual TasteNote TasteNote { get; set; }
        public virtual Recipe Recipe { get; set; }
        public virtual TechnicalData TechnicalData { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
