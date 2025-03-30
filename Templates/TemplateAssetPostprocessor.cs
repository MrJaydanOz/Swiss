#if UNITY_EDITOR
using System.IO;
using System.Linq;
using UnityEditor;

namespace Swiss.Editor.Templates
{
    public class TemplateAssetPostprocessor : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] importedAssetPaths, string[] deletedAssetPaths, string[] movedAssetPaths, string[] movedFromAssetPaths)
        {
            foreach (var importedAssetPath in importedAssetPaths)
            {
                if (!importedAssetPath.EndsWith(TemplateAssetHandler.TEMPLATE_SUFFIX, System.StringComparison.OrdinalIgnoreCase))
                    continue;

                string resultAssetPath = importedAssetPath[..^TemplateAssetHandler.TEMPLATE_SUFFIX.Length] + TemplateAssetHandler.TEMPLATE_RESULT_SUFFIX;

                if (!File.Exists(resultAssetPath))
                    File.WriteAllText(resultAssetPath,
                        "///\n" +
                        "/// This is a template result file that was supposed to be filled with generated code.\n" +
                        "///\n" +
                        "/// If you are seeing this message, an error has occured in the template system and this file can be safely deleted.\n" +
                        "///"
                    );

                AssetDatabase.ImportAsset(resultAssetPath);
            }

            foreach ((var fromAssetPath, var toAssetPath) in Enumerable.Zip(movedFromAssetPaths, movedAssetPaths, (f, t) => (f, t)))
            {
                if (!fromAssetPath.EndsWith(TemplateAssetHandler.TEMPLATE_SUFFIX, System.StringComparison.OrdinalIgnoreCase)
                    || !toAssetPath.EndsWith(TemplateAssetHandler.TEMPLATE_SUFFIX, System.StringComparison.OrdinalIgnoreCase))
                    continue;

                string resultFromAssetPath = fromAssetPath[..^TemplateAssetHandler.TEMPLATE_SUFFIX.Length] + TemplateAssetHandler.TEMPLATE_RESULT_SUFFIX;
                string resultToAssetPath = toAssetPath[..^TemplateAssetHandler.TEMPLATE_SUFFIX.Length] + TemplateAssetHandler.TEMPLATE_RESULT_SUFFIX;

                AssetDatabase.MoveAsset(resultFromAssetPath, resultToAssetPath);
            }

            foreach (var deletedAssetPath in deletedAssetPaths)
            {
                if (!deletedAssetPath.EndsWith(TemplateAssetHandler.TEMPLATE_SUFFIX, System.StringComparison.OrdinalIgnoreCase))
                    continue;

                string resultAssetPath = deletedAssetPath[..^TemplateAssetHandler.TEMPLATE_SUFFIX.Length] + TemplateAssetHandler.TEMPLATE_RESULT_SUFFIX;

                AssetDatabase.DeleteAsset(resultAssetPath);
            }
        }
    }
}
#endif