using Api.Data.Entities;
using Bogus;

namespace Api.Data;


public static class DataSeeder
{
    private static readonly Action<ApiContext, int>[] _seedMethods =
    [
        SeedCountries,
        SeedLocationTypes,
        SeedLocations,
        SeedOrganizationTypes,
        SeedOrganizations,
        SeedShipments,
        SeedMovementLocations,
        SeedProductAndServiceTypes,
        SeedProductsAndServices,
        SeedShipmentsProductsAndServices
    ];


    public static void Seed(ApiContext context, int length)
    {
        foreach (var seedMethod in _seedMethods)
        {
            seedMethod(context, length);
        }
    }


    private static void SeedCountries(ApiContext context, int length)
    {
        var countryFaker = new Faker<Country>()
            .RuleFor(c => c.CountryCode, f => f.Address.CountryCode())
            .RuleFor(c => c.CountryName, f => f.Address.Country())
            .RuleFor(c => c.CountryCurrency, f => f.Finance.Currency().Code)
            .RuleFor(
                c => c.LanguagesSpoken,
                f => string.Join(",", f.Random.WordsArray(1, 5))
            )
            .RuleFor(c => c.USDExchangeRate, f => f.Finance.Amount())
            .RuleFor(c => c.USDExchangeDate, f => f.Date.PastOffset());

        var countries = countryFaker
            .Generate(length)
            .DistinctBy(c => c.CountryCode);
        context.Countries.AddRange(countries);
        context.SaveChanges();
    }


    private static void SeedLocationTypes(ApiContext context, int length)
    {
        var countries = context.Countries.ToArray();
        var locationTypeFaker = new Faker<LocationType>()
            .RuleFor(lt => lt.LocationTypeCode, f => f.Random.Guid().ToString())
            .RuleFor(lt => lt.CountryCode, f => f.Random.ListItem(countries).CountryCode)
            .RuleFor(lt => lt.LocationTypeDescription, f => f.Lorem.Sentences());

        var locationTypes = locationTypeFaker
            .Generate(length)
            .DistinctBy(lt => lt.LocationTypeCode);
        context.LocationTypes.AddRange(locationTypes);
        context.SaveChanges();
    }


    private static void SeedLocations(ApiContext context, int length)
    {
        var locationTypes = context.LocationTypes.ToArray();
        var locationFaker = new Faker<Location>()
            .RuleFor(l => l.LocationTypeCode, f => f.Random.ListItem(locationTypes).LocationTypeCode)
            .RuleFor(l => l.LocationAddress, f => f.Address.FullAddress())
            .RuleFor(l => l.OtherDetails, f => f.Lorem.Sentence());

        var locations = locationFaker.Generate(length);
        context.Locations.AddRange(locations);
        context.SaveChanges();
    }


    private static void SeedOrganizationTypes(ApiContext context, int length)
    {
        var organizationTypeFaker = new Faker<OrganizationType>()
            .RuleFor(ot => ot.OrganizationTypeCode, f => f.Random.Guid().ToString())
            .RuleFor(ot => ot.OrganizationTypeDescription, f => f.Lorem.Sentence());

        var organizationTypes = organizationTypeFaker
            .Generate(length)
            .DistinctBy(ot => ot.OrganizationTypeCode);
        context.OrganizationTypes.AddRange(organizationTypes);
        context.SaveChanges();
    }


    private static void SeedOrganizations(ApiContext context, int length)
    {
        var organizationTypes = context.OrganizationTypes.ToArray();
        var organizationFaker = new Faker<Organization>()
            .RuleFor(o => o.OrganizationTypeCode, f => f.Random.ListItem(organizationTypes).OrganizationTypeCode)
            .RuleFor(o => o.OrganizationDetails, f => f.Lorem.Sentence());

        var organizations = organizationFaker.Generate(length);
        context.Organizations.AddRange(organizations);
        context.SaveChanges();
    }


