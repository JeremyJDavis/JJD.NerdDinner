using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    }
}
