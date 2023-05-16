using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Collect.DB;

namespace Collect.Controllers
{
    public class RestaurantController : Controller
    {
        // GET: Restaurant
        public ActionResult Index()
        {
            var db = new CustomEntities();
            var col = db.Creqs.ToList();
            return View(col);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Creq co)
        {
            try
            {
                var db = new CustomEntities();
                co.Status = "Pending";
                db.Creqs.Add(co);
                db.SaveChanges();
                var coll = new Clist();
                coll.Id = co.Id;
                coll.Rname = co.Rname;
                coll.Ptime = co.Ptime;
                coll.Status = co.Status;
                coll.Ename = "-";
                db.Clists.Add(coll);
                db.SaveChanges();
                TempData["msg"] = "Create successful!";
                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                TempData["msg"] = "Error";
                return RedirectToAction("Index");
            }
            
        }

    }
    
}