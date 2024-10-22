
using Microsoft.Extensions.Options;
using PackageTrackingInfoRetriever.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace PackageTrackingInfoRetriever.Services.DHL;

public class PullDHLTrackingInfoService(IHttpClientFactory httpClientFactory, IOptions<CourierAPIOptions> options)
{
    private Token? _authenticationToken;

    public async Task<DHL_TrackingInfo?> PullTrackingInfo()
    {
        using HttpClient client = httpClientFactory.CreateClient();

        client.DefaultRequestHeaders.Add("Authorization", $"Basic {GetCourierApiCredentials()}");

        return await client.GetFromJsonAsync<DHL_TrackingInfo>(GetCourierApiUrl(), new JsonSerializerOptions(JsonSerializerDefaults.Web));
    }

    private string GetCourierApiUrl() =>
        GetCourierAPIInfo()!.TrackingApiUrl;

    private string GetCourierApiCredentials()
    {
        var courierConfig = GetCourierAPIInfo()!;
        return Convert.ToBase64String(Encoding.ASCII.GetBytes(courierConfig.Username + ":" + courierConfig.Password));
    }

    private CourierAPIInfo? GetCourierAPIInfo()
    {
        var courierName = Enum.GetName(typeof(CourierEnum), CourierEnum.DHL);
        return options.Value.CourierApiInfos.SingleOrDefault(x => x.Name.Equals(courierName, StringComparison.InvariantCultureIgnoreCase));
    }

}
