using System;
using System.Collections.Generic;
using System.Text;

namespace I18nBuilder.Observer
{
    public sealed class I18nObservable<T> : IObservable<T>
    {
        private IList<IObserver<T>> _observerCollection=new List<IObserver<T>>();

        public I18nObservable()
        {
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            _observerCollection.Add(observer);
            return new Unsubscriber(() => _observerCollection.Remove(observer));
        }

        public IDisposable Subscribe(Action<T> observer)
        {
            return Subscribe(new I18nObserver<T>(observer));
        }

        public void Execute(T argment)
        {
            foreach(var observer in _observerCollection)
            {
                observer.OnNext(argment);
            }
        }

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
