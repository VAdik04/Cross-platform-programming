using Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class ApiContext(DbContextOptions<ApiContext> options) : DbContext(options)
{
    public required DbSet<Country> Countries { get; init; }

    public required DbSet<LocationType> LocationTypes { get; init; }

    public required DbSet<Location> Locations { get; init; }

    public required DbSet<OrganizationType> OrganizationTypes { get; init; }

    public required DbSet<Organization> Organizations { get; init; }

    public required DbSet<Shipment> Shipments { get; init; }

    public required DbSet<MovementLocation> MovementLocations { get; init; }

    public required DbSet<ProductAndServiceType> ProductAndServiceTypes { get; init; }

    public required DbSet<ProductAndService> ProductsAndServices { get; init; }

    public required DbSet<ShipmentProductAndService> ShipmentsProductsAndServices { get; init; }
}