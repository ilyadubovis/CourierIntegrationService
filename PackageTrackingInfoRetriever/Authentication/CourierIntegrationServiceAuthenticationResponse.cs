using System.Text.Json.Serialization;

namespace PackageTrackingInfoRetriever.Authentication;

public class CourierIntegrationServiceAuthenticationResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;

    [JsonPropertyName("token_type")]
    public string TokenType { get; set; } = "Bearer";

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; } = 3600;

    public string Scope { get; set; } = "create";
}

