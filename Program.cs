using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Data;
using RecipeApp.Services;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// EF Core with SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Authentication & Authorization (Cookie-based)
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
        options.AccessDeniedPath = "/login";

        options.Cookie.Name = "RecipeApp.Auth";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // Use Always in production with HTTPS
        options.Cookie.SameSite = SameSiteMode.Lax;

        options.ExpireTimeSpan = TimeSpan.FromHours(1);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();

// Blazor services
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// HttpContext accessor
builder.Services.AddHttpContextAccessor();

// App services
builder.Services.AddScoped<IUserService, UserService>();

// Provide authentication state to Blazor components via ASP.NET Core auth
builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

// register HttpClient (default client with BaseAddress)
builder.Services.AddHttpClient();

var app = builder.Build();

// Migrate & seed
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
    DbInitializer.Seed(db);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

/// <summary>
/// AuthenticationStateProvider that revalidates cookie auth so Blazor circuits
/// stay in sync with ASP.NET Core authentication.
/// </summary>
public class ServerAuthenticationStateProvider : RevalidatingServerAuthenticationStateProvider
{
    public ServerAuthenticationStateProvider(ILoggerFactory loggerFactory) : base(loggerFactory) { }

    protected override TimeSpan RevalidationInterval => TimeSpan.FromMinutes(5);

    protected override Task<bool> ValidateAuthenticationStateAsync(AuthenticationState state, CancellationToken token)
        => Task.FromResult(true);
}
