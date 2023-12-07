using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Data.Entities;

[Table("Products_and_Services")]
public class ProductAndService
{
    [Key]
    [Column("product_svc_id")]
    public int ProductSvcId { get; set; }

    [Column("product_svc_type_code")]
    public string ProductSvcTypeCode { get; set; } = null!;

    [ForeignKey(nameof(ProductSvcTypeCode))]
    public ProductAndServiceType ProductAndServiceType { get; set; } = null!;

    [Column("product_svc_details")]
    public string ProductSvcDetails { get; set; } = null!;

    [Column("quantity")]
    public int Quantity { get; set; }
}
