using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsPortal.DataAccess;
using NewsPortal.Models;
using NewsPortal.Utility;

namespace NewsPortalWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View(new Category());
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            if(Validation.CategoryNameExists(objCategoryList, obj.Name, obj.Id))
            {
                ModelState.AddModelError("Name", "Category name already exists");
            }
            if (Validation.IsStringANumber(obj.Name))
            {
                ModelState.AddModelError("Name", "Category's name cannot be a number.");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if(id==null || id==0)
            {
                return NotFound();
            }
            Category? category = _db.Categories.Find(id);
            if(category==null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            List<Category> objCategoryList = _db.Categories.AsNoTracking().ToList();
            if (Validation.CategoryNameExists(objCategoryList, obj.Name, obj.Id))
            {
                ModelState.AddModelError("Name", "Category name already exists");
            }
            if (Validation.IsStringANumber(obj.Name))
            {
                ModelState.AddModelError("Name", "Category's name cannot be a number.");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category edited successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _db.Categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index", "Category");
        }
    }
}
