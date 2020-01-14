using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BazarDeLaHess.Models;
using System.Net;
using System.Data.Entity;
using System.Threading.Tasks;

namespace BazarDeLaHess.Controllers
{
    public class HomeController : Controller
    {
        private readonly BazarDeLaHessEntities _db = new BazarDeLaHessEntities();
        protected void itemDetails(int i, System.EventArgs e)
        {
            Response.Redirect("/Home/Details/" + i);
        }
        // GET: Home
        public async Task<ActionResult> Index(string search)
        {
            HomeView hv = new HomeView() { Category = _db.MasterCategory.ToList() };
            
            if (!String.IsNullOrEmpty(search))
            {
                var items = (from m in _db.Item where m.name.Contains(search)
                             select m);
                if(items != null)
                {
                    hv.Item = items.ToList();
                    return View(hv);
                }
            }
            hv.Item = _db.Item.ToList();
            return View(hv);
        }

        public ActionResult Category(int? subcategory)
        {
            HomeView hv = new HomeView() { Category = _db.MasterCategory.ToList() };

            if(subcategory != null)
            {
                Category category = (from m in _db.Category where m.id_category == subcategory select m).First();
                if(category != null)
                {
                    hv.Item = category.Item.ToList();
                    return View("Index", hv);
                }
            }
            hv.Item = _db.Item.ToList();
            return View("Index", hv);
        }

        /****************************** Details / Creat / Edit / Remove ******************************/

        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Item item = _db.Item.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            ViewBag.categories = _db.Category.ToList();
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(Item itemToCreate, int category)
        {
            ModelState.Remove("id_item"); // This will remove the key 

            if (!ModelState.IsValid)
            {
                ViewBag.categories = _db.Category.ToList();
                return View();
            }

            try
            {
                //On ajoute à la collection
                _db.Item.Add(itemToCreate);
                //On sauvgarde
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.categories = _db.Category.ToList();
                return View();
            }
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var itemToEdit = (from i in _db.Item
                              where i.id_item == id
                              select i).First();
            if(itemToEdit == null)
            {
                return HttpNotFound();
            }

            return View(itemToEdit);
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(Item itemToEdit)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                // TODO: Add update logic here
                _db.Entry(itemToEdit).State = EntityState.Modified;
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Item item = _db.Item.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            return View();
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                Item itemToEdit = _db.Item.Find(id);
                _db.Entry(itemToEdit).State = EntityState.Modified;
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
