using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMediaChatApp.Data;
using SocialMediaChatApp.Hubs;
using SocialMediaChatApp.Models; // For ApplicationUser

var builder = WebApplication.CreateBuilder(args);

// 🔹 Add database context using SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Add ASP.NET Core Identity
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

// 🔹 Add Blazor, Razor pages, and SignalR
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSignalR();

// 🔹 Add authentication & authorization services
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();

// 🔹 Enable authentication & authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// 🔹 Map Blazor and SignalR endpoints
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapHub<ChatHub>("/chathub");

app.Run();
