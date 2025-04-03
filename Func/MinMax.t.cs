using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Swiss.Editor.Templates;
using Swiss;

namespace Swiss.Templates
{
    internal static class _Template_MinMax
    {
        private abstract class _TestingType { }

        private class _TestingTypeScaler : _TestingType
        {
            public TemplateType type;
        }

        private class _TestingTypeVector : _TestingType
        {
            public TemplateType type;
            public TemplateType elementType;
            public string[] elementNames;
        }

        public static string Generate(TemplateContext context)
        {
            var types = new List<_TestingType>();

            foreach (var type in context.GetTypes())
            {
                if (type.HasOperator(returnType: typeof(bool), OperatorType.LessThan, otherType: type))
                {
                    types.Add(new _TestingTypeScaler { type = type });
                    continue;
                }

                foreach (var elementType in context.GetTypes())
                {
                    var fields = new List<(Type type, string name)>();

                    foreach (var field in type.type.GetFields(BindingFlags.Public | BindingFlags.Instance))
                        if (!field.IsInitOnly)
                            fields.Add((field.FieldType, field.Name));

                    foreach (var property in type.type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                        if (property.CanRead && property.CanWrite)
                            ;//fields.Add((property.PropertyType, property.Name));

                    if (fields.Count > 0 && fields.All((v) => v.type == elementType))
                    {
                        types.Add(new _TestingTypeVector { type = type, elementType = elementType, elementNames = fields.Select((v) => v.name).ToArray() });
                        continue;
                    }
                }
            }

            context.GetTypes()
                .Where((v) => v.HasOperator(returnType: typeof(bool), OperatorType.LessThan, otherType: v))
                .ToList();

            return "using "+nameof(System)+";"
                + "\nusing "+nameof(System)+"."+nameof(System.Collections)+"."+nameof(System.Collections.Generic)+";"
                + "\nusing "+nameof(System)+"."+nameof(System.Linq)+";"
                + "\n"
                + "\nnamespace " + nameof(Swiss)
                + "\n{"
                + "\n    public static partial class Func"
                + "\n    {"
                + types.SelectMany((type) => _GenerateInstance(type, false)).JoinToString()
                + "\n"
                + types.SelectMany((type) => _GenerateInstance(type, true)).JoinToString()
                + "\n    }"
                + "\n}";
        }

        private static string[] _GenerateInstance(_TestingType testingType, bool isMax)
        {
            var methodName = isMax ? "Max" : "Min";

            if (testingType is _TestingTypeScaler testingTypeScaler)
            {
                var type = testingTypeScaler.type;

                return new string[] {""
                    + "\n        /// <inheritdoc cref=\"",methodName,"{T}(T, T)\"/>"
                    + "\n        public static ",type.FullName," ",methodName,"(",type.FullName," a, ",type.FullName," b) => ",isMax ? "a < b ? b : a" : "a < b ? a : b",";"
                    + "\n        /// <inheritdoc cref=\"",methodName,"{T}(T, T, T)\"/>"
                    + "\n        public static ",type.FullName," ",methodName,"(",type.FullName," a, ",type.FullName," b, ",type.FullName," c) => ",isMax ? "a < b ? (b < c ? c : b) : (a < c ? c : a)" : "a < b ? (a < c ? a : c) : (b < c ? b : c)",";"
                    + (typeof(Enumerable).GetMethods(BindingFlags.Public | BindingFlags.Static).Any((method) =>
                        {
                            if (method.Name != methodName)
                                return false;

                            var methodParameters = method.GetParameters();

                            if (methodParameters.Length != 1)
                                return false;

                            {
                                var parameterType = methodParameters[0].ParameterType;

                                if (parameterType.GetGenericTypeDefinition() != typeof(IEnumerable<>)
                                    || parameterType.GetGenericArguments()[0] != type)
                                    return false;
                            }

                            return true;
                        })
                        ? null : new string[] {""
                        + "\n        /// <inheritdoc cref=\""+nameof(Enumerable)+".",methodName,"("+nameof(IEnumerable<int>)+"{",type.FullName,"})\"/>"
                        + "\n        public static ",type.FullName," ",methodName,"(this "+nameof(IEnumerable<int>)+"<",type.FullName,"> collection)"
                        + "\n        {"
                        + "\n            if (collection == null)"
                        + "\n                throw "+nameof(Expt)+"."+nameof(Expt.ArgumentNull)+"(nameof(collection));"
                        + "\n"
                        + "\n            var result = default(",type.FullName,");"
                        + "\n            var isFirst = true;"
                        + "\n"
                        + "\n            foreach (var item in collection)"
                        + "\n            {"
                        + "\n                if (isFirst)"
                        + "\n                {"
                        + "\n                    result = item;"
                        + "\n                    isFirst = false;"
                        + "\n                }"
                        + "\n                else if (",isMax ? "item < result" : "result < item",")"
                        + "\n                    result = item;"
                        + "\n            }"
                        + "\n"
                        + "\n            if (isFirst)"
                        + "\n                throw "+nameof(Expt)+"."+nameof(Expt.InvalidOperation)+"(\"Collection cannot be empty.\");"
                        + "\n"
                        + "\n            return result;"
                        + "\n        }"
                        }).JoinToString()
                    + (typeof(Enumerable).GetMethods(BindingFlags.Public | BindingFlags.Static).Any((method) =>
                        {
                            if (method.Name != methodName)
                                return false;

                            var methodGenericArguments = method.GetGenericArguments();

                            if (methodGenericArguments.Length != 1)
                                return false;

                            var methodParameters = method.GetParameters();

                            if (methodParameters.Length != 2)
                                return false;

                            {
                                var parameterType = methodParameters[0].ParameterType;

                                if (parameterType.GetGenericTypeDefinition() != typeof(IEnumerable<>)
                                    || parameterType.GetGenericArguments()[0] != methodGenericArguments[0])
                                    return false;
                            }

                            {
                                var parameterType = methodParameters[1].ParameterType;

                                if (parameterType.GetGenericTypeDefinition() != typeof(Func<,>))
                                    return false;

                                var parameterGenericArguments = parameterType.GetGenericArguments();

                                if (parameterGenericArguments[0] != methodGenericArguments[0]
                                    || parameterGenericArguments[1] != type)
                                    return false;
                            }

                            return true;
                        })
                        ? null : new string[] {""
                        + "\n        /// <inheritdoc cref=\""+nameof(Enumerable)+".",methodName,"("+nameof(IEnumerable<int>)+"{",type.FullName,"})\"/>"
                        + "\n        public static ",type.FullName," ",methodName,"<T>(this "+nameof(IEnumerable<int>)+"<T> collection, "+nameof(Func<int, int>)+"<T, ",type.FullName,"> selector)"
                        + "\n        {"
                        + "\n            if (collection == null)"
                        + "\n                throw "+nameof(Expt)+"."+nameof(Expt.ArgumentNull)+"(nameof(collection));"
                        + "\n"
                        + "\n            var result = default(",type.FullName,");"
                        + "\n            var isFirst = true;"
                        + "\n"
                        + "\n            foreach (var item in collection)"
                        + "\n            {"
                        + "\n                var selectedItem = selector.Invoke(item);"
                        + "\n"
                        + "\n                if (isFirst)"
                        + "\n                {"
                        + "\n                    result = selectedItem;"
                        + "\n                    isFirst = false;"
                        + "\n                }"
                        + "\n                else if (",isMax ? "selectedItem < result" : "result < selectedItem",")"
                        + "\n                    result = selectedItem;"
                        + "\n            }"
                        + "\n"
                        + "\n            if (isFirst)"
                        + "\n                throw "+nameof(Expt)+"."+nameof(Expt.InvalidOperation)+"(\"Collection cannot be empty.\");"
                        + "\n"
                        + "\n            return result;"
                        + "\n        }"
                        }).JoinToString()
                    + "\n        /// <inheritdoc cref=\"",methodName,"{T}("+nameof(IEnumerable<int>)+"{T}, "+nameof(Func<int, int>)+"{T, ",type.FullName,"})\"/>"
                    + "\n        public static T Find",methodName,"<T>(this "+nameof(IEnumerable<int>)+"<T> collection, "+nameof(Func<int, int>)+"<T, ",type.FullName,"> selector)"
                    + "\n        {"
                    + "\n            if (collection == null)"
                    + "\n                throw "+nameof(Expt)+"."+nameof(Expt.ArgumentNull)+"(nameof(collection));"
                    + "\n"
                    + "\n            if (selector == null)"
                    + "\n                throw "+nameof(Expt)+"."+nameof(Expt.ArgumentNull)+"(nameof(selector));"
                    + "\n"
                    + "\n            var result = default(T);"
                    + "\n            var resultComparer = default(",type.FullName,");"
                    + "\n            var isFirst = true;"
                    + "\n"
                    + "\n            foreach (var item in collection)"
                    + "\n            {"
                    + "\n                var itemComparer = selector.Invoke(item);"
                    + "\n"
                    + "\n                if (isFirst)"
                    + "\n                {"
                    + "\n                    result = item;"
                    + "\n                    resultComparer = itemComparer;"
                    + "\n                    isFirst = false;"
                    + "\n                }"
                    + "\n                else if (",isMax ? "itemComparer < resultComparer" : "resultComparer < itemComparer",")"
                    + "\n                {"
                    + "\n                    result = item;"
                    + "\n                    resultComparer = itemComparer;"
                    + "\n                }"
                    + "\n            }"
                    + "\n"
                    + "\n            if (isFirst)"
                    + "\n                throw "+nameof(Expt)+"."+nameof(Expt.InvalidOperation)+"(\"Collection cannot be empty.\");"
                    + "\n"
                    + "\n            return result;"
                    + "\n        }"
                };
            }
            else
                throw Expt.InvalidOperation();
        }
    }
}

namespace Swiss
{
    public static partial class Func
    {
        /// <summary> Returns the smallest of the given values. </summary><remarks> See <seealso href="https://www.desmos.com/calculator/khtnohugnx">example on Desmos</seealso>. </remarks>
        public static T Min<T>(T a, T b) where T : IComparable<T> => a.CompareTo(b) < 0 ? a : b;
        /// <inheritdoc cref="Min{T}(T, T)"/>
        public static T Min<T>(T a, T b, T c) where T : IComparable<T> => a.CompareTo(b) < 0 ? (a.CompareTo(c) < 0 ? a : c) : (b.CompareTo(c) < 0 ? b : c);

