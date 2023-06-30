using IdentityFunction.Entity;
using IdentityFunction.Exceptions;
using IdentityFunction.Requests;
using IdentityFunction.Settings;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Net;
using System.Net.Mime;

namespace IdentityFunction
{
    public class CreateUserFunction
    {
        private readonly ILogger _logger;
        private readonly ServiceBusSettings _serviceBusSettings;

        public CreateUserFunction(ILoggerFactory loggerFactory, IOptions<ServiceBusSettings> serviceBusSettings)
        {
            _logger = loggerFactory.CreateLogger<CreateUserFunction>();
            _serviceBusSettings = serviceBusSettings.Value;
        }

        [Function("CreateUser")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "Post")] HttpRequestData req)
        {
            try
            {
                _logger.LogInformation("Start processing request...");

                var request = await req.ReadFromJsonAsync<CreateUser>();
                var user = User.Create(request.FirstName, request.LastName, request.Email);

                _logger.LogInformation("Some logic to register account");

                return await GetResponse(req, HttpStatusCode.OK, "Account is registered, welcome in our family");
            }
            catch (CustomException ex)
            {
                _logger.LogError(ex.ToString());
                return await GetResponse(req, HttpStatusCode.BadRequest, ex.Message);
            }
        }

            private async Task<HttpResponseData> GetResponse(HttpRequestData request, HttpStatusCode statusCode, string message)
        {
            var response = request.CreateResponse(statusCode);
            response.Headers.Add(HeaderNames.ContentType, MediaTypeNames.Text.Plain);
            await response.WriteStringAsync(message);

            return response;
        }
    }
}
