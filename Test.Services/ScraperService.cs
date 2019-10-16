using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Test.Utilities.Extensibility;

namespace Test.Services
{
    public class ScraperService
    {
        private readonly IAppendStrategyExtensionsReader _appendStrategyReader;
        private readonly IExcludeStrategyExtensionsReader _excludeStrategyReader;

        public ScraperService(IAppendStrategyExtensionsReader appendStrategyReader, IExcludeStrategyExtensionsReader excludeStrategyReader)
        {
            _appendStrategyReader = appendStrategyReader;
            _excludeStrategyReader = excludeStrategyReader;
        }

        public async Task<IEnumerable<string>> Process(ExtensibleGetOptions options)
        {
            HtmlWeb website = new HtmlWeb();
            website.AutoDetectEncoding = false;
            website.OverrideEncoding = Encoding.Default;

            HtmlDocument document = website.Load(options.SourceUrl);
            document.OptionComputeChecksum = true;

            List<string> urls = new List<string>();

            await _appendStrategyReader.Process(document, options, urls);
            await _excludeStrategyReader.Process(options, urls);
            return urls.Distinct();
        }
    }
}