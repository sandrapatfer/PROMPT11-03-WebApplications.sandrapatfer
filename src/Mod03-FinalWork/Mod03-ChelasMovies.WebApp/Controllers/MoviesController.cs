using System;
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
        public ActionResult Index(string search_title)
        {
            if (string.IsNullOrWhiteSpace(search_title))
                return View(_moviesService.GetAllMovies());
            else
                return View(_moviesService.GetAllMovies(string.Format("Title.Contains(\"{0}\")", search_title)));
        }

        public ActionResult Details(int id)
        {
            Movie movie = _moviesService.Get(id);
            if(movie == null)
            {
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

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _moviesService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
