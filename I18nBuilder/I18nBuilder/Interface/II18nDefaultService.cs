using System;
using System.Collections.Generic;
using System.Text;

namespace I18nBuilder.Interface
{
    public interface II18nDefaultService
    {
        string[] Laangeuages { get; }

        string CurrentLanguage { get; }
        
        string DefaultLanguage { get; }

        bool ChangeCurrent(string language);
    }
}
