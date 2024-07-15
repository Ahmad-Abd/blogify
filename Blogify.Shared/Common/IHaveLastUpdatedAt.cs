namespace Blogify.Common;

/// <summary>
/// Interface Defined To Add Capability To Resolve Entity Last Updating Time
/// </summary>
public interface IHaveLastUpdatedAt
{
    /// <summary>
    /// Last Updating Timestamp
    /// </summary>
    public long? LastUpdatedAt { get; set; }
}