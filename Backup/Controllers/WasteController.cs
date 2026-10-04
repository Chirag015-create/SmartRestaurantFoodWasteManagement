using System;
using System.Web.Mvc;
using SmartRestaurantWasteManagement.Models;
using System.Linq;
using System.Data.Entity;

namespace SmartRestaurantWasteManagement.Controllers
{
    public class WasteController : Controller
    {
        private readonly SmartRestaurantDbContext _context = new SmartRestaurantDbContext();

        public ActionResult Index()
        {
            var wastes = _context.Wastes.Include(w => w.Food).OrderByDescending(w => w.LoggedDate).ToList();
            return View(wastes);
        }

        public ActionResult Create()
        {
            ViewBag.FoodId = new SelectList(_context.Foods, "FoodId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Waste waste)
        {
            if (ModelState.IsValid)
            {
                var food = _context.Foods.Find(waste.FoodId);
                if (food != null)
                {
                    waste.CostLoss = (decimal)waste.QuantityWasted * food.Price;
                }
                waste.LoggedDate = DateTime.Now;
                _context.Wastes.Add(waste);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FoodId = new SelectList(_context.Foods, "FoodId", "Name", waste.FoodId);
            return View(waste);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _context.Dispose();
            base.Dispose(disposing);
        }
    }
}
