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
}