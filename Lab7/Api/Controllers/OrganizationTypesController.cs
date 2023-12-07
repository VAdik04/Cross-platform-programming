using Api.Data;
using Api.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class OrganizationTypesController(ApiContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<OrganizationType[]>> GetOrganizationTypes()
    {
        return await context.OrganizationTypes.ToArrayAsync();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<OrganizationType>> GetOrganizationType(string id)
    {
        var organizationType = await context.OrganizationTypes.FindAsync(id);
        if (organizationType == null)
        {
            return NotFound();
        }
        return organizationType;
    }


    [HttpPost]
    public async Task<ActionResult<OrganizationType>> PostOrganizationType(OrganizationType organizationType)
    {
        context.OrganizationTypes.Add(organizationType);
        await context.SaveChangesAsync();
        return CreatedAtAction(
            nameof(GetOrganizationType),
            new
            {
                id = organizationType.OrganizationTypeCode
            },
            organizationType
        );
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOrganizationType(string id)
    {
        var organizationType = await context.OrganizationTypes.FindAsync(id);
        if (organizationType == null)
        {
            return NotFound();
        }
        context.OrganizationTypes.Remove(organizationType);
        await context.SaveChangesAsync();
        return NoContent();
    }
}