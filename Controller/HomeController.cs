using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Youtube2.Data;

namespace Youtube2.Controller
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        // GET: Home
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // login functionality
            IdentityUser user = await _userManager.FindByNameAsync(username);

            if (user != null)
            {
                // Sign user
                var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);

                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }


            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            // register functionality
            var user = new IdentityUser
            {
                UserName = username,
                Email = " ",
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);

                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index");
        }
    }
}