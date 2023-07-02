using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotyficationFunction.Email;
using NotyficationFunction.Settings;
using System.Reflection;

var host = new HostBuilder()
    .ConfigureAppConfiguration((context, builder) =>
    {
        var configPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

        builder
        .AddJsonFile(configPath, false, true)
        .AddEnvironmentVariables()
        .AddUserSecrets(Assembly.GetExecutingAssembly(), true, true)
        .Build();
    })
    .ConfigureServices((context, builder) =>
    {
        builder
        .AddOptions<SendGridSettings>()
        .Bind(context.Configuration.GetSection(SendGridSettings.Section));

        builder.AddSingleton<IEmailSender, EmailSender>();
    })
    .ConfigureFunctionsWorkerDefaults()
    .Build();

host.Run();
