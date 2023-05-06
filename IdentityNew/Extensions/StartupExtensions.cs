using System;
using IdentityNew.CustomValidator;
using IdentityNew.Localizations;
using IdentityNew.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityNew.Extensions
{
	public static class StartupExtensions
	{
		public static void AddIdentityWithExt(this IServiceCollection Services)
		{
            Services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // 4 başarısız girişte 5 dakika boyunca giriş yapamayacak kullanıcı
                options.Lockout.MaxFailedAccessAttempts = 4; //Başarısız giriş sayısı
            }
    ).AddPasswordValidator<PasswordValidator>()
    .AddErrorDescriber<LocalizationIdentityErrorDescriber>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<AppDbContext>();//ıdentity ayarlaması

        }
       
	}
}

