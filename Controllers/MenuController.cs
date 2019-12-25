using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BazarDeLaHess.Models;

namespace BazarDeLaHess.Controllers
{
    public class MenuController : Controller
    {

        private BazarDeLaHessEntities _db = new BazarDeLaHessEntities();
        // GET: Menu
        [ChildActionOnly]
        public ActionResult Header()
        {
            var categories = (from m in _db.Category
                         select m);
            return View(categories);
        }
    }
}