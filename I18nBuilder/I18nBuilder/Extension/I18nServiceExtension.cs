using I18nBuilder.EventArg;
using I18nBuilder.Interface;
using I18nBuilder.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace I18nBuilder.Extension
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

        public static IDisposable Subscribe(this IObservable<LanguageChangeEventArg> observer,Action<LanguageChangeEventArg> action)
        {
            if(observer is not I18nService service)
            {
                throw new I18nException.I18nBuilderException("observer argment is not IObservable", new NotImplementedException());
            }
            return service.Subscribe(action);
        }
    }
}
