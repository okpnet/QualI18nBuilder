using I18nBuilder.Interface;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace I18nBuilder
{
    public class TranslationBuilder : II18nBuilder, IDisposable
    {
        protected readonly II18nDefaultService _i18NDefaultService = default!;

        protected System.Text.Json.JsonSerializerOptions options =
            new System.Text.Json.JsonSerializerOptions()
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,

            };

        protected IDictionary<string, II18nTranslater> _currentI18NTranslations = new Dictionary<string, II18nTranslater>();

        public string[] Laangeuages => _i18NDefaultService.Laangeuages;

        public string CurrentLanguage => _i18NDefaultService.CurrentLanguage;

        public string DefaultLanguage => _i18NDefaultService.DefaultLanguage;

        public TranslationBuilder(II18nDefaultService i18NDefaultService)
        {
            _i18NDefaultService = i18NDefaultService;
        }

        public async Task<bool> ChangeLocalizeAsync(string language)
        {
            if (!_i18NDefaultService.ChangeCurrent(language))
            {
                return false;
            }
            foreach (var keyvalue in _currentI18NTranslations)
            {
                var i18NTranslation= await CreateI18nInstanceFactory(keyvalue.Value.I18nTranslation.GetType());
                if (i18NTranslation is null)
                {
                    continue;
                }
                keyvalue.Value.SetValue(i18NTranslation);
            }
            return true;
        }

        public async Task<T> CreateTranslationsAsync<T>() where T : class, II18nTranslation, new()
        {
            var i18nTranslation = await CreateI18nInstanceFactory(typeof(T));
            if (i18nTranslation is null)
            {
                return new T();
            }
            if (!_currentI18NTranslations.ContainsKey(typeof(T).Name))
            {
                var sourceParam = Expression.Parameter(typeof(T), "source");
                var destinationParam = Expression.Parameter(typeof(T), "destination");

                var bindings = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(prop => prop.CanRead && prop.CanWrite)
                    .Select(prop =>
                    {
                        var sourceProperty = Expression.Property(sourceParam, prop);
                        var destinationProperty = Expression.Property(destinationParam, prop);
                        return Expression.Assign(destinationProperty, sourceProperty);
                    });

                var body = Expression.Block(bindings);
                var action = Expression.Lambda<Action<T, T>>(body, sourceParam, destinationParam).Compile();
                var translator = new I18nTranslater<T>(i18nTranslation, action);
                _currentI18NTranslations.Add(typeof(T).Name, translator);
            }
            else
            {
                _currentI18NTranslations[typeof(T).Name].SetValue(i18nTranslation);
            }
            return (T)_currentI18NTranslations[typeof(T).Name].I18nTranslation;
        }

        protected virtual string GetI18nDir()
        {
            try
            {
                var path = Assembly.GetExecutingAssembly().Location;
                var dir = System.IO.Path.GetDirectoryName(path);
                var i18nDir = System.IO.Path.Combine(dir, "i18n");
                return !System.IO.Directory.Exists(i18nDir) ? string.Empty : i18nDir;
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
            if (buffer is (null or ""))
            {
                return null;
            }
            try
            {
                var instance = System.Text.Json.JsonSerializer.Deserialize(buffer, i18nClassType);
                if (instance is not null)
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
            var dir = GetI18nDir();
            if (dir is (null or ""))
            {
                return Task.FromResult(string.Empty);
            }

            var fileName = $"{className.ToLower()}.json";
            var fullPath = System.IO.Path.Combine(dir, fileName);
            if (!System.IO.File.Exists(fullPath))
            {
                return Task.FromResult(string.Empty);
            }
            var buffer = File.ReadAllText(fullPath);
            var jsonDocument = JsonDocument.Parse(buffer);
            var jsonBuffer = jsonDocument.RootElement.GetProperty(_i18NDefaultService.CurrentLanguage).ToString();
            return Task.FromResult(jsonBuffer);
        }

        public void Dispose()
        {
            _currentI18NTranslations.Clear();
        }


        class I18nTranslater<T> : II18nTranslater where T :II18nTranslation
        {
            private readonly Action<T, T> _copyAction;
            public II18nTranslation I18nTranslation { get; }

            public I18nTranslater(II18nTranslation i18nTranslation, Action<T, T> copyAction)
            {
                I18nTranslation = i18nTranslation;
                _copyAction=copyAction;
            }

            public void SetValue(II18nTranslation value) => _copyAction((T)value, (T)I18nTranslation);
        }
    }
}
