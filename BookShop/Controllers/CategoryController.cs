using BookShop.DataAccess.Data;
using BookShop.DataAccess.IRepository;
using BookShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository db)
        {
            _categoryRepo = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _categoryRepo.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if(category.Name==category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("DisplayOrder", "Display Order must be different from Category Name");
            }
            //if(category.Name!=null && category.Name.ToLower()=="test")
            //{
            //       ModelState.AddModelError("", "Test is an invalid value");
            //}
            if (ModelState.IsValid)
            {
                _categoryRepo.Add(category);
                 _categoryRepo.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id) 
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDB = _categoryRepo.Get(u=>u.Id==id);
            if(categoryFromDB == null)
            {
                return NotFound();
            }
            return View(categoryFromDB);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepo.Update(category);
                _categoryRepo.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDB = _categoryRepo.Get(u => u.Id == id);
            if (categoryFromDB == null)
            {
                return NotFound();
            }
            return View(categoryFromDB);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj= _categoryRepo.Get(u => u.Id == id);
            if (obj==null)
            {
                return NotFound();
            }
            _categoryRepo.Remove(obj);
            _categoryRepo.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
            
        }
    }
}
