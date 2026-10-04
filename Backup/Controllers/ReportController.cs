using System.Web.Mvc;
using SmartRestaurantWasteManagement.Models;
using System.Linq;

namespace SmartRestaurantWasteManagement.Controllers
{
    public class ReportController : Controller
    {
        private readonly SmartRestaurantDbContext _context = new SmartRestaurantDbContext();

        public ActionResult Index()
        {
            ViewBag.TotalWasteKg = _context.Wastes.Sum(w => (double?)w.QuantityWasted) ?? 0;
            ViewBag.TotalLossAmount = _context.Wastes.Sum(w => (decimal?)w.CostLoss) ?? 0;
            ViewBag.TotalItemsTracked = _context.Foods.Count();

            var topWasted = _context.Wastes
                .GroupBy(w => w.Food.Name)
                .Select(g => new { FoodName = g.Key, TotalQty = g.Sum(x => x.QuantityWasted) })
                .OrderByDescending(x => x.TotalQty)
                .Take(5)
                .ToList();

            ViewBag.TopWasted = topWasted;
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _context.Dispose();
            base.Dispose(disposing);
        }
    }
}
