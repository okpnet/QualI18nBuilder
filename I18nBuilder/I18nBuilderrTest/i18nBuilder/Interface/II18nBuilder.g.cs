
using System.Threading.Tasks;

namespace I18nBuilderrTest.I18nBuilder.Interface
{
    public interface II18nBuilder
    {
        string[] Laangeuages { get; }

        string CurrentLanguage { get; }
        
        string DefaultLanguage { get; }

        Task<bool> ChangeLocalizeAsync(string language);

        Task<T> CreateTranslationsAsync<T>() where T: class,II18nTranslation,new();
    }
}