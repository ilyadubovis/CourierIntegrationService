using CourierIntegrationService.Models;
using Microsoft.EntityFrameworkCore;

namespace CourierIntegrationService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach(var status in Enum.GetValues(typeof(ShipmentStatusEnum)))
        {
            modelBuilder.Entity<ShipmentStatus>().HasData(new ShipmentStatus
            {
                ShipmentStatusId = (int)status,
                ShipmentStatusName = Enum.GetName(typeof(ShipmentStatusEnum), status)!
            });
        }
    }

    public DbSet<Shipment> Shipments { get; set; }
}
