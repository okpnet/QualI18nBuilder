using I18nBuilder.Interface;

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
