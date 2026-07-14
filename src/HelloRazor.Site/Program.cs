using HelloRazor.Core;
using HelloRazor.Core.Internals;
using RazorSSG.Site.Model;

// TODO keep upper and lower case in mind

var sb = new SiteBuilder();
sb.AddPage(new PageInfo<IndexModel>("Index"));
await sb.Generate();