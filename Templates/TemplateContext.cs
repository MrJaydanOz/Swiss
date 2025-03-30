#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Swiss.Editor.Templates
{
    public class TemplateContext
    {
        private static readonly Regex _CHECK_ASSEMBLIES_WITH_NAME_PATTERN = new(@"^(?!Unity|System)", RegexOptions.Singleline | RegexOptions.ExplicitCapture);

        private static Dictionary<string, List<bool>> _boolValues = new();
        private static Dictionary<string, List<float>> _floatValues = new();
        private static Dictionary<string, List<int>> _intValues = new();
        private static Dictionary<string, List<string>> _stringValues = new();
        private static Dictionary<string, List<Type>> _typeValues = new();

        public static void FindAndCacheAllParameterValues()
        {
            _floatValues.Clear();
            _intValues.Clear();
            _stringValues.Clear();
            _typeValues.Clear();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (!_CHECK_ASSEMBLIES_WITH_NAME_PATTERN.IsMatch(assembly.GetName().Name))
                    continue;

                foreach (var type in assembly.GetTypes())
                {
                    static void SetValue(string name, object value)
                    {
                        name ??= "";

                        if (value is bool boolValue)
                        {
                            if (_boolValues.TryGetValue(name, out var boolValueList))
                                boolValueList.Add(boolValue);
                            else
                                _boolValues[name] = new() { boolValue };
                        }
                        else if (value is float floatValue)
                        {
                            if (_floatValues.TryGetValue(name, out var floatValueList))
                                floatValueList.Add(floatValue);
                            else
                                _floatValues[name] = new() { floatValue };
                        }
                        else if (value is int intValue)
                        {
                            if (_intValues.TryGetValue(name, out var intValueList))
                                intValueList.Add(intValue);
                            else
                                _intValues[name] = new() { intValue };
                        }
                        else if (value is string stringValue)
                        {
                            if (_stringValues.TryGetValue(name, out var stringValueList))
                                stringValueList.Add(stringValue);
                            else
                                _stringValues[name] = new() { stringValue };
                        }
                        else if (value is Type typeValue)
                        {
                            if (_typeValues.TryGetValue(name, out List<Type> typeValueList))
                                typeValueList.Add(typeValue);
                            else
                                _typeValues[name] = new List<Type> { typeValue };
                        }
                        else if (value is IEnumerable<object> enumerableValue)
                        {
                            foreach (var element in enumerableValue)
                                SetValue(name, element);
                        }
                        else
                            Debug.LogError($"Members with a {nameof(TemplateParameterAttribute)} must be static and be of type bool, float, int, string or System.Type");
                    }

                    foreach (var attribute in type.GetCustomAttributes<TemplateTypeAtttribute>())
                        SetValue(attribute.parameterName, type);

                    if (!type.GetCustomAttributes<TemplateParameterContainerAttribute>().Any())
                        continue;

                    foreach (var method in type.GetMethods())
                    {
                        var attributes = method.GetCustomAttributes<TemplateParameterAttribute>();

                        if (!attributes.Any())
                            continue;

                        if (!method.IsStatic
                            || method.GetParameters().Length != 0
                            || (method.ReturnType != typeof(bool)
                                && method.ReturnType != typeof(float)
                                && method.ReturnType != typeof(int)
                                && method.ReturnType != typeof(string)
                                && method.ReturnType != typeof(Type)
                                && !typeof(IEnumerable<object>).IsAssignableFrom(method.ReturnType)))
                        {
                            Debug.LogError($"Methods with a {nameof(TemplateParameterAttribute)} must be static, have no parameters and return a bool, float, int, string or System.Type");

                            continue;
                        }

                        foreach (var attribute in attributes)
                            SetValue(attribute.parameterName, method.Invoke(null, null));
                    }

                    foreach (var property in type.GetProperties())
                    {
                        var attributes = property.GetCustomAttributes<TemplateParameterAttribute>();

                        if (!attributes.Any())
                            continue;

                        var getMethod = property.GetMethod;

                        if (getMethod == null
                            || !getMethod.IsStatic
                            || getMethod.GetParameters().Length != 0
                            || (getMethod.ReturnType != typeof(bool)
                                && getMethod.ReturnType != typeof(float)
                                && getMethod.ReturnType != typeof(int)
                                && getMethod.ReturnType != typeof(string)
                                && getMethod.ReturnType != typeof(Type)
                                && !typeof(IEnumerable<object>).IsAssignableFrom(getMethod.ReturnType)))
                        {
                            Debug.LogError($"Properties with a {nameof(TemplateParameterAttribute)} must have a static get and be of type bool, float, int, string or System.Type");

                            continue;
                        }

                        foreach (var attribute in attributes)
                            SetValue(attribute.parameterName, getMethod.Invoke(null, null));
                    }

                    foreach (var field in type.GetFields())
                    {
                        var attributes = field.GetCustomAttributes<TemplateParameterAttribute>();

                        if (!attributes.Any())
                            continue;

                        if (!field.IsStatic
                            || (field.FieldType != typeof(bool)
                                && field.FieldType != typeof(float)
                                && field.FieldType != typeof(int)
                                && field.FieldType != typeof(string)
                                && field.FieldType != typeof(Type)
                                && !typeof(IEnumerable<object>).IsAssignableFrom(field.FieldType)))
                        {
                            Debug.LogError($"Fields with a {nameof(TemplateParameterAttribute)} must be static and of type bool, float, int, string or System.Type");

                            continue;
                        }

                        foreach (var attribute in attributes)
                            SetValue(attribute.parameterName, field.GetValue(null));
                    }
                }
            }
        }

        public static bool GetBoolParameter(string parameterName)
        {
            if (!_boolValues.TryGetValue(parameterName ?? "", out var list) || list.Count <= 0)
            {
                Debug.LogError($"There is no template parameter of type bool and name '{parameterName}'");
                return default;
            }

            return list[0];
        }
        public static float GetFloatParameter(string parameterName)
        {
            if (!_floatValues.TryGetValue(parameterName ?? "", out var list) || list.Count <= 0)
            {
                Debug.LogError($"There is no template parameter of type bool and name '{parameterName}'");
                return default;
            }

            return list[0];
        }
        public static int GetIntParameter(string parameterName)
        {
            if (!_intValues.TryGetValue(parameterName ?? "", out var list) || list.Count <= 0)
            {
                Debug.LogError($"There is no template parameter of type bool and name '{parameterName}'");
                return default;
            }

            return list[0];
        }
        public static string GetStringParameter(string parameterName)
        {
            if (!_stringValues.TryGetValue(parameterName ?? "", out var list) || list.Count <= 0)
            {
                Debug.LogError($"There is no template parameter of type bool and name '{parameterName}'");
                return default;
            }

            return list[0];
        }
        public static Type GetTypeParameter(string parameterName)
        {
            if (!_typeValues.TryGetValue(parameterName ?? "", out var list) || list.Count <= 0)
            {
                Debug.LogError($"There is no template parameter of type bool and name '{parameterName}'");
                return default;
            }

            return list[0];
        }
        
        public static IEnumerable<bool> GetBoolParameters(string parameterName)
        {
            if (!_boolValues.TryGetValue(parameterName ?? "", out var list) || list.Count <= 0)
                return Enumerable.Empty<bool>();

            return list;
        }
        public static IEnumerable<float> GetFloatParameters(string parameterName)
        {
            if (!_floatValues.TryGetValue(parameterName ?? "", out var list) || list.Count <= 0)
                return Enumerable.Empty<float>();

            return list;
        }
        public static IEnumerable<int> GetIntParameters(string parameterName)
        {
            if (!_intValues.TryGetValue(parameterName ?? "", out var list) || list.Count <= 0)
                return Enumerable.Empty<int>();

            return list;
        }
        public static IEnumerable<string> GetStringParameters(string parameterName)
        {
            if (!_stringValues.TryGetValue(parameterName ?? "", out var list) || list.Count <= 0)
                return Enumerable.Empty<string>();

            return list;
        }
        public static IEnumerable<Type> GetTypeParameters(string parameterName)
        {
            if (!_typeValues.TryGetValue(parameterName ?? "", out var list) || list.Count <= 0)
                return Enumerable.Empty<Type>();

            return list;
        }

        public bool GetBool(string parameterName) => GetBoolParameter(parameterName);
        public float GetFloat(string parameterName) => GetFloatParameter(parameterName);
        public int GetInt(string parameterName) => GetIntParameter(parameterName);
        public string GetString(string parameterName) => GetStringParameter(parameterName);
        public Type GetType(string parameterName) => GetTypeParameter(parameterName);

        public IEnumerable<bool> GetBools(string parameterName) => GetBoolParameters(parameterName);
        public IEnumerable<float> GetFloats(string parameterName) => GetFloatParameters(parameterName);
        public IEnumerable<int> GetInts(string parameterName) => GetIntParameters(parameterName);
        public IEnumerable<string> GetStrings(string parameterName) => GetStringParameters(parameterName);
        public IEnumerable<Type> GetTypes(string parameterName = "") => GetTypeParameters(parameterName);
    }
}
#endif