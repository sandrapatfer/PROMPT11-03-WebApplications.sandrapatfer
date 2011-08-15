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
            // ler os scripts e executar
            StreamReader fs = new StreamReader(@"C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallCommon.sql");
            string str = fs.ReadToEnd();
            fs.Close();
            str = str.Replace("aspnetdb", "movies");
            foreach (string stm in str.Split(new string[] {"GO"}, System.StringSplitOptions.RemoveEmptyEntries))
            {
                try
                {
                    context.Database.ExecuteSqlCommand(stm);
                }
                catch
                {
                    //TODO???
                }
            }

            fs = new StreamReader(@"C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallMembership.sql");
            str = fs.ReadToEnd();
            fs.Close();
            str = str.Replace("aspnetdb", "movies");
            foreach (string stm in str.Split(new string[] { "GO" }, System.StringSplitOptions.RemoveEmptyEntries))
            {
                try
                {
                    context.Database.ExecuteSqlCommand(stm);
                }
                catch
                { }
            }

            // TODO usar a api de membership para criar users
            var user = Membership.CreateUser("spf", "spf12345");

            var movies = new InMemoryMoviesService().GetAllMovies().ToList();
            //var comments = (from m in movies
            //               from c in m.Comments
            //               select c).ToList();

            //comments.ForEach(c => context.Comments.Add(c));
            movies.ForEach(d => context.Movies.Add(d));
        }
    }
}