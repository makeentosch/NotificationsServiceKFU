using MailService.Application.Interfaces;
using MailService.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MailService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IMailNotificationService, MailNotificationService>();
        
        return services;
    }
}