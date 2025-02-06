using I18nBuilder.EventArg;
using I18nBuilder.Extension;
using I18nBuilder.Interface;
using I18nBuilder.Observer;

namespace I18nBuilder.Service
{
    public sealed class I18nService:II18nDefaultService,IObservable<LanguageChangeEventArg>
    {
        IList<IObserver<LanguageChangeEventArg>> _observers=new List<IObserver<LanguageChangeEventArg>>();
        string _deffaultLanguage=string.Empty;
        string _currentLanguage=string.Empty;
        string[] _languages = [];
        public string[] Langeuages => _languages;

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
            var beforeLanguage = _currentLanguage;
            _currentLanguage = language;
            foreach(var observer in _observers)
            {
                observer.OnNext(new LanguageChangeEventArg(beforeLanguage, language));
            }
            return true;
        }

        public IDisposable Subscribe(IObserver<LanguageChangeEventArg> observer)
        {
            _observers.Add(observer);
            return new Unsubscriber(()=>_observers.Remove(observer));
        }

        public IDisposable Subscribe(Action<LanguageChangeEventArg> observer)=> Subscribe(new LanguageChangeObserver(observer));

        private class Unsubscriber : IDisposable
        {
            private Action _ansubscribe;

            public Unsubscriber(Action ansubscribe)
            {
                _ansubscribe = ansubscribe;
            }

            public void Dispose()
            {
                _ansubscribe.Invoke();
            }
        }
    }
}
