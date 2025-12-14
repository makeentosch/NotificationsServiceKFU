using Core.Infrastructure.MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PushService.Application.Interfaces;
using PushService.Infrastructure.Consumers;
using PushService.Infrastructure.Senders;

namespace PushService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddPushInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPushSender, FakePushSender>();
        services.AddInfrastructureMassTransit(configuration, typeof(PushNotificationCreatedConsumer).Assembly);

        return services;
    }
}