using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NerdDinner.Models;

namespace NerdDinner.Tests.Fakes
{
    public class FakeDinnerRepository : IDinnerRepository
    {
        private List<Dinner> dinnerList;

        private List<Country> countryList;

        public FakeDinnerRepository(List<Dinner> dinners)
        {
            dinnerList = dinners;
        }

        public void Add(Dinner dinner)
        {
            dinnerList.Add(dinner);
        }

        public void Delete(Dinner dinner)
        {
            dinnerList.Remove(dinner);
        }

        public IQueryable<Dinner> FindAllDinners()
        {
            return dinnerList.AsQueryable();
        }

        public IQueryable<Dinner> FindByLocation(float latitude, float longitude)
        {
            return (from dinner in dinnerList
                where dinner.Latitude == latitude && dinner.Longitude == longitude
                select dinner).AsQueryable();
        }

        public IQueryable<Dinner> FindUpcomingDinners()
        {
            return (from dinner in dinnerList
                where dinner.EventDate > DateTime.Now
                select dinner).AsQueryable();
        }

        public IQueryable<Country> GetCountries()
        {
            return countryList.AsQueryable();
        }

        public Country GetCountry(int id)
        {
            return countryList.SingleOrDefault(c => c.CountryID == id);
        }

        public Dinner GetDinner(int id)
        {
            return dinnerList.SingleOrDefault(d => d.DinnerId == id);
        }

        public void Save()
        {
            foreach (Dinner dinner in dinnerList )
            {
                if (!dinner.IsValid)
                    throw new ApplicationException("Rule Violations");
            }
        }
    }
}
