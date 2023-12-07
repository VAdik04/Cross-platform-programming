using App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.Pages.Tables.OrganizationTypes;

[Authorize]
public class DetailsModel(IHttpClientFactory clientFactory) : PageModel
{
    public OrganizationType OrganizationType { get; private set; } = null!;

    public async Task<IActionResult> OnGetAsync(string id)
    {
        var organizationType = await this.AuthorizedApiGet<OrganizationType>(clientFactory, $"OrganizationTypes/{id}");

        if (organizationType == null)
        {
            return NotFound();
        }

        OrganizationType = organizationType;
        return Page();
    }
}