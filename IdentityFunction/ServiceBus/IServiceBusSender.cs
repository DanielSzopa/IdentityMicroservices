namespace IdentityFunction.ServiceBus;

public interface IServiceBusSender
{
    Task SendAsync(string message, bool isNewsletterSubscriber);
}
