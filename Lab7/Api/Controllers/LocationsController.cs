using Api.Data;
using Api.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class LocationsController(ApiContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<Location[]>> GetLocations()
    {
        return await context.Locations.ToArrayAsync();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Location>> GetLocation(int id)
    {
        var location = await context.Locations.FindAsync(id);
        if (location == null)
        {
            return NotFound();
        }
        return location;
    }


    [HttpPost]
    public async Task<ActionResult<Location>> PostLocation(Location location)
    {
        context.Locations.Add(location);
        await context.SaveChangesAsync();
        return CreatedAtAction(
            nameof(GetLocation),
            new
            {
                id = location.LocationId
            },
            location
        );
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteLocation(int id)
    {
        var location = await context.Locations.FindAsync(id);
        if (location == null)
        {
            return NotFound();
        }
        context.Locations.Remove(location);
        await context.SaveChangesAsync();
        return NoContent();
    }
}