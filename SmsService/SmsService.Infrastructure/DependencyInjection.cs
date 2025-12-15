using Core.Infrastructure.MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmsService.Application.Interfaces;
using SmsService.Infrastructure.Consumers;
using SmsService.Infrastructure.Senders;

namespace SmsService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddSmsInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISmsSender, FakeSmsSender>();
        services.AddInfrastructureMassTransit(configuration, typeof(SmsNotificationCreatedConsumer).Assembly);

        return services;
    }
}