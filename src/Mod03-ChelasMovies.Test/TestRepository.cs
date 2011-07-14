using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Mod03_ChelasMovies.RepImpl;
using Mod03_ChelasMovies.DomainModel;

namespace Mod03_ChelasMovies.Test
{
    [TestFixture]
    public class TestRepository
    {
        public void ShouldDeleteCommentFromMovie()
        {
            var rep = new EFIMDBAPIMoviesRepository(new MovieDbContext());
            Movie movie = rep.Get(4);
            int count = movie.Comments.Count;
            movie.Comments.Remove(movie.Comments.First());
            Assert.AreEqual(count - 1, movie.Comments.Count);
        }
    }
}
