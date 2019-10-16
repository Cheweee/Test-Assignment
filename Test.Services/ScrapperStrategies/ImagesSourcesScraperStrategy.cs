
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Test.Utilities.Extensibility;

namespace Test.Services.ScraperStrategies
{
    public class ImagesSourcesScraperStrategy : IAppendStrategyExtensionsReader
    {
        private const string name = "imagessources";

        public async Task<IEnumerable<string>> Process(HtmlDocument document, ExtensibleGetOptions options, List<string> urls)
        {
            if (!options.PartIsUsed(name))
                return urls;
            await Task.Run(() =>
            {
                var links = document.DocumentNode.SelectNodes("//img[@src]").ToList();

                foreach (var link in links)
                {
                    HtmlAttribute attr = link.Attributes["src"];
                    if (!string.IsNullOrEmpty(attr.Value))
                        urls.Add(attr.Value);
                }
            });

            return urls;
        }
    }
}