using RazorLight;

namespace Okisioli.HelloRazor.Core.Internals;

internal class TemplateProcessor
{
    // TODO make configurable?
    private const string TemplateRoot = "Templates";
    private const string OutputRoot = "wwwroot";
    
    private readonly RazorLightEngine engine;
    private readonly string outputRoot;

    public TemplateProcessor(string appRoot)
    {
        var templateRoot = Path.Combine(appRoot, TemplateRoot);
        
        engine = new RazorLightEngineBuilder()
            .UseFileSystemProject(templateRoot)
            .UseMemoryCachingProvider()
            .Build();
        
        outputRoot = Path.Combine(appRoot, OutputRoot);
        Directory.CreateDirectory(outputRoot);
    }
    
    public async Task Process<T>(string templateName, T? model)
    {
        var html = model == null
            ? await engine.CompileRenderAsync($"{templateName}.cshtml", new PageModel(templateName))
            : await engine.CompileRenderAsync($"{templateName}.cshtml", model);

        await File.WriteAllTextAsync(
            Path.Combine(outputRoot, $"{templateName.ToLower()}.html"),
            html);
    }
}