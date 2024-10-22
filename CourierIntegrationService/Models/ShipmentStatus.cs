using System.ComponentModel.DataAnnotations;
namespace CourierIntegrationService.Models;

public class ShipmentStatus
{
    [Key]
    public int ShipmentStatusId { get; set; }

    [Required]
    required public string ShipmentStatusName { get; set; }
}


public enum ShipmentStatusEnum
{
    Unknown = 1,
    PreTransit = 2,
    Transit = 3,
    Delivered = 4,
    Failure = 5
}

