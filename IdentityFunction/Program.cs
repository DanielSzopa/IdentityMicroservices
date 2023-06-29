using IdentityFunction.Middlewares;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults(builder =>
    {
        builder.UseMiddleware<ErrorHandlingMiddleware>();
    })
    .Build();

host.Run();