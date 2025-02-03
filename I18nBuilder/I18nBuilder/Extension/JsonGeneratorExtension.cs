using I18nBuilder.I18nException;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace I18nBuilder.Extension
{
    public static class JsonGeneratorExtension
    {
        public static IEnumerable<string> GetLanguages(string buffer)
        {
            var jsonDocument= JsonDocument.Parse(buffer);
            if (jsonDocument is null)
            {
                throw new I18nBuilderException("jsonDocument is null", new NullReferenceException());
            }
            try
            {
                return jsonDocument.RootElement.EnumerateObject().Select(t => t.Name);
            }
            catch (Exception ex)
            {
                throw new I18nBuilderException("array languages exception",ex);
            }
        }

        public static IEnumerable<string> JsonValidationToKey(string className,string buffer)
        {
            var languageArray=GetLanguages(buffer);
            try
            {
                var jsonDocument = JsonDocument.Parse(buffer);
                var dictionaries = new List<Dictionary<string, string>>();
                foreach (var lng in languageArray)
                {
                    var jsonBuffer=jsonDocument.RootElement.GetProperty(lng).ToString();
                    var keyvalues=JsonSerializer.Deserialize<Dictionary<string, string>>(jsonBuffer);
                    if(keyvalues is null)
                    {
                        throw new NullReferenceException($"Property '{lng}' is null");
                    }
                    dictionaries.Add(keyvalues);
                }
                var counterCheck = dictionaries.Select(t => t.Count);
                if (counterCheck.Max() != counterCheck.Min())
                {
                    throw new ArgumentException("number of key not match");
                }
                var keyCheck = dictionaries.Select(t => t.Keys.ToArray());
                var hashset = keyCheck.First();
                
                foreach (var keyArray in keyCheck.Skip(1))
                {
                    var excepts = keyArray.Except(hashset);
                    if (excepts.Count() > 0)
                    {
                        throw new Exception($"{string.Join(",",excepts)} is the difference key.");
                    }
                }
                return hashset;
            }
            catch (Exception ex)
            {
                throw new I18nBuilderException($"Exception throw json check processing.[Buffer:{className}] {ex.Message}", ex);
            }
        }
    }
}
