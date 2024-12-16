using Microsoft.Extensions.Options;
using PackageTrackingInfoRetriever.Models;
using System.Net.Http.Json;

namespace PackageTrackingInfoRetriever.Services.TrackingService.DHL;

public class PushDHLTrackingInfoService : TrackingInfoService
{
    public PushDHLTrackingInfoService(IHttpClientFactory httpClientFactory, IOptions<CourierIntegrationServiceOptions> options) : base(httpClientFactory, options)
    {
    }


    public async Task<bool> PushTrackingInfo(DHL_TrackingInfo trackingInfo)
    {
        using HttpClient client = _httpClientFactory.CreateClient();
        var accessToken = (await GetAuthenticationToken()).AccessToken;
        client.DefaultRequestHeaders.Add("Authorization", $"Bearedr {accessToken}");

        using HttpResponseMessage response = await client.PostAsJsonAsync(_options.Value.BaseUrl, trackingInfo);

        return response.IsSuccessStatusCode;
    }
}
