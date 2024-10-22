using CourierIntegrationService.Models;
using CourierIntegrationService.Repositories;

namespace CourierIntegrationService.Services.Tracking;

public class TrackingService(ITrackingRepository trackingRepository) : ITrackingService
{
    public async Task<TrackingInfo> GetTrackingInfo(CourierEnum courier) =>
        new TrackingInfo()
        {
            Shipments = (await trackingRepository.GetAllShipments(courier)).ToList<Shipment>()
        };

    public async Task<Shipment?> GetShipment(Guid shipmentId) =>
        await trackingRepository.GetShipment(shipmentId);

    public async Task<Shipment?> GetShipment(string trackingNumber) =>
        await trackingRepository.GetShipment(trackingNumber);
    
    public async Task CreateShipment(Shipment shipment) =>
        await trackingRepository.CreateShipment(shipment);

    public async Task UpdateShipment(Guid shipmentId, Shipment shipment) =>
        await trackingRepository.UpdateShipment(shipmentId, shipment);

    public async Task DeleteShipment(Guid shipmentId) =>
        await trackingRepository.DeleteShipment(shipmentId);
}
