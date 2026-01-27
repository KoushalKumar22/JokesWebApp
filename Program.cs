using JokesWebApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

    if (builder.Environment.IsDevelopment())
{
    Console.WriteLine("DB PROVIDER: SQLite");
}
else
{
    Console.WriteLine("DB PROVIDER: (Azure)");
}
Console.WriteLine($"DB CONNECTION: {connectionString}");//delete this line in production

// DbContext

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        var sqliteConn = builder.Configuration.GetConnectionString("SqliteConnection")
            ?? throw new InvalidOperationException("SqliteConnection not found");

        options.UseSqlite(sqliteConn);
    }
    else
    {
        var sqlServerConn = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("DefaultConnection not found");

        options.UseSqlServer(sqlServerConn, sql =>
            sql.EnableRetryOnFailure());
    }
});

// Identity with Roles
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders()
.AddDefaultUI();


// ✅ REQUIRED for Identity UI
builder.Services.AddRazorPages();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

// Admin seeding
try
{
    using var scope = app.Services.CreateScope();

    await SeedData.SeedAdminAsync(scope.ServiceProvider);
}
catch (Exception ex)
{
    Console.WriteLine($"SEED WARNING: {ex.Message}");
}

if(app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.Run();
