using Azure.Messaging.ServiceBus;
using IdentityFunction.Settings;
using Microsoft.Extensions.Options;
using System.Net.Mime;

namespace IdentityFunction.ServiceBus
{
    public class ServiceBusSender : IServiceBusSender
    {
        private readonly ServiceBusSettings _serviceBusSettings;

        public ServiceBusSender(IOptions<ServiceBusSettings> serviceBusSettings)
        {
            _serviceBusSettings = serviceBusSettings.Value;
        }

        public async Task SendAsync(string message, bool isNewsletterSubscriber)
        {
            var client = new ServiceBusClient(_serviceBusSettings.ConnectionString);
            var serviceBusMessage = new ServiceBusMessage(message)
            {
                ContentType = MediaTypeNames.Application.Json,
            };

            if (isNewsletterSubscriber)
                serviceBusMessage.Subject = _serviceBusSettings.NewsletterSubject;

            var sender = client.CreateSender(_serviceBusSettings.PartnerTopic);
            await sender.SendMessageAsync(serviceBusMessage);
        }
    }
}
