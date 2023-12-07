using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace IdentityServer;


public static class Configuration
{
    public static ApiScope[] ApiScopes { get; } =
    [
        new("api")
    ];


    public static IdentityResource[] IdentityResources { get; } =
    [
        new IdentityResources.OpenId(),
        new IdentityResources.Profile()
        {
            UserClaims =
            {
                JwtClaimTypes.Name,
                JwtClaimTypes.PhoneNumber,
                JwtClaimTypes.Email,
                "full_name"
            }
        }
    ];


    public static Client[] Clients { get; } =
    [
        new()
        {
            ClientId = "tests",
            ClientSecrets =
            {
                new("tests".Sha256())
            },
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            AllowedScopes =
            {
                "api"
            }
        },
        new()
        {
            ClientId = "app",
            ClientSecrets =
            {
                new("app".Sha256())
            },
            AllowedGrantTypes = GrantTypes.Code,
            AllowedScopes =
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile
            },
            AllowOfflineAccess = true,
            RedirectUris =
            {
                "http://localhost:5002/signin-oidc"
            },
            PostLogoutRedirectUris =
            {
                "http://localhost:5002/signout-callback-oidc"
            }
        }
    ];
}