using Blogify.Common;

namespace Blogify;

/// <summary>
/// Represents a category for blog posts.
/// </summary>
public class Category:Entity
{
    /// <summary>
    /// Gets or sets the name of the category.
    /// </summary>
    public string Name { get; set; }
}