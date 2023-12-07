using Api.Data;
using Api.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class OrganizationsController(ApiContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<Organization[]>> GetOrganizations()
    {
        return await context.Organizations.ToArrayAsync();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Organization>> GetOrganization(int id)
    {
        var organization = await context.Organizations.FindAsync(id);
        if (organization == null)
        {
            return NotFound();
        }
        return organization;
    }


    [HttpPost]
    public async Task<ActionResult<Organization>> PostOrganization(Organization organization)
    {
        context.Organizations.Add(organization);
        await context.SaveChangesAsync();
        return CreatedAtAction(
            nameof(GetOrganization),
            new
            {
                id = organization.OrganizationId
            },
            organization
        );
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOrganization(int id)
    {
        var organization = await context.Organizations.FindAsync(id);
        if (organization == null)
        {
            return NotFound();
        }
        context.Organizations.Remove(organization);
        await context.SaveChangesAsync();
        return NoContent();
    }
}