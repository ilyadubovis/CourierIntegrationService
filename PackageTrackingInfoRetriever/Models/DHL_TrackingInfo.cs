#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor
namespace PackageTrackingInfoRetriever.Models;

public class DHL_TrackingInfo
{
    public List<DHL_Shipment> Shipments { get; set; }
}

public class DHL_ActualDimensions
{
}

public class DHL_Dimensions
{
    public int Length { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
}

public class DHL_ShipmentEvent
{
    public string Date { get; set; }
    public string Time { get; set; }
    public string TypeCode { get; set; }
    public string Description { get; set; }
    public List<DHL_ServiceArea> ServiceArea { get; set; }
    public List<DHL_Remark> Remarks { get; set; }
    public string SignedBy { get; set; }
}

public class DHL_ShipmentPiece
{
    public int Number { get; set; }
    public string TypeCode { get; set; }
    public string ShipmentTrackingNumber { get; set; }
    public string TrackingNumber { get; set; }
    public int Weight { get; set; }
    public double ActualWeight { get; set; }
    public DHL_Dimensions Dimensions { get; set; }
    public DHL_ActualDimensions ActualDimensions { get; set; }
    public string UnitOfMeasurements { get; set; }
    public List<DHL_ShipmentEvent> Events { get; set; }
}

public class DHL_PostalAddress
{
    public string CityName { get; set; }
    public string PostalCode { get; set; }
    public string ProvinceCode { get; set; }
    public string CountryCode { get; set; }
}

public class DHL_ReceiverDetails
{
    public string Name { get; set; }
    public DHL_PostalAddress PostalAddress { get; set; }
    public List<DHL_ServiceArea> ServiceArea { get; set; }
}

public class DHL_Remark
{
    public string Value { get; set; }
    public string Details { get; set; }
}

public class DHL_ServiceArea
{
    public string Code { get; set; }
    public string Description { get; set; }
    public string FacilityCode { get; set; }
}

public class DHL_Shipment
{
    public string ShipmentTrackingNumber { get; set; }
    public string Status { get; set; }
    public DateTime ShipmentTimestamp { get; set; }
    public string ProductCode { get; set; }
    public string Description { get; set; }
    public DHL_ShipperDetails ShipperDetails { get; set; }
    public DHL_ReceiverDetails ReceiverDetails { get; set; }
    public double TotalWeight { get; set; }
    public string UnitOfMeasurements { get; set; }
    public List<DHL_ShipmentEvent> Events { get; set; }
    public int NumberOfPieces { get; set; }
    public List<DHL_ShipmentPiece> Pieces { get; set; }
    public DateTime EstimatedDeliveryDate { get; set; }
}

public class DHL_ShipperDetails
{
    public string Name { get; set; }
    public DHL_PostalAddress PostalAddress { get; set; }
    public List<DHL_ServiceArea> ServiceArea { get; set; }
}
