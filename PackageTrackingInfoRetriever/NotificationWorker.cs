using Microsoft.Extensions.Options;
using PackageTrackingInfoRetriever.Models;
using PackageTrackingInfoRetriever.Services.TibcoEMC;
using PackageTrackingInfoRetriever.Services.TrackingService.DHL;
using PackageTrackingInfoRetriever.Services.TrackingService.UPS;
using System.Xml.Linq;
using TIBCO.EMS;

namespace PackageTrackingInfoRetriever;

public class NotificationWorker(
    ITibcoEMCService tibcoEMCService,
    ILogger<TrackingWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        tibcoEMCService.ConsumeMessage();
    }

}
