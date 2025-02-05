using I18nBuilder.EventArg;
using System;
using System.Collections.Generic;
using System.Text;

namespace I18nBuilder.Observer
{
    public sealed class LanguageChangeObserver : IObserver<LanguageChangeEventArg>
    {
        Action<LanguageChangeEventArg> _action;
        public LanguageChangeObserver(Action<LanguageChangeEventArg> action) 
        {
            _action = action;
        }
        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(LanguageChangeEventArg value)=>_action(value);
    }
}
