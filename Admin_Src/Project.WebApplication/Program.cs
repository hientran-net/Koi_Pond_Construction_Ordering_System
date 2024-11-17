using ConstructionOdering.Repositories.Entities;
using ConstructionOdering.Repositories.Interface;
using ConstructionOdering.Repositories.Repository;
using ConstructionOrdering.Service.Interface;
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


//Employee
builder.Services.AddScoped<INhanVienRepository, NhanVienRepository>();
builder.Services.AddScoped<INhanVienService, NhanVienService>();

// Add services to the container.
builder.Services.AddRazorPages();

// xác thực với cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.LogoutPath = "/Logout";
        options.Cookie.Name = "X_KoiAdminCookie"; // Đặt tên cookie của bạn
        options.ExpireTimeSpan = TimeSpan.FromHours(24);
        options.SlidingExpiration = true;
        options.Cookie.HttpOnly = true;
        // Thêm các options bảo mật
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Nếu dùng HTTPS
    });

builder.Services.AddAuthorization();

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
