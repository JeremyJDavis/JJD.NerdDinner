using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NerdDinner.Models;

namespace NerdDinner.Controllers
{
    public class HomeController : Controller
    {
        NerdDinnersDBContext nerdDinners = new NerdDinnersDBContext();

        //
        // GET: /
        public ActionResult Index()
        {
            var dinners = from d in nerdDinners.Dinners
                where d.EventDate > DateTime.Now
                select d;

            return View(dinners.ToList());
        }

        //
        //GET: /Home/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        //POST: /Home/Create

        [HttpPost]
        public ActionResult Create(Dinner dinner)
        {
            if (ModelState.IsValid)
            {
                nerdDinners.Dinners.Add(dinner);
                nerdDinners.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(dinner);
        }




        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}