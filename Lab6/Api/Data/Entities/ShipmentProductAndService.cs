using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Data.Entities;

[PrimaryKey(nameof(ShipmentId), nameof(ProductSvcId))]
[Table("Shipments_Products_and_Services")]
public class ShipmentProductAndService
{
    [Column("shipment_id")]
    public int ShipmentId { get; set; }

    [ForeignKey("ShipmentId")]
    public Shipment Shipment { get; set; } = null!;

    [Column("product_svc_id")]
    public int ProductSvcId { get; set; }

    [ForeignKey("ProductSvcId")]
    public ProductAndService ProductAndService { get; set; } = null!;

    [Column("quantity")]
    public int Quantity { get; set; }
}