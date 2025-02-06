﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン: 17.0.0.0
//  
//     このファイルへの変更は、正しくない動作の原因になる可能性があり、
//     コードが再生成されると失われます。
// </auto-generated>
// ------------------------------------------------------------------------------
namespace I18nBuilder.Template
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\Users\htakahashi\Documents\GitRep\CsCommonLibrary\QualI18nBuilder\I18nBuilder\I18nBuilder\Template\TranslationBuilderTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class TranslationBuilderTemplate : TranslationBuilderTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("using ");
            
            #line 7 "C:\Users\htakahashi\Documents\GitRep\CsCommonLibrary\QualI18nBuilder\I18nBuilder\I18nBuilder\Template\TranslationBuilderTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ProjectNamespace));
            
            #line default
            #line hidden
            this.Write(".I18nBuilder.EventArg;\r\nusing ");
            
            #line 8 "C:\Users\htakahashi\Documents\GitRep\CsCommonLibrary\QualI18nBuilder\I18nBuilder\I18nBuilder\Template\TranslationBuilderTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ProjectNamespace));
            
            #line default
            #line hidden
            this.Write(@".I18nBuilder.Interface;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
namespace ");
            
            #line 19 "C:\Users\htakahashi\Documents\GitRep\CsCommonLibrary\QualI18nBuilder\I18nBuilder\I18nBuilder\Template\TranslationBuilderTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ProjectNamespace));
            
            #line default
            #line hidden
            this.Write(".I18nBuilder\r\n{\r\n    public class TranslationBuilder : II18nBuilder, IDisposable\r" +
                    "\n    {\r\n        protected readonly II18nDefaultService _i18NDefaultService = def" +
                    "ault!;\r\n\r\n        protected System.Text.Json.JsonSerializerOptions options =\r\n  " +
                    "          new System.Text.Json.JsonSerializerOptions()\r\n            {\r\n         " +
                    "       WriteIndented = true,\r\n                Encoder = JavaScriptEncoder.Create" +
                    "(UnicodeRanges.All),\r\n                PropertyNamingPolicy = System.Text.Json.Js" +
                    "onNamingPolicy.CamelCase,\r\n            };\r\n\r\n        protected IDictionary<strin" +
                    "g, II18nTranslater> _currentI18NTranslations = new Dictionary<string, II18nTrans" +
                    "later>();\r\n\r\n        public string[] Langeuages => _i18NDefaultService.Langeuage" +
                    "s;\r\n\r\n        public string CurrentLanguage => _i18NDefaultService.CurrentLangua" +
                    "ge;\r\n\r\n        public string DefaultLanguage => _i18NDefaultService.DefaultLangu" +
                    "age;\r\n        \r\n        public IObservable<LanguageChangeEventArg> LanguageChang" +
                    "eObservable => (IObservable<LanguageChangeEventArg>)_i18NDefaultService;\r\n\r\n    " +
                    "    public TranslationBuilder(II18nDefaultService i18NDefaultService)\r\n        {" +
                    "\r\n            _i18NDefaultService = i18NDefaultService;\r\n        }\r\n\r\n        pu" +
                    "blic async Task<bool> ChangeLocalizeAsync(string language)\r\n        {\r\n         " +
                    "   var current = _i18NDefaultService.CurrentLanguage;\r\n            if (!_i18NDef" +
                    "aultService.ChangeCurrent(language))\r\n            {\r\n                return fals" +
                    "e;\r\n            }\r\n            foreach (var keyvalue in _currentI18NTranslations" +
                    ")\r\n            {\r\n                var i18NTranslation= await CreateI18nInstanceF" +
                    "actory(keyvalue.Value.I18nTranslation.GetType());\r\n                if (i18NTrans" +
                    "lation is null)\r\n                {\r\n                    continue;\r\n             " +
                    "   }\r\n                keyvalue.Value.SetValue(i18NTranslation);\r\n            }\r\n" +
                    "            _i18NDefaultService.ObserverExecute(current);\r\n            return tr" +
                    "ue;\r\n        }\r\n\r\n        public async Task<T> CreateTranslationsAsync<T>() wher" +
                    "e T : class, II18nTranslation, new()\r\n        {\r\n            var i18nTranslation" +
                    " = await CreateI18nInstanceFactory(typeof(T));\r\n            if (i18nTranslation " +
                    "is null)\r\n            {\r\n                return new T();\r\n            }\r\n       " +
                    "     if (!_currentI18NTranslations.ContainsKey(typeof(T).Name))\r\n            {\r\n" +
                    "                var sourceParam = Expression.Parameter(typeof(T), \"source\");\r\n  " +
                    "              var destinationParam = Expression.Parameter(typeof(T), \"destinatio" +
                    "n\");\r\n\r\n                var bindings = typeof(T).GetProperties(BindingFlags.Publ" +
                    "ic | BindingFlags.Instance)\r\n                    .Where(prop => prop.CanRead && " +
                    "prop.CanWrite)\r\n                    .Select(prop =>\r\n                    {\r\n    " +
                    "                    var sourceProperty = Expression.Property(sourceParam, prop);" +
                    "\r\n                        var destinationProperty = Expression.Property(destinat" +
                    "ionParam, prop);\r\n                        return Expression.Assign(destinationPr" +
                    "operty, sourceProperty);\r\n                    });\r\n\r\n                var body = " +
                    "Expression.Block(bindings);\r\n                var action = Expression.Lambda<Acti" +
                    "on<T, T>>(body, sourceParam, destinationParam).Compile();\r\n                var t" +
                    "ranslator = new I18nTranslater<T>(i18nTranslation, action);\r\n                _cu" +
                    "rrentI18NTranslations.Add(typeof(T).Name, translator);\r\n            }\r\n         " +
                    "   else\r\n            {\r\n                _currentI18NTranslations[typeof(T).Name]" +
                    ".SetValue(i18nTranslation);\r\n            }\r\n            return (T)_currentI18NTr" +
                    "anslations[typeof(T).Name].I18nTranslation;\r\n        }\r\n\r\n        protected virt" +
                    "ual string GetI18nDir()\r\n        {\r\n            try\r\n            {\r\n            " +
                    "    var path = Assembly.GetExecutingAssembly().Location;\r\n                var di" +
                    "r = System.IO.Path.GetDirectoryName(path);\r\n                var i18nDir = System" +
                    ".IO.Path.Combine(dir, \"i18n\");\r\n                return !System.IO.Directory.Exis" +
                    "ts(i18nDir) ? string.Empty : i18nDir;\r\n            }\r\n            catch (Excepti" +
                    "on ex)\r\n            {\r\n                System.Diagnostics.Debug.WriteLine(ex.Mes" +
                    "sage);\r\n                return string.Empty;\r\n            }\r\n        }\r\n\r\n      " +
                    "  async Task<II18nTranslation?> CreateI18nInstanceFactory(Type i18nClassType)\r\n " +
                    "       {\r\n            var buffer = await ReadJsonStringFromFileAsync(i18nClassTy" +
                    "pe.Name);\r\n            if (buffer is (null or \"\"))\r\n            {\r\n             " +
                    "   return null;\r\n            }\r\n            try\r\n            {\r\n                " +
                    "var instance = System.Text.Json.JsonSerializer.Deserialize(buffer, i18nClassType" +
                    ");\r\n                if (instance is not null)\r\n                {\r\n              " +
                    "      return (II18nTranslation)instance;\r\n                }\r\n            }\r\n    " +
                    "        catch (Exception ex)\r\n            {\r\n                System.Diagnostics." +
                    "Debug.WriteLine($\"{ex.Message}\");\r\n            }\r\n            return null;\r\n    " +
                    "    }\r\n\r\n        Task<string> ReadJsonStringFromFileAsync(string className)\r\n   " +
                    "     {\r\n            var dir = GetI18nDir();\r\n            if (dir is (null or \"\")" +
                    ")\r\n            {\r\n                return Task.FromResult(string.Empty);\r\n       " +
                    "     }\r\n\r\n            var fileName = $\"{className.ToLower()}.json\";\r\n           " +
                    " var fullPath = System.IO.Path.Combine(dir, fileName);\r\n            if (!System." +
                    "IO.File.Exists(fullPath))\r\n            {\r\n                return Task.FromResult" +
                    "(string.Empty);\r\n            }\r\n            var buffer = File.ReadAllText(fullPa" +
                    "th);\r\n            var jsonDocument = JsonDocument.Parse(buffer);\r\n            va" +
                    "r jsonBuffer = jsonDocument.RootElement.GetProperty(_i18NDefaultService.CurrentL" +
                    "anguage).ToString();\r\n            return Task.FromResult(jsonBuffer);\r\n        }" +
                    "\r\n\r\n        public void Dispose()\r\n        {\r\n            _currentI18NTranslatio" +
                    "ns.Clear();\r\n        }\r\n\r\n        class I18nTranslater<T> : II18nTranslater wher" +
                    "e T :II18nTranslation\r\n        {\r\n            private readonly Action<T, T> _cop" +
                    "yAction;\r\n            public II18nTranslation I18nTranslation { get; }\r\n\r\n      " +
                    "      public I18nTranslater(II18nTranslation i18nTranslation, Action<T, T> copyA" +
                    "ction)\r\n            {\r\n                I18nTranslation = i18nTranslation;\r\n     " +
                    "           _copyAction=copyAction;\r\n            }\r\n\r\n            public void Set" +
                    "Value(II18nTranslation value) => _copyAction((T)value, (T)I18nTranslation);\r\n   " +
                    "     }\r\n    }\r\n}\r\n");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public class TranslationBuilderTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        public System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
