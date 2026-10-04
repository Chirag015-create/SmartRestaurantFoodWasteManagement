using System;
using System.Web.Mvc;
using SmartRestaurantWasteManagement.Models;
using System.Linq;

namespace SmartRestaurantWasteManagement.Controllers
{
    public class InventoryController : Controller
    {
        private readonly SmartRestaurantDbContext _context = new SmartRestaurantDbContext();

        public ActionResult Index()
        {
            var inventory = _context.Inventories.OrderBy(i => i.ItemName).ToList();
            return View(inventory);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inventory item)
        {
            if (ModelState.IsValid)
            {
                item.LastUpdated = DateTime.Now;
                _context.Inventories.Add(item);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _context.Dispose();
            base.Dispose(disposing);
        }
    }
}
