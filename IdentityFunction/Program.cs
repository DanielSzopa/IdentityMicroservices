using IdentityFunction.Middlewares;
using IdentityFunction.ServiceBus;
using IdentityFunction.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

var host = new HostBuilder()
    .ConfigureAppConfiguration((context, builder) =>
    {
        var configPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

        builder
        .AddJsonFile(configPath, false, true)
        .AddEnvironmentVariables()
        .AddUserSecrets(Assembly.GetExecutingAssembly(), false, true)
        .Build();
    })
    .ConfigureServices((context, builder) =>
    {
        builder
        .AddOptions<ServiceBusSettings>()
        .Bind(context.Configuration.GetSection(ServiceBusSettings.SectionName));

        builder.AddSingleton<IServiceBusSender, ServiceBusSender>();
    })
    .ConfigureFunctionsWorkerDefaults(builder =>
    {
        builder.UseMiddleware<ErrorHandlingMiddleware>();
    })
    .Build();

host.Run();