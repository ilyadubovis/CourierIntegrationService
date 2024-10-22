using CourierIntegrationService.Models;

namespace CourierIntegrationService.Services.Tracking;

public interface ITrackingService
{
    public Task<TrackingInfo> GetTrackingInfo(CourierEnum courier);

    public Task<Shipment?> GetShipment(Guid shipmentId);

    public Task<Shipment?> GetShipment(string trackingNumber);

    public Task CreateShipment(Shipment shipment);

    public Task UpdateShipment(Guid shipmentId, Shipment shipment);

    public Task DeleteShipment(Guid shipmentId);
}
