using System;
using System.Collections.Generic;
using System.Text;

namespace I18nBuilder.Template
{
    public partial class LanguageChangeEventArgTemplate
    {
        public string ProjectNamespace { get; }

        public LanguageChangeEventArgTemplate(string projectNamespace)
        {
            ProjectNamespace = projectNamespace;
        }
    }
}
