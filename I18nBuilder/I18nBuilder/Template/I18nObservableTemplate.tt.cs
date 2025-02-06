using System;
using System.Collections.Generic;
using System.Text;

namespace I18nBuilder.Template
{
    public partial class I18nObservableTemplate
    {
        public string ProjectNamespace { get; }

        public I18nObservableTemplate(string projectNamespace)
        {
            ProjectNamespace = projectNamespace;
        }
    }
}
