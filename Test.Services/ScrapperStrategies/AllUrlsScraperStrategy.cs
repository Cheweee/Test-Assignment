
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Test.Utilities.Extensibility;

namespace Test.Services.ScraperStrategies
{
    public class AllUrlsScraperStrategy : IAppendStrategyExtensionsReader
    {
        private const string name = "allurls";

        public async Task<IEnumerable<string>> Process(HtmlDocument document, ExtensibleGetOptions options, List<string> urls)
        {
            if (!options.PartIsUsed(name))
                return urls;
            await Task.Run(() =>
            {
                var aLinks = document.DocumentNode.SelectNodes("//a[@href]");
                var liLinks = document.DocumentNode.SelectNodes("//link[@href]");

                foreach (var link in aLinks)
                {
                    HtmlAttribute attr = link.Attributes["href"];
                    if (!string.IsNullOrEmpty(attr.Value))
                        urls.Add(attr.Value);
                }
                foreach (var link in liLinks)
                {
                    HtmlAttribute attr = link.Attributes["href"];
                    if (!string.IsNullOrEmpty(attr.Value))
                        urls.Add(attr.Value);
                }
            });

            return urls;
        }
    }
}