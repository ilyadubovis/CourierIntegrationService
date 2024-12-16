using Microsoft.Extensions.Options;
using TIBCO.EMS;

namespace CourierIntegrationService.Services.TibcoEMC;

public class TibcoEMCService(IOptions<TibcoEMCOptions> options) : ITibcoEMCService
{
    public void ProduceMessage(string jsonString)
    {
        Connection? connection = null;
        try
        {
            var factory = new ConnectionFactory(options.Value.ServerUrl);
            EMSSSL.SetTargetHostName(options.Value.SSLTargetHost);

            // create connection
            connection = factory.CreateConnection(options.Value.Username, options.Value.Password);

            // create the session
            var session = connection.CreateSession(false, Session.AUTO_ACKNOWLEDGE);

            // create the destination
            var destination = session.CreateQueue(options.Value.Queue);

            // create the producer
            var msgProducer = session.CreateProducer(destination);

            // create the message
            var message = session.CreateTextMessage(jsonString);

            // send the message
            msgProducer.Send(message);
        }
        finally
        {
            connection?.Close();
        }
    }
}
