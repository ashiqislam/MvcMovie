using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace MvcMovie.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcMovieContext(
                serviceProvider.GetRequiredService<DbContextOptions<MvcMovieContext>>()))
            {
                // Look for any movies.
                if (context.Movie.Any())
                {
                    return;   // DB has been seeded
                }

                context.Movie.AddRange(
                     new Movie
                     {
                         Title = "When Harry Met Sally",
                         ReleaseDate = DateTime.Parse("1989-1-11"),
                         Rating = "R",
                         Genre = "Romantic Comedy",
                         Price = 7.99M,
                         Poster = ""
                     },

                     new Movie
                     {
                         Title = "Ghostbusters ",
                         ReleaseDate = DateTime.Parse("1984-3-13"),
                         Genre = "Comedy",
                         Rating = "PG",
                         Price = 8.99M,
                         Poster = ""
                     },

                     new Movie
                     {
                         Title = "Ghostbusters 2",
                         ReleaseDate = DateTime.Parse("1986-2-23"),
                         Genre = "Comedy",
                         Rating = "PG",
                         Price = 9.99M,
                         Poster = ""
                     },

                   new Movie
                   {
                       Title = "Rio Bravo",
                       ReleaseDate = DateTime.Parse("1959-4-15"),
                       Genre = "Western",
                       Rating = "NR",
                       Price = 3.99M,
                       Poster = ""
                   }
                );

                // Look for any reviews.
                if (context.Reviews.Any())
                {
                    return;   // DB has been seeded
                }

                context.Reviews.AddRange(
                     new Reviews
                     {
                         Title = "TEST 1 Title",
                         Comment = "TEST 2 Comment",
                         Reviewer = "AUTH1"
                     },

                    new Reviews
                    {
                        Title = "TEST 2 Title",
                        Comment = "TEST 2 Comment",
                        Reviewer = "AUTH2"
                    },
                     new Reviews
                     {
                         Title = "TEST 3 Title",
                         Comment = "TEST 3 Comment",
                         Reviewer = "AUTH3"
                     },

                   new Reviews
                   {
                       Title = "TEST 4 Title",
                       Comment = "TEST 4 Comment",
                       Reviewer = "AUTH4"
                   }

                 );

                context.SaveChanges();
            }
        }
    }
}