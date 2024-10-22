using CourierIntegrationService.Models;
using CourierIntegrationService.Models.UPS;

namespace CourierIntegrationService.Services.Mappers;

public class UPSTrackingInfoMapper
{
    public TrackingInfo Map(UPS_TrackingInfo trackingInfo)
    {
        List<Shipment> shipments = [];

        foreach (var shipmentInfo in trackingInfo.Shipments)
        {
            shipments.Add(
                new()
                {
                    CourierName = Enum.GetName(typeof(CourierEnum), CourierEnum.UPS)!,
                    TrackingNumber = shipmentInfo.ShipmentTrackingNumber,
                    Shipper = MapShipperInfo(shipmentInfo.ShipperDetails),
                    Receiver = MapReceiverInfo(shipmentInfo.ReceiverDetails)
                }
            );
        }

        return new TrackingInfo
        {
            Shipments = shipments
        };
    }

    private static Shipper MapShipperInfo(UPS_ShipperDetails shipperInfo) =>
        new()
        {
            Name = shipperInfo.Name,
            Address = MapAddress(shipperInfo.PostalAddress)
        };

    private static Receiver MapReceiverInfo(UPS_ReceiverDetails receiverInfo) =>
        new()
        {
            Name = receiverInfo.Name,
            Address = MapAddress(receiverInfo.PostalAddress)
        };

    private static Address MapAddress(UPS_PostalAddress address) =>
        new()
        {
            City = address.CityName,
            StateCode = address.ProvinceCode,
            ZipCode = address.PostalCode,
            CountryCode = address.CountryCode

        };
}
