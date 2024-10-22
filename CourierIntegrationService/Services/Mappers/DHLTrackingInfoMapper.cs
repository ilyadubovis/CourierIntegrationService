using CourierIntegrationService.Models;
using CourierIntegrationService.Models.DHL;

namespace CourierIntegrationService.Services.Mappers;

public class DHLTrackingInfoMapper
{
    public TrackingInfo Map(DHL_TrackingInfo trackingInfo)
    {
        List<Shipment> shipments = [];

        foreach (var dhlShipmentInfo in trackingInfo.Shipments)
        {
            shipments.Add(
                new()
                {
                    CourierName = Enum.GetName(typeof(CourierEnum), CourierEnum.DHL)!,
                    TrackingNumber = dhlShipmentInfo.ShipmentTrackingNumber,
                    Shipper = MapShipperInfo(dhlShipmentInfo.ShipperDetails),
                    Receiver = MapReceiverInfo(dhlShipmentInfo.ReceiverDetails)
                }
            );
        }

        return new TrackingInfo
        {
            Shipments = shipments
        };
    }

    private static Shipper MapShipperInfo(DHL_ShipperDetails shipperInfo) =>
        new()
        {
            Name = shipperInfo.Name,
            Address = MapAddress(shipperInfo.PostalAddress)
        };

    private static Receiver MapReceiverInfo(DHL_ReceiverDetails receiverInfo) =>
        new()
        {
            Name = receiverInfo.Name,
            Address = MapAddress(receiverInfo.PostalAddress)
        };

    private static Address MapAddress(DHL_PostalAddress address) =>
        new()
        {
            City = address.CityName,
            StateCode = address.ProvinceCode,
            ZipCode = address.PostalCode,
            CountryCode = address.CountryCode

        };
}
