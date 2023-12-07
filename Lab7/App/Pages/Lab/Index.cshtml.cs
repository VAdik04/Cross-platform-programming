using Labs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.Pages.Lab;


[Authorize]
public class IndexModel : PageModel
{
    private static readonly Func<string, string>[] _solveMethods =
    {
        Lab1.Solve,
        Lab2.Solve,
        Lab3.Solve
    };


    [BindProperty(SupportsGet = true)]
    public required int Number { get; init; }

    [BindProperty]
    public string? Input { get; set; }

    [BindProperty]
    public string? Output { get; set; }


    public void OnGet()
    {
        if (ModelState.IsValid)
        {
            ValidateLab();
        }
    }


    public void OnPost()
    {
        if (ModelState.IsValid)
        {
            ValidateLab();
        }
        if (!ModelState.IsValid)
        {
            return;
        }
        try
        {
            Output = _solveMethods[Number - 1](Input ?? "");
        }
        catch (Exception exception)
        {
            Output = exception.Message;
        }
    }


    private void ValidateLab()
    {
        if (!(Number >= 1 && Number <= _solveMethods.Length))
        {
            ModelState.AddModelError("", $"Laboratory work ¹{Number} does not exist.");
        }
    }
}