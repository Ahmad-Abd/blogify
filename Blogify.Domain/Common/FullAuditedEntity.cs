namespace Blogify.Common;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TCreatedOrUpdatedByKey"></typeparam>
public class FullAuditedEntity<TKey,TCreatedOrUpdatedByKey>:Entity<TKey>,IHaveCreatedBy<TCreatedOrUpdatedByKey>,IHaveCreatedAt,IHaveLastUpdatedBy<TCreatedOrUpdatedByKey>,IHaveLastUpdatedAt where TKey : IEquatable<TKey> where TCreatedOrUpdatedByKey : IEquatable<TCreatedOrUpdatedByKey>
{
    public TCreatedOrUpdatedByKey? CreatedBy { get; set; }
    public long CreatedAt { get; set; }
    public TCreatedOrUpdatedByKey? UpdatedBy { get; set; }
    public long? LastUpdatedAt { get; set; }
}

/// <summary>
/// 
/// </summary>
/// <typeparam name="TKey"></typeparam>
public class FullAuditedEntity<TKey>:FullAuditedEntity<TKey,string> where TKey : IEquatable<TKey>
{
}

/// <summary>
/// 
/// </summary>
public class FullAuditedEntity:FullAuditedEntity<string,string>
{
}