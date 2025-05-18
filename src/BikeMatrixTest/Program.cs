using BikeMatrixTest.Factories;
using BikeMatrixTest.Interfaces;
using BikeMatrixTest.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = new HostBuilder();

builder.ConfigureFunctionsWebApplication();
builder.ConfigureServices(services =>
{
    // Add the services here
    services.AddScoped<IBikeServices, BikeServices>();
    services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();
});

builder.ConfigureLogging(logging =>
{
    logging.Services.Configure<LoggerFilterOptions>(options =>
    {
        LoggerFilterRule defaultRule = options.Rules.FirstOrDefault(rule => rule.ProviderName
            == "Microsoft.Extensions.Logging.ApplicationInsights.ApplicationInsightsLoggerProvider");
        if (defaultRule is not null)
        {
            options.Rules.Remove(defaultRule);
        }
    });
    logging.SetMinimumLevel(LogLevel.Trace);
    logging.AddFilter("Microsoft", LogLevel.Information);
});
// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
// builder.Services
//     .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();

builder.Build().Run();
