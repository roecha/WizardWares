using Microsoft.AspNetCore.Mvc;
using WizardWares.DataAccess.Repositiory;
using WizardWares.DataAccess.Repositiory.IRepository;
using WizardWares.Models;

namespace WizardWares.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RarityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public RarityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            // Use entitiy framework core to retrieve the list
            List<Rarity> rarityList = _unitOfWork.Rarity.GetAll().ToList();
            return View(rarityList);
        }

        /* This class either updates or inserts objects in the rarity database */
        public IActionResult Upsert(int? id)
        {
            Rarity rarityObj = new Rarity();

            if (id == null || id == 0)
            {
                // If there is no ID, then we need to create an object
                return View(rarityObj);
            }
            else
            {
                // If there is an ID then we need to edit the object
                rarityObj = _unitOfWork.Rarity.Get(u => u.Id == id);
                return View(rarityObj);
            }


        }

        [HttpPost]
        public IActionResult Upsert(Rarity rarity)
        {
            if (ModelState.IsValid)
            {

                if (rarity.Id == 0)
                {
                    // Add new rarity to DB
                    _unitOfWork.Rarity.Add(rarity);
                }
                else
                {
                    // Add updated rarity to DB
                    _unitOfWork.Rarity.Update(rarity);
                }

                _unitOfWork.Save();
                TempData["success"] = "Rarity created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(rarity);
            }
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Rarity> objRarityList = _unitOfWork.Rarity.GetAll().ToList();
            return Json(new { data = objRarityList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var rarityToBeDeleted = _unitOfWork.Rarity.Get(u => u.Id == id);
            if (rarityToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Rarity.Remove(rarityToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion

    }
}