        /// <summary> Returns the smallest of the given values according to the given <paramref name="comparison"/>. </summary><remarks> See <seealso href="https://www.desmos.com/calculator/khtnohugnx">example on Desmos</seealso>. </remarks>
        public static T Min<T>(T a, T b, IComparer<T> comparer) =>
            comparer == null ? throw Expt.ArgumentNull(nameof(comparer)) :
            Min(a, b, comparer.Compare);
        /// <inheritdoc cref="Min{T}(T, T, Comparison{T})"/>
        public static T Min<T>(T a, T b, T c, IComparer<T> comparer) =>
            comparer == null ? throw Expt.ArgumentNull(nameof(comparer)) :
            Min(a, b, c, comparer.Compare);

        /// <summary> Returns the smallest of the given values according to the given <paramref name="comparison"/>. </summary><remarks> See <seealso href="https://www.desmos.com/calculator/khtnohugnx">example on Desmos</seealso>. </remarks>
        public static T Min<T>(T a, T b, Comparison<T> comparison) =>
            comparison == null ? throw Expt.ArgumentNull(nameof(comparison)) :
            comparison.Invoke(a, b) < 0 ? a : b;
        /// <inheritdoc cref="Min{T}(T, T, Comparison{T})"/>
        public static T Min<T>(T a, T b, T c, Comparison<T> comparison) =>
            comparison == null ? throw Expt.ArgumentNull(nameof(comparison)) :
            comparison.Invoke(a, b) < 0 ? (comparison.Invoke(a, c) < 0 ? a : c) : (comparison.Invoke(b, c) < 0 ? b : c);

