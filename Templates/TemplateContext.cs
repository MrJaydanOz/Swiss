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

        private Dictionary<string, List<bool>> _boolValues;
        private Dictionary<string, List<float>> _floatValues;
        private Dictionary<string, List<int>> _intValues;
        private Dictionary<string, List<string>> _stringValues;
        private Dictionary<string, List<TemplateType>> _typeValues;

        private TemplateContext() { }

        public static TemplateContext CreateInstance()
        {
            var boolValues = new Dictionary<string, List<bool>>();
            var floatValues = new Dictionary<string, List<float>>();
            var intValues = new Dictionary<string, List<int>>();
            var stringValues = new Dictionary<string, List<string>>();
            var typeValues = new Dictionary<string, List<TemplateType>>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (!_CHECK_ASSEMBLIES_WITH_NAME_PATTERN.IsMatch(assembly.GetName().Name))
                    continue;

                foreach (var type in assembly.GetTypes())
                {
                    void SetValue(string name, object value)
                    {
                        name ??= "";

                        if (value is bool boolValue)
                        {
                            if (boolValues.TryGetValue(name, out var boolValueList))
                                boolValueList.Add(boolValue);
                            else
                                boolValues[name] = new() { boolValue };
                        }
                        else if (value is float floatValue)
                        {
                            if (floatValues.TryGetValue(name, out var floatValueList))
                                floatValueList.Add(floatValue);
                            else
                                floatValues[name] = new() { floatValue };
                        }
                        else if (value is int intValue)
                        {
                            if (intValues.TryGetValue(name, out var intValueList))
                                intValueList.Add(intValue);
                            else
                                intValues[name] = new() { intValue };
                        }
                        else if (value is string stringValue)
                        {
                            if (stringValues.TryGetValue(name, out var stringValueList))
                                stringValueList.Add(stringValue);
                            else
                                stringValues[name] = new() { stringValue };
                        }
                        else if (value is Type typeValue)
                        {
                            if (typeValues.TryGetValue(name, out var typeValueList))
                                typeValueList.Add(typeValue);
                            else
                                typeValues[name] = new() { typeValue };
                        }
                        else if (value is TemplateType templateTypeValue)
                        {
                            if (typeValues.TryGetValue(name, out var templateTypeValueList))
                                templateTypeValueList.Add(templateTypeValue);
                            else
                                typeValues[name] = new() { templateTypeValue };
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
                                && method.ReturnType != typeof(TemplateType)
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
                                && getMethod.ReturnType != typeof(TemplateType)
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
                                && field.FieldType != typeof(TemplateType)
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

            return new TemplateContext()
            {
                _boolValues = boolValues,
                _floatValues = floatValues,
                _intValues = intValues,
                _stringValues = stringValues,
                _typeValues = typeValues,
            };
        }

        public bool GetBool(string parameterName = null)
        {
            if (!_boolValues.TryGetValue(parameterName ?? "", out var list) || list.Count <= 0)
            {
                Debug.LogError($"There is no template parameter of type bool and name '{parameterName}'");
                return default;
            }

            return list[0];
        }
        public float GetFloat(string parameterName = null)
        {
            if (!_floatValues.TryGetValue(parameterName ?? "", out var list) || list.Count <= 0)
            {
                Debug.LogError($"There is no template parameter of type bool and name '{parameterName}'");
                return default;
            }

            return list[0];
        }
        public int GetInt(string parameterName = null)
        {
            if (!_intValues.TryGetValue(parameterName ?? "", out var list) || list.Count <= 0)
            {
                Debug.LogError($"There is no template parameter of type bool and name '{parameterName}'");
                return default;
            }

            return list[0];
        }
        public string GetString(string parameterName = null)
        {
            if (!_stringValues.TryGetValue(parameterName ?? "", out var list) || list.Count <= 0)
            {
                Debug.LogError($"There is no template parameter of type bool and name '{parameterName}'");
                return default;
            }

            return list[0];
        }
        public TemplateType GetType(string parameterName = null)
        {
            if (!_typeValues.TryGetValue(parameterName ?? "", out var list) || list.Count <= 0)
            {
                Debug.LogError($"There is no template parameter of type bool and name '{parameterName}'");
                return default;
            }

            return list[0];
        }
        
        public IEnumerable<bool> GetBools(string parameterName = null)
        {
            if (!_boolValues.TryGetValue(parameterName ?? "", out var list) || list.Count <= 0)
                return Enumerable.Empty<bool>();

            return list;
        }
        public IEnumerable<float> GetFloats(string parameterName = null)
        {
            if (!_floatValues.TryGetValue(parameterName ?? "", out var list) || list.Count <= 0)
                return Enumerable.Empty<float>();

            return list;
        }
        public IEnumerable<int> GetInts(string parameterName = null)
        {
            if (!_intValues.TryGetValue(parameterName ?? "", out var list) || list.Count <= 0)
                return Enumerable.Empty<int>();

            return list;
        }
        public IEnumerable<string> GetStrings(string parameterName = null)
        {
            if (!_stringValues.TryGetValue(parameterName ?? "", out var list) || list.Count <= 0)
                return Enumerable.Empty<string>();

            return list;
        }
        public IEnumerable<TemplateType> GetTypes(string parameterName = null)
        {
            if (!_typeValues.TryGetValue(parameterName ?? "", out var list) || list.Count <= 0)
                return Enumerable.Empty<TemplateType>();

            return list;
        }

        public static bool OperatorExists(Type returnType, OperatorType operatorType, Type[] parameters, bool explicitCastResult = false) =>
            parameters.Length == 1 ? OperatorExists(returnType, operatorType, parameters[0], explicitCastResult) :
            parameters.Length == 2 ? OperatorExists(returnType, operatorType, parameters[0], parameters[1], explicitCastResult) :
            false;
        public static bool OperatorExists(Type returnType, OperatorType operatorType, Type leftParameter, Type rightParameter, bool explicitCastResult = false)
        {
            bool _Is(Type returnType, Type targetReturnType, Type leftParameter, Type targetLeftParameter, Type rightParameter, Type targetRightParameter, bool primativeMode)
            {
                if (explicitCastResult ? !OperatorExists(returnType, OperatorType.Explicit, targetReturnType) : !returnType.IsAssignableFrom(targetReturnType))
                    return false;

                byte leftMatch = 0;
                byte rightMatch = 0;

                if (targetLeftParameter.IsAssignableFrom(leftParameter))
                    leftMatch = 2;
                else if (OperatorExists(targetLeftParameter, OperatorType.Implicit, leftParameter))
                    leftMatch = 1;

                if (targetRightParameter.IsAssignableFrom(rightParameter))
                    rightMatch = 2;
                else if (OperatorExists(targetRightParameter, OperatorType.Implicit, rightParameter))
                    rightMatch = 1;

                return primativeMode
                    ? (leftMatch >= 1 && rightMatch >= 1)
                    : (leftMatch >= 2 && rightMatch >= 1) || (leftMatch >= 1 && rightMatch >= 2);
            }

            static IEnumerable<Type> _BaseTypes(Type type)
            {
                while (type != null)
                {
                    yield return type;
                    type = type.BaseType;
                }
            }

            return operatorType switch
            {
                OperatorType.Addition
                or OperatorType.Subtraction
                or OperatorType.Multiply
                or OperatorType.Division
                or OperatorType.Modulus =>
                    _Is(returnType, typeof(int), leftParameter, typeof(int), rightParameter, typeof(int), primativeMode: true)
                    || _Is(returnType, typeof(int), leftParameter, typeof(int), rightParameter, typeof(int), primativeMode: true)
                    || _Is(returnType, typeof(uint), leftParameter, typeof(uint), rightParameter, typeof(uint), primativeMode: true)
                    || _Is(returnType, typeof(long), leftParameter, typeof(long), rightParameter, typeof(long), primativeMode: true)
                    || _Is(returnType, typeof(ulong), leftParameter, typeof(ulong), rightParameter, typeof(ulong), primativeMode: true)
                    || _Is(returnType, typeof(float), leftParameter, typeof(float), rightParameter, typeof(float), primativeMode: true)
                    || _Is(returnType, typeof(double), leftParameter, typeof(double), rightParameter, typeof(double), primativeMode: true)
                    || (operatorType.ToMethodName() is var operatorName
                        && Enumerable.Concat(_BaseTypes(leftParameter), _BaseTypes(rightParameter)).Distinct().SelectMany((v) =>
                            v.GetMethods(BindingFlags.Public | BindingFlags.Static)
                            .Where((v) => v.Name == operatorName)).Any((v) =>
                                v.GetParameters().Length == 2
                                && _Is(returnType, v.ReturnType, leftParameter, v.GetParameters()[0].ParameterType, rightParameter, v.GetParameters()[1].ParameterType, primativeMode: false))),

                OperatorType.ExclusiveOr
                or OperatorType.BitwiseAnd
                or OperatorType.BitwiseOr =>
                    _Is(returnType, typeof(int), leftParameter, typeof(int), rightParameter, typeof(int), primativeMode: true)
                    || _Is(returnType, typeof(uint), leftParameter, typeof(uint), rightParameter, typeof(uint), primativeMode: true)
                    || _Is(returnType, typeof(long), leftParameter, typeof(long), rightParameter, typeof(long), primativeMode: true)
                    || _Is(returnType, typeof(ulong), leftParameter, typeof(ulong), rightParameter, typeof(ulong), primativeMode: true)
                    || (operatorType.ToMethodName() is var operatorName
                        && Enumerable.Concat(_BaseTypes(leftParameter), _BaseTypes(rightParameter)).Distinct().SelectMany((v) =>
                            v.GetMethods(BindingFlags.Public | BindingFlags.Static)
                            .Where((v) => v.Name == operatorName)).Any((v) =>
                                v.GetParameters().Length == 2
                                && _Is(returnType, v.ReturnType, leftParameter, v.GetParameters()[0].ParameterType, rightParameter, v.GetParameters()[1].ParameterType, primativeMode: false))),

                OperatorType.LogicalAnd
                or OperatorType.LogicalOr =>
                    _Is(returnType, typeof(bool), leftParameter, typeof(bool), rightParameter, typeof(bool), primativeMode: true)
                    || (operatorType.ToMethodName() is var operatorName
                        && Enumerable.Concat(_BaseTypes(leftParameter), _BaseTypes(rightParameter)).Distinct().SelectMany((v) =>
                            v.GetMethods(BindingFlags.Public | BindingFlags.Static)
                            .Where((v) => v.Name == operatorName)).Any((v) =>
                                v.GetParameters().Length == 2
                                && _Is(returnType, v.ReturnType, leftParameter, v.GetParameters()[0].ParameterType, rightParameter, v.GetParameters()[1].ParameterType, primativeMode: false))),
                OperatorType.Assign => OperatorExists(leftParameter, OperatorType.Implicit, rightParameter),

                OperatorType.LeftShift
                or OperatorType.RightShift =>
                    _Is(returnType, typeof(int), leftParameter, typeof(int), rightParameter, typeof(int), primativeMode: true)
                    || _Is(returnType, typeof(uint), leftParameter, typeof(uint), rightParameter, typeof(int), primativeMode: true)
                    || _Is(returnType, typeof(long), leftParameter, typeof(long), rightParameter, typeof(int), primativeMode: true)
                    || _Is(returnType, typeof(ulong), leftParameter, typeof(ulong), rightParameter, typeof(int), primativeMode: true)
                    || (operatorType.ToMethodName() is var operatorName
                        && Enumerable.Concat(_BaseTypes(leftParameter), _BaseTypes(rightParameter)).Distinct().SelectMany((v) =>
                            v.GetMethods(BindingFlags.Public | BindingFlags.Static)
                            .Where((v) => v.Name == operatorName)).Any((v) =>
                                v.GetParameters().Length == 2
                                && _Is(returnType, v.ReturnType, leftParameter, v.GetParameters()[0].ParameterType, rightParameter, v.GetParameters()[1].ParameterType, primativeMode: false))),

                OperatorType.Equality
                or OperatorType.Inequality
                or OperatorType.GreaterThan
                or OperatorType.LessThan
                or OperatorType.GreaterThanOrEqual
                or OperatorType.LessThanOrEqual =>
                    _Is(returnType, typeof(bool), leftParameter, typeof(bool), rightParameter, typeof(bool), primativeMode: true)
                    || _Is(returnType, typeof(bool), leftParameter, typeof(int), rightParameter, typeof(int), primativeMode: true)
                    || _Is(returnType, typeof(bool), leftParameter, typeof(uint), rightParameter, typeof(uint), primativeMode: true)
                    || _Is(returnType, typeof(bool), leftParameter, typeof(long), rightParameter, typeof(long), primativeMode: true)
                    || _Is(returnType, typeof(bool), leftParameter, typeof(ulong), rightParameter, typeof(ulong), primativeMode: true)
                    || _Is(returnType, typeof(bool), leftParameter, typeof(float), rightParameter, typeof(float), primativeMode: true)
                    || _Is(returnType, typeof(bool), leftParameter, typeof(double), rightParameter, typeof(double), primativeMode: true)
                    || (operatorType.ToMethodName() is var operatorName
                        && Enumerable.Concat(_BaseTypes(leftParameter), _BaseTypes(rightParameter)).Distinct().SelectMany((v) =>
                            v.GetMethods(BindingFlags.Public | BindingFlags.Static)
                            .Where((v) => v.Name == operatorName)).Any((v) =>
                                v.GetParameters().Length == 2
                                && _Is(returnType, v.ReturnType, leftParameter, v.GetParameters()[0].ParameterType, rightParameter, v.GetParameters()[1].ParameterType, primativeMode: false))),

                OperatorType.AdditionAssignment => OperatorExists(returnType, OperatorType.Implicit, leftParameter) && OperatorExists(leftParameter, OperatorType.Addition, rightParameter),
                OperatorType.SubtractionAssignment => OperatorExists(returnType, OperatorType.Implicit, leftParameter) && OperatorExists(leftParameter, OperatorType.Subtraction, rightParameter),
                OperatorType.MultiplicationAssignment => OperatorExists(returnType, OperatorType.Implicit, leftParameter) && OperatorExists(leftParameter, OperatorType.Multiply, rightParameter),
                OperatorType.DivisionAssignment => OperatorExists(returnType, OperatorType.Implicit, leftParameter) && OperatorExists(leftParameter, OperatorType.Division, rightParameter),
                OperatorType.ModulusAssignment => OperatorExists(returnType, OperatorType.Implicit, leftParameter) && OperatorExists(leftParameter, OperatorType.Modulus, rightParameter),
                OperatorType.ExclusiveOrAssignment => OperatorExists(returnType, OperatorType.Implicit, leftParameter) && OperatorExists(leftParameter, OperatorType.ExclusiveOr, rightParameter),
                OperatorType.BitwiseAndAssignment => OperatorExists(returnType, OperatorType.Implicit, leftParameter) && OperatorExists(leftParameter, OperatorType.BitwiseAnd, rightParameter),
                OperatorType.BitwiseOrAssignment => OperatorExists(returnType, OperatorType.Implicit, leftParameter) && OperatorExists(leftParameter, OperatorType.BitwiseOr, rightParameter),
                OperatorType.LeftShiftAssignment => OperatorExists(returnType, OperatorType.Implicit, leftParameter) && OperatorExists(leftParameter, OperatorType.LeftShift, rightParameter),

                OperatorType.Comma =>
                    operatorType.ToMethodName() is var operatorName
                        && Enumerable.Concat(_BaseTypes(leftParameter), _BaseTypes(rightParameter)).Distinct().SelectMany((v) =>
                            v.GetMethods(BindingFlags.Public | BindingFlags.Static)
                            .Where((v) => v.Name == operatorName)).Any((v) =>
                                v.GetParameters().Length == 2
                                && _Is(returnType, v.ReturnType, leftParameter, v.GetParameters()[0].ParameterType, rightParameter, v.GetParameters()[1].ParameterType, false)),

                _ => false,
            };
        }
        public static bool OperatorExists(Type returnType, OperatorType operatorType, Type parameter, bool explicitCastResult = false)
        {
            bool _Is(Type returnType, Type targetReturnType, Type parameter, Type targetParameter)
            {
                if (explicitCastResult ? !OperatorExists(returnType, OperatorType.Explicit, targetReturnType) : !returnType.IsAssignableFrom(targetReturnType))
                    return false;

                return targetParameter.IsAssignableFrom(parameter);
            }

            static IEnumerable<Type> _BaseTypes(Type type)
            {
                while (type != null)
                {
                    yield return type;
                    type = type.BaseType;
                }
            }

            return operatorType switch
            {
                OperatorType.Implicit =>
                    returnType.IsAssignableFrom(parameter)
                    || (explicitCastResult ? OperatorExists(returnType, OperatorType.Explicit, parameter)
                        : (returnType == typeof(bool)
                            && parameter == typeof(bool))
                        || (returnType == typeof(byte)
                            && (parameter == typeof(byte)
                                || parameter == typeof(int)))
                        || (returnType == typeof(sbyte)
                            && (parameter == typeof(sbyte)
                                || parameter == typeof(int)))
                        || (returnType == typeof(char)
                            && (parameter == typeof(char)))
                        || (returnType == typeof(decimal)
                            && (parameter == typeof(decimal)
                                || parameter == typeof(byte)
                                || parameter == typeof(sbyte)
                                || parameter == typeof(char)
                                || parameter == typeof(int)
                                || parameter == typeof(uint)
                                || parameter == typeof(nint)
                                || parameter == typeof(nuint)
                                || parameter == typeof(long)
                                || parameter == typeof(ulong)
                                || parameter == typeof(short)
                                || parameter == typeof(ushort)))
                        || (returnType == typeof(double)
                            && (parameter == typeof(double)
                                || parameter == typeof(byte)
                                || parameter == typeof(sbyte)
                                || parameter == typeof(char)
                                || parameter == typeof(float)
                                || parameter == typeof(int)
                                || parameter == typeof(uint)
                                || parameter == typeof(nint)
                                || parameter == typeof(nuint)
                                || parameter == typeof(long)
                                || parameter == typeof(ulong)
                                || parameter == typeof(short)
                                || parameter == typeof(ushort)))
                        || (returnType == typeof(float)
                            && (parameter == typeof(float)
                                || parameter == typeof(byte)
                                || parameter == typeof(sbyte)
                                || parameter == typeof(char)
                                || parameter == typeof(int)
                                || parameter == typeof(uint)
                                || parameter == typeof(nint)
                                || parameter == typeof(nuint)
                                || parameter == typeof(long)
                                || parameter == typeof(ulong)
                                || parameter == typeof(short)
                                || parameter == typeof(ushort)))
                        || (returnType == typeof(int)
                            && (parameter == typeof(int)
                                || parameter == typeof(byte)
                                || parameter == typeof(sbyte)
                                || parameter == typeof(char)
                                || parameter == typeof(short)
                                || parameter == typeof(ushort)))
                        || (returnType == typeof(uint)
                            && (parameter == typeof(uint)
                                || parameter == typeof(byte)
                                || parameter == typeof(char)
                                || parameter == typeof(int)
                                || parameter == typeof(ushort)))
                        || (returnType == typeof(nint)
                            && (parameter == typeof(nint)
                                || parameter == typeof(byte)
                                || parameter == typeof(sbyte)
                                || parameter == typeof(char)
                                || parameter == typeof(int)
                                || parameter == typeof(short)
                                || parameter == typeof(ushort)))
                        || (returnType == typeof(nuint)
                            && (parameter == typeof(nuint)
                                || parameter == typeof(byte)
                                || parameter == typeof(char)
                                || parameter == typeof(int)
                                || parameter == typeof(uint)
                                || parameter == typeof(ushort)))
                        || (returnType == typeof(long)
                            && (parameter == typeof(long)
                                || parameter == typeof(byte)
                                || parameter == typeof(sbyte)
                                || parameter == typeof(char)
                                || parameter == typeof(int)
                                || parameter == typeof(uint)
                                || parameter == typeof(nint)
                                || parameter == typeof(short)
                                || parameter == typeof(ushort)))
                        || (returnType == typeof(ulong)
                            && (parameter == typeof(ulong)
                                || parameter == typeof(byte)
                                || parameter == typeof(char)
                                || parameter == typeof(int)
                                || parameter == typeof(uint)
                                || parameter == typeof(nuint)
                                || parameter == typeof(long)
                                || parameter == typeof(ushort)))
                        || (returnType == typeof(short)
                            && (parameter == typeof(short)
                                || parameter == typeof(byte)
                                || parameter == typeof(sbyte)
                                || parameter == typeof(int)))
                        || (returnType == typeof(ushort)
                            && (parameter == typeof(ushort)
                                || parameter == typeof(byte)
                                || parameter == typeof(char)
                                || parameter == typeof(int))))
                    || (operatorType.ToMethodName() is var operatorName
                        && _BaseTypes(parameter).SelectMany((v) => v.GetMethods(BindingFlags.Public | BindingFlags.Static).Where((v) => v.Name == operatorName)).Any((v) =>
                            v.GetParameters().Length == 1
                            && _Is(returnType, v.ReturnType, parameter, v.GetParameters()[0].ParameterType))),

                OperatorType.Explicit =>
                    returnType.IsAssignableFrom(parameter)
                    || (returnType == typeof(bool)
                        && parameter == typeof(bool))
                    || ((returnType == typeof(byte)
                            || returnType == typeof(sbyte)
                            || returnType == typeof(char)
                            || returnType == typeof(decimal)
                            || returnType == typeof(double)
                            || returnType == typeof(float)
                            || returnType == typeof(int)
                            || returnType == typeof(uint)
                            || returnType == typeof(nint)
                            || returnType == typeof(nuint)
                            || returnType == typeof(long)
                            || returnType == typeof(ulong)
                            || returnType == typeof(short)
                            || returnType == typeof(ushort))
                        && (parameter == typeof(byte)
                            || parameter == typeof(sbyte)
                            || parameter == typeof(char)
                            || parameter == typeof(decimal)
                            || parameter == typeof(double)
                            || parameter == typeof(float)
                            || parameter == typeof(int)
                            || parameter == typeof(uint)
                            || parameter == typeof(nint)
                            || parameter == typeof(nuint)
                            || parameter == typeof(long)
                            || parameter == typeof(ulong)
                            || parameter == typeof(short)
                            || parameter == typeof(ushort)))
                    || (operatorType.ToMethodName() is var operatorName
                        &&_BaseTypes(parameter).SelectMany((v) => v.GetMethods(BindingFlags.Public | BindingFlags.Static).Where((v) => v.Name == operatorName)).Any((v) =>
                            v.GetParameters().Length == 1
                            && _Is(returnType, v.ReturnType, parameter, v.GetParameters()[0].ParameterType))),

                OperatorType.Decrement
                or OperatorType.Increment =>
                    _Is(returnType, typeof(byte), parameter, typeof(byte))
                    || _Is(returnType, typeof(sbyte), parameter, typeof(sbyte))
                    || _Is(returnType, typeof(char), parameter, typeof(char))
                    || _Is(returnType, typeof(decimal), parameter, typeof(decimal))
                    || _Is(returnType, typeof(double), parameter, typeof(double))
                    || _Is(returnType, typeof(float), parameter, typeof(float))
                    || _Is(returnType, typeof(int), parameter, typeof(int))
                    || _Is(returnType, typeof(uint), parameter, typeof(uint))
                    || _Is(returnType, typeof(nint), parameter, typeof(nint))
                    || _Is(returnType, typeof(nuint), parameter, typeof(nuint))
                    || _Is(returnType, typeof(long), parameter, typeof(long))
                    || _Is(returnType, typeof(ulong), parameter, typeof(ulong))
                    || _Is(returnType, typeof(short), parameter, typeof(short))
                    || _Is(returnType, typeof(ushort), parameter, typeof(ushort))
                    || (operatorType.ToMethodName() is var operatorName
                        && _BaseTypes(parameter).SelectMany((v) =>
                            v.GetMethods(BindingFlags.Public | BindingFlags.Static)
                            .Where((v) => v.Name == operatorName)).Any((v) =>
                                v.GetParameters().Length == 1
                                && _Is(returnType, v.ReturnType, parameter, v.GetParameters()[0].ParameterType))),

                OperatorType.UnaryNegation =>
                    _Is(returnType, typeof(int), parameter, typeof(sbyte))
                    || _Is(returnType, typeof(decimal), parameter, typeof(decimal))
                    || _Is(returnType, typeof(double), parameter, typeof(double))
                    || _Is(returnType, typeof(float), parameter, typeof(float))
                    || _Is(returnType, typeof(int), parameter, typeof(int))
                    || _Is(returnType, typeof(nint), parameter, typeof(nint))
                    || _Is(returnType, typeof(long), parameter, typeof(long))
                    || _Is(returnType, typeof(int), parameter, typeof(short))
                    || (operatorType.ToMethodName() is var operatorName
                        && _BaseTypes(parameter).SelectMany((v) =>
                            v.GetMethods(BindingFlags.Public | BindingFlags.Static)
                            .Where((v) => v.Name == operatorName)).Any((v) =>
                                v.GetParameters().Length == 1
                                && _Is(returnType, v.ReturnType, parameter, v.GetParameters()[0].ParameterType))),

                OperatorType.UnaryPlus =>
                    _Is(returnType, typeof(int), parameter, typeof(byte))
                    || _Is(returnType, typeof(int), parameter, typeof(sbyte))
                    || _Is(returnType, typeof(int), parameter, typeof(char))
                    || _Is(returnType, typeof(decimal), parameter, typeof(decimal))
                    || _Is(returnType, typeof(double), parameter, typeof(double))
                    || _Is(returnType, typeof(float), parameter, typeof(float))
                    || _Is(returnType, typeof(int), parameter, typeof(int))
                    || _Is(returnType, typeof(uint), parameter, typeof(uint))
                    || _Is(returnType, typeof(nint), parameter, typeof(nint))
                    || _Is(returnType, typeof(nuint), parameter, typeof(nuint))
                    || _Is(returnType, typeof(long), parameter, typeof(long))
                    || _Is(returnType, typeof(ulong), parameter, typeof(ulong))
                    || _Is(returnType, typeof(int), parameter, typeof(short))
                    || _Is(returnType, typeof(int), parameter, typeof(ushort))
                    || (operatorType.ToMethodName() is var operatorName
                        && _BaseTypes(parameter).SelectMany((v) =>
                            v.GetMethods(BindingFlags.Public | BindingFlags.Static)
                            .Where((v) => v.Name == operatorName)).Any((v) =>
                                v.GetParameters().Length == 1
                                && _Is(returnType, v.ReturnType, parameter, v.GetParameters()[0].ParameterType))),

                OperatorType.OnesComplement =>
                    _Is(returnType, typeof(int), parameter, typeof(byte))
                    || _Is(returnType, typeof(int), parameter, typeof(sbyte))
                    || _Is(returnType, typeof(int), parameter, typeof(char))
                    || _Is(returnType, typeof(int), parameter, typeof(int))
                    || _Is(returnType, typeof(uint), parameter, typeof(uint))
                    || _Is(returnType, typeof(nint), parameter, typeof(nint))
                    || _Is(returnType, typeof(nuint), parameter, typeof(nuint))
                    || _Is(returnType, typeof(long), parameter, typeof(long))
                    || _Is(returnType, typeof(ulong), parameter, typeof(ulong))
                    || _Is(returnType, typeof(int), parameter, typeof(short))
                    || _Is(returnType, typeof(int), parameter, typeof(ushort))
                    || (operatorType.ToMethodName() is var operatorName
                        && _BaseTypes(parameter).SelectMany((v) =>
                            v.GetMethods(BindingFlags.Public | BindingFlags.Static)
                            .Where((v) => v.Name == operatorName)).Any((v) =>
                                v.GetParameters().Length == 1
                                && _Is(returnType, v.ReturnType, parameter, v.GetParameters()[0].ParameterType))),

                _ => false,
            };
        }
    }
}
#endif