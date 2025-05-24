using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;
using MvcMovie.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using MvcMovie.Models.Process;
using Microsoft.AspNetCore.Authorization;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOptions();
var mailSettings = builder.Configuration.GetSection("MailSettings");
builder.Services.Configure<MailSettings>(mailSettings);
builder.Services.AddTransient<IEmailSender , SendMailService>();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenide";
    options.SlidingExpiration = true;
});
builder.Services.Configure<IdentityOptions>(options =>
{

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
    ///pass
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = false;
    ///login
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    /// user
    options.User.RequireUniqueEmail = true;
});
builder.Services.AddRazorPages();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    // Các cấu hình khác...
})
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddAuthorization(options =>
{
    foreach (var permission in Enum.GetValues(typeof(SystemPermissions)).Cast<SystemPermissions>())
    {
        options.AddPolicy(permission.ToString(), policy => 
        policy.RequireClaim("Permission", permission.ToString()));
    }
    options.AddPolicy("Role", policy => policy.RequireClaim("Role", "AdminOnly"));
    options.AddPolicy("Permission", policy => policy.RequireClaim("Role", "EmployeeOnly"));
    options.AddPolicy("PolicyAdmin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("PolicyEmployee", policy => policy.RequireRole("Employee"));
    options.AddPolicy("PolicyByPhoneNumber", policy => policy.Requirements.Add(new PolicyByPhoneNumberRequirement()));
});
builder.Services.AddSingleton<IAuthorizationHandler, PolicyByPhoneNumberHandler>();
builder.Services.AddTransient<EmployeeSeeder>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"Identity/Account/Logout";
    options.AccessDeniedPath = $"Identity/Account/AccessDenied";
});
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var seeder = services.GetRequiredService<EmployeeSeeder>();
    seeder.SeedEmployees(1000);
}
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
