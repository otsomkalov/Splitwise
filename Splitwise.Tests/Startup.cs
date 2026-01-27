using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Splitwise.Clients;
using Splitwise.Clients.Interfaces;
using Splitwise.Tests.Settings;

namespace Splitwise.Tests;

public class Startup
{
    public void ConfigureHost(IHostBuilder hostBuilder)
    {
        hostBuilder
            .ConfigureAppConfiguration(ConfigureConfiguration);
    }

    private void ConfigureConfiguration(HostBuilderContext context, IConfigurationBuilder builder)
    {
        builder
            .AddEnvironmentVariables()
            .AddJsonFile("appsettings.json");
    }

    public void ConfigureServices(IServiceCollection services, HostBuilderContext hostBuilderContext)
    {
        services.Configure<ExpenseClientTestsSettings>(hostBuilderContext.Configuration.GetSection(ExpenseClientTestsSettings.SectionName));

        services.AddSingleton<ISplitwiseClient, SplitwiseClient>(provider =>
        {
            var options = provider.GetRequiredService<IOptions<ExpenseClientTestsSettings>>().Value;

            return new(options.ApiKey);
        });
    }
}