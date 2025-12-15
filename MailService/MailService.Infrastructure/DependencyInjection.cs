using Core.Infrastructure.MassTransit;
using MailService.Application.Interfaces;
using MailService.Infrastructure.Consumers;
using MailService.Infrastructure.Senders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MailService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddMailInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMailSender, FakeMailSender>();
        services.AddInfrastructureMassTransit(configuration, typeof(MailNotificationCreatedConsumer).Assembly);

        return services;
    }
}