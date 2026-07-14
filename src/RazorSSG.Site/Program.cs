using RazorSSG;
using RazorSSG.Core.Model;
using RazorSSG.Core.Services;
using RazorSSG.Model;

// TODO keep upper and lower case in mind

var sb = new SiteBuilder();
sb.AddPage(new PageInfo<IndexModel>("Index"));
await sb.Generate();