using YourTutor.Application.Models.EmailBase;

namespace NotyficationFunction.Email;

internal class Newsletter : EmailBase
{
    public Newsletter(string firstName, string lastName, string to) : base($"Welcome {firstName} {lastName} in our newsletter","In our newsletter, we write about curious technologies...","newsletter", to)
    {
        
    }
}
