namespace App.Pages.Tables.MovementLocations;


public class MovementLocation
{
    public int ShipmentLocationId { get; set; }

    public int ShipmentId { get; set; }

    public int FromLocationId { get; set; }

    public int ToLocationId { get; set; }

    public DateTimeOffset DateStarted { get; set; }

    public DateTimeOffset DateCompleted { get; set; }

    public string OtherDetails { get; set; } = null!;
}