using ConstructionOdering.Repositories.Entities;
using ConstructionOdering.Repositories.Repository;
using ConstructionOrdering.Service.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

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
    ).AddCookie(option =>
    {
        option.LoginPath = "/login";
        option.LogoutPath = "/logout";

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
