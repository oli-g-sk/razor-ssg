using System.Text.Json;
using System.Text.Json.Serialization;
using RazorLight;
using RazorSSG.Model;
using RazorSSG.Services;

var appRoot = AppContext.BaseDirectory;

var itemsJson = await File.ReadAllTextAsync(
    Path.Combine(appRoot, "Data", "items.json"));

var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
options.Converters.Add(new JsonStringEnumConverter());
var items = JsonSerializer.Deserialize<List<Item>>(itemsJson, options) ?? [];

var model = new { Items = items };

var processor = new TemplateProcessor();
await processor.Process("Index", model);