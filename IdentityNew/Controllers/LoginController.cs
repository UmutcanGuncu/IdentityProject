using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityNew.Models;
using IdentityNew.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IdentityNew.Extensions;


namespace IdentityNew.Controllers
{
    public class LoginController : Controller
    {
        public UserManager<AppUser> _userManager;
        public SignInManager<AppUser> _signInManager;

        

        public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        { 
            _userManager = userManager;
            _signInManager = signInManager;
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
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model,string? returnUrl=null)
        {
            returnUrl = returnUrl ?? Url.Action("Index", "Home");
            if(!ModelState.IsValid)
            {
                return View();
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user==null)
            {
                ModelState.AddModelError(string.Empty, "E Mail Veya Şifre Yanlış");
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe,true);
            if(result.Succeeded)
            {
                return Redirect(returnUrl);
            }
            if(result.IsLockedOut)
            {
                ModelState.AddModelErrorList(new List<string> { "4 Dakika Boyunca Giriş Yapamazsınız" });
                return View();
            }
            ModelState.AddModelErrorList(new List<string> { "E Mail Veya Şifre Yanlış" });
            return View();
        }

    }
}

