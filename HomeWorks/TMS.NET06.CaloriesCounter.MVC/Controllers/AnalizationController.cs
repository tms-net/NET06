using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.NET06.CaloriesCounter.MVC.Models;

namespace TMS.NET06.CaloriesCounter.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AnalizationController : Controller
    {
        [HttpGet]
        [ActionName("Index")]
        public ActionResult GetIndex()
        {
            var list = TempData["DataBetweenRequests"];
            if (list != null)
                return View(list as IList<ProductRow>);
            return View();
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult PostIndex(IList<ProductRow> entries)
        {
            entries.Add(new ProductRow());
            TempData["DataBetweenRequests"] = entries.ToArray();
            return Redirect("Index");
            //return View(entries);
        }

        [HttpPost]
        public ActionResult DeleteLastRow(IList<ProductRow> entries)
        {
            if (entries.Count > 0)
                entries.RemoveAt(entries.Count - 1);

            return Redirect("Index");
            //return View("Index", entries);
        }

        // GET: AnalizationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AnalizationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AnalizationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AnalizationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AnalizationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AnalizationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AnalizationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
