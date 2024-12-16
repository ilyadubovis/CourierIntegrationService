using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PackageTrackingInfoRetriever.Authentication;
using PackageTrackingInfoRetriever.Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace PackageTrackingInfoRetriever.Services.TrackingService.UPS;

public class PullUPSTrackingInfoService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
{
    private Token? _authenticationToken;

    public async Task<UPS_TrackingInfo?> PullTrackingInfo()
    {
        using HttpClient client = httpClientFactory.CreateClient();

        return await client.GetFromJsonAsync<UPS_TrackingInfo>(GetCourierApiUrl(), new JsonSerializerOptions(JsonSerializerDefaults.Web));
    }

    protected async Task<Token> GetAuthenticationToken()
    {
        if (_authenticationToken == default || _authenticationToken.IsExpired())
        {
            // request a new authentication token from UPS Tracking API
            using HttpClient client = httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Add("x-merchant-id", GetCourierAPIInfo()!.X_Merchant_Id);
            client.DefaultRequestHeaders.Add("Authorization", $"Basic {GetCourierApiCredentials()}");

            List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));

            using HttpResponseMessage response = await client.PostAsJsonAsync(GetCourierAPIInfo()!.AuthenticationUrl, new FormUrlEncodedContent(postData));

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

    private string GetCourierApiUrl() =>
        GetCourierAPIInfo()!.TrackingApiUrl;

    private string GetCourierApiCredentials()
    {
        var courierConfig = GetCourierAPIInfo()!;
        return Convert.ToBase64String(Encoding.ASCII.GetBytes(courierConfig.Username + ":" + courierConfig.Password));
    }

    private CourierAPIOptions? GetCourierAPIInfo()
    {
        var courierName = Enum.GetName(typeof(CourierEnum), CourierEnum.DHL);
        var courierApiInfos = configuration.GetSection("CourierAPI").Get<CourierAPIOptions[]>();
        return courierApiInfos?.SingleOrDefault(x => x.Name.Equals(courierName, StringComparison.InvariantCultureIgnoreCase));
    }
}
