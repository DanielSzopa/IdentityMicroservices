using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using NotyficationFunction.Constants;

namespace NotyficationFunction
{
    public class VeryficationEmailFunction
    {
        private readonly ILogger<VeryficationEmailFunction> _logger;

        public VeryficationEmailFunction(ILogger<VeryficationEmailFunction> logger)
        {
            _logger = logger;
        }

        [Function("VeryficationEmail")]
        public void Run([ServiceBusTrigger(Topic.Notyfication, Subscription.VeryficationEmail, Connection = "ServiceBusConnection")] string mySbMsg)
        {
            _logger.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
        }
    }
}
