using IdentityServer;
using IdentityServer.Data;
using IdentityServer.Data.Entities;
using IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContextPool<IdentityContext>(optionsBuilder =>
        {
            var dataSource = Path.Combine(
                builder.Environment.ContentRootPath,
                "db"
            );
            optionsBuilder.UseSqlite($"Data source={dataSource}");
        });
        builder.Services.AddIdentityCore<User>(options =>
        {
            options.User.RequireUniqueEmail = true;
        })
            .AddSignInManager()
            .AddEntityFrameworkStores<IdentityContext>();
        builder.Services.AddScoped<
            IUserClaimsPrincipalFactory<User>,
            UserClaimsPrincipalFactory
        >();
        builder.Services.AddAuthentication()
            .AddIdentityCookies();
        builder.Services.AddIdentityServer()
            .AddInMemoryApiScopes(Configuration.ApiScopes)
            .AddInMemoryIdentityResources(Configuration.IdentityResources)
            .AddInMemoryClients(Configuration.Clients)
            .AddAspNetIdentity<User>();
        builder.Services.AddRazorPages();

        var app = builder.Build();
        using (var scope = app.Services.CreateScope())
        {
            using var context = scope.ServiceProvider.GetRequiredService<IdentityContext>();
            context.Database.EnsureCreated();
        }
        app.UseStaticFiles();
        app.UseCookiePolicy(new CookiePolicyOptions
        {
            Secure = CookieSecurePolicy.Always
        });
        app.UseIdentityServer();
        app.UseAuthorization();
        app.MapRazorPages();
        app.Run();
    }
}