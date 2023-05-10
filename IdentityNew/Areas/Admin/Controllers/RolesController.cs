using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityNew.Areas.Admin.Models;
using IdentityNew.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using IdentityNew.Extensions;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdentityNew.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public RolesController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task <IActionResult> Index()
        {
            var roles = await _roleManager.Roles.Select(x => new RoleViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            return View(roles);
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            var result = await _roleManager.CreateAsync(new AppRole() { Name=model.Name});
            if(!result.Succeeded)
            {
                ModelState.AddModelErrorList(result.Errors);
                return View();
            }
            return RedirectToAction(nameof(Admin.Controllers.RolesController.Index));
        }
        [HttpGet]
        public async Task<IActionResult> UpdateRole(string id)
        {
            var result = await _roleManager.FindByIdAsync(id);
            return View(new UpdateRoleViewModel () { Id=result.Id,Name=result.Name});
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(UpdateRoleViewModel model)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelErrorList(new List<string> { "Model Geçerli Değil" });
                return View();
            }
            var role = await _roleManager.FindByIdAsync(model.Id);
            
            role.Name = model.Name;
            var result = await _roleManager.UpdateAsync(role);

            return View();
        }
        public async Task<IActionResult> DeleteRole(string id)
        {
            var result = await _roleManager.FindByIdAsync(id);
            var deleteRole = await _roleManager.DeleteAsync(result);
            return View();
        }
    }
}

