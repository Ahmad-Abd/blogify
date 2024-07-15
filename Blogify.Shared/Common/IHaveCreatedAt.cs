namespace Blogify.Common;


/// <summary>
/// Interface Defined To Add Capability To Resolve Entity Creation Time
/// </summary>
public interface IHaveCreatedAt
{
    /// <summary>
    /// Creation Timestamp
    /// </summary>
    public long CreatedAt { get; set; }
}