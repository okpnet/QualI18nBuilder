using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace I18nBuilder.Interface
{
    public interface II18nBuilder
    {
        string[] Laangeuages { get; }

        string Current { get; }

        Task ChangeLocalizeAsync(string language);

        Task<T> CreateTranslationsAsync<T>() where T:II18nTranslation;
    }
}
