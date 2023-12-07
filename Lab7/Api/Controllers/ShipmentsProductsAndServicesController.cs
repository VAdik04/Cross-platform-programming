using Api.Data;
using Api.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ShipmentsProductsAndServicesController(ApiContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ShipmentProductAndService[]>> GetShipmentsProductsAndServices()
    {
        return await context.ShipmentsProductsAndServices.ToArrayAsync();
    }


    [HttpGet("{shipmentId}/{productSvcId}")]
    public async Task<ActionResult<ShipmentProductAndService>> GetShipmentProductAndService(int shipmentId, int productSvcId)
    {
        var shipmentProductAndService = await context.ShipmentsProductsAndServices.FindAsync(shipmentId, productSvcId);
        if (shipmentProductAndService == null)
        {
            return NotFound();
        }
        return shipmentProductAndService;
    }


    [HttpPost]
    public async Task<ActionResult<ShipmentProductAndService>> PostShipmentProductAndService(ShipmentProductAndService shipmentProductAndService)
    {
        context.ShipmentsProductsAndServices.Add(shipmentProductAndService);
        await context.SaveChangesAsync();
        return CreatedAtAction(
            nameof(GetShipmentProductAndService),
            new
            {
                shipmentId = shipmentProductAndService.ShipmentId,
                productSvcId = shipmentProductAndService.ProductSvcId
            },
            shipmentProductAndService
        );
    }


    [HttpDelete("{shipmentId}/{productSvcId}")]
    public async Task<ActionResult> DeleteShipmentProductAndService(int shipmentId, int productSvcId)
    {
        var shipmentProductAndService = await context.ShipmentsProductsAndServices.FindAsync(shipmentId, productSvcId);
        if (shipmentProductAndService == null)
        {
            return NotFound();
        }
        context.ShipmentsProductsAndServices.Remove(shipmentProductAndService);
        await context.SaveChangesAsync();
        return NoContent();
    }
}