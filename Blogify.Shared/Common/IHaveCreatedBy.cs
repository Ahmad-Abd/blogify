namespace Blogify.Common;

/// <summary>
/// Interface Defined To Add Capability To Resolve Entity Creator
/// </summary>
/// <typeparam name="TKey">Type Of CreatedBy (User Id)</typeparam>
///  <example> <code>class x : IHaveCreatedBy {
/// }</code></example>
public interface IHaveCreatedBy<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Id Of User That Create This Entity
    /// </summary>
    public TKey? CreatedBy { get; set; }
}

/// <summary>
/// Default Interface <see cref="IHaveCreatedBy{TKey}"/> Uses string As An Id
/// </summary>
public interface IHaveCreatedBy : IHaveCreatedBy<string>
{
}