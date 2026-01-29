using DataService.Application.Interfaces;
using DataService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DataService.Infrastructure.Repositories;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class
{
    protected readonly DataServiceDbContext DbContext;
    protected readonly DbSet<TEntity> DbSet;

    public Repository(DataServiceDbContext dbContext)
    {
        DbContext = dbContext;
        DbSet = dbContext.Set<TEntity>();
    }

    public virtual Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
    {
        return DbSet.FindAsync([id!], cancellationToken).AsTask();
    }

    public virtual async Task<IReadOnlyList<TEntity>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var skip = Math.Max(0, (pageNumber - 1) * pageSize);
        return await DbSet.AsNoTracking()
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public virtual async Task<long> CountAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet.AsNoTracking().LongCountAsync(cancellationToken);
    }

    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken);
    }

    public virtual void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }

    public virtual void Remove(TEntity entity)
    {
        DbSet.Remove(entity);
    }
}
