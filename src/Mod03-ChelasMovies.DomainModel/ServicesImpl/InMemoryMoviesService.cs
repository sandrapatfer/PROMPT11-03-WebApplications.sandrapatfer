using System;
using System.Collections.Generic;
using Mod03_ChelasMovies.DomainModel.Services;

namespace Mod03_ChelasMovies.DomainModel.ServicesImpl
{
    using System.Threading;

    public class InMemoryMoviesService : IMoviesService
    {
        static List<Movie> movies;
        static List<Comment> comments;
        static int newId;


        static InMemoryMoviesService()
        {

            movies = new List<Movie>
                             {

                                 new Movie
                                     {
                                         ID = 1,
                                         Title = "When Harry Met Sally1",
                                         Genre = "Romantic Comedy",
                                         Year = 2004,
                                         Comments = new List<Comment>()
                                     },

                                 new Movie
                                     {
                                         ID = 2,
                                         Title = "Ghostbusters 2",
                                         Genre = "Comedy",
                                         Year = 2002,
                                         Comments = new List<Comment>()
                                     },
                                 new Movie
                                     {
                                         ID = 3,
                                         Title = "Ninja das Caldas",
                                         Genre = "Comedy",
                                         Year = 2000,
                                         Comments = new List<Comment>()
                                     },
                             };

        comments = new List<Comment>
                               {
                                   new Comment
                                       {
                                           Description = "Description 1",
                                           Rating = Comment.CommentRating.Average,
                                           MovieID = movies[0].ID
                                       },
                                   new Comment
                                       {
                                           Description = "Description 2",
                                           Rating = Comment.CommentRating.Good,
                                           MovieID = movies[0].ID
                                       },

                                   new Comment
                                       {
                                           Description = "Description 3",
                                           Rating = Comment.CommentRating.ReallyGood,
                                           MovieID = movies[1].ID
                                       },
                                       new Comment
                                       {
                                           Description = "Description 4",
                                           Rating = Comment.CommentRating.Good,
                                           MovieID = movies[1].ID
                                       },

                               };

        newId = movies.Count;

        }

        public ICollection<Movie> GetAllMovies()
        {
            return movies;
        }

        public Movie Get(int id)
        {
            foreach (Movie m in movies) {
                if (m.ID == id) return m;
            }
            return null;
        }

        public Movie GetWithComments(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(Movie newMovie)
        {
            newMovie.ID = Interlocked.Increment(ref newId);
            movies.Add(newMovie);
        }

        public void Update(Movie movie)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
        public void DeleteComment(int commentId)
        {
            throw new NotImplementedException();
        }

        public Movie Search(string title)
        {
            throw new NotImplementedException();
        }

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}