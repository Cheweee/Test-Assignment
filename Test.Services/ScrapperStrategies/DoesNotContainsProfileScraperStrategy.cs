
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Utilities.Extensibility;

namespace Test.Services.ScraperStrategies
{
    public class DoesNotContainsProfileScraperStrategy : IExcludeStrategyExtensionsReader
    {
        private const string name = "doesnotcontainsprofile";

        public async Task<IEnumerable<string>> Process(ExtensibleGetOptions options, List<string> urls)
        {
            if (!options.PartIsUsed(name))
                return urls;
                
            await Task.Run(() =>
            {
                foreach (string url in urls)
                {
                    if (url.ToLower().Contains("profile"))
                        urls.Remove(url);
                }
            });

            return urls;
        }
    }
}