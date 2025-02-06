using I18nBuilder.EventArg;
using I18nBuilder.Extension;
using I18nBuilder.Interface;
using I18nBuilder.Observer;

namespace I18nBuilder.Service
{
    public sealed class I18nService:II18nDefaultService
    {
        string _deffaultLanguage =string.Empty;

        string _currentLanguage=string.Empty;
        
        string[] _languages = [];

        public string[] Langeuages => _languages;

        public string CurrentLanguage => _currentLanguage;

        public string DefaultLanguage => _deffaultLanguage;

        public IObservable<LanguageChangeEventArg> AfterLanguageChangeObservable { get; }=new I18nObservable<LanguageChangeEventArg>();

        public IObservable<string> LanguageChangeObservable { get; } = new I18nObservable<string>();

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
            var beforeLanguage = _currentLanguage;
            _currentLanguage = language;
            return true;
        }

        public void ObserverExecute(string beforeLanguage)
        {
            (LanguageChangeObservable as I18nObservable<string>)?.Execute(_currentLanguage);
            (AfterLanguageChangeObservable as I18nObservable<LanguageChangeEventArg>)?.Execute(new LanguageChangeEventArg(beforeLanguage, _currentLanguage));
        }
    }
}
