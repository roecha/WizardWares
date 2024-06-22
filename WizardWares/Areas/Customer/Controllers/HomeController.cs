using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WizardWares.Models;
using WizardWares.DataAccess.Repositiory.IRepository;
using WizardWares.Models;
using WizardWares.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TomesNScrolls.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            // Create a view model to access both Products and Advertisements 
            HomeVM homeVM = new()
            {
                ProductList = _unitOfWork.Product.GetAll(includeProperties: "Category,Rarity"),
                AdList = _unitOfWork.Advertisement.GetAll()
            };

            return View(homeVM);
        }
        public IActionResult Details(int productId)
        {
            ShoppingCart cart = new()
            {
                Product = _unitOfWork.Product.Get(u => u.Id == productId, includeProperties: "Category,Rarity"),
                Count = 1,
                ProductId = productId
            };

            return View(cart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            // This is where the user id will be stored when a user logs in
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.UserId = userId;

            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.UserId == userId && u.ProductId == shoppingCart.ProductId);
            if (cartFromDb != null)
            {
                // Cart is already in the DB
                cartFromDb.Count += shoppingCart.Count + 1;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
            }
            else
            {
                shoppingCart.Count += 1;
                _unitOfWork.ShoppingCart.Add(shoppingCart);
            }

            TempData["success"] = "Cart Updated Successfully";
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}