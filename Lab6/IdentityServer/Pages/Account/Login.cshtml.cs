using Duende.IdentityServer.Extensions;
using IdentityServer.Data;
using IdentityServer.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Pages.Account;


public class LoginModel(
    IdentityContext context,
    SignInManager<User> signInManager
) : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string? ReturnUrl { get; init; }

    [BindProperty]
    public required string Email { get; set; }

    [BindProperty]
    public required string Password { get; set; }


    public IActionResult OnGet() => User.IsAuthenticated()
        ? RedirectToPage("/Index")
        : Page();


    public async Task<IActionResult> OnPostAsync()
    {
        if (User.IsAuthenticated())
        {
            return RedirectToPage("/Index");
        }
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var username = await context.Users
            .AsNoTracking()
            .Where(u => u.Email == Email)
            .Select(u => u.UserName)
            .FirstOrDefaultAsync();
        if (username is not null)
        {
            var result = await signInManager.PasswordSignInAsync(
                username,
                Password,
                true,
                lockoutOnFailure: false
            );
            if (result.Succeeded)
            {
                return ReturnUrl is null
                    ? RedirectToPage("/Index")
                    : Redirect(ReturnUrl);
            }
        }

        ModelState.AddModelError("", "Email or password is incorrect.");
        return Page();
    }
}