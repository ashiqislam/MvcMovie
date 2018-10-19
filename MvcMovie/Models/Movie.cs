using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcMovie.Models
{
    public class Movie
    {
   
        public int ID { get; set; }


        public string Title { get; set; }

        
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MMMMM d, yyyy}")]
        [Required(ErrorMessage = "Please enter a valid date")]
        public DateTime ReleaseDate { get; set; }


        public string Genre { get; set; }
        public string Rating { get; set; }


        [Column(TypeName = "decimal(18, 2)")]


        [Required(ErrorMessage = "Please enter a valid price")]
        public decimal Price { get; set; }

    }

}
