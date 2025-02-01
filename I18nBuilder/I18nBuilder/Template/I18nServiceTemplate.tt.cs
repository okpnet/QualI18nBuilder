using System;
using System.Collections.Generic;
using System.Text;

namespace I18nBuilder.Template
{
    public partial class I18nServiceTemplate
    {
        public string ProjectNamespace { get; }

        public I18nServiceTemplate(string projectNamespace)
        {
            ProjectNamespace = projectNamespace;
        }
    }
}
