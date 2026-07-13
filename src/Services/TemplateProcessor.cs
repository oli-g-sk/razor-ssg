using RazorLight;

namespace RazorSSG.Services;

public class TemplateProcessor
{
    private const string TemplateRoot = "Templates";
    private const string OutputRoot = "wwwroot";
    
    private readonly RazorLightEngine engine;
    private readonly string outputRoot;

    public TemplateProcessor()
    {
        var appRoot = AppContext.BaseDirectory;
        var templateRoot = Path.Combine(appRoot, TemplateRoot);
        
        engine = new RazorLightEngineBuilder()
            .UseFileSystemProject(templateRoot)
            .UseMemoryCachingProvider()
            .Build();
        
        outputRoot = Path.Combine(appRoot, OutputRoot);
        Directory.CreateDirectory(outputRoot);
    }
    
    public async Task Process(string templateName, dynamic model)
    {
        var html = await engine.CompileRenderAsync(
            $"{templateName}.cshtml",
            model);

        await File.WriteAllTextAsync(
            Path.Combine(outputRoot, $"{templateName.ToLower()}.html"),
            html);
    }
}