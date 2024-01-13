using ChatApp.Models;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChatApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MychatappDbContext _context;

        public HomeController(ILogger<HomeController> logger, MychatappDbContext context)
        {
            _logger = logger;
            _context = context; 
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(TblUser user)
        {
            if(ModelState.IsValid)
            {
                user.CreatedAt = DateTime.Now;
                user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(user.HashedPassword);
                _context.Add(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
