using System.Data;
using Okisioli.HelloRazor.Core.Internals;

namespace Okisioli.HelloRazor.Core;

public class SiteBuilder
{
    // TODO make path configurable?
    private readonly DataProvider dataProvider = new(AppContext.BaseDirectory);
    private readonly TemplateProcessor processor = new(AppContext.BaseDirectory);
    
    private readonly List<PageInfo> pages = new();

    /// <summary>
    /// Registers a page to be generated as part of the static site.
    /// </summary>
    /// <param name="pageInfo">
    /// Metadata about the page to be generated.
    /// </param>
    /// <typeparam name="T">
    /// A custom type inheriting from <see cref="PageModel"/> if the given page requires one.
    /// Use <see cref="PageModel"/> itself if no custom type is needed.
    /// </typeparam>
    /// <returns></returns>
    public SiteBuilder WithPage<T>(PageInfo<T> pageInfo) where T : PageModel
    {
        Validate(pageInfo);
        pages.Add(pageInfo);
        return this;
    }
    
    public async Task Generate()
    {
        foreach (var pageInfo in pages)
        {
            var dataFile = pageInfo.DataFile;
            var model = dataFile == null ? null : await dataProvider.LoadData<object>(dataFile);
            await processor.Process(pageInfo.TemplateName, model);
        }
    }

    private void Validate<T>(PageInfo<T> pageInfo) where T : PageModel
    {
        // TODO unit tests
        
        if (string.IsNullOrWhiteSpace(pageInfo.TemplateName))
            throw new ArgumentException("Page name cannot be empty", nameof(pageInfo));
        
        if (pageInfo.TemplateName.First() != char.ToUpper(pageInfo.TemplateName.First()))
            throw new ArgumentException("Page name must start with a capital letter", nameof(pageInfo));

        if (pages.Any(p => p.TemplateName == pageInfo.TemplateName))
            throw new DuplicateNameException($"Page {pageInfo.TemplateName} already exists");
    }
}