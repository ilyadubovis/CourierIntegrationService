using System.ComponentModel.DataAnnotations;

namespace CourierIntegrationService.Models;

public class Address
{
    [Key]
    public Guid AddressId { get; set; }

    [Required]
    [StringLength(50)]
    public required string City { get; set; }

    [Required]
    [StringLength(10)]
    public required string ZipCode { get; set; }

    [Required]
    [StringLength(5)]
    public required string StateCode { get; set; }

    [Required]
    [StringLength(5)]
    public required string CountryCode { get; set; }
}
