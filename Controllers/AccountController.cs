using Microsoft.AspNetCore.Mvc;
using ProjectActivity.Models;

namespace ProjectActivity.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationDbContext _context;
        public AccountController(ApplicationDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            var users=_context.Users.ToList();
            return View(users);
        }
        public IActionResult Login()
        {
            HttpContext.Session.SetString("islogged", "false");
            return View();
        }
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            
            var user =_context.Users.SingleOrDefault(u=>u.UserName == username);
            
            if (username != null && password != null)
            {
                if (username.Equals(user.UserName) && password.Equals(user.Password))
                {
                    HttpContext.Session.SetString("username", username);
                    HttpContext.Session.SetString("islogged", "true");
                    ViewBag.isLogged = HttpContext.Session.GetString("islogged");
                    HttpContext.Session.SetString("UserRole", user.Role);
                    
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ViewBag.Error = "Invalid Credentials";
                    return View("Login");
                }
            }
            ViewBag.Error = "please enter your credentials";
            return View("Login");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
          
            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Login");
            
        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            HttpContext.Session.SetString("islogged", "false");
            ViewBag.isLogged = HttpContext.Session.GetString("islogged");
            return RedirectToAction("Index","Home");
        }
    }
}
