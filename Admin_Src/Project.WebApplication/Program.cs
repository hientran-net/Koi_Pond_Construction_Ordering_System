using ConstructionOdering.Repositories.Entities;
using ConstructionOrdering.Service.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AdminDbConsturctionOderingSystemContext>(option =>
{
    option.UseSqlServer("DefaultConnection");
});

// xác thực với cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";  // Đường dẫn đến trang đăng nhập
        options.AccessDeniedPath = "";  // Đường dẫn khi người dùng không có quyền
    });

builder.Services.AddScoped<AccountService>();


// Add services to the container.
builder.Services.AddRazorPages();

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
