
namespace I18nBuilderrTest.I18nBuilder.Interface
{
    public interface II18nDefaultService
    {
        string[] Laangeuages { get; }

        string CurrentLanguage { get; }
        
        string DefaultLanguage { get; }

        bool ChangeCurrent(string language);
    }
}