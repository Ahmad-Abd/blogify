namespace Blogify.Common;

public class PagedResult<TEntity>
{
    public IReadOnlyList<TEntity> Items { get; set; } = new List<TEntity>();
    public int TotalCount { get; set; }
}