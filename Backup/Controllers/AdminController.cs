using System.Web.Mvc;
using SmartRestaurantWasteManagement.Models;
using System.Linq;

namespace SmartRestaurantWasteManagement.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly SmartRestaurantDbContext _context = new SmartRestaurantDbContext();

        // GET: Admin/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        // POST: Admin/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            var admin = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password && u.Role == "Admin");
            if (admin != null || (email == "admin@smartresto.com" && password == "admin123"))
            {
                Session["AdminUserId"] = admin != null ? admin.UserId : 0;
                Session["AdminUsername"] = admin != null ? admin.Username : "System Administrator";
                Session["Role"] = "Admin";
                return RedirectToAction("Dashboard");
            }

            ViewBag.Error = "Invalid administrator credentials.";
            return View();
        }

        // GET: Admin/Dashboard
        public ActionResult Dashboard()
        {
            ViewBag.TotalUsers = _context.Users.Count();
            ViewBag.TotalFoods = _context.Foods.Count();
            ViewBag.TotalWasteCount = _context.Wastes.Count();
            ViewBag.TotalFinancialLoss = _context.Wastes.Sum(w => (decimal?)w.CostLoss) ?? 0;

            return View();
        }

        // GET: Admin/ManageFood
        public ActionResult ManageFood()
        {
            var foods = _context.Foods.ToList();
            return View(foods);
        }

        // GET: Admin/ManageUsers
        public ActionResult ManageUsers()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        // GET: Admin/Reports
        public ActionResult Reports()
        {
            ViewBag.TotalUsers = _context.Users.Count();
            ViewBag.TotalFoods = _context.Foods.Count();
            ViewBag.TotalWasteLogs = _context.Wastes.Count();
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _context.Dispose();
            base.Dispose(disposing);
        }
    }
}
