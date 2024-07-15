namespace Blogify.Common;

/// <summary>
/// Reuqest
/// </summary>
public class GetPageRequestInput
{
    public string? SearchQuery { get; set; }
    public string? Sorting { get; set; }
    public int SkipCount { get; set; }
    public int MaxResultCount { get; set; }
}