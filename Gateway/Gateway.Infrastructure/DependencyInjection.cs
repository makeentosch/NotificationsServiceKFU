using Core.Domain.Abstractions;
using Core.Infrastructure.EntityFramework.Extensions;
using Core.Infrastructure.MassTransit;
using Gateway.Application.Interfaces;
using Gateway.Infrastructure.Messaging;
using Gateway.Infrastructure.DataStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gateway.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("postgresNotifications");

        services.AddDbContext<GatewayDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<DbContext>(provider => provider.GetRequiredService<GatewayDbContext>());
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<GatewayDbContext>());

        services.AddRepositories();

        services.AddInfrastructureMassTransit(configuration, typeof(GatewayDbContext).Assembly);

        services.AddScoped<INotificationPublisher, MassTransitPublisher>();

        return services;
    }
}