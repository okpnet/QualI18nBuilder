
using I18nBuilderrTest.I18nBuilder.Interface;
using I18nBuilderrTest.I18nBuilder.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace I18nBuilderrTest.I18nBuilder.Extension
{
    public static class I18nServiceExtension
    {
        public static IServiceCollection AddI18nBuilderService(this IServiceCollection serviceCollection,Action<I18nBuilderOption> options)
        {
            var option=new I18nBuilderOption();
            options.Invoke(option);

            if (!serviceCollection.Any(t => t.ServiceType == typeof(II18nDefaultService)))
            {
                serviceCollection.AddSingleton<II18nDefaultService, I18nService>(provider=>{
                    return new I18nService(option);
                });
            }

            serviceCollection.AddTransient<II18nBuilder, TranslationBuilder>((provider) =>
            {
                var i18nService=provider.GetRequiredService<II18nDefaultService>();
                var builder = new TranslationBuilder(i18nService);
                return builder;
            });
            return serviceCollection;
        }
    }
}
