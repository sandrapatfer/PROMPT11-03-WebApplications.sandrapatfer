using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mod03_ChelasMovies.Tests.Controllers
{
    using WebApp.Controllers;
    using DomainModel;
    using DomainModel.Services;
    using DomainModel.ServicesImpl;

    using NUnit.Framework;

    using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
    using System.Web.Mvc;

    [TestFixture]
    class MoviesControllerTests
    {
        private IMoviesService m_moviesService;
        private MoviesController m_controller;

        // runs before all tests
        [SetUp] 
        private void Setup()
        {
            m_moviesService = new InMemoryMoviesService();
            m_controller = new MoviesController(m_moviesService);
        }

        #region Tests when Index action executes
        #endregion

        #region Tests when Create action executes
        #endregion

        [Test]
        public void ShouldRenderTheDefaultForCreate()
        {
            // Arrange

            // Act
            ViewResult result = m_controller.Create() as ViewResult;

            // Assert
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [Test]
        public void ShouldRenderToDetails()
        {
            // Arrange
            //m_controller.ControllerContext

//            Movie m = new Movie() { ID = 1, Title = "xxx" };
            // Act
            RedirectToRouteResult result = m_controller.Create("xxx") as RedirectToRouteResult;
            // Assert
            Assert.AreEqual(string.Empty, result.RouteName);
            Assert.AreEqual("Details", result.RouteValues["action"]);
            Assert.AreEqual(null, result.RouteValues["controller"]);
            //Assert.AreEqual(m.ID, result.RouteValues["id"]);
        }
    }
}
