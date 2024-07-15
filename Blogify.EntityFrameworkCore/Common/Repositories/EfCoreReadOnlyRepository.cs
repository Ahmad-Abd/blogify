using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Blogify.Common.Repositories;

public class EfCoreReadOnlyRepository<TEntity, TKey> : EfCoreRepository<TEntity, TKey>,
    IReadOnlyRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    public EfCoreReadOnlyRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<TEntity?> GetAsync(TKey id, bool includeDetails = false,
        CancellationToken cancellationToken = default)
    {
        var queryable = includeDetails ? await WithDetailsAsync() : await GetQueryableAsync();
        var result = await queryable
            .OrderBy(e => e.Id)
            .FirstOrDefaultAsync(e => e.Id.Equals(id), GetCancellationToken(cancellationToken));
        return result;
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, bool includeDetails = false,
        CancellationToken cancellationToken = default)
    {
        var queryable = includeDetails ? await WithDetailsAsync() : await GetQueryableAsync();
        var result = await queryable
            .OrderBy(e => e.Id)
            .Where(predicate)
            .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        return result;
    }

    public async Task<long> GetCountAsync(CancellationToken cancellationToken = default)
    {
        var queryable = await GetQueryableAsync();
        var count = await queryable
            .LongCountAsync(GetCancellationToken(cancellationToken));
        return count;
    }

    public async Task<long> GetCountAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        var queryable = await GetQueryableAsync();
        var count = await queryable
            .Where(predicate)
            .LongCountAsync(GetCancellationToken(cancellationToken));
        return count;
    }

    public async Task<PagedResult<TEntity>> GetPageAsync(GetPageRequestInput input, bool includeDetails = false,
        CancellationToken cancellationToken = default)
    {
        var queryable = includeDetails ? await WithDetailsAsync() : await GetQueryableAsync();
        var filteredQuery = await GetFilteredQueryable(queryable, input.SearchQuery);
        var sortedQuery = await GetSortedQueryable(filteredQuery, input.Sorting);
        var paginatedQuery = sortedQuery.Skip(input.SkipCount).Take(input.MaxResultCount);
        var totalCount = await sortedQuery.CountAsync(GetCancellationToken(cancellationToken));
        return new PagedResult<TEntity>
        {
            Items = await paginatedQuery.ToListAsync(GetCancellationToken(cancellationToken)),
            TotalCount = totalCount
        };
    }

    public async Task<IEnumerable<TEntity>> GetListAsync(bool includeDetails = false,
        CancellationToken cancellationToken = default)
    {
        var queryable = includeDetails ? await WithDetailsAsync() : await GetQueryableAsync();
        var result = await queryable
            .ToListAsync(GetCancellationToken(cancellationToken));
        return result;
    }

    public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        bool includeDetails = false, CancellationToken cancellationToken = default)
    {
        var queryable = includeDetails ? await WithDetailsAsync() : await GetQueryableAsync();
        var result = await queryable
            .Where(predicate)
            .ToListAsync(GetCancellationToken(cancellationToken));
        return result;
    }
}

public class EfCoreReadOnlyRepository<TEntity> : EfCoreReadOnlyRepository<TEntity, string>
    where TEntity : class, IEntity<string>
{
    public EfCoreReadOnlyRepository(DbContext dbContext) : base(dbContext)
    {
    }
}