        /// <summary> Returns the smallest of the given values after they have been mapped via the given <paramref name="selector"/>. </summary><remarks> See <seealso href="https://www.desmos.com/calculator/khtnohugnx">example on Desmos</seealso>. </remarks>
        public static TResult Min<TSource, TResult>(TSource a, TSource b, Func<TSource, TResult> selector) where TResult : IComparable<TResult> =>
            selector == null ? throw Expt.ArgumentNull(nameof(selector)) :
            Min(selector.Invoke(a), selector.Invoke(b));
        /// <inheritdoc cref="Min{TSource, TResult}(TSource, TSource, Func{TSource, TResult})"/>
        public static TResult Min<TSource, TResult>(TSource a, TSource b, TSource c, Func<TSource, TResult> selector) where TResult : IComparable<TResult> =>
            selector == null ? throw Expt.ArgumentNull(nameof(selector)) :
            Min(selector.Invoke(a), selector.Invoke(b), selector.Invoke(c));

        /// <summary> Returns the smallest of the given values where they are compared via the given <paramref name="selector"/>. </summary><remarks> See <seealso href="https://www.desmos.com/calculator/khtnohugnx">example on Desmos</seealso>. </remarks>
        public static TSource FindMin<TSource, TComparer>(TSource a, TSource b, Func<TSource, TComparer> selector) where TComparer : IComparable<TComparer> =>
            selector == null ? throw Expt.ArgumentNull(nameof(selector)) :
            selector.Invoke(a).CompareTo(selector.Invoke(b)) < 0 ? a : b;
        /// <inheritdoc cref="FindMin{TSource, TResult}(TSource, TSource, Func{TSource, TResult})"/>
        public static TSource FindMin<TSource, TComparer>(TSource a, TSource b, TSource c, Func<TSource, TComparer> selector) where TComparer : IComparable<TComparer> =>
            selector == null ? throw Expt.ArgumentNull(nameof(selector)) :
            selector.Invoke(a).CompareTo(selector.Invoke(b)) < 0 ? (selector.Invoke(a).CompareTo(selector.Invoke(c)) < 0 ? a : c) : (selector.Invoke(b).CompareTo(selector.Invoke(c)) < 0 ? b : c);
        /// <inheritdoc cref="FindMin{TSource, TResult}(TSource, TSource, Func{TSource, TResult})"/>
        public static TSource FindMin<TSource, TComparer>(this IEnumerable<TSource> collection, Func<TSource, TComparer> selector) where TComparer : IComparable<TComparer>
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            if (selector == null)
                throw Expt.ArgumentNull(nameof(selector));

