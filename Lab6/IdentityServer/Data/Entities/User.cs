using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Data.Entities;


public class User : IdentityUser
{
    public required string FullName { get; set; }
}