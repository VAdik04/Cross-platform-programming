using Api.Data;
using Api.Data.Entities;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class MovementLocationsController(ApiContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<MovementLocation[]>> GetMovementLocations()
    {
        var movementLocations = await context.MovementLocations.ToArrayAsync();
        foreach (var movementLocation in movementLocations)
        {
            movementLocation.TimeZoneToUkrainian();
        }
        return movementLocations;
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<MovementLocation>> GetMovementLocation(int id)
    {
        var movementLocation = await context.MovementLocations.FindAsync(id);

        if (movementLocation == null)
        {
            return NotFound();
        }

        movementLocation.TimeZoneToUkrainian();
        return movementLocation;
    }


    [HttpGet("Search")]
    public async Task<IEnumerable<MovementLocation>> SearchMovementLocation([FromQuery] string?[]? patterns)
    {
        patterns ??= [];
        var movementLocations = (await context.MovementLocations
            .Include(ml => ml.Shipment)
            .Include(ml => ml.FromLocation)
            .Include(ml => ml.ToLocation)
            .ToArrayAsync()
        ).AsEnumerable();

        foreach (var originalPattern in patterns)
        {
            if (string.IsNullOrWhiteSpace(originalPattern))
            {
                continue;
            }
            var pattern = originalPattern.Trim();

            var isDateTimeOffset = DateTimeOffset.TryParse(pattern, out var dateTimeOffset);
            movementLocations = movementLocations.Where(x =>
                ContainsPattern(x.ShipmentLocationId)
                || ContainsPattern(x.ShipmentId)
                || ContainsPattern(x.FromLocationId)
                || ContainsPattern(x.ToLocationId)
                || ContainsPattern(x.OtherDetails)
                || ContainsPattern(x.DateStarted)
                || ContainsPattern(x.DateCompleted)

                || isDateTimeOffset && (
                    x.DateStarted >= dateTimeOffset
                    || x.DateCompleted >= dateTimeOffset
                )

                || ContainsPattern(x.Shipment.FromOrganizationId)
                || ContainsPattern(x.Shipment.ToOrganizationId)
                || ContainsPattern(x.Shipment.ShipmentDetails)

                || ContainsPattern(x.FromLocation.LocationTypeCode)
                || ContainsPattern(x.FromLocation.LocationAddress)
                || ContainsPattern(x.FromLocation.OtherDetails)

                || ContainsPattern(x.ToLocation.LocationTypeCode)
                || ContainsPattern(x.ToLocation.LocationAddress)
                || ContainsPattern(x.ToLocation.OtherDetails)
            );


            bool ContainsPattern<T>(T value) =>
                value?.ToString()?.Contains(pattern, StringComparison.OrdinalIgnoreCase) is true;
        }

        foreach (var movementLocation in movementLocations)
        {
            movementLocation.TimeZoneToUkrainian();
        }
        return movementLocations;
    }
}