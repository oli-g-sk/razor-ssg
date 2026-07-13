using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using RazorLight;
using RazorSSG.Model;

var appRoot = AppContext.BaseDirectory;

var templateRoot = Path.Combine(appRoot, "Templates");
var outputRoot = Path.Combine(appRoot, "wwwroot");

Directory.CreateDirectory(outputRoot);

var itemsJson = await File.ReadAllTextAsync(
    Path.Combine(appRoot, "Data", "items.json"));

var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
options.Converters.Add(new JsonStringEnumConverter());
var items = JsonSerializer.Deserialize<List<Item>>(itemsJson, options) ?? [];

var model = new { Items = items };

var engine = new RazorLightEngineBuilder()
    .UseFileSystemProject(templateRoot)
    .UseMemoryCachingProvider()
    .Build();

var html = await engine.CompileRenderAsync(
    "index.cshtml",
    model);

await File.WriteAllTextAsync(
    Path.Combine(outputRoot, "index.html"),
    html);