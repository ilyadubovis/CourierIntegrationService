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
    LabelCreated = 2,
    OutForDelivery = 3,
    Shipped = 4,
    AtLocalFacility = 5,
    Delivered = 6,
    Delayed = 7,
    Cancelled = 8
}

