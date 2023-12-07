using Api.Data;
using Api.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class LocationTypesController(ApiContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<LocationType[]>> GetLocationTypes()
    {
        return await context.LocationTypes.ToArrayAsync();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<LocationType>> GetLocationType(string id)
    {
        var locationType = await context.LocationTypes.FindAsync(id);
        if (locationType == null)
        {
            return NotFound();
        }
        return locationType;
    }


    [HttpPost]
    public async Task<ActionResult<LocationType>> PostLocationType(LocationType locationType)
    {
        context.LocationTypes.Add(locationType);
        await context.SaveChangesAsync();
        return CreatedAtAction(
            nameof(GetLocationType),
            new
            {
                id = locationType.LocationTypeCode
            },
            locationType
        );
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteLocationType(string id)
    {
        var locationType = await context.LocationTypes.FindAsync(id);
        if (locationType == null)
        {
            return NotFound();
        }
        context.LocationTypes.Remove(locationType);
        await context.SaveChangesAsync();
        return NoContent();
    }
}