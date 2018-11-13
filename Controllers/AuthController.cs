using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeddingPlanner.Data;
using WeddingPlanner.Models;
using WeddingPlanner.ViewModels;

namespace WeddingPlanner.Controllers
{
    public class AuthController : Controller
    {
        private readonly DataContext _context;

        public AuthController (DataContext context)
        {
            _context = context;
        }

        [HttpGet ("")]
        public IActionResult Index ()
        {
            return View ();
        }

        [HttpPost ("register")]
        public IActionResult Registration (RegistrationViewModel reg)
        {
            if (!ModelState.IsValid)
                return View ("Index");

            User userInDB = _context.Users
                .FirstOrDefault (u => u.EmailAddress == reg.EmailAddress);

            if (userInDB != null)
            {
                ModelState.AddModelError ("EmailAddress", "User already exists");
                return View ("Index");
            }

            PasswordHasher<RegistrationViewModel> hasher = new PasswordHasher<RegistrationViewModel> ();

            string hashedPW = hasher.HashPassword (reg, reg.Password);
            User newUser = new User
            {
                FirstName = reg.FirstName,
                LastName = reg.LastName,
                EmailAddress = reg.EmailAddress,
                Password = hashedPW
            };

            _context.Users.Add (newUser);
            _context.SaveChanges ();

            User loggedIn = _context.Users
                .FirstOrDefault (u => u.EmailAddress == reg.EmailAddress);

            HttpContext.Session.SetInt32 ("userId", loggedIn.UserId);
            HttpContext.Session.SetString ("userName", loggedIn.FirstName);

            return RedirectToAction ("Index", "Wedding");
        }

        [HttpPost ("login")]
        public IActionResult Login (LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View ("Index");
            }
            User userInDb = _context.Users.FirstOrDefault (u => u.EmailAddress == login.LoginEmail);

            if (userInDb is null)
            {
                ModelState.AddModelError ("LoginEmail", "Invalid Login");
                return View ("Index");
            }

            PasswordHasher<LoginViewModel> hasher = new PasswordHasher<LoginViewModel> ();

            var result = hasher.VerifyHashedPassword (login, userInDb.Password, login.LoginPassword);

            if (result == 0)
            {
                ModelState.AddModelError ("LoginEmail", "Invalid Login");
                return View ("Index");
            }

            User loggedIn = _context.Users
                .FirstOrDefault (u => u.EmailAddress == login.LoginEmail);

            HttpContext.Session.SetInt32 ("userId", loggedIn.UserId);
            HttpContext.Session.SetString ("userName", loggedIn.FirstName);

            return RedirectToAction ("Index", "Wedding");
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}