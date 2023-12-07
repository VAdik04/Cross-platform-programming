using Api.Data;
using Api.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ProductsAndServicesController(ApiContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ProductAndService[]>> GetProductsAndServices()
    {
        return await context.ProductsAndServices.ToArrayAsync();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<ProductAndService>> GetProductAndService(int id)
    {
        var productAndService = await context.ProductsAndServices.FindAsync(id);
        if (productAndService == null)
        {
            return NotFound();
        }
        return productAndService;
    }


    [HttpPost]
    public async Task<ActionResult<ProductAndService>> PostProductAndService(ProductAndService productAndService)
    {
        context.ProductsAndServices.Add(productAndService);
        await context.SaveChangesAsync();
        return CreatedAtAction(
            nameof(GetProductAndService),
            new
            {
                id = productAndService.ProductSvcId
            },
            productAndService
        );
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProductAndService(int id)
    {
        var productAndService = await context.ProductsAndServices.FindAsync(id);
        if (productAndService == null)
        {
            return NotFound();
        }
        context.ProductsAndServices.Remove(productAndService);
        await context.SaveChangesAsync();
        return NoContent();
    }
}