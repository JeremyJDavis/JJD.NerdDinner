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


            //return View(db.Dinners.ToList());   
        }

        // GET: Dinners/Details/5
        public ActionResult Details(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Dinner dinner = db.Dinners.Find(id);
            //if (dinner == null)
            //{
            //    return View("NotFound");
            //}
            //return View(dinner);
            var singleDinner = iDinnerRepos.GetDinner(id);
            if (singleDinner == null)
            {
                return View("NotFound");
            }
            return View(singleDinner);
        }

        // GET: Dinners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dinners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DinnerId,Title,EventDate,HostedBy,ContactPhone,Address,Country")] Dinner dinner)
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
        public ActionResult Edit([Bind(Include = "DinnerId,Title,EventDate,HostedBy,ContactPhone,Address,Country")] Dinner dinner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dinner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dinner);
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
