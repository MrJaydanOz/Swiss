using System;

namespace Swiss.Editor.Templates
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
    public class TemplateParameterAttribute : Attribute
    {
#if UNITY_EDITOR
        public readonly string parameterName;
#endif

        public TemplateParameterAttribute(string parameterName = null)
        {
#if UNITY_EDITOR
            this.parameterName = parameterName;
#endif
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class TemplateParameterContainerAttribute : Attribute
    {
        public TemplateParameterContainerAttribute() { }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate)]
    public class TemplateTypeAtttribute : Attribute
    {
#if UNITY_EDITOR
        public readonly string parameterName;
#endif

        public TemplateTypeAtttribute(string parameterName = null)
        {
#if UNITY_EDITOR
            this.parameterName = parameterName;
#endif
        }
    }
}