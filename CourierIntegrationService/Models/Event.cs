using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourierIntegrationService.Models;

public class Event
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public DateTime Timestamp { get; set; }

    [Required]
    public string Type { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string ServiceAreaCode { get; set; } = string.Empty;

    [Required]
    public string ServiceAreaDescription { get; set; } = string.Empty;

    public Guid ShipmentId { get; set; }

    [ForeignKey("ShipmentId")]
    public Shipment Shipment { get; set; } = null!;
}
