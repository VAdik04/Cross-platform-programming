using IdentityServer.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Data;


public class IdentityContext(
    DbContextOptions<IdentityContext> options
) : IdentityUserContext<User>(options)
{ }