using Microsoft.Extensions.Options;
using PackageTrackingInfoRetriever.Models;
using PackageTrackingInfoRetriever.Services.TrackingService.DHL;
using PackageTrackingInfoRetriever.Services.TrackingService.UPS;

namespace PackageTrackingInfoRetriever;

public class TrackingWorker(
    PullDHLTrackingInfoService pullDHLTrackingInfoService,
    PullUPSTrackingInfoService pullUPSTrackingInfoService,
    PushDHLTrackingInfoService pushDHLTrackingInfoService,
    PushUPSTrackingInfoService pushUPSTrackingInfoService,
    IOptions<WorkerOptions> options,
    ILogger<TrackingWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(options.Value.Interval, stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            await RetrieveTrackingInfo();

            await Task.Delay(options.Value.Interval, stoppingToken);
        }
    }

    private async Task RetrieveTrackingInfo()
    {
        // retieve DHL Tracking Info
        var dhlTrackingInfo = await pullDHLTrackingInfoService.PullTrackingInfo();

        // post DHL Tracking Info to CourierIntegrationService
        if (dhlTrackingInfo != default)
        {
            await pushDHLTrackingInfoService.PushTrackingInfo(dhlTrackingInfo);
        }

        // retieve UPS Tracking Info
        var upsTrackingInfo = await pullUPSTrackingInfoService.PullTrackingInfo();

        // post UPS Tracking Info to CourierIntegrationService
        if (upsTrackingInfo != default)
        {
            await pushUPSTrackingInfoService.PushTrackingInfo(upsTrackingInfo);
        }
    }
}
