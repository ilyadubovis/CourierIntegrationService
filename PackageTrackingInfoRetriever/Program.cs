using PackageTrackingInfoRetriever;
using PackageTrackingInfoRetriever.Models;
using PackageTrackingInfoRetriever.Services.TibcoEMC;
using PackageTrackingInfoRetriever.Services.TrackingService.DHL;
using PackageTrackingInfoRetriever.Services.TrackingService.UPS;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<WorkerOptions>(builder.Configuration.GetSection("TrackingWorker"));
builder.Services.Configure<CourierIntegrationServiceOptions>(builder.Configuration.GetSection("CourierIntegrationService"));
builder.Services.Configure<TibcoEMCOptions>(builder.Configuration.GetSection("NotificationWorker"));

builder.Services.AddHttpClient();

builder.Services.AddSingleton<PullDHLTrackingInfoService>();
builder.Services.AddSingleton<PullUPSTrackingInfoService>();

builder.Services.AddSingleton<PushDHLTrackingInfoService>();
builder.Services.AddSingleton<PushUPSTrackingInfoService>();

builder.Services.AddSingleton<ITibcoEMCService, TibcoEMCService>();

builder.Services.AddHostedService<TrackingWorker>();

builder.Services.AddHostedService<NotificationWorker>();

var host = builder.Build();
host.Run();
