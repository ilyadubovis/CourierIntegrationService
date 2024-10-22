namespace PackageTrackingInfoRetriever.Models;

public class Token
{
    public string AccessToken { get; set; } = string.Empty;

    public DateTime ExpirationTime { get; set; } = DateTime.MinValue;

    public bool IsExpired() => ExpirationTime < DateTime.UtcNow;
}
