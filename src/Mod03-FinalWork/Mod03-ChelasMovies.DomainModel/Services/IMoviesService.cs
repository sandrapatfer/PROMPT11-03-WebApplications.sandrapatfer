using System;
using System.Collections.Generic;

namespace Mod03_ChelasMovies.DomainModel.Services
{
    public interface IMoviesService : IDisposable
    {
        ICollection<Movie> GetAllMovies();
        ICollection<Movie> GetAllMovies(string filter);
        Movie Get(int id);
        Movie GetWithComments(int id);
        void Add(Movie newMovie);
        void Update(Movie movie);
        void Delete(int id);
        Movie Search(string title);
    }
}