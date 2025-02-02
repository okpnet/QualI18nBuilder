using System;
using System.Collections.Generic;
using System.Text;

namespace I18nBuilder.Template
{
    public partial class II18nTranslaterTemplate
    {
        public string ProjectNamespace { get; }

        public II18nTranslaterTemplate(string projectNamespace)
        {
            ProjectNamespace = projectNamespace;
        }
    }
}
