using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Blogify.Common.Repositories;

public abstract class EfCoreRepository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    protected readonly DbContext DbContext;

    protected EfCoreRepository(DbContext dbContext)
    {
        DbContext = dbContext;
    }
    
    protected DbSet<TEntity> DbSet => DbContext.Set<TEntity>();


    public virtual Task<IQueryable<TEntity>> GetFilteredQueryable(IQueryable<TEntity> queryable, string? searchQuery)
    {
        // If searchQuery is null or empty then return queryable itself
        if (string.IsNullOrEmpty(searchQuery))
        {
            return Task.FromResult(queryable);
        }

        // Get all string properties of TEntity
        var stringProperties = typeof(TEntity).GetProperties()
            .Where(p => new List<Type> { typeof(string) }.Contains(p.PropertyType))
            .ToList();

        if (!stringProperties.Any())
        {
            return Task.FromResult(queryable);
        }

        // Create the filter expression
        ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "e");
        Expression? containsExpression = null;

        foreach (var property in stringProperties)
        {
            var propertyExpression = Expression.Property(parameter, property);
            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

            if (containsMethod == null) continue;

            var searchExpression = Expression.Constant(searchQuery, typeof(string));
            var containsCall = Expression.Call(propertyExpression, containsMethod, searchExpression);

            if (containsExpression == null)
            {
                containsExpression = containsCall;
            }
            else
            {
                containsExpression = Expression.OrElse(containsExpression, containsCall);
            }
        }

        if (containsExpression == null)
        {
            return Task.FromResult(queryable);
        }

        var lambda = Expression.Lambda<Func<TEntity, bool>>(containsExpression, parameter);
        var filteredQueryable = queryable.Where(lambda);

        return Task.FromResult(filteredQueryable);
    }

    public virtual Task<IOrderedQueryable<TEntity>> GetSortedQueryable(IQueryable<TEntity> queryable, string? sorting)
    {
        return Task.FromResult(typeof(TEntity).IsAssignableTo(typeof(IHaveCreatedAt))
            ? queryable.OrderByDescending(e => ((IHaveCreatedAt)e).CreatedAt)
            : queryable.OrderByDescending(e => e.Id));
    }

    public async Task<IQueryable<TEntity>> GetQueryableAsync()
    {
        return (await Task.FromResult(DbSet)).AsQueryable();
    }

    public async Task<IQueryable<TEntity>> WithDetailsAsync()
    {
        return (await Task.FromResult(DbSet)).AsQueryable();
    }

    public virtual CancellationToken GetCancellationToken(CancellationToken defaultValue)
    {
        return CancellationToken.None;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await DbContext.SaveChangesAsync(GetCancellationToken(cancellationToken));
    }
}