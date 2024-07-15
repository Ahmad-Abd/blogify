namespace Blogify.Common;

/// <summary>
/// Represent Base Class For All Entities
/// </summary>
/// <typeparam name="TKey">Type Of User Identifier</typeparam>
public class Entity<TKey>:IEntity<TKey> where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; }
}

/// <summary>
/// Default <see cref="Entity{TKey}"/> Uses string As An Id
/// </summary>
public class Entity:Entity<string>
{
}