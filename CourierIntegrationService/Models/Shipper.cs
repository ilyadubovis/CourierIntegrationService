using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourierIntegrationService.Models;

public class Shipper
{
    [Key]
    public Guid ShipperId { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public Guid AddressId { get; set; }

    [ForeignKey("AddressId")]
    public required Address Address { get; set; }
}
