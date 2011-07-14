using System;
using System.Collections.Generic;
using System.Linq;
using Mod03_ChelasMovies.DomainModel.Services;
using Mod03_ChelasMovies.DomainModel.ServicesRepositoryInterfaces;

namespace Mod03_ChelasMovies.DomainModel.ServicesImpl
{
    public class RepositoryMoviesService : IMoviesService
    {
        private readonly IMoviesRepository _moviesRepository;
        private readonly ICommentsRepository _commentsRepository;

        public RepositoryMoviesService(IMoviesRepository moviesRepository, ICommentsRepository commentsRepository)
        {
            _moviesRepository = moviesRepository;
            _commentsRepository = commentsRepository;
        }

        public ICollection<Movie> GetAllMovies()
        {
            return _moviesRepository.GetAll().ToList();
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

        public void DeleteComment(int commentId)
        {
            try
            {
                _commentsRepository.Delete(commentId);
                _commentsRepository.Save();
            }
            catch (Exception e)
            {
                throw new ArgumentException(String.Format("Comment with id {0} could not be found", commentId), "id", e);
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