namespace Blogify.Common;


/// <summary>
/// Used To Define A Repository
/// </summary>
/// <typeparam name="TEntity">Type Of Entity Must Be <see cref="IEntity{TKey}"/></typeparam>
/// <typeparam name="TKey">Type Of Entity Identifier</typeparam>
public interface IRepository<TEntity, in TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    /// <summary>
    /// To override searching criteria depending on your state.
    /// </summary>
    /// <param name="queryable">The entity queryable</param>
    /// <param name="searchQuery">The search query</param>
    /// <returns>Filtered queryable</returns>
    Task<IQueryable<TEntity>> GetFilteredQueryable(IQueryable<TEntity> queryable, string? searchQuery);
    
    
    /// <summary>
    /// To override sorting criteria depending on your state.
    /// </summary>
    /// <param name="queryable">The entity queryable</param>
    /// <param name="sorting"></param>
    /// <returns>Ordered queryable</returns>
    Task<IOrderedQueryable<TEntity>> GetSortedQueryable(IQueryable<TEntity> queryable, string? sorting);
    
    
    /// <summary>
    /// To return queryable of current entity.
    /// </summary>
    /// <returns>The entity queryable</returns>
    Task<IQueryable<TEntity>> GetQueryableAsync();
    
    
    /// <summary>
    /// To return queryable of current entity with subclasses.
    /// </summary>
    /// <returns>The entity queryable with subclasses</returns>
    Task<IQueryable<TEntity>> WithDetailsAsync();


    /// <summary>
    /// 
    /// </summary>
    /// <param name="defaultValue">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Current Cancellation Token</returns>
    CancellationToken GetCancellationToken(CancellationToken defaultValue);


    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Awaitable <see cref="Task"/>.</returns>
    Task SaveChangesAsync(CancellationToken cancellationToken);
}


/// <summary>
/// Default <see cref="IRepository{TEntity,TKey}"/> Uses string As An Id
/// </summary>
public interface IRepository<TEntity> : IRepository<TEntity, string>
    where TEntity : class, IEntity<string>
{
}