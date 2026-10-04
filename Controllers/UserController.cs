using System;
using System.Web.Mvc;
using SmartRestaurantWasteManagement.Models;
using System.Linq;
using System.Data.Entity;

namespace SmartRestaurantWasteManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly SmartRestaurantDbContext _context = new SmartRestaurantDbContext();

        // GET: User/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (Session["UserId"] != null)
                return RedirectToAction("Dashboard");
            return View();
        }

        // POST: User/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                Session["UserId"] = user.UserId;
                Session["Username"] = user.Username;
                Session["Role"] = user.Role ?? "User";
                return RedirectToAction("Dashboard");
            }
            ViewBag.Error = "Invalid email or password.";
            return View();
        }

        // GET: User/Logout
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "User");
        }

        // GET: User/Dashboard
        public ActionResult Dashboard()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login");

            ViewBag.TotalFoodCount = _context.Foods.Count();
            ViewBag.DailyPrepared = _context.DailyFoods.Sum(d => (double?)d.QuantityPrepared) ?? 0;
            ViewBag.TotalWasted = _context.Wastes.Sum(w => (double?)w.QuantityWasted) ?? 0;
            ViewBag.LowStockItems = _context.Inventories.Count(i => i.CurrentStock <= i.MinimumThreshold);

            var recentWaste = _context.Wastes
                                      .Include(w => w.Food)
                                      .OrderByDescending(w => w.LoggedDate)
                                      .Take(5)
                                      .ToList();

            return View(recentWaste);
        }

        // GET: User/Profile
        public ActionResult Profile()
        {
            if (Session["UserId"] == null) return RedirectToAction("Login");

            int userId = (int)Session["UserId"];
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                user = new User
                {
                    Username = Session["Username"] as string ?? "Staff Member",
                    Email = "staff@restaurant.com",
                    Role = "User",
                    RestaurantName = "Smart Gourmet Resto"
                };
            }
            return View(user);
        }

        // GET: User/Menu
        public ActionResult Menu()
        {
            if (Session["UserId"] == null) return RedirectToAction("Login");
            return View(_context.Foods.Where(f => f.IsActive).ToList());
        }

        // GET: User/AddFood
        public ActionResult AddFood()
        {
            if (Session["UserId"] == null) return RedirectToAction("Login");
            return View();
        }

        // POST: User/AddFood
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddFood(Food food)
        {
            if (Session["UserId"] == null) return RedirectToAction("Login");

            if (ModelState.IsValid)
            {
                _context.Foods.Add(food);
                _context.SaveChanges();
                return RedirectToAction("FoodList");
            }
            return View(food);
        }

        // GET: User/FoodList
        public ActionResult FoodList()
        {
            if (Session["UserId"] == null) return RedirectToAction("Login");
            return View(_context.Foods.ToList());
        }

        // GET: User/DailyFood
        public ActionResult DailyFood()
        {
            if (Session["UserId"] == null) return RedirectToAction("Login");

            ViewBag.Foods = new SelectList(_context.Foods.ToList(), "FoodId", "Name");
            var dailyList = _context.DailyFoods.Include(d => d.Food)
                                               .OrderByDescending(d => d.PrepDate)
                                               .ToList();
            return View(dailyList);
        }

        // POST: User/DailyFood
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DailyFood(DailyFood dailyFood)
        {
            if (Session["UserId"] == null) return RedirectToAction("Login");

            if (ModelState.IsValid)
            {
                dailyFood.QuantityLeft = dailyFood.QuantityPrepared - dailyFood.QuantitySold;
                _context.DailyFoods.Add(dailyFood);
                _context.SaveChanges();
                return RedirectToAction("DailyFood");
            }
            ViewBag.Foods = new SelectList(_context.Foods.ToList(), "FoodId", "Name", dailyFood.FoodId);
            return View(_context.DailyFoods.Include(d => d.Food).ToList());
        }

        // GET: User/WasteEntry
        public ActionResult WasteEntry()
        {
            if (Session["UserId"] == null) return RedirectToAction("Login");
            ViewBag.Foods = new SelectList(_context.Foods.ToList(), "FoodId", "Name");
            return View();
        }

        // POST: User/WasteEntry
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WasteEntry(Waste waste)
        {
            if (Session["UserId"] == null) return RedirectToAction("Login");

            if (ModelState.IsValid)
            {
                var food = _context.Foods.Find(waste.FoodId);
                if (food != null)
                    waste.CostLoss = (decimal)waste.QuantityWasted * food.Price;

                waste.LoggedDate = DateTime.Now;
                _context.Wastes.Add(waste);
                _context.SaveChanges();
                return RedirectToAction("WasteHistory");
            }
            ViewBag.Foods = new SelectList(_context.Foods.ToList(), "FoodId", "Name", waste.FoodId);
            return View(waste);
        }

        // GET: User/WasteHistory
        public ActionResult WasteHistory()
        {
            if (Session["UserId"] == null) return RedirectToAction("Login");
            return View(_context.Wastes.Include(w => w.Food)
                                       .OrderByDescending(w => w.LoggedDate)
                                       .ToList());
        }

        // GET: User/Prediction
        public ActionResult Prediction()
        {
            if (Session["UserId"] == null) return RedirectToAction("Login");
            return View(_context.Predictions.Include(p => p.Food)
                                            .OrderByDescending(p => p.PredictionDate)
                                            .ToList());
        }

        // GET: User/Inventory
        public ActionResult Inventory()
        {
            if (Session["UserId"] == null) return RedirectToAction("Login");
            return View(_context.Inventories.OrderBy(i => i.ItemName).ToList());
        }

        // GET: User/Donation
        public ActionResult Donation()
        {
            if (Session["UserId"] == null) return RedirectToAction("Login");
            return View();
        }

        // GET: User/Reports
        public ActionResult Reports()
        {
            if (Session["UserId"] == null) return RedirectToAction("Login");

            ViewBag.TotalWasteKg = _context.Wastes.Sum(w => (double?)w.QuantityWasted) ?? 0;
            ViewBag.TotalLossAmount = _context.Wastes.Sum(w => (decimal?)w.CostLoss) ?? 0;
            ViewBag.TotalItemsTracked = _context.Foods.Count();
            return View();
        }

        // GET: User/Notifications
        public ActionResult Notifications()
        {
            if (Session["UserId"] == null) return RedirectToAction("Login");
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _context.Dispose();
            base.Dispose(disposing);
        }
    }
}