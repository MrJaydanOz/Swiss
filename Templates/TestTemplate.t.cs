#if UNITY_EDITOR
using UnityEngine;

namespace Swiss.Editor.Templates
{
    internal static class _Template_Test
    {
        public static string Generate(TemplateContext context)
        {
            var a = new A();

            return "class A {}";
        }
    }
}
#endif