using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace NerdDinner.Models
{
    public class DinnerRepository
    {
        private NerdDinnersDBContext db = new NerdDinnersDBContext();

        public IQueryable<Dinner> FindAllDinners()
        {
            return db.Dinners;
        }

        public Dinner GetDinner(int id)
        {
            return db.Dinners.SingleOrDefault(d => d.DinnerId == id);
        }

        //public void Add(Dinner dinner)
        //{
        //    db.Dinners.InsertOnSubmit(dinner);
        //}

        //public void Delete(Dinner dinner)
        //{
        //    db.RSVPs.DeleteAllOnSubmit(dinner.RSVPs);
        //    db.Dinners.DeleteOnSubmit(dinner);
        //}

        //public void Save()
        //{
        //    db.SubmitChanges();
        //}
    }
}