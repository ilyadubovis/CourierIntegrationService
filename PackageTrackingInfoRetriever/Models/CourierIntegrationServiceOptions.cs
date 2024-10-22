namespace PackageTrackingInfoRetriever.Models;

public class CourierIntegrationServiceOptions
{
    public string BaseUrl { get; set; } = string.Empty;

    public string AuthenticationUrl { get; set; } = string.Empty;

    public string ClientId { get; set; } = string.Empty;

    public string ClientSecret { get; set; } = string.Empty;
}
