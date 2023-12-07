using Api.Data;
using Api.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class CountriesController(ApiContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<Country[]>> GetCountries()
    {
        return await context.Countries.ToArrayAsync();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Country>> GetCountry(string id)
    {
        var country = await context.Countries.FindAsync(id);
        if (country == null)
        {
            return NotFound();
        }
        return country;
    }


    [HttpPost]
    public async Task<ActionResult<Country>> PostCountry(Country country)
    {
        context.Countries.Add(country);
        await context.SaveChangesAsync();
        return CreatedAtAction(
            nameof(GetCountry),
            new
            {
                id = country.CountryCode
            },
            country
        );
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCountry(string id)
    {
        var country = await context.Countries.FindAsync(id);
        if (country == null)
        {
            return NotFound();
        }
        context.Countries.Remove(country);
        await context.SaveChangesAsync();
        return NoContent();
    }
}