namespace Blogify.Common;

/// <summary>
/// Repository Interface Supports CRUD Operations With Bulk Implementation For Create, Update, and Delete
/// </summary>
/// <typeparam name="TEntity">Type Of Entity Must Be <see cref="IEntity{TKey}"/></typeparam>
/// <typeparam name="TKey">Type Of Entity Identifier</typeparam>
public interface IBasicRepository<TEntity, in TKey> : IReadOnlyRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Inserts an entity. 
    /// </summary>
    /// <param name="entity">The entity to be inserted</param>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Inserted entity</returns>
    Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Inserts multiple entities.
    /// </summary>
    /// /// <param name="entities">The entities to be inserted.</param>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Awaitable <see cref="Task"/>.</returns>
    Task InsertManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="entity">In memory updated entity.</param>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Updated entity</returns>
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="entities">In memory updated entities.</param>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Awaitable <see cref="Task"/>.</returns>
    Task UpdateManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an entity.
    /// </summary>
    /// <param name="entity">Entity to be deleted</param>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Awaitable <see cref="Task"/>.</returns>
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes multiple entities.
    /// </summary>
    /// <param name="entities">Entities to be deleted.</param>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Awaitable <see cref="Task"/>.</returns>
    Task DeleteManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
}

/// <summary>
/// Default <see cref="IBasicRepository{TEntity,TKey}"/> Uses string As An Id
/// </summary>
public interface IBasicRepository<TEntity> : IBasicRepository<TEntity, string>
    where TEntity : class, IEntity<string>
{
}
