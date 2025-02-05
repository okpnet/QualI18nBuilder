using global::System.Threading.Tasks;
using I18nBuilder.EventArg;

namespace I18nBuilder.Interface
{
    public interface II18nBuilder
    {
        string[] Langeuages { get; }

        string CurrentLanguage { get; }

        string DefaultLanguage { get; }

        IObserver<LanguageChangeEventArg> LanguageChangeObservable { get; }

        Task<bool> ChangeLocalizeAsync(string language);

        Task<T> CreateTranslationsAsync<T>() where T: class,II18nTranslation,new();
    }
}
