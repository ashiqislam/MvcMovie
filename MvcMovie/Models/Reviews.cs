using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Models
{
    public class Reviews
    {
        [Key]
        public int reviewID { get; set; }

        [ForeignKey("ID")]
        public virtual Movie Reviews1 { get; set; }

        public List<Reviews> revs;
        public SelectList Reviews2;
        public string Reviewer { get; set; }
        public string Comment { get; set; }

        [Display(Name = "Movie")]
        public string Title { get; set; }
    }
}
 