using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourierIntegrationService.Models;

public class Receiver
{
    [Key]
    public Guid ReceiverId { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public Guid AddressId { get; set; }

    [ForeignKey("AddressId")]
    public virtual Address? Address { get; set; }
}
