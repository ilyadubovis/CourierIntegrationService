using CourierIntegrationService.Data;
using CourierIntegrationService.Models;
using Microsoft.EntityFrameworkCore;

namespace CourierIntegrationService.Repositories;

public class TrackingRepository(AppDbContext dbContext) : ITrackingRepository
{
    public async Task<IEnumerable<Shipment>> GetAllShipments(CourierEnum courier) =>
        await GetBaseQuery()
        .Where(x => x.CourierName.ToLower() == Enum.GetName(typeof(CourierEnum), courier)!.ToLower())
        .ToListAsync<Shipment>();

    public async Task<Shipment?> GetShipment(Guid shipmentId) =>
        await GetBaseQuery().SingleOrDefaultAsync<Shipment>(x => x.ShipmentId == shipmentId);

    public async Task<Shipment?> GetShipment(string trackingNumber) =>
        await GetBaseQuery().SingleOrDefaultAsync<Shipment>(x => x.TrackingNumber == trackingNumber);

    public async Task CreateShipment(Shipment shipment)
    {
        var shipmentExists = await GetShipment(shipment.TrackingNumber) != default;
        if (shipmentExists)
        {
            throw new Exception($"Shipment with tracking number {shipment.TrackingNumber} already exists in the database.");
        }

        await dbContext.Shipments.AddAsync(shipment);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateShipment(Guid shipmentId, Shipment shipment)
    {
        var existingShipment = await GetShipment(shipmentId);
        if (existingShipment ==  default)
        {
            throw new Exception($"Shipment with Id {shipmentId} does not exist in the database.");
        }

        existingShipment.ShipmentStatus = shipment.ShipmentStatus;
        existingShipment.Events = shipment.Events;

        dbContext.Update<Shipment>(existingShipment);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteShipment(Guid shipmentId)
    {
        var existingShipment = await GetShipment(shipmentId);
        if (existingShipment == default)
        {
            throw new Exception($"Shipment with Id {shipmentId} does not exist in the database.");
        }
        dbContext.Remove(existingShipment);
        await dbContext.SaveChangesAsync();
    }

    private IQueryable<Shipment> GetBaseQuery() =>
        dbContext.Shipments
        .AsNoTracking()
        .Include(x => x.Shipper)
        .Include(x => x.Receiver)
        .Include(x => x.ShipmentStatus)
        .Include(x => x.Events);
}
