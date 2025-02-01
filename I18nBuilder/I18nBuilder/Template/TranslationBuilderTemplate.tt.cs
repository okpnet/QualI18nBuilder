using System;
using System.Collections.Generic;
using System.Text;

namespace I18nBuilder.Template
{
    public partial class TranslationBuilderTemplate
    {
        public string ProjectNamespace { get; }

        public TranslationBuilderTemplate(string projectNamespace)
        {
            ProjectNamespace = projectNamespace;
        }
    }
}
