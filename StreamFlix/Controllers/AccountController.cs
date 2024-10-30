using Microsoft.AspNetCore.Mvc;
using StreamFlix.Data;
using StreamFlix.Models;

namespace StreamFlix.Controllers
{
    public class AccountController : Controller
    {
        private readonly StreamFlixDbContext _context;

        public AccountController(StreamFlixDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            // Basic password hashing, e.g., SHA256 or BCrypt, is recommended here.
            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Implement login validation and redirection
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.PasswordHash == password);
            if (user != null)
            {
                // Authentication logic, e.g., set session or cookie
                return RedirectToAction("Index", "Movie");
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }
    }
}
