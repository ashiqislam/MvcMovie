using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Models
{
    public class Main
    { 
       public Movie Movie { get; set; }
       public Reviews Reviews { get; set; }
    }
}
