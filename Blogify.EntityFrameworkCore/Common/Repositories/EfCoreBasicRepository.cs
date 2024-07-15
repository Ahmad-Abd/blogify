using Microsoft.EntityFrameworkCore;

namespace Blogify.Common.Repositories;

public class EfCoreBasicRepository<TEntity, TKey> : EfCoreReadOnlyRepository<TEntity, TKey>, IBasicRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    public EfCoreBasicRepository(DbContext dbContext) : base(dbContext)
    {
    }
    
    public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var savedEntity = (await DbSet.AddAsync(entity, GetCancellationToken(cancellationToken))).Entity;
        await SaveChangesAsync(cancellationToken);
        return savedEntity;
    }

    public async Task InsertManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await DbSet.AddRangeAsync(entities,GetCancellationToken(cancellationToken));
        await SaveChangesAsync(cancellationToken);
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (DbSet.Local.All(e => e != entity))
        {
            DbSet.Attach(entity);
            DbContext.Update(entity);
        }
        await SaveChangesAsync(GetCancellationToken(cancellationToken));
        return entity;
    }

    public async Task UpdateManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        var enumerable = entities.ToList();
        if (enumerable.All(t=>DbSet.Local.All(e => e != t)))
        {
            DbSet.AttachRange(enumerable);
            DbContext.UpdateRange(enumerable);
        }
        await SaveChangesAsync(GetCancellationToken(cancellationToken));
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        DbSet.Remove(entity);
        await SaveChangesAsync(GetCancellationToken(cancellationToken));
    }

    public async Task DeleteManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        DbSet.RemoveRange(entities);
        await SaveChangesAsync(GetCancellationToken(cancellationToken));
    }
}


public class EfCoreBasicRepository<TEntity> : EfCoreBasicRepository<TEntity,string> 
    where TEntity : class, IEntity<string>
{
    public EfCoreBasicRepository(DbContext dbContext) : base(dbContext)
    {
    }
}

