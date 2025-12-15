using Microsoft.Extensions.DependencyInjection;
using SmsService.Application.Interfaces;
using SmsService.Application.Services;

namespace SmsService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ISmsNotificationService, SmsNotificationService>();

        return services;
    }
}