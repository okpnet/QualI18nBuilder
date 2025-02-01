using System;
using System.Collections.Generic;
using System.Text;

namespace I18nBuilder.Template
{
    public partial class I18nBuilderExceptionTemplate
    {
        public string ProjectNamespace { get; }

        public I18nBuilderExceptionTemplate(string projectNamespace)
        {
            ProjectNamespace = projectNamespace;
        }
    }
}
