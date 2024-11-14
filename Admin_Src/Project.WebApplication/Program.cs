using ConstructionOdering.Repositories.Entities;
using ConstructionOdering.Repositories.Repository;
using ConstructionOrdering.Service.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AdminDbConsturctionOderingSystemContext>(option =>
{
    option.UseSqlServer("DefaultConnection");
});

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<AccountService>();

// Add services to the container.
builder.Services.AddRazorPages();

// xác thực với cookies
builder.Services.AddAuthentication(
    Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults
    .AuthenticationScheme
    ).AddCookie("Cookies", option =>
    {
        option.LoginPath = "/login";      // Đường dẫn đến trang login
        option.LogoutPath = "/logout";    // Đường dẫn đến trang logout (nếu có)
        option.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Thời gian hết hạn
        option.SlidingExpiration = true;  // Tự động gia hạn cookie
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
