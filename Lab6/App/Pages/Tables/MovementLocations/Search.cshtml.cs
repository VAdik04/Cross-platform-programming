using App.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace App.Pages.Tables.MovementLocations;

public class SearchModel(IHttpClientFactory clientFactory) : PageModel
{
    [BindProperty]
    public required List<string> Patterns { get; init; } = [];

    public MovementLocation[] MovementLocations { get; private set; } = [];

    [BindProperty]
    public string? NewPattern { get; init; }

    [BindProperty]
    public int? DeletionPatternIndex { get; init; }

    [BindProperty]
    public string? Button { get; init; }


    public async Task OnPost()
    {
        if (DeletionPatternIndex.HasValue)
        {
            Patterns.RemoveAt(DeletionPatternIndex.Value);
        }
        else if (Button == "Add")
        {
            if (!string.IsNullOrWhiteSpace(NewPattern))
            {
                Patterns.Add(NewPattern);
            }
        }
        else
        {
            var searchUri = QueryHelpers.AddQueryString(
                "MovementLocations/Search",
                new Dictionary<string, StringValues>
                {
                    ["Patterns"] = new StringValues(Patterns.ToArray())
                }
            );
            MovementLocations = (await this.AuthorizedApiGet<MovementLocation[]>(clientFactory, searchUri))!;
        }
    }
}