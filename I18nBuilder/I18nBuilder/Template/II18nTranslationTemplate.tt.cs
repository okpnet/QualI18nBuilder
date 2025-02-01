using System;
using System.Collections.Generic;
using System.Text;

namespace I18nBuilder.Template
{
    public partial class II18nTranslationTemplate
    {

        public string ProjectNamespace { get; }

        public II18nTranslationTemplate(string projectNamespace)
        {
            ProjectNamespace = projectNamespace;
        }
    }
}
