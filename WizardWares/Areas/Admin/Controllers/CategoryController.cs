using Microsoft.AspNetCore.Mvc;
using WizardWares.DataAccess.Repositiory;
using WizardWares.DataAccess.Repositiory.IRepository;
using WizardWares.Models;

namespace WizardWares.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
       // private readonly IUnitOfWork _unitOfWork;
        public CategoryController()
        {
          //  _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            // Use entitiy framework core to retrieve the list
            
            return View();
        }
    }
}