    private static void SeedShipments(ApiContext context, int length)
    {
        var organizations = context.Organizations.ToArray();
        var shipmentFaker = new Faker<Shipment>()
            .RuleFor(s => s.FromOrganizationId, f => f.Random.ListItem(organizations).OrganizationId)
            .RuleFor(s => s.ToOrganizationId, f => f.Random.ListItem(organizations).OrganizationId)
            .RuleFor(s => s.ShipmentDetails, f => f.Lorem.Sentence());

        var shipments = shipmentFaker.Generate(length);
        context.Shipments.AddRange(shipments);
        context.SaveChanges();
    }


    private static void SeedMovementLocations(ApiContext context, int length)
    {
        var shipments = context.Shipments.ToArray();
        var locations = context.Locations.ToArray();
        var movementLocationFaker = new Faker<MovementLocation>()
            .RuleFor(ml => ml.ShipmentId, f => f.Random.ListItem(shipments).ShipmentId)
            .RuleFor(ml => ml.FromLocationId, f => f.Random.ListItem(locations).LocationId)
            .RuleFor(ml => ml.ToLocationId, f => f.Random.ListItem(locations).LocationId)
            .RuleFor(ml => ml.DateStarted, f => f.Date.PastOffset())
            .RuleFor(ml => ml.DateCompleted, f => f.Date.RecentOffset())
            .RuleFor(ml => ml.OtherDetails, f => f.Lorem.Sentence());

        var movementLocations = movementLocationFaker.Generate(length);
        context.MovementLocations.AddRange(movementLocations);
        context.SaveChanges();
    }


    private static void SeedProductAndServiceTypes(ApiContext context, int length)
    {
        var productAndServiceTypeFaker = new Faker<ProductAndServiceType>()
            .RuleFor(pst => pst.ProductSvcTypeCode, f => f.Random.Guid().ToString())
            .RuleFor(pst => pst.ProductSvcTypeDescription, f => f.Lorem.Sentence());

        var productAndServiceTypes = productAndServiceTypeFaker
            .Generate(length)
            .DistinctBy(pst => pst.ProductSvcTypeCode);
        context.ProductAndServiceTypes.AddRange(productAndServiceTypes);
        context.SaveChanges();
    }


    private static void SeedProductsAndServices(ApiContext context, int length)
    {
        var productAndServiceTypes = context.ProductAndServiceTypes.ToArray();
        var productAndServiceFaker = new Faker<ProductAndService>()
            .RuleFor(ps => ps.ProductSvcTypeCode, f => f.Random.ListItem(productAndServiceTypes).ProductSvcTypeCode)
            .RuleFor(ps => ps.ProductSvcDetails, f => f.Lorem.Sentence())
            .RuleFor(ps => ps.Quantity, f => f.Random.Number(1, 1000));

        var productsAndServices = productAndServiceFaker.Generate(length);
        context.ProductsAndServices.AddRange(productsAndServices);
        context.SaveChanges();
    }


    private static void SeedShipmentsProductsAndServices(ApiContext context, int length)
    {
        var shipments = context.Shipments.ToArray();
        var productsAndServices = context.ProductsAndServices.ToArray();
        var shipmentProductAndServiceFaker = new Faker<ShipmentProductAndService>()
            .RuleFor(sp => sp.ShipmentId, f => f.Random.ListItem(shipments).ShipmentId)
            .RuleFor(sp => sp.ProductSvcId, f => f.Random.ListItem(productsAndServices).ProductSvcId)
            .RuleFor(sp => sp.Quantity, f => f.Random.Number(1, 1000));

        var shipmentsProductsAndServices = shipmentProductAndServiceFaker
            .Generate(length)
            .DistinctBy(sp => (sp.ShipmentId, sp.ProductSvcId));
        context.ShipmentsProductsAndServices.AddRange(shipmentsProductsAndServices);
        context.SaveChanges();
    }
}