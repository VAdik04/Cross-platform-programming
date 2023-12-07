using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Data.Entities;

[Table("Locations")]
public class Location
{
    [Key]
    [Column("location_id")]
    public int LocationId { get; set; }

    [Column("location_type_code")]
    public string LocationTypeCode { get; set; } = null!;

    [ForeignKey(nameof(LocationTypeCode))]
    public LocationType LocationType { get; set; } = null!;

    [Column("location_address")]
    public string LocationAddress { get; set; } = null!;

    [Column("other_details")]
    public string OtherDetails { get; set; } = null!;
}