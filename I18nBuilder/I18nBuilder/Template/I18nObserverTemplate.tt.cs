using System;
using System.Collections.Generic;
using System.Text;

namespace I18nBuilder.Template
{
    public partial class I18nObserverTemplate
    {
        public string ProjectNamespace { get; }

        public I18nObserverTemplate(string projectNamespace)
        {
            ProjectNamespace = projectNamespace;
        }
    }
}
