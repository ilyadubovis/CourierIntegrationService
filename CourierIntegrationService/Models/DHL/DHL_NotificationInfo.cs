#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor
namespace CourierIntegrationService.Models.DHL;


public class DHL_TimestampNotification
{
    public Guid NotificationID { get; set; }
    public string Type { get; set; }
    public string ShipmentID { get; set; }
    public string HousebillNumber { get; set; }
    public string TimestampCode { get; set; }
    public string TimestampDecription { get; set; }
    public DateTime TimestampDateTime { get; set; }
    public string TimestampText { get; set; }
    public string ShipmentTrackingURL { get; set; }
}

