using Api.Data;
using Api.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ProductAndServiceTypesController(ApiContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ProductAndServiceType[]>> GetProductAndServiceTypes()
    {
        return await context.ProductAndServiceTypes.ToArrayAsync();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<ProductAndServiceType>> GetProductAndServiceType(string id)
    {
        var productAndServiceType = await context.ProductAndServiceTypes.FindAsync(id);
        if (productAndServiceType == null)
        {
            return NotFound();
        }
        return productAndServiceType;
    }


    [HttpPost]
    public async Task<ActionResult<ProductAndServiceType>> PostProductAndServiceType(ProductAndServiceType productAndServiceType)
    {
        context.ProductAndServiceTypes.Add(productAndServiceType);
        await context.SaveChangesAsync();
        return CreatedAtAction(
            nameof(GetProductAndServiceType),
            new
            {
                id = productAndServiceType.ProductSvcTypeCode
            },
            productAndServiceType
        );
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProductAndServiceType(string id)
    {
        var productAndServiceType = await context.ProductAndServiceTypes.FindAsync(id);
        if (productAndServiceType == null)
        {
            return NotFound();
        }
        context.ProductAndServiceTypes.Remove(productAndServiceType);
        await context.SaveChangesAsync();
        return NoContent();
    }
}