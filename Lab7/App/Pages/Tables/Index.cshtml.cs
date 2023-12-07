using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.Pages.Tables;

[Authorize]
public class IndexModel : PageModel
{
    private static readonly string[] _tableNames =
    [
        "LocationTypes",
        "MovementLocations",
        "OrganizationTypes"
    ];


    public IEnumerable<string> TableNames => _tableNames;
}