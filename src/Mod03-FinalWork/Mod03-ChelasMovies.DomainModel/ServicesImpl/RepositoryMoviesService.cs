using System;
using System.Collections.Generic;
using System.Linq;
using Mod03_ChelasMovies.DomainModel.Services;
using Mod03_ChelasMovies.DomainModel.ServicesRepositoryInterfaces;
using Mod03_ChelasMovies.Rep.Helpers.Collections;
using System.Text;

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

        public IPagedList<Movie> GetAllMovies(SearchCollection filter, int pageIndex, int pageSize, string sortingCriteria)
        {
            return _moviesRepository.GetAll(CreateFilter(filter), pageIndex, pageSize, sortingCriteria);
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

        private string CreateFilter(SearchCollection searchFields)
        {
            if (searchFields.Count == 0)
            {
                return string.Empty;
            }

            var filter = new StringBuilder();
            foreach (var fieldName in searchFields.AllKeys)
            {
                if (string.IsNullOrEmpty(searchFields[fieldName]))
                {
                    continue;
                }

                var fieldProp = typeof(Movie).GetProperty(fieldName);
                if (fieldProp != null)
                {
                    string fmt = null;
                    var attrs = fieldProp.GetCustomAttributes(typeof(SearchableAttribute), false);
                    if (attrs.Length == 1)
                    {
                        fmt = ((SearchableAttribute)attrs[0]).FilterFormat;
                    }
                    if (fmt == null)
                    {
                        if (fieldProp.PropertyType == typeof(string))
                        {
                            fmt = string.Format("{0} == \"{{0}}\"", fieldName);
                        }
                        else
                        {
                            fmt = string.Format("{0} == {{0}}", fieldName);
                        }
                    }

                    if (filter.Length > 0)
                    {
                        filter.Append(" and ");
                    }
                    filter.AppendFormat(fmt, searchFields[fieldName]);
                }
            }
            return filter.ToString();
        }
    }
}