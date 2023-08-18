using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsPortal.DataAccess.Data;
using NewsPortal.DataAccess.Repository.IRepository;
using NewsPortal.Models;
using NewsPortal.Utility;

namespace NewsPortalWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View(new Category());
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
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
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
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
            Category? category = _unitOfWork.Category.Get(u=>u.Id==id);
            if(category==null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll(false).ToList();
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
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
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
            Category? category = _unitOfWork.Category.Get(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? category = _unitOfWork.Category.Get(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Delete(category);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index", "Category");
        }
    }
}
