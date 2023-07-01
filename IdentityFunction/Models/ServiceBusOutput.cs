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

        private ServiceBusOutput(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        internal static string Create(User user)
        {
            var output = new ServiceBusOutput(user.FirstName, user.LastName, user.Email);

            return JsonConvert.SerializeObject(output);
        }
    }
}
