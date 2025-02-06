using I18nBuilder.EventArg;
using System;
using System.Collections.Generic;
using System.Text;

namespace I18nBuilder.Observer
{
    public sealed class I18nObserver<T> : IObserver<T>
    {
        Action<T> _action;
        public I18nObserver(Action<T> action) 
        {
            _action = action;
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(T value)=>_action(value);
    }
}
