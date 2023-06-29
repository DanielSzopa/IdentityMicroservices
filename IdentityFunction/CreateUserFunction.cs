using IdentityFunction.Entity;
using IdentityFunction.Requests;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System.Net;
using System.Net.Mime;

namespace IdentityFunction
{
    public class CreateUserFunction
    {
        private readonly ILogger _logger;

        public CreateUserFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CreateUserFunction>();
        }

        [Function("CreateUser")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous,  "Post")] HttpRequestData req)
        {
            _logger.LogInformation("Start processing request...");

            var request = await req.ReadFromJsonAsync<CreateUser>();
            var user = User.Create(request.FirstName, request.LastName, request.Email);

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add(HeaderNames.ContentType, MediaTypeNames.Text.Plain);

            _logger.LogInformation("Some logic to register account");

            await response.WriteStringAsync("Account is registered, welcome in our family");

            return response;
        }
    }
}
