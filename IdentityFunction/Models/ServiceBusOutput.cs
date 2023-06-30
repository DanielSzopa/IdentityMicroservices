using IdentityFunction.Entity;
using Newtonsoft.Json;

namespace IdentityFunction.Models
{
    internal class ServiceBusOutput
    {
        [JsonProperty]
        internal string FirstName { get; private set; }
        [JsonProperty]
        internal string LastName { get; private set; }
        [JsonProperty]
        internal string Email { get; private set; }
        [JsonProperty]
        internal bool IsNewsletterSubscriber { get; private set; }

        private ServiceBusOutput(string firstName, string lastName, string email, bool isNewsletterSubscriber)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            IsNewsletterSubscriber = isNewsletterSubscriber;
        }

        internal static string Create(User user, bool isNewsletterSubscriber)
        {
            var output = new ServiceBusOutput(user.FirstName, user.LastName, user.Email, isNewsletterSubscriber);

            return JsonConvert.SerializeObject(output);
        }
    }
}
