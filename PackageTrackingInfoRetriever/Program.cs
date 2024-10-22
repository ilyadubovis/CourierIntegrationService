using PackageTrackingInfoRetriever;
using PackageTrackingInfoRetriever.Models;
using PackageTrackingInfoRetriever.Services.DHL;
using PackageTrackingInfoRetriever.Services.UPS;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<WorkerOptions>(builder.Configuration.GetSection("Worker"));
builder.Services.Configure<CourierIntegrationServiceOptions>(builder.Configuration.GetSection("CourierIntegrationService"));
builder.Services.Configure<CourierAPIOptions>(builder.Configuration.GetSection("CourierAPI"));

builder.Services.AddHttpClient();

builder.Services.AddScoped<PullDHLTrackingInfoService>();
builder.Services.AddScoped<PullUPSTrackingInfoService>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
