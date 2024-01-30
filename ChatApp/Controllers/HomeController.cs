using ChatApp.Models;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MychatappDbContext db;
        private readonly IHubContext<ChatHub> _chathubContext;
        public HomeController(ILogger<HomeController> logger, MychatappDbContext context, IHubContext<ChatHub> chathubContext)
        {
            _logger = logger;
            db = context;
            _chathubContext = chathubContext;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                ViewBag.Username = HttpContext.Session.GetString("Username");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(TblUser Login)
        {

            var user = db.TblUsers.FirstOrDefault(u => u.Username == Login.Username);

            if(user == null)
            {
                ViewBag.Error = "Incorrect username or password";
                return View();
            }
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(Login.HashedPassword, user.HashedPassword);

                if(isValidPassword == true && user != null)
                {
                    HttpContext.Session.SetString("Username", Login.Username);
                    return RedirectToAction("Index", "Home");
                } else
                {
                    ViewBag.Error = "Incorrect username or password";
                }
            return View(user);
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
                db.Add(user);
                await db.SaveChangesAsync();

                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(string Message)
        {
            _chathubContext.Clients.All.SendAsync("ReceiveMessage", "Username", Message);
            return Json(new { success = true }); 
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
