using System.ComponentModel.DataAnnotations;
using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.MassTransit;

public static class MassTransitExtensions
{
    public static IServiceCollection AddInfrastructureMassTransit(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly consumersAssembly)
    {
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            x.AddConsumers(consumersAssembly);

            x.UsingRabbitMq((context, cfg) =>
            {
                var connectionString = configuration.GetConnectionString("rabbitmq");

                cfg.Host(connectionString);

                cfg.UseMessageRetry(retryConfig => ConfigureRetry(retryConfig, configuration));

                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }

    private static void ConfigureRetry(IRetryConfigurator retryConfigurator, IConfiguration configuration)
    {
        var retryLimit = configuration.GetValue<int?>("MassTransit:RetryLimit") ?? 3;
        var minInterval = configuration.GetValue<int?>("MassTransit:MinIntervalMilliseconds") ?? 200;
        var maxInterval = configuration.GetValue<int?>("MassTransit:MaxIntervalMinutes") ?? 2;
        var intervalDelta = configuration.GetValue<int?>("MassTransit:IntervalDeltaMilliseconds") ?? 200;

        retryConfigurator.Exponential(
            retryLimit,
            TimeSpan.FromMilliseconds(minInterval),
            TimeSpan.FromMinutes(maxInterval),
            TimeSpan.FromMilliseconds(intervalDelta));

        retryConfigurator.Ignore<ValidationException>();
    }
}