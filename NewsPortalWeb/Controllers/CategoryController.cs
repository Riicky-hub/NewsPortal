using Microsoft.AspNetCore.Mvc;
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
            if(Validation.CategoryNameExists(objCategoryList, obj.Name))
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
                return RedirectToAction("Index", "Category");
            }
            return View();
        }
    }
}
