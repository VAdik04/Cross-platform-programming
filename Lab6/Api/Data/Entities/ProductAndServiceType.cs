using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Data.Entities;

[Table("Product_and_Service_Types")]
public class ProductAndServiceType
{
    [Key]
    [Column("product_svc_type_code")]
    public string ProductSvcTypeCode { get; set; } = null!;

    [Column("product_svc_type_description")]
    public string ProductSvcTypeDescription { get; set; } = null!;
}
