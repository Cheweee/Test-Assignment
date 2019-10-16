using System;
using Microsoft.Extensions.DependencyInjection;

namespace Test.Utilities.Extensibility
{
    public static class ExtensibilityExtensions
    {
        public static IAppendStrategyExtensionsReader ComposeAppendStrategyReaders(this IServiceProvider provider)
        {
            return new AppendStrategyExtensionsCompositeReader(provider.GetServices<IAppendStrategyExtensionsReader>);
        }
        public static IExcludeStrategyExtensionsReader ComposeExcludeStrategyReaders(this IServiceProvider provider)
        {
            return new ExcludeStrategyExtensionsCompositeReader(provider.GetServices<IExcludeStrategyExtensionsReader>);
        }
    }
}