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
}