using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Test.Utilities.Extensibility
{
    public interface IAppendStrategyExtensionsReader
    {
        Task<IEnumerable<string>> Process(HtmlDocument document, ExtensibleGetOptions options, List<string> urls);
    }

    public interface IExcludeStrategyExtensionsReader
    {
        Task<IEnumerable<string>> Process(ExtensibleGetOptions options, List<string> urls);        
    }

    public static class AppendStrategyExtensionsReaderExtension
    {
        public static async Task<IEnumerable<string>> ProcessExtensions(
            this IEnumerable<IAppendStrategyExtensionsReader> members,
            HtmlDocument document,
            ExtensibleGetOptions options,
            List<string> urls
        )
        {
            foreach (var member in members)
                await member.Process(document, options, urls);

            return urls;
        }
    }

    public static class ExcludeStrategyExtensionsReaderExtension
    {
        public static async Task<IEnumerable<string>> ProcessExtensions(
            this IEnumerable<IExcludeStrategyExtensionsReader> members,
            ExtensibleGetOptions options,
            List<string> urls
        )
        {
            foreach (var member in members)
                await member.Process(options, urls);

            return urls;
        }
    }

    public class AppendStrategyExtensionsCompositeReader : IAppendStrategyExtensionsReader
    {
        private readonly Func<IEnumerable<IAppendStrategyExtensionsReader>> _membersFactory;

        public AppendStrategyExtensionsCompositeReader(Func<IEnumerable<IAppendStrategyExtensionsReader>> membersFactory)
        {
            _membersFactory = membersFactory ?? throw new System.ArgumentNullException(nameof(membersFactory));
        }

        public async Task<IEnumerable<string>> Process(HtmlDocument document, ExtensibleGetOptions options, List<string> urls)
        {
            return await _membersFactory().ProcessExtensions(document, options, urls);
        }
    }

    public class ExcludeStrategyExtensionsCompositeReader : IExcludeStrategyExtensionsReader
    {
        private readonly Func<IEnumerable<IExcludeStrategyExtensionsReader>> _membersFactory;

        public ExcludeStrategyExtensionsCompositeReader(Func<IEnumerable<IExcludeStrategyExtensionsReader>> membersFactory)
        {
            _membersFactory = membersFactory ?? throw new System.ArgumentNullException(nameof(membersFactory));
        }

        public async Task<IEnumerable<string>> Process(ExtensibleGetOptions options, List<string> urls)
        {
            return await _membersFactory().ProcessExtensions(options, urls);
        }
    }
}