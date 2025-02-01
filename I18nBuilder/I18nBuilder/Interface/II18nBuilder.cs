using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using global::System.Threading.Tasks;

namespace  I18nBuilder.Interface
{
    public interface II18nBuilder
    {

        Task ChangeLocalizeAsync(string language);

        Task<T> CreateTranslationsAsync<T>() where T: class,II18nTranslation,new();
    }
}
