using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Data.Entities;

[Table("Organizations")]
public class Organization
{
    [Key]
    [Column("organization_id")]
    public int OrganizationId { get; set; }

    [Column("organization_type_code")]
    public string OrganizationTypeCode { get; set; } = null!;

    [ForeignKey(nameof(OrganizationTypeCode))]
    public OrganizationType OrganizationType { get; set; } = null!;

    [Column("organization_details")]
    public string OrganizationDetails { get; set; } = null!;
}