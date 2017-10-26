using System;
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

        }

        protected override void Seed(NerdDinnersDBContext context)
        {
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
                    Address = "1918 Eighth Ave",
                    Country = countires.Single(c => c.Name == "USA"),
                    Latitude = 47.6156899,
                    Longitude = -122.3381664
                },
                new Dinner
                {
                    Title = "Second Dinner",
                    EventDate = DateTime.Parse("11/21/2017"),
                    ContactEmail = "jdavis@paraport.com",
                    ContactPhone = "(206) 555-1212",
                    Address = "1918 Eighth Ave",
                    Country = countires.Single(c => c.Name == "USA"),
                    Latitude = 47.6156899,
                    Longitude = -122.3381664
                },new Dinner
                {
                    Title = "Porcacalypse",
                    EventDate = DateTime.Parse("12/1/2017"),
                    ContactEmail = "jdavis@paraport.com",
                    ContactPhone = "(206) 555-1212",
                    Address = "1918 Eighth Ave",
                    Country = countires.Single(c => c.Name == "Canada"),
                    Latitude = 47.6156899,
                    Longitude = -122.3381664
                },
                new Dinner
                {
                    Title = "Chicken Dinner",
                    EventDate = DateTime.Parse("12/11/2017"),
                    ContactEmail = "jdavis@paraport.com",
                    ContactPhone = "(206) 555-1212",
                    Address = "1918 Eighth Ave",
                    Country = countires.Single(c => c.Name == "Brazil"),
                    Latitude = 47.6156899,
                    Longitude = -122.3381664
                }
            };

            dinners.ForEach(d => context.Dinners.Add(d));
            context.SaveChanges();
        }
    }
}