using System;
using System.Collections.Generic;
using System.Text;

namespace I18nBuilder.Template
{
    public partial class I18nServiceExtensionTemplate
    {
        public string ProjectNamespace { get; }

        public I18nServiceExtensionTemplate(string projectNamespace)
        {
            ProjectNamespace = projectNamespace;
        }
    }
}
