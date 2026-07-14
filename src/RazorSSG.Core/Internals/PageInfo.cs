namespace RazorSSG.Core.Internals;

public record PageInfo(string TemplateName, string? DataFile);

public record PageInfo<T>(string TemplateName, string? DataFile = null)
    : PageInfo(TemplateName, DataFile) where T : PageModel;