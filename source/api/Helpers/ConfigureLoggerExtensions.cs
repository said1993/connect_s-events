using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace connect_s_events_api.Helpers;

public static class ConfigureLoggerExtensions
{
    public static void ConfigureLogs(this ConfigureHostBuilder hostBuilder, ELSConfigurations ELSconfigurations)
    {
        hostBuilder.UseSerilog((context, configurations) =>
        {
            configurations.Enrich.FromLogContext()
                .Enrich.WithEnvironmentName()
                .Enrich.WithExceptionDetails()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(ConfigureELS(context, ELSconfigurations))
                .ReadFrom.Configuration(context.Configuration);
        });
    }

    private static ElasticsearchSinkOptions ConfigureELS(HostBuilderContext context, ELSConfigurations configurations)
    {
        ArgumentNullException.ThrowIfNull(configurations?.Uri);

        var env = context.HostingEnvironment.EnvironmentName?.ToLower().Replace('.', '-');
        return new ElasticsearchSinkOptions(new Uri(configurations.Uri))
        {
            AutoRegisterTemplate = true,
            IndexFormat = $"{configurations.Index}-logs-{env}-{DateTimeOffset.UtcNow:yyyy-MM}",
            NumberOfShards = 2,
            NumberOfReplicas = 1
        };
    }
}
