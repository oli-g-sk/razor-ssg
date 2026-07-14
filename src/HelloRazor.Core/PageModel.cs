namespace HelloRazor.Core;

/// <summary>
/// A simple class representing a page with
/// additional data loaded from a specified source.
/// </summary>
public class PageModel(string title)
{
    /// <summary>
    /// The title of the page.
    /// </summary>
    public string Title { get; } = title;
}