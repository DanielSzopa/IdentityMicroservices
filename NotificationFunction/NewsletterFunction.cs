using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using NotificationFunction.Constants;
using NotificationFunction.Email;
using NotificationFunction.Messages;
using System.Text.Json;

namespace NotificationFunction
{
    public class NewsletterFunction
    {
        private readonly ILogger<NewsletterFunction> _logger;
        private readonly IEmailSender _emailSender;

        public NewsletterFunction(ILogger<NewsletterFunction> logger, IEmailSender emailSender)
        {
            _logger = logger;
            _emailSender = emailSender;
        }

        [Function("Newsletter")]
        public async Task Run([ServiceBusTrigger(Topic.Notification, Subscription.Newsletter, Connection = "ServiceBusConnection")] string mySbMsg)
        {
            _logger.LogInformation($"Start processing newsletter action...");

            var user = JsonSerializer.Deserialize<User>(mySbMsg);

            var newsletter = new Newsletter(user.FirstName, user.LastName, user.Email);
            var isSuccessful = await _emailSender.SendEmail(newsletter);

            if (isSuccessful)
            {
                _logger.LogInformation("Successful send email with newsletter");
            }
            else
            {
                _logger.LogError($"Problem with sending email with newsletter, message: {mySbMsg}");
            }
        }
    }
}
