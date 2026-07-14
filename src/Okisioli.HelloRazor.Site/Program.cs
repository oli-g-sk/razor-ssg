using Okisioli.HelloRazor.Core;
using Okisioli.HelloRazor.Core.Internals;
using Okisioli.HelloRazor.Site.Model;

// TODO keep upper and lower case in mind

var sb = new SiteBuilder();
sb.AddPage(new PageInfo<IndexModel>("Index"));
await sb.Generate();