            var result = default(TSource);
            var resultComparer = default(TComparer);
            var isFirst = true;

            foreach (var item in collection)
            {
                var itemComparer = selector.Invoke(item);

                if (isFirst)
                {
                    result = item;
                    resultComparer = itemComparer;
                    isFirst = false;
                }
                else if (resultComparer.CompareTo(itemComparer) < 0)
                {
                    result = item;
                    resultComparer = itemComparer;
                }
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }

        /// <summary> Returns the largest of the given values. </summary><remarks> See <seealso href="https://www.desmos.com/calculator/rkba3ahaur">example on Desmos</seealso>. </remarks>
        public static T Max<T>(T a, T b) where T : IComparable<T> => a.CompareTo(b) < 0 ? b : a;
        /// <inheritdoc cref="Max{T}(T, T)"/>
        public static T Max<T>(T a, T b, T c) where T : IComparable<T> => a.CompareTo(b) < 0 ? (b.CompareTo(c) < 0 ? c : b) : (a.CompareTo(c) < 0 ? c : a);

        /// <summary> Returns the largest of the given values according to the given <paramref name="comparison"/>. </summary><remarks> See <seealso href="https://www.desmos.com/calculator/rkba3ahaur">example on Desmos</seealso>. </remarks>
        public static T Max<T>(T a, T b, IComparer<T> comparer) =>
            comparer == null ? throw Expt.ArgumentNull(nameof(comparer)) :
            Max(a, b, comparer.Compare);
        /// <inheritdoc cref="Max{T}(T, T, Comparison{T})"/>
        public static T Max<T>(T a, T b, T c, IComparer<T> comparer) =>
            comparer == null ? throw Expt.ArgumentNull(nameof(comparer)) :
            Max(a, b, c, comparer.Compare);

