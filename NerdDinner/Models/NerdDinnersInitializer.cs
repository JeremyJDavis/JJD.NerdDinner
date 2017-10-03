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

            var dinners = new List<Dinner>
            {
                new Dinner
                {
                    Title = "Porkocalypse2017",
                    EventDate = DateTime.Parse("12/31/2017"),
                    Address = "1918 Eigth Ave",
                    ContactPhone = "206-555-1212",
                    Country = "USA",
                    HostedBy = "City of Seattle PD"
                },

                new Dinner
                {
                    Title = "Saladfest, the Regret",
                    EventDate = DateTime.Parse("01/01/2018"),
                    Address = "1918 Eigth Ave",
                    ContactPhone = "206-555-1212",
                    Country = "USA",
                    HostedBy = "Health Dept."
                }
            };
            dinners.ForEach(d => context.Dinners.Add(d));
        }
    }
}