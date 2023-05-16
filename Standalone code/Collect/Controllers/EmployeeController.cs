using Collect.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Collect.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            var db = new CustomEntities();
            //var col = db.Creqs.ToList();
            var col = (from c in db.Creqs
                       where c.Status == "Assigned"
                       select c).ToList();
            return View(col);
        }
        [HttpGet]
        public ActionResult Complete(int id)
        {
            var db = new CustomEntities();
            var co = (from c in db.Creqs
                      where c.Id == id
                      select c).SingleOrDefault();
            if (co.Status == "Assigned")
            {
                return View(co);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }
        [HttpPost]
        public ActionResult Complete(Creq co)
        {
            try
            {
                var db = new CustomEntities();
                var ext = (from c in db.Creqs
                           where c.Id == co.Id
                           select c).SingleOrDefault();
                co.Status = "Completed";
                db.Entry(ext).CurrentValues.SetValues(co);
                db.SaveChanges();
                var ext2 = (from c in db.Clists
                            where c.Id == co.Id
                            select c).SingleOrDefault();
                var coll = ext2;
                coll.Status = "Completed";
                db.Entry(ext2).CurrentValues.SetValues(coll);
                db.SaveChanges();
                TempData["msg"] = "Completed!";
                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                TempData["msg"] = "Error!";
                return RedirectToAction("Index");
            }
            
        }
    }
}
