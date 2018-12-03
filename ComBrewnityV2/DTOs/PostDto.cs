using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComBrewnityV2.DTOs
{
    public class PostDto
    {
        public int PostId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public string Title { get; set; }
        public string Introduction { get; set; }

        public string UserName { get; set; }
        public string UserId { get; set; }
        public TasteNoteDto TasteNote { get; set; }
        public RecipeDto Recipe { get; set; }
        public TechnicalDataDto TechnicalData { get; set; }
    }
}
