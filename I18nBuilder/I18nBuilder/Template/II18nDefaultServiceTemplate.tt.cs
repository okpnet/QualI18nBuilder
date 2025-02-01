using System;
using System.Collections.Generic;
using System.Text;

namespace I18nBuilder.Template
{
    public partial class II18nDefaultServiceTemplate
    {

        public string ProjectNamespace { get; }

        public II18nDefaultServiceTemplate(string projectNamespace)
        {
            ProjectNamespace = projectNamespace;
        }
    }
}
