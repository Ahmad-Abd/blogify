using System.Linq.Expressions;

namespace Blogify.Common;

/// <summary>
/// Readonly Repository Interface Supports Only Read Operations 
/// </summary>
/// <typeparam name="TEntity">Type Of Entity Must Be <see cref="IEntity{TKey}"/></typeparam>
/// <typeparam name="TKey">Type Of Entity Identifier</typeparam>
public interface IReadOnlyRepository<TEntity, in TKey> : IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Get entity by id
    /// </summary>
    /// <param name="id">The entity id</param>
    /// <param name="includeDetails">Set true if you want to import sub-entities on the fly</param>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Nullable entity</returns>
    Task<TEntity?> GetAsync(TKey id, bool includeDetails = false, CancellationToken cancellationToken = default);


    /// <summary>
    ///  Get first entity by the given predicate.
    /// </summary>
    /// <param name="predicate">The predicate to use</param>
    /// <param name="includeDetails">Set true if you want to import sub-entities on the fly</param>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Nullable entity</returns>
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, bool includeDetails = false,
        CancellationToken cancellationToken = default);


    /// <summary>
    /// Get total count of repository entities.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Total count of entities</returns>
    Task<long> GetCountAsync(CancellationToken cancellationToken = default);


    /// <summary>
    /// Get total count of repository entities those match the predicate.
    /// </summary>
    /// <param name="predicate">The predicate to use</param>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Total count of entities</returns>
    Task<long> GetCountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);


    /// <summary>
    /// Get paged result of repository entities.
    /// </summary>
    /// <param name="input">A <see cref="Blogify.Common.GetPageRequestInput"/> to fetch page.</param>
    /// <param name="includeDetails">Set true if you want to import sub-entities on the fly</param>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Paged result of entity</returns>
    Task<PagedResult<TEntity>> GetPageAsync(GetPageRequestInput input, bool includeDetails = false,
        CancellationToken cancellationToken = default);


    /// <summary>
    /// Get all repository entities. 
    /// </summary>
    /// <param name="includeDetails">Set true if you want to import sub-entities on the fly</param>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Collection of entities</returns>
    Task<IEnumerable<TEntity>> GetListAsync(bool includeDetails = false, CancellationToken cancellationToken = default);


    /// <summary>
    /// Get total count of repository entities those match the predicate.
    /// </summary>
    /// <param name="predicate">The predicate to use</param>
    /// <param name="includeDetails">Set true if you want to import sub-entities on the fly</param>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Collection of entities</returns>
    Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, bool includeDetails = false,
        CancellationToken cancellationToken = default);
}

/// <summary>
/// Default <see cref="IReadOnlyRepository{TEntity,TKey}"/> Uses string As An Id
/// </summary>
public interface IReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity, string>
    where TEntity : class, IEntity<string>
{
}