using Collect.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace Collect.Controllers
{
    public class NGOController : Controller
    {
        // GET: NGO
        public ActionResult Index()
        {
            var db = new CustomEntities();
            var col = db.Creqs.ToList();
            
            return View(col);
        }
        [HttpGet]
        public ActionResult Accept(int id)
        {
            var db = new CustomEntities();
            var co = (from c in db.Creqs
                      where c.Id == id
                      select c).SingleOrDefault();
            if (co.Status == "Pending")
            {
                return View(co);
            }
            else
            {
                TempData["msg"] = "Already accepted!";
                return RedirectToAction("Index");
            }

        }
        [HttpPost]
        public ActionResult Accept(Creq co)
        {
            try
            {
                var db = new CustomEntities();
            var ext = (from c in db.Creqs
                       where c.Id == co.Id
                       select c).SingleOrDefault();
            co.Status = "Assigned";
            var e = Convert.ToInt32(Request["EmpID"]);
            CE ce = new CE();
            ce.eid = e;
            ce.cid = co.Id;
            db.CEs.Add(ce);
            db.SaveChanges();
            var en = (from c in db.Emps
                      where c.Id == e
                      select c.Ename).SingleOrDefault();
            db.Entry(ext).CurrentValues.SetValues(co);
            db.SaveChanges();
            var ext2 = (from c in db.Clists
                       where c.Id == co.Id
                       select c).SingleOrDefault();
            var coll = ext2;
            coll.Status = "Assigned";
            coll.Ename = en;
            db.Entry(ext2).CurrentValues.SetValues(coll);
            db.SaveChanges();
            TempData["msg"] = "Accepted!";
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