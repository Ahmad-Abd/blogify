using Blogify.Common;

namespace Blogify;

/// <summary>
/// Represents a blog post.
/// </summary>
public class Blog:FullAuditedEntity
{

    /// <summary>
    /// Gets or sets the title of the blog post.
    /// </summary>
    public virtual string Title { get; set; }

    /// <summary>
    /// Gets or sets the content of the blog post.
    /// </summary>
    public virtual string Content { get; set; }

    /// <summary>
    /// Gets or sets the list of categories associated with the blog post.
    /// </summary>
    public virtual List<Category> Categories { get; set; } = new List<Category>();

    /// <summary>
    /// Gets or sets the list of comments on the blog post.
    /// </summary>
    public virtual List<Comment> Comments { get; set; } = new List<Comment>();
}