using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace I18nBuilderGenerator
{


    [Generator]
    public class I18nBuilderGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            ///デバッグ
            if (!Debugger.IsAttached)
            {
                Debugger.Launch();
            }
            // Msgフォルダ内のJSONファイルを取得
            var jsonFiles = context.AdditionalTextsProvider
                .Where(file => file.Path.EndsWith(".json"))
                .Collect();

            // 取得したJSONファイルからキーを解析し、コードを生成
            context.RegisterSourceOutput(jsonFiles, (spc, files) =>
            {
                foreach (var file in files)
                {
                    var content = file.GetText()?.ToString();
                    if (string.IsNullOrWhiteSpace(content)) continue;

                    try
                    {
                        var keys = JsonSerializer.Deserialize<Dictionary<string, string>>(content);
                        if (keys == null) continue;

                        var fileName = Path.GetFileNameWithoutExtension(file.Path);
                        var className = $"{fileName}Keys";
                        var generatedCode = GenerateKeyClass("MSGLibTest.MsgKeys", className, keys.Keys);

                        spc.AddSource($"{className}.g.cs", SourceText.From(generatedCode, Encoding.UTF8));
                    }
                    catch (JsonException)
                    {
                        // JSON パースエラー時は無視
                    }
                }
            });
        }

        private string GenerateKeyClass(string namespaceName, string className, IEnumerable<string> keys)
        {
            var sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine();
            sb.AppendLine($"namespace {namespaceName}");
            sb.AppendLine("{");
            sb.AppendLine($"    public static partial class {className}");
            sb.AppendLine("    {");

            foreach (var key in keys)
            {
                var formattedKey = key.Replace(" ", "_").Replace(".", "_");
                sb.AppendLine($"        public const string {formattedKey} = \"{key}\";");
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }
    }

}
