﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MvcMovieContext _context;

        public MoviesController(MvcMovieContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string movieGenre, string searchString)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Movie
                                            orderby m.Genre
                                            select m.Genre;

            var movies = from m in _context.Movie
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }


            var movieGenreVM = new MovieGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Movies = await movies.ToListAsync()
            };

            return View(movieGenreVM);
        }

        // GET: Movies/Details/
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }


            TempData["MovieID"] = id;
            TempData.Keep();


            return View(movie);
        }


        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,ReleaseDate,Genre,Price,Rating, Poster")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(movie);
        }




        // GET: Movies/Edit/
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,ReleaseDate,Genre,Price,Rating, Poster")] Movie movie)
        {
            if (id != movie.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.ID == id);
        }


        public async Task<IActionResult> RetrieveData(string title)
        {
            HttpClient client = new HttpClient();

            string url = "http://www.omdbapi.com/?t=" + (string)title + "&apikey=95a6f310";
            var response = await client.GetAsync(url);
            var data = await response.Content.ReadAsStringAsync();

            var json = JsonConvert.DeserializeObject(data).ToString();
            dynamic omdbMovie = JObject.Parse(json);

            Movie m = new Movie();
            try
            {
                m.Title = omdbMovie["Title"];
                m.ReleaseDate = omdbMovie["Released"];
                m.Genre = omdbMovie["Genre"];
                m.Rating = omdbMovie["Rated"];
                m.Poster = omdbMovie["Poster"];

            }
            catch
            {
                return View("Create");
            }

            return View("Create", m);
        }


        // GET: Reviews/WriteReview/
        //public IActionResult WriteReview()
       // {
       //     return View();
        //}


        // GET: Movies/WriteReview/
        public IActionResult WriteReview(int? id)
        {
            return View();
        }


        // POST: Movies/WriteReview/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WriteReview([Bind("ID, Reviewer, Comment, Title")] Reviews review)
        {

            if (ModelState.IsValid)
            {

                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Movies", new { id = TempData.Peek("MovieID") });
            }

            return View(review);
        }
    }
}
