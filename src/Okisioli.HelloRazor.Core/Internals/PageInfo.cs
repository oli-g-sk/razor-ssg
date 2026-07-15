namespace Okisioli.HelloRazor.Core.Internals;

public record PageInfo(string TemplateName, string? DataFile);

/// <summary>
/// Represents a single static page on the site.
/// </summary>
/// <param name="TemplateName">
/// Name of the .cshtml template file in the Templates folder.
/// Should start with a capital letter.
/// </param>
/// <param name="DataFile">
/// Optional name of a .json file in the Data folder which,
/// if provided, will be deserialized into a PageModel of type <typeparamref name="T"/>.
/// </param>
/// <typeparam name="T"></typeparam>
public record PageInfo<T>(string TemplateName, string? DataFile = null)
    : PageInfo(TemplateName, DataFile) where T : PageModel;