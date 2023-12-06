using Microsoft.AspNetCore.Mvc;
using Sing_Char_Zubakov.Models;
using Sing_Char_Zubakov.Data;
using System.Diagnostics;
using System.Xml.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace Sing_Char_Zubakov.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ChatingContext _context;

        public HomeController(ILogger<HomeController> logger, ChatingContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(string name)
        {
           var user =  _context.Users.FirstOrDefault(x => x.Name == name);

            if (user == null)
            {
                user = new User
                {
                    Name = name,
                    Role = UserRole.User
                };

                _context.Users.Add(user);
                _context.SaveChanges();
            }
            if (user.Role == UserRole.Banned)
            {
              

                ModelState.AddModelError("userBannes", "Пользователь заблокирован");
                return View("Index", GetMessages());

            }

            var role = user.Role.ToString();


            var claims = new List<Claim> {
              new Claim("Id", user.UserId.ToString()),
              new Claim(ClaimTypes.Name, user.Name),
              new Claim(ClaimTypes.Role,role )

            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

           

            return RedirectToAction("Index");

        }

        public IActionResult Index()
        {
         
            return View(GetMessages());
        }
       



        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private List<Mess> GetMessages()
        { 
              var messaages = _context.Mess
               .Include(p => p.User)
              .OrderByDescending(x => x.Id).Take(50).ToList();

            return messaages;
        }
    }
}