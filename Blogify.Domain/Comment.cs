using Blogify.Common;

namespace Blogify;

/// <summary>
/// Represents a comment on a blog post.
/// </summary>
public class Comment:FullAuditedEntity
{
    /// <summary>
    /// Gets or sets the content of the comment.
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// Gets or sets the ID of the parent comment, if this is a reply.
    /// </summary>
    public string? ParentId { get; set; }

    /// <summary>
    /// Gets or sets the list of replies to this comment.
    /// </summary>
    public List<Comment> Replies { get; set; } = new List<Comment>();

    /// <summary>
    /// Gets or sets the list of reactions to the comment.
    /// </summary>
    public List<CommentReaction> Reactions { get; set; } = new List<CommentReaction>();
}