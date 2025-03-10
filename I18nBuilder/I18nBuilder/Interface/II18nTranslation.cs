using System;
using System.Collections.Generic;
using System.Text;

namespace I18nBuilder.Interface
{
    public interface II18nTranslation
    {
        string this[string key] { get; }

        bool ContainsKey(string key);
    }
}
