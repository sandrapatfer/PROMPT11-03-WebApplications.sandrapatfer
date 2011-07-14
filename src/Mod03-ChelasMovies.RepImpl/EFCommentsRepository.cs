using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mod03_ChelasMovies.RepImpl
{
    using Rep;
    using DomainModel;
    using DomainModel.ServicesRepositoryInterfaces;

    public class EFCommentsRepository : EFDbContextRepository<Comment, int>, ICommentsRepository
    {
        public EFCommentsRepository(MovieDbContext context) : base(context) { }
    }
}
