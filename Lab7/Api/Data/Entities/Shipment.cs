using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Data.Entities;

[Table("Shipments")]
public class Shipment
{
    [Key]
    [Column("shipment_id")]
    public int ShipmentId { get; set; }

    [Column("from_organization_id")]
    public int FromOrganizationId { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [ForeignKey(nameof(FromOrganizationId))]
    public Organization? FromOrganization { get; set; } = null!;

    [Column("to_organization_id")]
    public int ToOrganizationId { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [ForeignKey(nameof(ToOrganizationId))]
    public Organization? ToOrganization { get; set; } = null!;

    [Column("shipment_details")]
    public string ShipmentDetails { get; set; } = null!;
}