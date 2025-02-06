using Microsoft.Extensions.DependencyInjection;
//using I18nBuilder;
using ConsoleApp1.I18nBuilder.Extension;
using ConsoleApp1.I18nBuilder.Interface;

await Task.Delay(10);
Console.WriteLine("Hello, World!");
var serviceCollection = (IServiceCollection)new ServiceCollection();
serviceCollection.AddI18nBuilderService(option =>
{
    option.DefaultLanguage = "ja";
    option.Languages = ["ja", "en"];
});
var provider = serviceCollection.BuildServiceProvider();
var i18n = provider.GetRequiredService<II18nBuilder>();
i18n.LanguageChangeObservable.Subscribe(arg => Console.WriteLine($"!!!!==>{arg.ToLanguage}"));
var page = await i18n.CreateTranslationsAsync<ConsoleApp1.I18nBuilder.Page1>();
var common = await i18n.CreateTranslationsAsync<ConsoleApp1.I18nBuilder.Common>();

Console.WriteLine(page.Name);
Console.WriteLine(common.Unit);
await Task.Delay(1500);
await i18n.ChangeLocalizeAsync("en");
await Task.Delay(1500);
Console.WriteLine($"vn={await i18n.ChangeLocalizeAsync("vn")}");
await Task.Delay(1500);
Console.WriteLine($"en={await i18n.ChangeLocalizeAsync("en")}");
await Task.Delay(1500);
Console.WriteLine($"ja={await i18n.ChangeLocalizeAsync("ja")}");
await Task.Delay(1500);
Console.WriteLine($"en={await i18n.ChangeLocalizeAsync("en")}");
Console.WriteLine(page.Label);
Console.WriteLine(common.ExceptionMsg);
