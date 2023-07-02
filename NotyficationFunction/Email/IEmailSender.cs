using YourTutor.Application.Models.EmailBase;

namespace NotyficationFunction.Email;

public interface IEmailSender
{
    Task<bool> SendEmail(EmailBase email);
}
