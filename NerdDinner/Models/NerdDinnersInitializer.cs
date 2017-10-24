﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;

namespace NerdDinner.Models
{
    public class NerdDinnersInitializer : DropCreateDatabaseIfModelChanges<NerdDinnersDBContext>
    {

        public override void InitializeDatabase(NerdDinnersDBContext context)
        {
            base.InitializeDatabase(context);

            var countires = new List<Country>
            {
                new Country {Name = "USA"},
                new Country {Name = "Canada"},
                new Country {Name = "Brazil"}
            };

            var dinners = new List<Dinner>
            {
                new Dinner
                {
                    Title = "First Dinner",
                    EventDate = DateTime.Parse("11/20/2017"),
                    ContactEmail = "jdavis@paraport.com",
                    ContactPhone = "(206) 555-1212",
                    Address = "1918 Eigth Ave",
                    Country = countires.Single(c => c.Name == "USA"),
                    Latitude = 41.2,
                    Longitude = -122.3
                },
                new Dinner
                {
                    Title = "Second Dinner",
                    EventDate = DateTime.Parse("11/21/2017"),
                    ContactEmail = "jdavis@paraport.com",
                    ContactPhone = "(206) 555-1212",
                    Address = "1918 Eigth Ave",
                    Country = countires.Single(c => c.Name == "USA"),
                    Latitude = 45.2,
                    Longitude = -22.3
                },new Dinner
                {
                    Title = "Porcacalypse",
                    EventDate = DateTime.Parse("12/1/2017"),
                    ContactEmail = "jdavis@paraport.com",
                    ContactPhone = "(206) 555-1212",
                    Address = "1918 Eigth Ave",
                    Country = countires.Single(c => c.Name == "Canada"),
                    Latitude = 60.5,
                    Longitude = -100.5
                },
                new Dinner
                {
                    Title = "Chicken Dinner",
                    EventDate = DateTime.Parse("12/11/2017"),
                    ContactEmail = "jdavis@paraport.com",
                    ContactPhone = "(206) 555-1212",
                    Address = "1918 Eigth Ave",
                    Country = countires.Single(c => c.Name == "Brazil"),
                    Latitude = 50.2,
                    Longitude = -122.3
                }
            };

            dinners.ForEach(d => context.Dinners.Add(d));
            context.SaveChanges();
        }
    }
}