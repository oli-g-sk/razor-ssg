using System.Data;
using HelloRazor.Core.Internals;

namespace HelloRazor.Core;

public class SiteBuilder
{
    private readonly DataProvider dataProvider = new(AppContext.BaseDirectory);
    private readonly TemplateProcessor processor = new(AppContext.BaseDirectory);
    
    private List<PageInfo> pages = new();

    public void AddPage<T>(PageInfo<T> pageInfo) where T : PageModel
    {
        Validate(pageInfo);
        pages.Add(pageInfo);
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