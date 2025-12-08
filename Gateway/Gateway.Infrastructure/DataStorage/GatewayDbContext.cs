using Core.Domain.Abstractions;
using Gateway.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gateway.Infrastructure.DataStorage;

public class GatewayDbContext(DbContextOptions<GatewayDbContext> options) 
    : DbContext(options), IUnitOfWork
{
    public DbSet<Notification> Notifications { get; set; }
}