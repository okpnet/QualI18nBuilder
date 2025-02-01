using System;
using System.Collections.Generic;
using System.Text;

namespace I18nBuilder.Template
{
    public partial class I18nBuilderOptionTemplate
    {
        public string ProjectNamespace { get; }

        public I18nBuilderOptionTemplate(string projectNamespace)
        {
            ProjectNamespace = projectNamespace;
        }
    }
}
