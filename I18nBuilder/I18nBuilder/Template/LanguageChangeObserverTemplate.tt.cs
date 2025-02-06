using System;
using System.Collections.Generic;
using System.Text;

namespace I18nBuilder.Template
{
    public partial class LanguageChangeObserverTemplate
    {
        public string ProjectNamespace { get; }

        public LanguageChangeObserverTemplate(string projectNamespace)
        {
            ProjectNamespace = projectNamespace;
        }
    }
}
