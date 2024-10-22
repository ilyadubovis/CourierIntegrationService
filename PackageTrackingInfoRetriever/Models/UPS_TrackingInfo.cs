#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor
namespace PackageTrackingInfoRetriever.Models;

public class UPS_TrackingInfo
{
    public List<UPS_Shipment> Shipments { get; set; }
}

public class UPS_ActualDimensions
{
}

public class UPS_Dimensions
{
    public int Length { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
}

public class UPS_ShipmentEvent
{
    public string Date { get; set; }
    public string Time { get; set; }
    public string TypeCode { get; set; }
    public string Description { get; set; }
    public List<UPS_ServiceArea> ServiceArea { get; set; }
    public List<UPS_Remark> Remarks { get; set; }
    public string SignedBy { get; set; }
}

public class UPS_ShipmentPiece
{
    public int Number { get; set; }
    public string TypeCode { get; set; }
    public string ShipmentTrackingNumber { get; set; }
    public string TrackingNumber { get; set; }
    public int Weight { get; set; }
    public double ActualWeight { get; set; }
    public UPS_Dimensions Dimensions { get; set; }
    public UPS_ActualDimensions ActualDimensions { get; set; }
    public string UnitOfMeasurements { get; set; }
    public List<UPS_ShipmentEvent> Events { get; set; }
}

public class UPS_PostalAddress
{
    public string CityName { get; set; }
    public string PostalCode { get; set; }
    public string ProvinceCode { get; set; }
    public string CountryCode { get; set; }
}

public class UPS_ReceiverDetails
{
    public string Name { get; set; }
    public UPS_PostalAddress PostalAddress { get; set; }
    public List<UPS_ServiceArea> ServiceArea { get; set; }
}

public class UPS_Remark
{
    public string Value { get; set; }
    public string Details { get; set; }
}

public class UPS_ServiceArea
{
    public string Code { get; set; }
    public string Description { get; set; }
    public string FacilityCode { get; set; }
}

public class UPS_Shipment
{
    public string ShipmentTrackingNumber { get; set; }
    public string Status { get; set; }
    public DateTime ShipmentTimestamp { get; set; }
    public string ProductCode { get; set; }
    public string Description { get; set; }
    public UPS_ShipperDetails ShipperDetails { get; set; }
    public UPS_ReceiverDetails ReceiverDetails { get; set; }
    public double TotalWeight { get; set; }
    public string UnitOfMeasurements { get; set; }
    public List<UPS_ShipmentEvent> Events { get; set; }
    public int NumberOfPieces { get; set; }
    public List<UPS_ShipmentPiece> Pieces { get; set; }
    public DateTime EstimatedDeliveryDate { get; set; }
}

public class UPS_ShipperDetails
{
    public string Name { get; set; }
    public UPS_PostalAddress PostalAddress { get; set; }
    public List<UPS_ServiceArea> ServiceArea { get; set; }
}
