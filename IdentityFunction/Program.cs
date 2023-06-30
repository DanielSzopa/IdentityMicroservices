using IdentityFunction.Middlewares;
using IdentityFunction.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureAppConfiguration((context, builder) =>
    {
        var configPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

        builder
        .AddJsonFile(configPath, false, true)
        .AddEnvironmentVariables()
        .Build();
    })
    .ConfigureServices((context, builder) =>
    {
        builder
        .AddOptions<ServiceBusSettings>()
        .Bind(context.Configuration.GetSection(ServiceBusSettings.SectionName));
    })
    .ConfigureFunctionsWorkerDefaults(builder =>
    {
        builder.UseMiddleware<ErrorHandlingMiddleware>();
    })
    .Build();

host.Run();