using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

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
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous,  "Post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            return response;
        }
    }
}
