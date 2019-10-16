using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Test.Utilities.Extensibility;

namespace Test.Services
{
    public class ScraperService
    {
        private readonly IAppendStrategyExtensionsReader _appendStrategyReader;
        private readonly IExcludeStrategyExtensionsReader _excludeStrategyReader;
        private readonly ILogger _logger;

        public ScraperService(ILogger logger, IAppendStrategyExtensionsReader appendStrategyReader, IExcludeStrategyExtensionsReader excludeStrategyReader)
        {
            _logger = logger;
            _appendStrategyReader = appendStrategyReader;
            _excludeStrategyReader = excludeStrategyReader;
        }

        public async Task<IEnumerable<string>> Process(ExtensibleGetOptions options)
        {
            return await Process(options, options.SourceUrl);
        }

        public async Task<IEnumerable<string>> Process(ExtensibleGetOptions options, string originalUrl)
        {
            List<string> urls = new List<string>();
            try
            {
                HtmlWeb website = new HtmlWeb();
                website.AutoDetectEncoding = false;
                website.OverrideEncoding = Encoding.Default;

                HtmlDocument document = website.Load(options.SourceUrl);
                document.OptionComputeChecksum = true;

                await _appendStrategyReader.Process(document, options, urls);
                await _excludeStrategyReader.Process(options, urls);

                string domain = GetDomainUrl(originalUrl).ToLower();

                IEnumerable<string> newUrls = new List<string>();
                foreach (string url in urls)
                {
                    if (!url.ToLower().Contains(domain) || url == originalUrl)
                        continue;

                    newUrls = await Process(new ExtensibleGetOptions { Parts = options.Parts, SourceUrl = url }, originalUrl);
                }

                urls.AddRange(newUrls);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
            }

            return urls.Distinct();
        }

        private string GetDomainUrl(string url)
        {
            try
            {
                Uri uri = new Uri(url);
                return uri.Host;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw exception;
            }
        }
    }
}