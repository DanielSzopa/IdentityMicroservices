using IdentityFunction.Entity;
using IdentityFunction.Exceptions;
using IdentityFunction.Models;
using IdentityFunction.Requests;
using IdentityFunction.ServiceBus;
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
        private readonly ILogger<CreateUserFunction> _logger;
        private readonly IServiceBusSender _serviceBusSender;

        public CreateUserFunction(ILogger<CreateUserFunction> logger, IServiceBusSender serviceBusSender)
        {
            _logger = logger;
            _serviceBusSender = serviceBusSender;
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

                var serviceBusOutput = ServiceBusOutput.Create(user, request.IsNewsletterSubscriber);
                await _serviceBusSender.SendAsync(serviceBusOutput, request.IsNewsletterSubscriber);

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
