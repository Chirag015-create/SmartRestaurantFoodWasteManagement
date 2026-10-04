using System.Web.Mvc;
using SmartRestaurantWasteManagement.Models;
using System.Linq;
using System.Net;

namespace SmartRestaurantWasteManagement.Controllers
{
    public class FoodController : Controller
    {
        private readonly SmartRestaurantDbContext _context = new SmartRestaurantDbContext();

        // GET: Food
        public ActionResult Index()
        {
            return View(_context.Foods.ToList());
        }

        // GET: Food/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Food food = _context.Foods.Find(id);
            if (food == null) return HttpNotFound();
            return View(food);
        }

        // GET: Food/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Food/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FoodId,Name,Category,Price,Unit,ShelfLifeDays,Description,IsActive")] Food food)
        {
            if (ModelState.IsValid)
            {
                _context.Foods.Add(food);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(food);
        }

        // GET: Food/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Food food = _context.Foods.Find(id);
            if (food == null) return HttpNotFound();
            return View(food);
        }

        // POST: Food/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FoodId,Name,Category,Price,Unit,ShelfLifeDays,Description,IsActive,CreatedAt")] Food food)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(food).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(food);
        }

        // GET: Food/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Food food = _context.Foods.Find(id);
            if (food == null) return HttpNotFound();
            return View(food);
        }

        // POST: Food/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Food food = _context.Foods.Find(id);
            _context.Foods.Remove(food);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _context.Dispose();
            base.Dispose(disposing);
        }
    }
}
