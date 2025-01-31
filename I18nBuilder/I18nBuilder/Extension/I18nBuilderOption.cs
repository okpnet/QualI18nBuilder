using System;
using System.Collections.Generic;
using System.Text;

namespace I18nBuilder.Extension
{
    public sealed class I18nBuilderOption
    {
        public string[] Languages { get; set; } = [];
        public string DefaultLanguage { get; set; } = "";
    }
}
