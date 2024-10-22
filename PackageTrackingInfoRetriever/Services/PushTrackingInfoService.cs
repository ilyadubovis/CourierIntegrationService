
using Microsoft.Extensions.Options;
using PackageTrackingInfoRetriever.Authentication;
using PackageTrackingInfoRetriever.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace PackageTrackingInfoRetriever.Services;

abstract public class PushTrackingInfoService
{ 
    protected readonly IHttpClientFactory _httpClientFactory;

    protected readonly IOptions<CourierIntegrationServiceOptions> _options;

    private Token? _authenticationToken;

    public PushTrackingInfoService(IHttpClientFactory httpClientFactory, IOptions<CourierIntegrationServiceOptions> options)
    {
        _httpClientFactory = httpClientFactory;
        _options = options;
    }

    protected async Task<Token> GetAuthenticationToken()
    {
        if (_authenticationToken == default || _authenticationToken.IsExpired())
        {
            // request a new authentication token from Courier Integration Service
            using HttpClient client = _httpClientFactory.CreateClient();

            using HttpResponseMessage response = await client.PostAsJsonAsync(_options.Value.AuthenticationUrl,
                new CourierIntegrationServiceAuthenticationForm() { ClientId = _options.Value.ClientId, ClientSecret = _options.Value.ClientSecret });

            response.EnsureSuccessStatusCode();

            var tokenInfo = JsonSerializer.Deserialize<CourierIntegrationServiceAuthenticationResponse>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web));

            if (tokenInfo == default)
            {
                throw new Exception("Could not obtain acess token from Courier Integration Service");
            }

            _authenticationToken = new Token() { AccessToken = tokenInfo.AccessToken, ExpirationTime = DateTime.UtcNow.AddSeconds(tokenInfo.ExpiresIn) };

        }
        return _authenticationToken;
    }
}
