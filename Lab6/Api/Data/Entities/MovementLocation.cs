using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Entities;

[Table("Movement_Locations")]
public class MovementLocation
{
    [Key]
    [Column("shipment_location_id")]
    public int ShipmentLocationId { get; set; }

    [Column("shipment_id")]
    public int ShipmentId { get; set; }

    [ForeignKey(nameof(ShipmentId))]
    public Shipment Shipment { get; set; } = null!;

    [Column("from_location_id")]
    public int FromLocationId { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [ForeignKey(nameof(FromLocationId))]
    public Location FromLocation { get; set; } = null!;

    [Column("to_location_id")]
    public int ToLocationId { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [ForeignKey(nameof(ToLocationId))]
    public Location ToLocation { get; set; } = null!;

    [Column("date_started")]
    public DateTimeOffset DateStarted { get; set; }

    [Column("date_completed")]
    public DateTimeOffset DateCompleted { get; set; }

    [Column("other_details")]
    public string OtherDetails { get; set; } = null!;
}