using IdentityNew.Models;
using Microsoft.EntityFrameworkCore;
using IdentityNew.Extensions;
using Microsoft.AspNetCore.Identity;
using IdentityNew.OptionsModel;
using Microsoft.Extensions.DependencyInjection;
using IdentityNew.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();


builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    }) ; //database ayarlaması
builder.Services.AddIdentityWithExt();//identity configrasyonunu extensions klasöründe bulabilirsin
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSetting"));
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
{
    opt.TokenLifespan = TimeSpan.FromHours(2);//token'ın ömrünü ayarladık
});

builder.Services.ConfigureApplicationCookie(opt =>
{
    var cookieBuilder = new CookieBuilder();

    cookieBuilder.Name = "IdentityCookie";
    opt.LoginPath = "/Login/SignIn";
    opt.LogoutPath = "/Member/Logout";
    opt.Cookie = cookieBuilder;
    opt.ExpireTimeSpan = TimeSpan.FromDays(5); //5 gün boyunca giriş yapmazsan login sayfasına yönlendşrşr
    opt.SlidingExpiration = true; // her giriş yaptığında cookie'nin süresi 5 gün olacak
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); // kimlik doğrulama sıralamaları önemli
app.UseAuthorization(); // kimlik yetkilendirme

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=SignUp}/{id?}");
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.Run();

