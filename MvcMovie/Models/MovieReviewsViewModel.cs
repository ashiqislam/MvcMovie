using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Models
{
    public class MovieReviewsViewModel
    {
        public List<Movie> Movies;

        public string MovieReview { get; set; }
    }
}
