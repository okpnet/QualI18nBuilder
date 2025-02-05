using I18nBuilder.EventArg;
using I18nBuilder.Extension;
using I18nBuilder.Interface;
using I18nBuilder.Observer;
//using System.Reactive.Linq;
//using System.Reactive.Subjects;

namespace I18nBuilder.Service
{
    public sealed class I18nService:II18nDefaultService,IObservable<LanguageChangeEventArg>
    {
        //ISubject<object> _subject = new Subject<object>();
        IList<IObserver<LanguageChangeEventArg>> _observers;
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
            //LanguageChangeObservable = _subject.AsObservable<object>().OfType<LanguageChangeEventArg>();
        }

        //public void OnNext(LanguageChangeEventArg languageChangeEventArg)=>_subject.OnNext(languageChangeEventArg);

        public bool ChangeCurrent(string language)
        {
            if (!_languages.Any(t => t == language) || language == _currentLanguage)
            {
                return false;
            }
            _currentLanguage = language;
            return true;
        }

        public IDisposable Subscribe(IObserver<LanguageChangeEventArg> observer)
        {
            _observers.Add(observer);
        }

        public IDisposable Subscribe(Action<LanguageChangeEventArg> observer)=> Subscribe(new LanguageChangeObserver(observer));
    }
}
