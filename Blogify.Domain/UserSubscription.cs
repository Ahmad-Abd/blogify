using Blogify.Common;

namespace Blogify;

/// <summary>
/// Represents a user's subscription to a another user.
/// </summary>
public class UserSubscription:Entity,IHaveCreatedAt
{
    public long CreatedAt { get; set; }
    
    /// <summary>
    /// Gets or sets the user who is subscribing.
    /// </summary>
    public string SubscriberId { get; set; }

    /// <summary>
    /// Gets or sets the user who being subscribed to.
    /// </summary>
    public string SubscribedId { get; set; }
}