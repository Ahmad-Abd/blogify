using Microsoft.AspNetCore.Identity;

namespace Blogify;

/// <summary>
/// Represents a blogify user.
/// </summary>
public class BlogifyUser: IdentityUser<string>
{
    /// <summary>
    /// Gets or sets the list of blogs authored by the user.
    /// </summary>
    public List<Blog> Blogs { get; set; } = new List<Blog>();

    /// <summary>
    /// Gets or sets the list of comments made by the user.
    /// </summary>
    public List<Comment> Comments { get; set; } = new List<Comment>();

    /// <summary>
    /// Gets or sets the list of subscriptions of the user.
    /// </summary>
    public List<UserSubscription> Subscriptions { get; set; } = new List<UserSubscription>();

    /// <summary>
    /// Gets or sets the list of subscriptions of the user.
    /// </summary>
    public List<UserSubscription> Subscribers { get; set; } = new List<UserSubscription>();

    
    /// <summary>
    /// Gets or sets the list of reactions made by the user.
    /// </summary>
    public List<CommentReaction> Reactions { get; set; } = new List<CommentReaction>();
}