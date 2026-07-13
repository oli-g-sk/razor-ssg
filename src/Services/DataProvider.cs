using System.Text.Json;
using System.Text.Json.Serialization;
using RazorSSG.Model;

namespace RazorSSG.Services;

public class DataProvider(string appRoot)
{
    private const string DataFolderName = "Data";

    public async Task<T?> LoadData<T>(string dataFileName)
    {
        // TODO this needs to be more robust and/or documented
        if (!dataFileName.EndsWith(".json"))
            dataFileName += ".json";
        
        var dataJson = await File.ReadAllTextAsync(
            Path.Combine(appRoot, DataFolderName, $"{dataFileName}"));

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        options.Converters.Add(new JsonStringEnumConverter());
        
        return JsonSerializer.Deserialize<T>(dataJson, options) ?? default;
    }
}