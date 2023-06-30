using IdentityFunction.Middlewares;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureAppConfiguration((context, builder) =>
    {
        var configPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

        builder
        .AddJsonFile(configPath, false, true)
        .AddEnvironmentVariables();
    })
    .ConfigureFunctionsWorkerDefaults(builder =>
    {
        builder.UseMiddleware<ErrorHandlingMiddleware>();
    })
    .Build();

host.Run();