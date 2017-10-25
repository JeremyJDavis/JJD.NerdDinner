using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NerdDinner.Helpers;
using NerdDinner.Models;

namespace NerdDinner.Controllers
{
    public class DinnersController : Controller
    {
        private NerdDinnersDBContext db = new NerdDinnersDBContext();

        private IDinnerRepository iDinnerRepos;

        public DinnersController() : this(new DinnerRepository())
        {
        }

        public DinnersController(IDinnerRepository repository)
        {
            iDinnerRepos = repository;
        }

        // GET: Dinners
        public ActionResult Index(int? page)
        {
            const int pageSize = 10;

            var upcomingDinners = iDinnerRepos.FindUpcomingDinners();
            var paginatedDinners = new PaginatedList<Dinner>(upcomingDinners, page ?? 0, pageSize);

            return View(paginatedDinners);

            
        }

        // GET: Dinners/Details/5
        public ActionResult Details(int id)
        {

            var singleDinner = iDinnerRepos.GetDinner(id);
            if (singleDinner == null)
            {
                return View("NotFound");
            }
            return View(singleDinner);
        }

        // GET: Dinners/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dinners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DinnerId,Title,EventDate,ContactEmail,ContactPhone,Address,Country")] Dinner dinner)
        {
            if (ModelState.IsValid)
            {
                db.Dinners.Add(dinner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dinner);
        }

        // GET: Dinners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dinner dinner = db.Dinners.Find(id);
            if (dinner == null)
            {
                return HttpNotFound();
            }
            return View(dinner);
        }

        // POST: Dinners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DinnerId,Title,EventDate,ContactEmail,ContactPhone,Address,Country")] Dinner dinner)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(dinner).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //return View(dinner);
            //Dinner dinner = Dinner(dinner.DinnerId);
            if (!dinner.IsHostedBy(User.Identity.Name))
            {
                return View("NotFound");
            }

            try
            {
                UpdateModel(dinner);
                iDinnerRepos.Save();
                ViewBag.CountryID = new SelectList(iDinnerRepos.GetCountries(), "CountryID", "Name", dinner.CountryID);
                return RedirectToAction("Details", new { id = dinner.DinnerId });
            }
            catch
            {
                return View(new DinnerFormViewModel(dinner));
            }
        }

        // GET: Dinners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dinner dinner = db.Dinners.Find(id);
            if (dinner == null)
            {
                return HttpNotFound();
            }
            return View(dinner);
        }

        // POST: Dinners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dinner dinner = db.Dinners.Find(id);
            db.Dinners.Remove(dinner);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