        /// <summary> Returns the largest of the given values according to the given <paramref name="comparison"/>. </summary><remarks> See <seealso href="https://www.desmos.com/calculator/rkba3ahaur">example on Desmos</seealso>. </remarks>
        public static T Max<T>(T a, T b, Comparison<T> comparison) =>
            comparison == null ? throw Expt.ArgumentNull(nameof(comparison)) :
            comparison.Invoke(a, b) < 0 ? b : a;
        /// <inheritdoc cref="Max{T}(T, T, Comparison{T})"/>
        public static T Max<T>(T a, T b, T c, Comparison<T> comparison) =>
            comparison == null ? throw Expt.ArgumentNull(nameof(comparison)) :
            comparison.Invoke(a, b) < 0 ? (comparison.Invoke(b, c) < 0 ? c : b) : (comparison.Invoke(a, c) < 0 ? c : a);

        /// <summary> Returns the largest of the given values after they have been mapped via the given <paramref name="selector"/>. </summary><remarks> See <seealso href="https://www.desmos.com/calculator/rkba3ahaur">example on Desmos</seealso>. </remarks>
        public static TResult Max<TSource, TResult>(TSource a, TSource b, Func<TSource, TResult> selector) where TResult : IComparable<TResult> =>
            selector == null ? throw Expt.ArgumentNull(nameof(selector)) :
            Max(selector.Invoke(a), selector.Invoke(b));
        /// <inheritdoc cref="Max{TSource, TResult}(TSource, TSource, Func{TSource, TResult})"/>
        public static TResult Max<TSource, TResult>(TSource a, TSource b, TSource c, Func<TSource, TResult> selector) where TResult : IComparable<TResult> =>
            selector == null ? throw Expt.ArgumentNull(nameof(selector)) :
            Max(selector.Invoke(a), selector.Invoke(b), selector.Invoke(c));

        /// <summary> Returns the largest of the given values where they are compared via the given <paramref name="selector"/>. </summary><remarks> See <seealso href="https://www.desmos.com/calculator/rkba3ahaur">example on Desmos</seealso>. </remarks>
        public static TSource FindMax<TSource, TResult>(TSource a, TSource b, Func<TSource, TResult> selector) where TResult : IComparable<TResult> =>
            selector == null ? throw Expt.ArgumentNull(nameof(selector)) :
            selector.Invoke(a).CompareTo(selector.Invoke(b)) < 0 ? b : a;
        /// <inheritdoc cref="FindMax{TSource, TResult}(TSource, TSource, Func{TSource, TResult})"/>
        public static TSource FindMax<TSource, TResult>(TSource a, TSource b, TSource c, Func<TSource, TResult> selector) where TResult : IComparable<TResult> =>
            selector == null ? throw Expt.ArgumentNull(nameof(selector)) :
            selector.Invoke(a).CompareTo(selector.Invoke(b)) < 0 ? (selector.Invoke(b).CompareTo(selector.Invoke(c)) < 0 ? c : b) : (selector.Invoke(a).CompareTo(selector.Invoke(c)) < 0 ? c : a);
        /// <inheritdoc cref="FindMax{TSource, TResult}(TSource, TSource, Func{TSource, TResult})"/>
        public static TSource FindMax<TSource, TComparer>(this IEnumerable<TSource> collection, Func<TSource, TComparer> selector) where TComparer : IComparable<TComparer>
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            if (selector == null)
                throw Expt.ArgumentNull(nameof(selector));

            var result = default(TSource);
            var resultComparer = default(TComparer);
            var isFirst = true;

            foreach (var item in collection)
            {
                var itemComparer = selector.Invoke(item);

                if (isFirst)
                {
                    result = item;
                    resultComparer = itemComparer;
                    isFirst = false;
                }
                else if (itemComparer.CompareTo(resultComparer) < 0)
                {
                    result = item;
                    resultComparer = itemComparer;
                }
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
    }
}