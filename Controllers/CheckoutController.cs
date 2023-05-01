using Microsoft.AspNetCore.Mvc;
using ProjectActivity.Helpers;
using ProjectActivity.Models;

namespace ProjectActivity.Controllers
{
    public class CheckoutController : Controller
    {
        private ApplicationDbContext _context;
        public CheckoutController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.isLogged = HttpContext.Session.GetString("islogged");
            ViewBag.Role = HttpContext.Session.GetString("UserRole");
            var cart = SessionHelper.getObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.count = cart.Count;
            ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
            return View();
        }
        public IActionResult Success() {
            ViewBag.isLogged = HttpContext.Session.GetString("islogged");
            ViewBag.Role = HttpContext.Session.GetString("UserRole");
            //RemoveAll();
            return View();
        
        }
        public IActionResult RemoveAll()
        {

            List<Item> cart = SessionHelper.getObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            for (int i = 0; i <=cart.Count; i++)
            {
                cart.RemoveAt(i);
            }
            SessionHelper.setObjectAsJson(HttpContext.Session, "cart", cart);
            return View();

        }
    }
}
