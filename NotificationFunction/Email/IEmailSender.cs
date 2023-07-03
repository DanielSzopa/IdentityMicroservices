using YourTutor.Application.Models.EmailBase;

namespace NotificationFunction.Email;

public interface IEmailSender
{
    Task<bool> SendEmail(EmailBase email);
}
