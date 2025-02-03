
using I18nBuilderrTest.I18nBuilder.Extension;
using I18nBuilderrTest.I18nBuilder.Interface;
using System.Linq;

namespace I18nBuilderrTest.I18nBuilder.Service
{
    public sealed class I18nService:II18nDefaultService
    {
        string _deffaultLanguage=string.Empty;
        string _currentLanguage=string.Empty;
        string[] _languages = [];
        public string[] Laangeuages => _languages;

        public string CurrentLanguage => _currentLanguage;

        public string DefaultLanguage => _deffaultLanguage;

        public I18nService(I18nBuilderOption i18NBuilderOption)
        {
            _deffaultLanguage = i18NBuilderOption.DefaultLanguage;
            _languages = i18NBuilderOption.Languages;
            _currentLanguage = _deffaultLanguage;
        }

        public bool ChangeCurrent(string language)
        {
            if (!_languages.Any(t => t == language) || language == _currentLanguage)
            {
                return false;
            }
            _currentLanguage = language;
            return true;
        }
    }
}
