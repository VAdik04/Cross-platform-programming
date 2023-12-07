using App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.Pages.Tables.MovementLocations;

[Authorize]
public class IndexModel(IHttpClientFactory clientFactory) : PageModel
{
    public MovementLocation[] MovementLocations { get; private set; } = null!;


    public async Task OnGetAsync()
    {
        MovementLocations = (await this.AuthorizedApiGet<MovementLocation[]>(clientFactory, "v2/MovementLocations"))!;
    }
}