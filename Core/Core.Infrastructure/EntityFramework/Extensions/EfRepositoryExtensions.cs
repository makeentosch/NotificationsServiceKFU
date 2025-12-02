using Core.Domain.Abstractions;
using Core.Infrastructure.EntityFramework.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.EntityFramework.Extensions;

public static class EfRepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<,>), typeof(EfRepository<,>));

        return services;
    }
}