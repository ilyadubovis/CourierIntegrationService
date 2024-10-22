namespace CourierIntegrationService.Services.Authentication;

public class AuthenticationOptions
{
    public string SecretKey { get; set; } = string.Empty;

    public string ClientId { get; set; } = string.Empty;

    public string ClientSecret { get; set; } = string.Empty;
}
