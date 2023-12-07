using App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.Pages.Tables.LocationTypes;

[Authorize]
public class IndexModel(IHttpClientFactory clientFactory) : PageModel
{
    public LocationType[] LocationTypes { get; private set; } = null!;


    public async Task OnGetAsync()
    {
        LocationTypes = (await this.AuthorizedApiGet<LocationType[]>(clientFactory, "LocationTypes"))!;
    }
}