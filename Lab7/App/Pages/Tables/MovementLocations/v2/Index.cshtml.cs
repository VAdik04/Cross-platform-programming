using App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace App.Pages.Tables.MovementLocations.v2;


[Authorize]
public class IndexModel(IHttpClientFactory clientFactory) : PageModel
{
    public MovementLocation[] MovementLocations { get; private set; } = null!;


    public async Task OnGetAsync()
    {
        var weekAgo = DateTimeOffset.UtcNow.AddDays(-7D);
        var uri = QueryHelpers.AddQueryString("v2/MovementLocations", "dateStarted", weekAgo.ToString());
        MovementLocations = (await this.AuthorizedApiGet<MovementLocation[]>(clientFactory, uri))!;
    }
}