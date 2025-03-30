#if UNITY_EDITOR
using System.Linq;
using System.Text;

namespace Swiss.Editor.Templates
{
    internal static class _Template_Test
    {
        public static string Generate(TemplateContext context)
        {
            return new StringBuilder().AppendJoin("\n", context.GetTypes().Select((v) => $"// {v.Name}")).ToString();
        }
    }
}
#endif