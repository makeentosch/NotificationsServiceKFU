using System.Linq.Expressions;
using Core.Domain.Abstractions;
using Core.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.EntityFramework.Repositories;

public class EfRepository<TEntity, TId>(DbContext context) : IRepository<TEntity, TId>
    where TEntity : BaseEntity<TId>
{
    private readonly DbContext _context = context;
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync([id], cancellationToken);
    }

    public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public void Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }
}