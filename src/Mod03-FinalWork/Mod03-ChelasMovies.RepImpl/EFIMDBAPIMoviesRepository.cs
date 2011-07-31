using Mod03_ChelasMovies.Rep;

namespace Mod03_ChelasMovies.RepImpl {
    using DomainModel;
    using DomainModel.ServicesRepositoryInterfaces;

    public class EFIMDBAPIMoviesRepository : EFDbContextRepository<Movie, int>, IMoviesRepository
    {
        public EFIMDBAPIMoviesRepository(MovieDbContext moviesContext) : base(moviesContext) { }

        #region IMoviesRepository Members

        public Movie SearchByTitle(string title)
        {
            return TheIMDbAPI.SearchByTitle(title);
        }

        #endregion

    }
}