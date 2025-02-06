using I18nBuilder.EventArg;
using I18nBuilder.Observer;
using System;
using System.Collections.Generic;
using System.Text;

namespace I18nBuilder.Interface
{
    public interface II18nDefaultService
    {
        string[] Langeuages { get; }

        string CurrentLanguage { get; }
        
        string DefaultLanguage { get; }

        public IObservable<LanguageChangeEventArg> AfterLanguageChangeObservable { get; }

        public IObservable<string> LanguageChangeObservable { get; }

        bool ChangeCurrent(string language);

        void ObserverExecute(string beforeLanguage);
    }
}
