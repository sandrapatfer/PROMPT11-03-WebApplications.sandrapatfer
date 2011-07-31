using Mod03_ChelasMovies.DomainModel;
using Mod03_ChelasMovies.DomainModel.Services;
using Mod03_ChelasMovies.DomainModel.ServicesImpl;
using Mod03_ChelasMovies.DomainModel.ServicesRepositoryInterfaces;
using Mod03_ChelasMovies.RepImpl;
using StructureMap;
namespace Mod03_ChelasMovies.DependencyResolution {
    public static class IoC {
        public static IContainer Initialize() {
            ObjectFactory.Initialize(x =>
                        {
                            x.Scan(scan =>{
                                        scan.TheCallingAssembly();
                                        scan.WithDefaultConventions();
                                    });

                            // IMoviesService
                            //x.For<IMoviesService>().HttpContextScoped().Use<EFMoviesService>();
                            //x.For<IMoviesService>().HttpContextScoped().Use<InMemoryMoviesService>();
                            x.For<IMoviesService>().HttpContextScoped().Use<RepositoryMoviesService>();

                            // IMoviesRepository
                            x.For<IMoviesRepository>().HttpContextScoped().Use<EFIMDBAPIMoviesRepository>();

                            // MovieDbContext
                            x.For<MovieDbContext>().HttpContextScoped().Use<MovieDbContext>();
                        });
            return ObjectFactory.Container;
        }
    }
}