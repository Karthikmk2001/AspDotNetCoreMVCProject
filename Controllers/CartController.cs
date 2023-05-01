using Microsoft.AspNetCore.Mvc;
using ProjectActivity.Models;

using ProjectActivity.Helpers;

namespace ProjectActivity.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext _context;
        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {


            ViewBag.isLogged = HttpContext.Session.GetString("islogged");
            ViewBag.Role = HttpContext.Session.GetString("UserRole");
            var cart = SessionHelper.getObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int count=cart.Count;
            
                ViewBag.cart = cart;

                ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
                return View();
            
            




        }
        public ActionResult CartEmpty() {
            ViewBag.isLogged = HttpContext.Session.GetString("islogged");
            ViewBag.Role = HttpContext.Session.GetString("UserRole");
            return View();
        }
        public IActionResult Buy(int id)
        {
            if (SessionHelper.getObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item() { Product = _context.Products.SingleOrDefault(p => p.ProductId == id), Quantity = 1 });
                SessionHelper.setObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Item> cart = SessionHelper.getObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = isExists(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item() { Product = _context.Products.SingleOrDefault(p => p.ProductId == id), Quantity = 1 });

                }
                SessionHelper.setObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");


        }
        public int isExists(int id)
        {
            List<Item> cart = SessionHelper.getObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.ProductId == id)
                {
                    return i;
                }

            }
            return -1;
        }
        public IActionResult Remove(int id)
        {
            
            List<Item> cart = SessionHelper.getObjectFromJson<List<Item>>(HttpContext.Session, "cart");
           
                int index = isExists(id);
                cart.RemoveAt(index);

                SessionHelper.setObjectAsJson(HttpContext.Session, "cart", cart);
                return RedirectToAction("Index");
           
        }
       

    }       
}
