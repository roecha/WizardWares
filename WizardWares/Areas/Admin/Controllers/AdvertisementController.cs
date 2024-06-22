using WizardWares.DataAccess.Repository.IRepository;
using WizardWares.DataAccess.Data;
using WizardWares.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WizardWares.Models.ViewModels;
using Microsoft.Identity.Client;
using WizardWares.DataAccess.Repositiory.IRepository;

namespace WizardWares.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdvertisementController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        // This is used to access the wwwroot images folder 
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdvertisementController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            // The ability to include product in GetAll is implemented in repository file
            List<Advertisement> objAdvertisementList = _unitOfWork.Advertisement.GetAll(includeProperties:"Product").ToList();
            return View(objAdvertisementList);
        }

        public IActionResult Upsert(int? id)
        {
            // This class handles both updating and creating (inserting) advertisement objects
            AdvertisementVM advertisementVM = new()
            {
                ProductList = _unitOfWork.Product.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),

                Advertisement = new Advertisement()
            };

            if (id == null || id == 0)
            {
                // The object does not exist and needs to be created 
                return View(advertisementVM);
            }
            else
            {
                // The object needs to be edited
                advertisementVM.Advertisement = _unitOfWork.Advertisement.Get(u => u.Id == id);
                return View(advertisementVM);
            }
        }


        [HttpPost]
        public IActionResult Upsert(AdvertisementVM advertisementVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                // Getting the path to the wwwroot folder
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    // path to wwwroot/images/advertisement
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string advertisementPath = Path.Combine(wwwRootPath, @"images\advertisement");

                    if (!string.IsNullOrEmpty(advertisementVM.Advertisement.ImageUrl))
                    {
                        //delete the old image
                        var oldImagePath =
                            Path.Combine(wwwRootPath, advertisementVM.Advertisement.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // Add the image to the folder
                    using (var fileStream = new FileStream(Path.Combine(advertisementPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    // Add image url to Advertisement Object
                    advertisementVM.Advertisement.ImageUrl = @"\images\advertisement\" + fileName;
                }

                if (advertisementVM.Advertisement.Id == 0)
                {
                    // Need to add new advertisement to DB
                    _unitOfWork.Advertisement.Add(advertisementVM.Advertisement);
                }
                else
                {
                    // Need to update advertisement in DB
                    _unitOfWork.Advertisement.Update(advertisementVM.Advertisement);
                }

                _unitOfWork.Save();
                TempData["success"] = "Advertisement created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                advertisementVM.ProductList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(advertisementVM);
            }
        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Advertisement> objAdList = _unitOfWork.Advertisement.GetAll(includeProperties: "Product").ToList();
            return Json(new { data = objAdList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var adToBeDeleted = _unitOfWork.Advertisement.Get(u => u.Id == id);
            if (adToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            // Need to delete the image from wwwroot image folder
            var oldImagePath =
                           Path.Combine(_webHostEnvironment.WebRootPath,
                           adToBeDeleted.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Advertisement.Remove(adToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
