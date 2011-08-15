using System;
using System.Collections.Generic;
using Mod03_ChelasMovies.Rep.Helpers.Collections;

namespace Mod03_ChelasMovies.DomainModel.Services
{
    public interface IMoviesService : IDisposable
    {
        ICollection<Movie> GetAllMovies();
        IPagedList<Movie> GetAllMovies(string filter, int pageIndex, int pageSize, string sortingCriteria);
        Movie Get(int id);
        Movie GetWithComments(int id);
        void Add(Movie newMovie);
        void Update(Movie movie);
        void Delete(int id);
        Movie Search(string title);
    }
}