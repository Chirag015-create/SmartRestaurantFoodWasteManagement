using System.Web.Mvc;
using SmartRestaurantWasteManagement.Models;
using System.Linq;

namespace SmartRestaurantWasteManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly SmartRestaurantDbContext _context = new SmartRestaurantDbContext();

        public ActionResult Index()
        {
            ViewBag.TotalFoods = _context.Foods.Count();
            ViewBag.TotalWasteLogs = _context.Wastes.Count();
            ViewBag.TotalInventory = _context.Inventories.Count();
            ViewBag.ActiveUsers = _context.Users.Count();
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
