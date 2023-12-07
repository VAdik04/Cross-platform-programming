using App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.Pages.Tables.MovementLocations;

[Authorize]
public class DetailsModel(IHttpClientFactory clientFactory) : PageModel
{
    public MovementLocation MovementLocation { get; private set; } = null!;


    public async Task<IActionResult> OnGetAsync(int id)
    {
        var movementLocation = await this.AuthorizedApiGet<MovementLocation>(clientFactory, $"MovementLocations/{id}");

        if (movementLocation == null)
        {
            return NotFound();
        }

        MovementLocation = movementLocation;
        return Page();
    }
}