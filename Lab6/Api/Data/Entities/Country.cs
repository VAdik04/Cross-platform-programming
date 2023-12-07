using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Data.Entities;

[Table("Countries")]
public class Country
{
    [Key]
    [Column("country_code")]
    public string CountryCode { get; set; } = null!;

    [Column("country_name")]
    public string CountryName { get; set; } = null!;

    [Column("country_currency")]
    public string CountryCurrency { get; set; } = null!;

    [Column("languages_spoken")]
    public string LanguagesSpoken { get; set; } = null!;

    [Column("USD_exchange_rate")]
    public decimal USDExchangeRate { get; set; }

    [Column("USD_exchange_date")]
    public DateTimeOffset USDExchangeDate { get; set; }
}
