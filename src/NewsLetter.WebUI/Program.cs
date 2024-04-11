using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using NewsLetter.Application;
using NewsLetter.Application.Utilities;
using NewsLetter.Domain.Entities;
using NewsLetter.Persistance;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistance(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(configure =>
{
    configure.Cookie.Name = "NewsLetters.Auth";
    configure.LoginPath = "/Auth/Login";
    configure.LogoutPath = "/Auth/Login";
    //eger giriþ yapmadan hiç birþey yapýlmasýn istersek
    //If we want nothing to be done without logging in
    //configure.AccessDeniedPath = "/Auth/Login";
});

builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews();

//ServiceTool
builder.Services.CreateServiceTool();
//ServiceTool

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseStaticFiles();

app.UseRouting();

//eger giriþ yapmadan hiç birþey yapýlmasýn istersek
//If we want nothing to be done without logging in
app.UseAuthentication();
app.UseAuthorization();

using (var scoped = app.Services.CreateScope())
{
    var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
    if (!userManager.Users.Any())
    {
        //default user information
        AppUser? user = new()
        {
            Email = "admin@admin.com",
            UserName = "admin"
        };

        userManager.CreateAsync(user, "String1*").Wait();
    }
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
