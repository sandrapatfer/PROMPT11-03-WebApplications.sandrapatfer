using System.Linq;
using Mod03_ChelasMovies.DomainModel.ServicesImpl;

namespace Mod03_ChelasMovies.WebApp.Models {
    
    using System.Data.Entity;
    
    using DomainModel;
    using System.IO;
    using System.Web.Security;

    public class MoviesInitializer :
        //IDatabaseInitializer<MovieDbContext>
        DropCreateDatabaseAlways<MovieDbContext>
        //DropCreateDatabaseIfModelChanges<MovieDbContext>
    {
        //public void InitializeDatabase(MovieDbContext context)
        protected override void Seed(MovieDbContext context)
        {
            var user1 = new User() { MembershipUser = Membership.CreateUser("spf", "spf12345", "spf@xxx.com") };
            var user2 = new User() { MembershipUser = Membership.CreateUser("abc", "abc12345", "abc@xxx.com") };
            //context.Users.Add(user);

            // TODO activar roles do web.config
//            Roles.CreateRole("Admin");
//            Roles.AddUserToRole("spf", "Admin");

            var movies = new InMemoryMoviesService().GetAllMovies().ToList();
            //var comments = (from m in movies
            //               from c in m.Comments
            //               select c).ToList();

            //movies.ForEach(m => m.CreatedBy = user);
            //movies.ForEach(m => m.CreatedBy = user.UserName);

            for (int i = 0; i < ((movies.Count % 2 == 0) ? movies.Count : movies.Count - 1); i += 2)
            {
                movies[i].CreatedBy = user1.UserName;
                movies[i + 1].CreatedBy = user2.UserName;
            }
            if (movies.Count % 2 == 1)
            {
                movies[movies.Count - 1].CreatedBy = user1.UserName;
            }

            //comments.ForEach(c => context.Comments.Add(c));
            movies.ForEach(d => context.Movies.Add(d));
        }
    }
}