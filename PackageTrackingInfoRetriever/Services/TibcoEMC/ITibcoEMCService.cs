namespace PackageTrackingInfoRetriever.Services.TibcoEMC;

public interface ITibcoEMCService
{
    public void ProduceMessage(string jsonString);

    public void ConsumeMessage();
}
