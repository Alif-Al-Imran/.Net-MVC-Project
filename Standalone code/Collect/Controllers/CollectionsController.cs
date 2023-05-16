using Collect.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Collect.Controllers
{
    public class CollectionsController : Controller
    {
        // GET: Collections
        public ActionResult Index()
        {
            var db = new CustomEntities();
            var col = db.Clists.ToList();
            return View(col);
        }
    }
}