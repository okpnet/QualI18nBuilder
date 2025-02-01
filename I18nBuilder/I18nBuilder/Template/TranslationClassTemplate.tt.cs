using System;
using System.Collections.Generic;
using System.Text;

namespace I18nBuilder.Template
{
    public partial class TranslationClassTemplate
    {
        public string ProjectNamespace { get; }

        public string[] Keys { get; }

        public string ClassName { get; }

        public TranslationClassTemplate(string projectNamespace, string[] keys, string className)
        {
            ProjectNamespace = projectNamespace;
            Keys = keys;
            ClassName = className;
        }
    }
}
