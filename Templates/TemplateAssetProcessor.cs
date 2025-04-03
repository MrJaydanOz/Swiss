#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Swiss.Editor.Templates
{
    [InitializeOnLoad]
    public static class TemplateAssetHandler
    {
        [Serializable]
        private struct _JSONAssemblyDeclarationName
        {
            public string name;
        }

        public const string TEMPLATE_SUFFIX = ".t.cs";
        public const string TEMPLATE_RESULT_SUFFIX = ".tr.cs";

        private static readonly Regex TEMPLATE_NAME_FROM_FILE = new(@"
            ^((\s|/\*.*?\*/|//.*?\r?\n)*(
            |\#[^\r\n]*\r?\n
            |using(\s|/\*.*?\*/|//.*?\r?\n)+(static(\s|/\*.*?\*/|//.*?\r?\n)*)?\w+(\s|/\*.*?\*/|//.*?\r?\n)*(\.(\s|/\*.*?\*/|//.*?\r?\n)*\w+(\s|/\*.*?\*/|//.*?\r?\n)*)*;
            |namespace(\s|/\*.*?\*/|//.*?\r?\n)+\w+(\s|/\*.*?\*/|//.*?\r?\n)*(\.(\s|/\*.*?\*/|//.*?\r?\n)*\w+(\s|/\*.*?\*/|//.*?\r?\n)*)*[;{]
            ))*
            (\s|/\*.*?\*/|//.*?\r?\n)*(public|internal)(\s|/\*.*?\*/|//.*?\r?\n)*static(\s|/\*.*?\*/|//.*?\r?\n)*class(\s|/\*.*?\*/|//.*?\r?\n)*(?<name>_?Template\w*)",
            RegexOptions.Singleline | RegexOptions.ExplicitCapture | RegexOptions.IgnorePatternWhitespace
        );

        static TemplateAssetHandler()
        {
            EditorApplication.delayCall += _OnAfterCompilation;
        }

        private static void _OnAfterCompilation()
        {
            EditorApplication.delayCall -= _OnAfterCompilation;

            List<(string effectedDirectoryPath, Assembly assembly)> assemblyDeclarationFilePaths = new();

            var allAssemblies = AppDomain.CurrentDomain.GetAssemblies().Select((v) => (assembly: v, name: v.GetName().Name));

            var context = TemplateContext.CreateInstance();

            var assetPath = Application.dataPath;
            SearchIn(assetPath, allAssemblies.First((v) => v.name == "Assembly-CSharp").assembly);
            void SearchIn(string path, Assembly assemblyContext)
            {
                var filePaths = Directory.EnumerateFiles(path).Select((v) => (full: v, relative: v[(assetPath.Length - 6)..])).ToList();

                foreach (var filePath in filePaths)
                {
                    if (!AssetDatabase.AssetPathExists(filePath.relative))
                        continue;

                    var asset = AssetDatabase.LoadAssetAtPath<UnityEditorInternal.AssemblyDefinitionAsset>(filePath.relative);

                    if (asset == null)
                        continue;

                    var fileContents = JsonUtility.FromJson<_JSONAssemblyDeclarationName>(asset.text);
                    if (fileContents.name == null)
                        continue;

                    var foundAssembly = allAssemblies.First((v) => v.name == fileContents.name).assembly;
                    if (foundAssembly != null)
                    {
                        assemblyContext = foundAssembly;
                        break;
                    }
                };

                foreach (var directoryPath in Directory.EnumerateDirectories(path))
                    SearchIn(directoryPath, assemblyContext);

                foreach (var filePath in filePaths)
                {
                    if (!AssetDatabase.AssetPathExists(filePath.relative))
                        continue;

                    if (!filePath.full.EndsWith(TEMPLATE_SUFFIX))
                        continue;

                    var resultFilePath = filePath.full[..^TEMPLATE_SUFFIX.Length] + TEMPLATE_RESULT_SUFFIX;

                    var match = TEMPLATE_NAME_FROM_FILE.Match(AssetDatabase.LoadAssetAtPath<MonoScript>(filePath.relative).text);
                    if (!match.Success)
                    {
                        File.WriteAllText(resultFilePath,
                            "///\n" +
                            "/// This is a template result file that was supposed to be filled with generated code.\n" +
                            "///\n" +
                            "/// The template file has the correct file extention but is invalid in its contents, the template file must strictly\n" +
                            "/// be in the following format:\n" +
                            "///\n" +
                            "/// <code>\n" +
                            "/// #if UNITY_EDITOR // <- Optional but recommended conditional.\n" +
                            "///\n" +
                            "/// using <INSERT NAMESPACES>; // <- Optional namespaces.\n" +
                            "///\n" +
                            "/// namespace <INSERT NAMESPACE> // <- Optional namespace.\n" +
                            "/// {\n" +
                            "///     internal static class _Template_<INSERT NAME> // <- Specific definition with name starting with '_Template_'.\n" +
                            "///     {\n" +
                            "///         public static string Generate() // <- Method returning an object that implements 'ToString()'\n" +
                            "///         {\n" +
                            "///             return /* ... Generated Code ... */;\n" +
                            "///         }\n" +
                            "///     }\n" +
                            "/// }\n" +
                            "/// #endif\n" +
                            "/// </code>\n" +
                            "///"
                        );

                        continue;
                    }

                    var type = assemblyContext.GetTypes().First((v) => v.Name == match.Groups["name"].Value);

                    if (type == null)
                    {
                        // Ignoring here probably fixes the template deleting the result when Unity starts up with compilation errors.
                        /*
                        File.WriteAllText(resultFilePath,
                            "///\n" +
                            "/// This is a template result file that was supposed to be filled with generated code.\n" +
                            "///\n" +
                            "/// The template file has the correct file extention and contents, but an unknown error has caused the contents\n" +
                            "/// to be hidden to the template system.\n" +
                            "///"
                        );
                        */

                        continue;
                    }

                    var method = type.GetMethod(
                        "Generate",
                        0,
                        BindingFlags.Public | BindingFlags.Static,
                        null,
                        CallingConventions.Standard,
                        new Type[] { typeof(TemplateContext) },
                        null
                    );

                    if (method == null)
                    {
                        File.WriteAllText(resultFilePath,
                            "///\n" +
                            "/// This is a template result file that <see cref=\"" + type.FullName + "\"/> was supposed to provide generated code to.\n" +
                            "///\n" +
                            "/// The template file has the correct file extention and format but does not contain the static method 'Generate()'.\n" +
                            "///"
                        );

                        continue;
                    }

                    File.WriteAllText(resultFilePath,
                        "///\n" +
                        "/// This is a template result file that has been filled by <see cref=\"" + type.FullName + ".Generate\"/>.\n" +
                        "///\n" +
                        "\n" +
                        method.Invoke(null, new object[] { context }).ToString()
                    );
                };
            }

            AssetDatabase.Refresh();
        }
    }
}
#endif