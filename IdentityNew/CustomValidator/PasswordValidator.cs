using System;
using IdentityNew.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityNew.CustomValidator
{
    public class PasswordValidator : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string? password)
        {
            var errors = new List<IdentityError>();
            if (password!.ToLower().Contains(user.UserName!.ToLower())) //! runtime tarafında bir önemi yok
            {
                errors.Add(new() { Code = "PasswordContainsUserName",Description="Şifreniz Kullanıcı Adınızı İçeremez"});  
            }
            if(errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }
            return Task.FromResult(IdentityResult.Success);
            
        }
    }
}

