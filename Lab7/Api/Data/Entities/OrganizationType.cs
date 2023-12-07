using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Data.Entities;

[Table("Organization_Types")]
public class OrganizationType
{
    [Key]
    [Column("organization_type_code")]
    public string OrganizationTypeCode { get; set; } = null!;

    [Column("organization_type_description")]
    public string OrganizationTypeDescription { get; set; } = null!;
}