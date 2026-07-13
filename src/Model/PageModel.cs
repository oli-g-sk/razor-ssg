namespace RazorSSG.Model;

public class PageModel
{
    public IEnumerable<Item>? Items { get; set; } = Array.Empty<Item>();
}