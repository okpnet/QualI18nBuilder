using System;
using System.Collections.Generic;
using System.Text;

namespace I18nBuilder.EventArg
{
    public sealed class LanguageChangeEventArg
    {
        public string FromLanguage { get; }

        public string ToLanguage { get; }

        public LanguageChangeEventArg(string fromLanguage, string toLanguage)
        {
            FromLanguage = fromLanguage;
            ToLanguage = toLanguage;
        }
    }
}
