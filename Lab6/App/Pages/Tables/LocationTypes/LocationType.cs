namespace App.Pages.Tables.LocationTypes;


public class LocationType
{
    public required string LocationTypeCode { get; set; }

    public required string CountryCode { get; init; }

    public required string LocationTypeDescription { get; init; }
}