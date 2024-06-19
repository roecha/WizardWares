using Microsoft.AspNetCore.Mvc;
using WizardWares.DataAccess.Repositiory;
using WizardWares.DataAccess.Repositiory.IRepository;
using WizardWares.Models;

namespace WizardWares.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            // initialize a unit of work
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            // Entitiy framework uses unitOfWork to retrieve the list of category objects
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Upsert(Category obj)
        {
            // Adding some tests to category name restrictions
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
            }
            if (obj.Name == "test")
            {
                ModelState.AddModelError("", "Test is an invalid value");
            }
            if (ModelState.IsValid)
            {
                // Add category object to database
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                // Toastr notification
                TempData["success"] = "Category hath been conjured forth with success!";
                // Return to index
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }


        [HttpPost]
        public IActionResult Edit(Category obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Thy category hath been amended";
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
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _unitOfWork.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Gone from the realms of our store, thy category hath vanished.";
            return RedirectToAction("Index");
        }

    }
}
