using I18nBuilder.Interface;
using I18nBuilder.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace I18nBuilder
{
    public class TranslationBuilder : II18nBuilder,IDisposable
    {
        protected readonly II18nDefaultService _i18NDefaultService=default!;

        protected System.Text.Json.JsonSerializerOptions options=
            new System.Text.Json.JsonSerializerOptions()
                {
                    WriteIndented=true,
                    Encoder=JavaScriptEncoder.Create(UnicodeRanges.All) ,
                    PropertyNamingPolicy=System.Text.Json.JsonNamingPolicy.CamelCase,
                    
                };

        protected IDictionary<string,II18nTranslation> _currentI18NTranslations=new Dictionary<string,II18nTranslation>();


        public TranslationBuilder(II18nDefaultService i18NDefaultService)
        {
            _i18NDefaultService = i18NDefaultService;
        }

        public async Task ChangeLocalizeAsync(string language)
        {
            if (!_i18NDefaultService.ChangeCurrent(language))
            {
                return;
            }
            foreach(var keyvalue in _currentI18NTranslations)
            {
                await CreateI18nInstanceFactory(keyvalue.Value.GetType());
            }
        }

        public async Task<T> CreateTranslationsAsync<T>() where T :class, II18nTranslation,new()
        {
            var result = await CreateI18nInstanceFactory(typeof(T));
            if(result is null)
            {
                return new T();
            }
            if (!_currentI18NTranslations.ContainsKey(typeof(T).Name))
            {
                _currentI18NTranslations.Add(nameof(T), result);
            }
            else
            {
                _currentI18NTranslations[typeof(T).Name] = result;
            }
            return (T)_currentI18NTranslations[typeof(T).Name];
        }

        protected virtual string GetI18nDir()
        {
            try
            {
                var path=Assembly.GetExecutingAssembly().Location;
                var dir=System.IO.Path.GetDirectoryName(path);
                var i18nDir= System.IO.Path.Combine(path,InternalDefine.I18N_DIR);
                return !System.IO.Directory.Exists(i18nDir) ? string.Empty: i18nDir;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return string.Empty;
            }
        }

        async Task<II18nTranslation?> CreateI18nInstanceFactory(Type i18nClassType)
        {
            var buffer = await ReadJsonStringFromFileAsync(i18nClassType.Name);
            if(buffer is (null or ""))
            {
                return null;
            }
            try
            {
                //var argment = Expression.Parameter(i18nClassType, "x");
                //var currentI18NTranslation = Expression.Parameter(typeof(II18nTranslation), "y");
                //var expression=Expression.Call(typeof(Convert),nameof(Convert.ChangeType),null,argment,Expression.Constant(currentI18NTranslation.Type));
                //var castExpression=Expression.Convert(expression, currentI18NTranslation.Type);
                //var assignExpression= Expression.Assign(currentI18NTranslation, castExpression);
                //var lambda= Expression.Lambda<Func<object, II18nTranslation>>(assignExpression, argment);
                
                var instance =System.Text.Json.JsonSerializer.Deserialize(buffer, i18nClassType, options);
                if(instance is not null)
                {
                    return (II18nTranslation)instance;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{ex.Message}");
            }
            return null;
        }

        Task<string> ReadJsonStringFromFileAsync(string className)
        {
            var dir=GetI18nDir();
            if(dir is (null or ""))
            {
                return Task.FromResult(string.Empty);
            }

            var fileName = $"{className.ToLower()}.json";
            var fullPath=System.IO.Path.Combine(dir,fileName );
            if (!System.IO.File.Exists(fullPath))
            {
                return Task.FromResult(string.Empty);
            }
            var buffer=File.ReadAllText(fullPath);
            var jsonDocument = JsonDocument.Parse(buffer);
            var jsonBuffer = jsonDocument.RootElement.GetProperty(_i18NDefaultService.CurrentLanguage).ToString();
            return Task.FromResult(jsonBuffer);
        }

        public void Dispose()
        {
            _currentI18NTranslations.Clear();
        }
    }
}
