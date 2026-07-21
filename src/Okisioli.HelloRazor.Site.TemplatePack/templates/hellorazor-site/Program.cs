using Okisioli.HelloRazor.Core;
using Okisioli.HelloRazor.Core.Internals;
using Okisioli.HelloRazor.Site.Model;

var sb = new SiteBuilder()
    .WithPage(new PageInfo<IndexModel>("Index"));

await sb.Generate();
