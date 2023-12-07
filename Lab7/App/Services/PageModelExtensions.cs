using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace App.Services;


public static class PageModelExtensions
{
    private static readonly Uri _apiUri = new("http://api:5003/api/");


    public static async Task<T?> AuthorizedApiGet<T>(
        this PageModel model,
        IHttpClientFactory factory,
        string path
    )
    {
        var client = factory.CreateClient();
        var accessToken = await model.HttpContext.GetTokenAsync("access_token");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var response = await client.GetAsync(new Uri(_apiUri, path));
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<T>()
            : default;
    }
}