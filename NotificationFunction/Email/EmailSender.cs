using FluentEmail.Core;
using FluentEmail.SendGrid;
using Microsoft.Extensions.Options;
using NotificationFunction.Settings;
using YourTutor.Application.Models.EmailBase;

namespace NotificationFunction.Email;

public class EmailSender : IEmailSender
{
    private readonly SendGridSettings _settings;

    public EmailSender(IOptions<SendGridSettings> sendGridSettings)
    {
        _settings = sendGridSettings.Value;
    }

    public async Task<bool> SendEmail(EmailBase email)
    {
        var sender = new SendGridSender(_settings.ApiKey);

        IFluentEmail fluentEmail = FluentEmail.Core.Email
                .From(_settings.From)
                .To(email.To)
                .Subject(email.Subject)
                .Body(email.Body)
                .Tag(email.Tag);

        var result = await sender.SendAsync(fluentEmail);

        return result.Successful;
    }
}
