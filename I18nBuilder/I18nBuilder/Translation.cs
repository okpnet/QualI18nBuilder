using I18nBuilder.Interface;
using System;

namespace I18nBuilder
{
    public abstract class Translation:II18nTranslation  
    {
        public string this[string key] 
        {
            get
            {
                return key;
            }
        }
    }
}
