﻿using I18nBuilder.Interface;

namespace I18nBuilder
{
    public abstract class Translation : II18nTranslation
    {
        private IReadOnlyDictionary<string, Func<string>> _translations = new Dictionary<string, Func<string>>();

        public string this[string key] 
        {
            get
            {
                if (!_translations.ContainsKey(key))
                {
                    return key;
                }
                return _translations[key]();
            }
        }

        protected Translation()
        {
            _translations = new Dictionary<string, Func<string>>()
            {
                { "",()=>"" },
            };
        }

        public abstract bool ContainsKey(string key);
    }
}
