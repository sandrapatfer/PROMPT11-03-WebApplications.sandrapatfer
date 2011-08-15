using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Mod03_ChelasMovies.DependencyResolution;
using Mod03_ChelasMovies.DomainModel;
using Mod03_ChelasMovies.WebApp.Models;

namespace Mod03_ChelasMovies.WebApp
{
    using System.Data.Entity;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    "Comments", // Route name
            //    "Movies/{movieId}/{action}/{commentId}", // URL with parameters
            //    new { controller = "Movies", commentId = UrlParameter.Optional }, // Parameter defaults,
            //    new { movieId = @"\d+" }
            //);

            routes.MapRoute(
                "Paging",
                "{controller}/Page{PageNumber}/{SortingCriteria}/{Order}",
                new { controller = "Movies", action = "Index", PageNumber = 0, Order = "Asc", SortingCriteria = "none" },
                new { PageNumber = @"\d+", Order = "Asc|Desc|Ascending|Descending" }
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Movies", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            Database.SetInitializer(new MoviesInitializer());
            AppStart_Structuremap.Start();
            Database.SetInitializer<MovieDbContext>(new MoviesInitializer());


            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}