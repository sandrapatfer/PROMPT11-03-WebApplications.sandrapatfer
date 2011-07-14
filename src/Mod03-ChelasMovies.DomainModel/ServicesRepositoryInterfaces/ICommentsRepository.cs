using System;

namespace Mod03_ChelasMovies.DomainModel.ServicesRepositoryInterfaces
{
    using Rep;

    public interface ICommentsRepository : IRepository<Comment, int>
    {
    }
}
