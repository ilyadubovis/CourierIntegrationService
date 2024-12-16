using Microsoft.Extensions.Options;
using TIBCO.EMS;

namespace PackageTrackingInfoRetriever.Services.TibcoEMC;

public class TibcoEMCService(IOptions<TibcoEMCOptions> options) : ITibcoEMCService, IExceptionListener
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

    public void ConsumeMessage()
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

            // set the exception listener
            connection.ExceptionListener = this;

            // create the destination
            var destination = session.CreateQueue(options.Value.Queue);

            // create the consumer
            var msgConsumer = session.CreateConsumer(destination);

            // start the connection
            connection.Start();

            // read messages
            while (true)
            {
                // receive the message
                var msg = msgConsumer.Receive();
                if (msg == null)
                    break;

                Console.WriteLine("Received message: " + msg);
            }
        }
        finally
        {
            connection?.Close();
        }
    }

    public void OnException(EMSException e)
    {
        // print the connection exception status
        Console.Error.WriteLine("Connection Exception: " + e.Message);
    }
}
