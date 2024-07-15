namespace Blogify.Common;

/// <summary>
/// Interface Defined To Add The Unique Identifier To Entity
/// </summary>
/// <typeparam name="TKey">Type Of Id Must Be IEquatable</typeparam>
public interface IEntity<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Entity Identifier
    /// </summary>
    public TKey Id { get; set; }
}

/// <summary>
/// Default IEntity&lt;TKey&gt; Interface <see cref="IEntity{TKey}"/>
/// </summary>
public interface IEntity : IEntity<string>
{
    
}