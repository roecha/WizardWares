using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using WizardWares.DataAccess.Repositiory;
using WizardWares.DataAccess.Repositiory.IRepository;
using WizardWares.Models;
using WizardWares.Models.ViewModels;

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

        /* This class either updates or inserts objects in the category database */
        public IActionResult Upsert(int? id)
        {
            Category categoryObj = new Category();

            if (id == null || id == 0)
            {
                // If there is no ID, then we need to create an object
                return View(categoryObj);
            }
            else
            {
                // If there is an ID then we need to edit the object
                categoryObj = _unitOfWork.Category.Get(u => u.Id == id);
                return View(categoryObj);
            }


        }

        [HttpPost]
        public IActionResult Upsert(Category category)
        {
            if (ModelState.IsValid)
            {

                if (category.Id == 0)
                {
                    _unitOfWork.Category.Add(category);
                }
                else
                {
                    _unitOfWork.Category.Update(category);
                }

                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return Json(new { data = objCategoryList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var categoryToBeDeleted = _unitOfWork.Category.Get(u => u.Id == id);
            if (categoryToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Category.Remove(categoryToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

#endregion

    }
}
