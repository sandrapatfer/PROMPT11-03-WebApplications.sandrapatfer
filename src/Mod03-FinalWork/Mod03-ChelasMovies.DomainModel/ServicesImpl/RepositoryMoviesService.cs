using System;
using System.Collections.Generic;
using System.Linq;
using Mod03_ChelasMovies.DomainModel.Services;
using Mod03_ChelasMovies.DomainModel.ServicesRepositoryInterfaces;
using Mod03_ChelasMovies.Rep.Helpers.Collections;

namespace Mod03_ChelasMovies.DomainModel.ServicesImpl
{
    public class RepositoryMoviesService : IMoviesService
    {
        private readonly IMoviesRepository _moviesRepository;

        public RepositoryMoviesService(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }

        public ICollection<Movie> GetAllMovies()
        {
            IQueryable<Movie> movies = _moviesRepository.GetAll();
            return movies.ToList();
        }
        public IPagedList<Movie> GetAllMovies(string filter, int pageIndex, int pageSize, string sortingCriteria)
        {
            return _moviesRepository.GetAll(filter, pageIndex, pageSize, sortingCriteria);
        }

        private string GetTitle(string title)
        {
            return title;
        }

        public Movie Get(int id)
        {
            return _moviesRepository.Get(id);
        }

        public Movie GetWithComments(int id)
        {
            return _moviesRepository.Get(id);
        }

        public void Add(Movie newMovie)
        {
            _moviesRepository.Add(newMovie);
            _moviesRepository.Save();
        }

        public void Update(Movie movie)
        {
            _moviesRepository.Save();
        }

        public void Delete(int id)
        {
            try
            {
                _moviesRepository.Delete(id);
                _moviesRepository.Save();
            }
            catch (Exception e)
            {
                throw new ArgumentException(String.Format("Movie with id {0} could not be found", id), "id", e);
            }

        }

        public Movie Search(string title)
        {
            return _moviesRepository.SearchByTitle(title);
        }

        public void Dispose()
        {
            _moviesRepository.Dispose();
        }
    }
}