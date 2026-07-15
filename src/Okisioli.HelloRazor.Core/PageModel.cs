namespace Okisioli.HelloRazor.Core;

/// <summary>
/// Represents a base for a trivial page model (containing only a <see cref="Title"/>) which, if needed, can be extended
/// to any FooPageModel and registered via <see cref="SiteBuilder.WithPage(PageInfo)"/>
/// method to inject a deserialized data source into it.
/// </summary>
/// <param name="title"></param>
public class PageModel(string title)
{
    public string Title { get; } = title;
}