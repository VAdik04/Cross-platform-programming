using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Data.Entities;

[Table("Location_Types")]
public class LocationType
{
    [Key]
    [Column("location_type_code")]
    public string LocationTypeCode { get; set; } = null!;

    [Column("country_code")]
    public string CountryCode { get; set; } = null!;

    [ForeignKey(nameof(CountryCode))]
    public Country Country { get; set; } = null!;

    [Column("location_type_description")]
    public string LocationTypeDescription { get; set; } = null!;
}