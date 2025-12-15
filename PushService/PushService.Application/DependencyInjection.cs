using Microsoft.Extensions.DependencyInjection;
using PushService.Application.Interfaces;
using PushService.Application.Services;

namespace PushService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IPushNotificationService, PushNotificationService>();

        return services;
    }
}