using System;
using IdentityNew.Models;

namespace IdentityNew.Extensions
{
	public static class StartupExtensions
	{
		public static void AddIdentityWithExt(this IServiceCollection Services)
		{
            Services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            }
    ).AddEntityFrameworkStores<AppDbContext>(); //ıdentity ayarlaması

        }
	}
}

