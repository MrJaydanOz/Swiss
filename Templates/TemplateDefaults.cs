#if UNITY_EDITOR
using System;
using UnityEngine;

namespace Swiss.Editor.Templates
{
    [TemplateParameterContainer]
    public static class TemplateDefaults
    {
        [TemplateParameter]
        public static readonly Type[] defaultPrimativeParameterTypes =
        {
            typeof(bool),
            typeof(byte),
            typeof(sbyte),
            typeof(char),
            typeof(decimal),
            typeof(double),
            typeof(float),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(short),
            typeof(ushort),
        };

        [TemplateParameter]
        public static readonly Type[] defaultUnityParameterTypes =
        {
            typeof(Vector2),
            typeof(Vector3),
            typeof(Vector4),
            typeof(Vector2Int),
            typeof(Vector3Int),
        };
    }
}
#endif