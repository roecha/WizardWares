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

        public IActionResult Create(Rarity obj)
        {
            if (obj.Name == obj.ValueOrder.ToString())
            {
                ModelState.AddModelError("name", "The ValueOrder cannot exactly match the Name");
            }
            if (obj.Name == "test")
            {
                ModelState.AddModelError("", "Test is an invalid value");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Rarity.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Rarity hath been conjured forth with success!";
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
            Rarity? rarityFromDb = _unitOfWork.Rarity.Get(u => u.Id == id);

            if (rarityFromDb == null)
            {
                return NotFound();
            }
            return View(rarityFromDb);
        }


        [HttpPost]
        public IActionResult Edit(Rarity obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Rarity.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Thy rarity hath been amended";
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
            Rarity? rarityFromDb = _unitOfWork.Rarity.Get(u => u.Id == id);

            if (rarityFromDb == null)
            {
                return NotFound();
            }
            return View(rarityFromDb);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Rarity? obj = _unitOfWork.Rarity.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Rarity.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Gone from the realms of our store, thy rarity hath vanished.";
            return RedirectToAction("Index");
        }

    }
}
