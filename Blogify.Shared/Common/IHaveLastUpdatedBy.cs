namespace Blogify.Common;

/// <summary>
/// Interface Defined To Add Capability To Resolve Entity Updater
/// (The User Has Update The Entity At Las Time)
/// </summary>
/// <typeparam name="TKey">Type Of UpdatedBy (User Id)</typeparam>
public interface IHaveLastUpdatedBy<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Id Of User That Update This Entity Last Time
    /// </summary>
    public TKey? UpdatedBy { get; set; }
}

/// <summary>
/// Default Interface <see cref="IHaveLastUpdatedBy{TKey}"/> Uses string As An Id
/// </summary>
public interface IHaveLastUpdatedBy : IHaveLastUpdatedBy<string>
{
}