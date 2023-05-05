using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityNew.Models;
using IdentityNew.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;



namespace IdentityNew.Controllers
{
    public class LoginController : Controller
    {
        public UserManager<AppUser> _userManager;

        public LoginController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            var value = await _userManager.CreateAsync(new()
            {
                UserName=model.UserName,
                Email=model.Email,
                PhoneNumber=model.Phone
            }, model.Password);
            if(value.Succeeded)
            {
                return RedirectToAction("SignIn");
            }
            foreach(IdentityError item in value.Errors)
            {
                ModelState.AddModelError(string.Empty,item.Description);
            }
            return View();
            
        }
    }
}

