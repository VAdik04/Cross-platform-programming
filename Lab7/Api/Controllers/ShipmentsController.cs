using Api.Data;
using Api.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ShipmentsController(ApiContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<Shipment[]>> GetShipments()
    {
        return await context.Shipments.ToArrayAsync();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Shipment>> GetShipment(int id)
    {
        var shipment = await context.Shipments.FindAsync(id);
        if (shipment == null)
        {
            return NotFound();
        }
        return shipment;
    }


    [HttpPost]
    public async Task<ActionResult<Shipment>> PostShipment(Shipment shipment)
    {
        context.Shipments.Add(shipment);
        await context.SaveChangesAsync();
        return CreatedAtAction(
            nameof(GetShipment),
            new
            {
                id = shipment.ShipmentId
            },
            shipment
        );
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteShipment(int id)
    {
        var shipment = await context.Shipments.FindAsync(id);
        if (shipment == null)
        {
            return NotFound();
        }
        context.Shipments.Remove(shipment);
        await context.SaveChangesAsync();
        return NoContent();
    }
}