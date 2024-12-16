namespace PackageTrackingInfoRetriever.Services.TibcoEMC;

public class TibcoEMCOptions
{
    public string ServerUrl { get; set; } = string.Empty;

    public string SSLTargetHost { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Topic { get; set; } = string.Empty;

    public string Queue { get; set; } = string.Empty;
}
