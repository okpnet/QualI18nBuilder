using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace I18nBuilder.Template
{
    public partial class II18nBuilderTemplate
    {
        public string ProjectNamespace { get; }

        public II18nBuilderTemplate(string projectNamespace)
        {
            ProjectNamespace = projectNamespace;
        }
    }
}
