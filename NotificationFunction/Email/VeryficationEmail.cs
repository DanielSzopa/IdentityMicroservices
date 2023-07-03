using YourTutor.Application.Models.EmailBase;

namespace NotificationFunction.Email;

internal class VeryficationEmail : EmailBase
{
    public VeryficationEmail(string firstName, string lastName, string to) : base($"Welcome {firstName} {lastName} in our system","Please, click link to veryficate your email <some_link>","veryfication", to)
    {
        
    }
}
