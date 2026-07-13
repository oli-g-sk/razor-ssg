using RazorSSG.Model;
using RazorSSG.Services;

var dataProvider = new DataProvider(AppContext.BaseDirectory);
var items = await dataProvider.LoadData<List<Item>>("items");
var model = new { Items = items };

string appRoot = AppContext.BaseDirectory;
var processor = new TemplateProcessor(appRoot);
await processor.Process("Index", model);