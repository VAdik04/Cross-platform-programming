using App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.Pages.Tables.LocationTypes;

[Authorize]
public class DetailsModel(IHttpClientFactory clientFactory) : PageModel
{
    public LocationType LocationType { get; private set; } = null!;


    public async Task<IActionResult> OnGetAsync(string id)
    {
        var locationType = await this.AuthorizedApiGet<LocationType>(clientFactory, $"LocationTypes/{id}");

        if (locationType == null)
        {
            return NotFound();
        }

        LocationType = locationType;
        return Page();
    }
}