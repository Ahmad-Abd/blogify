using Blogify.Common;

namespace Blogify;

/// <summary>
/// Represents a reaction to a comment.
/// </summary>
public class CommentReaction:Entity,IHaveCreatedAt
{
    public long CreatedAt { get; set; }
    
    /// <summary>
    /// Gets or sets the type of reaction (e.g., like, dislike).
    /// </summary>
    public string ReactionType { get; set; }

    /// <summary>
    /// Gets or sets the comment that the reaction is associated with.
    /// </summary>
    public Comment Comment { get; set; }
}