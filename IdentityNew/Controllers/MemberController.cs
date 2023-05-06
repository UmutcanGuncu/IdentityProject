using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityNew.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdentityNew.Controllers
{
    [Authorize] // üyelerin erişebilmesini sağlar
    public class MemberController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        public MemberController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async  Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn", "Login");
            
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}

