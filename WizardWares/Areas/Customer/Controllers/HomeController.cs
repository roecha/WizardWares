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

        public IActionResult Index(string sortOrder)
        {
            
            // Create a view model to access both Products and Advertisements 
            HomeVM homeVM = new()
            {
                ProductList = _unitOfWork.Product.GetAll(includeProperties: "Category,Rarity"),
                AdList = RandomPermutation(_unitOfWork.Advertisement.GetAll()),
                SortOrder = sortOrder
            };

            switch (sortOrder)
            {
                case "price_desc":
                    homeVM.ProductList = homeVM.ProductList.OrderByDescending(i => i.Price);
                    break;
                case "price_asc":
                    homeVM.ProductList = homeVM.ProductList.OrderBy(i => i.Price);
                    break;
                case "rarity_desc":
                    homeVM.ProductList = homeVM.ProductList.OrderByDescending(i => i.Rarity.ValueOrder);
                    break;
                default:
                    homeVM.ProductList = homeVM.ProductList.OrderBy(i => i.Rarity.ValueOrder);
                    break;
            }

            return View(homeVM);
        }
        public IActionResult Details(int productId)
        {

            HomeVM homeVM = new()
            {
                ProductList = _unitOfWork.Product.GetAll(includeProperties: "Category,Rarity"),
                AdList = RandomPermutation(_unitOfWork.Advertisement.GetAll()),
                ShoppingCart = new()
                {
                    Product = _unitOfWork.Product.Get(u => u.Id == productId, includeProperties: "Category,Rarity"),
                    Count = 1,
                    ProductId = productId
                }
        };
            

             return View(homeVM);
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


        /* This Method Randomizes my advertisements (stolen from 
         * https://stackoverflow.com/questions/5807128/an-extension-method-on-ienumerable-needed-for-shuffling)
         * */
        static Random random = new Random();
        public static IEnumerable<T> RandomPermutation<T>(IEnumerable<T> sequence)
        {
            T[] retArray = sequence.ToArray();
            for (int i = 0; i < retArray.Length - 1; i += 1)
            {
                int swapIndex = random.Next(i, retArray.Length);
                if (swapIndex != i)
                {
                    T temp = retArray[i];
                    retArray[i] = retArray[swapIndex];
                    retArray[swapIndex] = temp;
                }
            }
            return retArray;
        }

    }
}
