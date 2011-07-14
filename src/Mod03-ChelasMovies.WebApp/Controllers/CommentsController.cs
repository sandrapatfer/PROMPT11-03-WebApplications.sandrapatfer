using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mod03_ChelasMovies.WebApp.Controllers
{
    using DomainModel;
    using DomainModel.Services;

    public class CommentsController : Controller
    {
        private readonly IMoviesService _moviesService;
        private List<SelectListItem> ViewBagOptionList = new List<SelectListItem>() { 
                new SelectListItem { Text = "Really Bad", Value="1" }, 
                new SelectListItem { Text = "Bad", Value="2" }, 
                new SelectListItem { Text = "Average", Value="3" }, 
                new SelectListItem { Text = "Good", Value="4" }, 
                new SelectListItem { Text = "Really Good", Value="5" } };

        public CommentsController(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        //
        // GET: /Comments/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Comments/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Comments/Create

        public ActionResult Create(int movieId)
        {
            Comment c = new Comment { MovieID = movieId };
            c.Rating = Comment.CommentRating.Average;
            ViewBag.OptionList = ViewBagOptionList;
            return View(c);
        }

        //
        // POST: /Comments/Create
        [HttpPost]
        public ActionResult Create(Comment c)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Movie movie = _moviesService.Get(c.MovieID);
                    movie.Comments.Add(c);
                    _moviesService.Update(movie);
                    return RedirectToRoute("Default", new { controller = "Movies", action = "Details", id = c.MovieID });
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", String.Format("Edit Failure, inner exception: {0}", e));
            }

            ViewBag.OptionList = ViewBagOptionList;
            return View(c);
        }

      
        //
        // GET: /Comments/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Comments/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Comments/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Comments/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
