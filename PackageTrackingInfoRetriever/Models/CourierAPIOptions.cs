namespace PackageTrackingInfoRetriever.Models;

public class CourierAPIOptions
{
    public string Name { get; set; } = string.Empty;
    public string TrackingApiUrl { get; set; } = string.Empty;
    public string AuthenticationUrl { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public string X_Merchant_Id { get; set; } = string.Empty;
}
