using System;
using System.Linq;
using System.Web.Mvc;
using Mod03_ChelasMovies.WebApp.Utils;
using Mod03_ChelasMovies.DomainModel;
using Mod03_ChelasMovies.DomainModel.Services;

namespace Mod03_ChelasMovies.WebApp.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _moviesService;

        public MoviesController(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        //
        // GET: /Movies/
        public ActionResult Index()
        {
            return View(_moviesService.GetAllMovies());
        }


        public ActionResult Details(int id)
        {
            Movie movie = _moviesService.Get(id);
            if (movie == null)
            {
                //return RedirectToAction("Index");
                return View("NotFound", id);
            }
            return View(movie);
        }

        public ActionResult Create()
        {
            return View(new Movie());
        }

        [HttpPost]
        [ActionName("Create")]
        [FormButton("button", "Fill")]
        public ActionResult Fill(string Title)
        {
            ModelState.Clear();
            return View(_moviesService.Search(Title));
        }

        [HttpPost]
        [FormButton("button", "Create")]
        public ActionResult Create(string Title)
        {
            Movie newMovie = new Movie();
            TryUpdateModel(newMovie);
            if (ModelState.IsValid)
            {
                _moviesService.Add(newMovie);
                return RedirectToAction("Details", new { id = newMovie.ID });
            }
            else
            {
                return View(newMovie);
            }
        }

        [HttpPost]
        public ActionResult DeleteComment(int movieId, int commentId)
        {
            _moviesService.DeleteComment(commentId);
            return RedirectToRoute("Default", new { action = "Details", id = movieId });
        }

        public ActionResult Edit(int id)
        {
            Movie movie = _moviesService.Get(id);
            return View(movie);
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditPost(Movie updatedMovie)
        {
            if (ModelState.IsValid)
            {
                Movie bdMovie = _moviesService.Get(updatedMovie.ID);
                UpdateModel(bdMovie);
                _moviesService.Update(bdMovie);
                return RedirectToAction("Details", new { id = updatedMovie.ID });
            }
            else
            {
                return View(updatedMovie);
            }
        }

        public ActionResult Delete(int id)
        {
            Movie movie = _moviesService.Get(id);
            return View(movie);
        }

        [HttpPost]
        public ActionResult Delete(Movie m)
        {
            _moviesService.Delete(m.ID);
            return RedirectToAction("Index");
        }
    }
}
