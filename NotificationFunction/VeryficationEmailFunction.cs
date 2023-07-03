using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using NotificationFunction.Constants;
using NotificationFunction.Email;
using NotificationFunction.Messages;
using System.Text.Json;

namespace NotificationFunction
{
    public class VeryficationEmailFunction
    {
        private readonly ILogger<VeryficationEmailFunction> _logger;
        private readonly IEmailSender _emailSender;

        public VeryficationEmailFunction(ILogger<VeryficationEmailFunction> logger, IEmailSender emailSender)
        {
            _logger = logger;
            _emailSender = emailSender;
        }

        [Function("VeryficationEmail")]
        public async Task Run([ServiceBusTrigger(Topic.Notification, Subscription.VeryficationEmail, Connection = "ServiceBusConnection")] string mySbMsg)
        {
            _logger.LogInformation($"Start processing veryficationEmail action...");

            var user = JsonSerializer.Deserialize<User>(mySbMsg);
            var veryficationEmail = new VeryficationEmail(user.FirstName, user.LastName, user.Email);

            var isSuccessful = await _emailSender.SendEmail(veryficationEmail);

            if (isSuccessful)
            {
                _logger.LogInformation("Successful send veryfication email");
            }
            else
            {
                _logger.LogError($"Problem with sending veryfication email, message: {mySbMsg}");
            }
        }
    }
}
