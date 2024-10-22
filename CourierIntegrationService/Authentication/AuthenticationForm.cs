using System.ComponentModel;
using System.Text.Json.Serialization;

namespace CourierIntegrationService.Authentication;

public class AuthenticationForm
{
    [JsonPropertyName("grant_type")]
    [DefaultValue("client_credentials")]
    public string GrantType { get; set; } = "client_credentials";

    [JsonPropertyName("client_id")]
    public required string ClientId { get; set; }

    [JsonPropertyName("client_secret")]
    public required string ClientSecret { get; set; }
}
