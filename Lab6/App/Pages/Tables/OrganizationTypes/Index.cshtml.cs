using App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.Pages.Tables.OrganizationTypes;

[Authorize]
public class IndexModel(IHttpClientFactory clientFactory) : PageModel
{
    public OrganizationType[] OrganizationTypes { get; private set; } = null!;


    public async Task OnGetAsync()
    {
        OrganizationTypes = (await this.AuthorizedApiGet<OrganizationType[]>(clientFactory, "OrganizationTypes"))!;
    }
}