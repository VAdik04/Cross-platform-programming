using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddHttpClient();
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
        })
            .AddCookie()
            .AddOpenIdConnect(options =>
            {
                options.Authority = "http://identity_server:5001";
                options.ClientId = "app";
                options.ClientSecret = "app";
                options.ResponseType = "code";
                options.Scope.Add("offline_access");
                options.ClaimActions.MapUniqueJsonKey("full_name", "full_name");
                options.ClaimActions.MapUniqueJsonKey("phone_number", "phone_number");
                options.RequireHttpsMetadata = false;
                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
            });
        builder.Services.AddRazorPages();

        var app = builder.Build();
        app.UseStaticFiles();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapRazorPages();
        app.Run();
    }
}