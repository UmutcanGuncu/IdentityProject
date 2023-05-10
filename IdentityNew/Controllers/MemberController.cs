using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using IdentityNew.Extensions;
using IdentityNew.Models;
using IdentityNew.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdentityNew.Controllers
{
    [Authorize] // üyelerin erişebilmesini sağlar
    public class MemberController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFileProvider _fileProvider;

        public MemberController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IFileProvider fileProvider)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _fileProvider = fileProvider;
        }

        public async  Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn", "Login");
            
        }
        public async Task <IActionResult> Index()
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var userViewModel = new UserViewModel()
            {
                Email = currentUser.Email,
                UserName=currentUser.UserName,
                PhoneNumber=currentUser.PhoneNumber,
                PictureUrl=currentUser.Picture
            };
            return View(userViewModel);
        }
        [HttpGet]
        public IActionResult PasswordChange()
        {
            return View();
        }
        [HttpPost]
         public async Task<IActionResult> PasswordChange(PasswordChangeViewModel model)
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);

            var checkOldPassword = await _userManager.CheckPasswordAsync(currentUser, model.OldPassword);
            if (!checkOldPassword)
            {
                ModelState.AddModelErrorList(new List<string> { "Eski Şifrenizi Yanlış Girdiniz" });
                return View();
            }
            var result = await _userManager.ChangePasswordAsync(currentUser, model.OldPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                ModelState.AddModelErrorList(result.Errors.Select(x=>x.Description).ToList());
                return View();
            }
            //Cookie'yi yenilemek için signout ve signin yapacaz
            await _userManager.UpdateSecurityStampAsync(currentUser); //security stamp ile
            await _signInManager.SignOutAsync();
            await _signInManager.PasswordSignInAsync(currentUser,model.NewPassword,false,true);
            TempData["SuccessMessage"] = "Şifreniz Başarıyla Değiştirilmiştir";
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UserEdit()
        {
            ViewBag.GenderList = new SelectList(Enum.GetNames(typeof(Gender)));
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userEditViewModel = new UserEditViewModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                BirthDay = user.BirthDay,
                City = user.City,
                Gender=user.Gender,
                

            };
            return View(userEditViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> UserEdit(UserEditViewModel model)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelErrorList(new List<string> { "Model Geçerli Değil" });
                return View();
            }
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            currentUser.UserName = model.UserName;
            currentUser.Email = model.Email;
            currentUser.BirthDay = model.BirthDay;
            currentUser.City = model.City;
            currentUser.Gender = model.Gender;
            currentUser.PhoneNumber = model.Phone;
            
            if (model.Picture !=null && model.Picture.Length>0)
            {
                var wwwroot = _fileProvider.GetDirectoryContents("wwwroot");
                var randomFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(model.Picture.FileName)}";
                var newPicturePath = Path.Combine(wwwroot!.FirstOrDefault(x => x.Name == "userPictures")!.PhysicalPath!, randomFileName);
                using var stream = new FileStream(newPicturePath, FileMode.Create);
                await model.Picture.CopyToAsync(stream);
                currentUser.Picture = randomFileName;

            }
           

            var result = await _userManager.UpdateAsync(currentUser);
            if(!result.Succeeded)
            {
                ModelState.AddModelErrorList(result.Errors);
                return View();
            }
            await _userManager.UpdateSecurityStampAsync(currentUser);
            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(currentUser, true);
            TempData["SuccessMessage"] = "Kullanıcı Bilgileriniz Başarılı Bir Şekilde Güncellenmiştir";
            var userEditViewModel = new UserEditViewModel()
            {
                UserName = currentUser.UserName,
                Email = currentUser.Email,
                Phone = currentUser.PhoneNumber,
                BirthDay = currentUser.BirthDay,
                City = currentUser.City,
                Gender = currentUser.Gender,


            };
            return View(userEditViewModel);
        }
    }
}

