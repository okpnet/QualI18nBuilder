using I18nBuilder.Extension;
using I18nBuilder.Template;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Operations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace I18nBuilder
{


    [Generator(LanguageNames.CSharp)]
    public class I18nBuilderGenerator : ISourceGenerator
    {

        public void Initialize(GeneratorInitializationContext context)
        {
            // デバッグ用: ソースジェネレーターの起動を確認
            ///デバッグ
            //if (!Debugger.IsAttached)
            //{
            //    Debugger.Launch();
            //}
        }

        public void Execute(GeneratorExecutionContext context)
        {
            // テストプロジェクトの名前空間を取得枠
            string projectNamespace = context.Compilation.AssemblyName ?? "I18nBuilderGenerator";

            // MSGLibTest プロジェクト直下の i18n ディレクトリを探索
            string? projectDir = GetProjectDirectory(context);
            if (projectDir == null) return;

            string i18nLibPath = Path.Combine(projectDir, "generated",InternalI18nBuilderDefine.I18NLIB_DIR);

            if (Directory.Exists(i18nLibPath))
            {
                Directory.Delete(i18nLibPath, true);
            }
            Directory.CreateDirectory(i18nLibPath);
            CreateInterface(context, i18nLibPath, projectNamespace);
            CreateService(context, i18nLibPath, projectNamespace);
            CreateLogic(context, i18nLibPath, projectNamespace);
            CreateObserver(context, i18nLibPath, projectNamespace);

            string i18nPath = Path.Combine(projectDir, InternalI18nBuilderDefine.I18N_DIR);
            if (!Directory.Exists(i18nPath)) return;
            var translationPath = Path.Combine(i18nLibPath, "Translation");
            Directory.CreateDirectory(translationPath);
            foreach (var jsonFile in Directory.GetFiles(i18nPath, "*.json"))
            {
                var fileName = Path.GetFileNameWithoutExtension(jsonFile);
                var className = ToValidClassName(fileName);
                var keys = GenerateClassCode(projectNamespace, className, jsonFile);
                var translationClassTemplate = new TranslationClassTemplate(projectNamespace, keys,className);
                var translationClassCode = translationClassTemplate.TransformText();
                context.AddSource($"{className}.g.cs", translationClassCode);
                //File.WriteAllText(Path.Combine(translationPath, $"{className}.g.cs"), translationClassCode);
            }

        }

        private static void CreateInterface(GeneratorExecutionContext context,string dirPath,string nameSpace)
        {
            var interfacePath=Path.Combine(dirPath,nameof(Interface));
            Directory.CreateDirectory(interfacePath);
            var iI18nTranslationTemplate = new II18nTranslationTemplate(nameSpace);
            var iI18nTranslationTemplateCode = iI18nTranslationTemplate.TransformText();
            context.AddSource($"II18nTranslation.g.cs", iI18nTranslationTemplateCode);
            //File.WriteAllText(Path.Combine(interfacePath, $"II18nTranslation.g.cs"), iI18nTranslationTemplateCode);


            var iI18nBuilderTemplate = new II18nBuilderTemplate(nameSpace);
            var iI18nBuilderCode = iI18nBuilderTemplate.TransformText();
            context.AddSource($"II18nBuilder.g.cs", iI18nBuilderCode);
            //File.WriteAllText(Path.Combine(interfacePath, $"II18nBuilder.g.cs"), iI18nBuilderCode);

            var iI18nDefaultServiceTemplate = new II18nDefaultServiceTemplate(nameSpace);
            var iI18nDefaultServiceCode = iI18nDefaultServiceTemplate.TransformText();
            context.AddSource($"II18nDefaultService.g.cs", iI18nDefaultServiceCode);
            //File.WriteAllText(Path.Combine(interfacePath, $"II18nDefaultService.g.cs"), iI18nDefaultServiceCode);

            var iI18nTranslaterTemplate = new II18nTranslaterTemplate(nameSpace);
            var iI18nTranslaterCode = iI18nTranslaterTemplate.TransformText();
            context.AddSource($"II18nTranslater.g.cs", iI18nTranslaterCode);
            //File.WriteAllText(Path.Combine(interfacePath, $"II18nTranslater.g.cs"), iI18nTranslaterCode);
        }

        private static void CreateObserver(GeneratorExecutionContext context, string dirPath, string nameSpace)
        {
            var i18nObservableTemplate = new I18nObservableTemplate(nameSpace);
            var i18nObservableCode = i18nObservableTemplate.TransformText();
            context.AddSource($"I18nObservable.g.cs", i18nObservableCode);

            var i18nObserverTemplate = new I18nObserverTemplate(nameSpace);
            var i18nObserverCode = i18nObserverTemplate.TransformText();
            context.AddSource($"I18nObserver.g.cs", i18nObserverCode);
        }
        private static void CreateService(GeneratorExecutionContext context,string dirPath, string nameSpace)
        {
            var extensionPath = Path.Combine(dirPath, nameof(Extension));
            Directory.CreateDirectory(extensionPath);
            var i18nServiceExtensionTemplate = new I18nServiceExtensionTemplate(nameSpace);
            var i18nServiceExtensionCode = i18nServiceExtensionTemplate.TransformText();
            context.AddSource($"I18nServiceExtension.g.cs", i18nServiceExtensionCode);
            //File.WriteAllText(Path.Combine(extensionPath, $"I18nServiceExtension.g.cs"), i18nServiceExtensionCode);

            var i18nBuilderOptionTemplate = new I18nBuilderOptionTemplate(nameSpace);
            var i18nBuilderOptionCode = i18nBuilderOptionTemplate.TransformText();
            context.AddSource($"I18nBuilderOption.g.cs", i18nBuilderOptionCode);
            //File.WriteAllText(Path.Combine(extensionPath, $"I18nBuilderOption.g.cs"), i18nBuilderOptionCode);

            var servicePath = Path.Combine(dirPath, nameof(Service));
            Directory.CreateDirectory(servicePath);
            var i18nServiceTemplate = new I18nServiceTemplate(nameSpace);
            var i18nServiceCode = i18nServiceTemplate.TransformText();
            context.AddSource($"I18nService.g.cs", i18nServiceCode);
            //File.WriteAllText(Path.Combine(servicePath, $"I18nService.g.cs"), i18nServiceCode);

        }

        private static void CreateLogic(GeneratorExecutionContext context,string dirPath, string nameSpace) 
        {
            var exceptionPath = Path.Combine(dirPath, $"I18n{nameof(Exception)}");
            Directory.CreateDirectory(exceptionPath);
            var i18nBuilderExceptionTemplate = new I18nBuilderExceptionTemplate(nameSpace);
            var i18nBuilderExceptionCode = i18nBuilderExceptionTemplate.TransformText();
            context.AddSource($"i18nBuilderException.g.cs", i18nBuilderExceptionCode);
            //File.WriteAllText(Path.Combine(exceptionPath, $"i18nBuilderException.g.cs"), i18nBuilderExceptionCode);


            var translationBuilderTemplate = new TranslationBuilderTemplate(nameSpace);
            var translationBuilderCode = translationBuilderTemplate.TransformText();
            context.AddSource($"TranslationBuilder.g.cs", translationBuilderCode);
            //File.WriteAllText(Path.Combine(dirPath, $"TranslationBuilder.g.cs"), translationBuilderCode);

            var languageChangeEventArgTemplate = new LanguageChangeEventArgTemplate(nameSpace);
            var languageChangeEventArgCode = languageChangeEventArgTemplate.TransformText();
            context.AddSource($"LanguageChangeEventArg.g.cs", languageChangeEventArgCode);
        }

        private static string? GetProjectDirectory(GeneratorExecutionContext context)
        {
            if (context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.ProjectDir", out var projectDir))
            {
                return projectDir;
            }
            return null;
        }

        private static string ToValidClassName(string fileName)
        {
            return SyntaxFacts.IsValidIdentifier(fileName) ? fileName : $"Msg_{fileName}";
        }

        private void FileRead(string jsonFilePath)
        {
            if(!File.Exists(jsonFilePath)) return;
            using var streamReder = new StreamReader(jsonFilePath, Encoding.UTF8);
            var buffer= streamReder.ReadToEnd();
            var jsonBuilder=JsonDocument.Parse(buffer);
            var languages = jsonBuilder.RootElement.EnumerateObject().Select(t => t.Name);

        }

        private string[] GenerateClassCode(string namespaceName, string className, string jsonFilePath)
        {
            try
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                var keys = JsonGeneratorExtension.JsonValidationToKey(className,jsonContent);
                return keys.ToArray();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{jsonFilePath} json error");
                throw;
            }

            //var sb = new StringBuilder();
            //sb.AppendLine("// <auto-generated/>");
            //sb.AppendLine("using System.Collections.Generic;");
            //sb.AppendLine("using I18nBuilder.Interface;");
            //sb.AppendLine("namespace " + namespaceName);
            //sb.AppendLine("{");
            //sb.AppendLine($"    public partial class {className} : {nameof(II18nTranslation)}");
            //sb.AppendLine($"    {{");
            //foreach (var key in keys)
            //{
            //    string propertyName = ToValidClassName(key);
            //    sb.AppendLine($"        public string {propertyName} {{ get; set; }};");
            //}
            //sb.AppendLine("    }");
            //sb.AppendLine("}");
            //return sb.ToString();
        }
    }

}
