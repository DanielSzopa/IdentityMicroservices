using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using NotyficationFunction.Constants;

namespace NotyficationFunction
{
    public class NewsletterFunction
    {
        private readonly ILogger<NewsletterFunction> _logger;

        public NewsletterFunction(ILogger<NewsletterFunction> logger)
        {
            _logger = logger;
        }

        [Function("Newsletter")]
        public void Run([ServiceBusTrigger(Topic.Notyfication, Subscription.Newsletter, Connection = "ServiceBusConnection")] string mySbMsg)
        {
            _logger.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
        }
    }
}
