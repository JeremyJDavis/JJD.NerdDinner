using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NerdDinner.Controllers;
using NerdDinner.Models;
using NerdDinner.Tests.Fakes;

namespace NerdDinner.Tests.Controllers
{
    [TestClass]
    public class DinnersControllerTest
    {
        List<Country> countires = new List<Country>
        {
            new Country {Name = "USA"},
            new Country {Name = "Canada"},
            new Country {Name = "Brazil"}
        };

        List<Dinner> CreateTestDinners()
        {

            List<Dinner> dinners = new List<Dinner>();

            for (int i = 0; i < 101; i++)
            {

                Dinner sampleDinner = new Dinner()
                {
                    DinnerId = i,
                    Title = "Sample Dinner",
                    ContactEmail = "SomeUser@somedomain.com",
                    Address = "1918 Eighth Ave Seattle WA 98101",
                    Country = countires.Single(c => c.Name == "USA"),
                    ContactPhone = "425-555-1212",
                    EventDate = DateTime.Now.AddDays(i),
                    Latitude = 99,
                    Longitude = -99
                };

                dinners.Add(sampleDinner);
            }

            return dinners;
        }

        DinnersController CreateDinnersController()
        {
            var repository = new FakeDinnerRepository(CreateTestDinners());
            return new DinnersController(repository);
        }

        DinnersController CreateDinnersControllerAs(string userName)
        {
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns(userName);
            mock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            var controller = CreateDinnersController();
            controller.ControllerContext = mock.Object;

            return controller;
        }

        [TestMethod]
        public void DetailsAction_Should_Return_View_For_ExistingDinner()
        {

            // Arrange
            var controller = CreateDinnersController(); //new DinnersController();

            // Act
            var result = controller.Details(1) as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void DetailsAction_Should_Return_NotFoundView_For_BogusDinner()
        {

            // Arrange
            var controller = CreateDinnersController();

            // Act
            var result = controller.Details(999) as ViewResult;

            // Assert
            Assert.AreEqual("NotFound", result.ViewName);
        }

        [TestMethod]
        public void EditAction_Should_Return_EditView_When_ValidOwner()
        {

            // Arrange
            var controller = CreateDinnersControllerAs("SomeUser");

            // Act
            var result = controller.Edit(1) as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(DinnerFormViewModel));
        }

        [TestMethod]
        public void EditAction_Should_Return_InvalidOwnerView_When_InvalidOwner()
        {

            // Arrange
            var controller = CreateDinnersControllerAs("NotOwnerUser");

            // Act
            var result = controller.Edit(1) as ViewResult;

            // Assert
            Assert.AreEqual("",result.ViewName);
        }
    }
}
