using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityNew.Models;
using IdentityNew.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IdentityNew.Extensions;
using IdentityNew.Services;

namespace IdentityNew.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;

        public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,IEmailService emailService)
        { 
        
            _userManager = userManager;
            _emailService = emailService;
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
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            var hasUser = await _userManager.FindByEmailAsync(model.Email);
            if(hasUser==null)
            {
                ModelState.AddModelErrorList(new List<string> { "Bu E Posta Adresine Sahip Kullanıcı Bulunamamıştır" });
                return View();
            }
            string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(hasUser);

            
            var passwordResetLink = Url.Action("ResetPassword", "Login", new { userId = hasUser.Id, Token = passwordResetToken },HttpContext.Request.Scheme);
            await _emailService.SendResetPasswordEmail(passwordResetLink, hasUser.Email);
            TempData["Success"] = "Şifreniz E Posta Adresinize Gönderilmiştir";
            return RedirectToAction("ForgetPassword","Login");
        }
        [HttpGet]
        public IActionResult ResetPassword(string userId,string Token)
        {
            TempData["userId"] = userId;
            TempData["Token"] = Token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var userId = model.UserId;
            var Token = model.Token;
            if(userId==null || Token== null)
            {
                throw new Exception("Bir Hata Meydana Geldi");
            }
            var hasUser = await _userManager.FindByIdAsync(userId);
            if(hasUser==null)
            {
                ModelState.AddModelErrorList(new List<string> { "Kullanıcı Bulunamadı" });
                return View();
            }
            var result = await _userManager.ResetPasswordAsync(hasUser, Token, model.Password);
            if(result.Succeeded)
            {
                return RedirectToAction("SignIn", "Login");
            }
            ModelState.AddModelErrorList(result.Errors.Select(x => x.Description).ToList());
            
            return View();
        }
    }
}

