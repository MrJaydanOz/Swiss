using System;

namespace Swiss
{
    public static partial class Expt
    {
        public static ArgumentException Argument(string message = "Argument must be valid.") => new(message);

        public static NullReferenceException Null<T>(string valueName = "value") => Null(valueName, typeof(T).Name);
        public static NullReferenceException Null(string valueName = "value", string typeName = null) => new(
            (valueName == null ? "Value" : valueName.Length > 0 ? char.ToUpper(valueName[0]) + valueName[1..] : valueName) +
            (typeName == null ? null : $"of type '{typeName}'") +
            " cannot be null.");

        public static ArgumentNullException ArgumentNull<T>(string valueName = "argument") => ArgumentNull(valueName, typeof(T).Name);
        public static ArgumentNullException ArgumentNull(string valueName = "argument", string typeName = null) => new(
            (valueName == null ? "Argument" : valueName.Length > 0 ? char.ToUpper(valueName[0]) + valueName[1..] : valueName) +
            (typeName == null ? null : $"of type '{typeName}'") +
            " cannot be null.");

        public static IndexOutOfRangeException OutOfRange(string valueName = "index", int? value = null, int? min = null, int? max = null) => OutOfRange(valueName, value?.ToString(), min?.ToString(), max?.ToString());
        public static IndexOutOfRangeException OutOfRange(string valueName = "index", float? value = null, float? min = null, float? max = null) => OutOfRange(valueName, value?.ToString("F2"), min?.ToString("F2"), max?.ToString("F2"));
        public static IndexOutOfRangeException OutOfRange(string valueName = "index", string value = null, string min = null, string max = null) => new(
            (valueName == null ? "Index" : valueName.Length > 0 ? char.ToUpper(valueName[0]) + valueName[1..] : valueName) +
            (value == null ? " must be " : $" (= {value}) must be") +
            (min == null ? max == null ? " in a valid range." : $" less than or equal to {max}." : max == null ? $" more than {min}." : $" in the range {min}..{max}."));

        public static ArgumentOutOfRangeException ArgumentOutOfRange(string valueName = "argument", int? value = null, int? min = null, int? max = null) => ArgumentOutOfRange(valueName, value?.ToString(), min?.ToString(), max?.ToString());
        public static ArgumentOutOfRangeException ArgumentOutOfRange(string valueName = "argument", float? value = null, float? min = null, float? max = null) => ArgumentOutOfRange(valueName, value?.ToString("F2"), min?.ToString("F2"), max?.ToString("F2"));
        public static ArgumentOutOfRangeException ArgumentOutOfRange(string valueName = "argument", string value = null, string min = null, string max = null) => new(
            (valueName == null ? "Argument" : valueName.Length > 0 ? char.ToUpper(valueName[0]) + valueName[1..] : valueName) +
            (value == null ? " must be " : $" (= {value}) must be") +
            (min == null ? max == null ? " in a valid range." : $" less than or equal to {max}." : max == null ? $" more than {min}." : $" in the range {min}..{max}."));

        public static IndexOutOfRangeException EnumInvalid<T>(string valueName = "enum") where T : Enum => EnumInvalid(typeof(T).Name, valueName);
        public static IndexOutOfRangeException EnumInvalid(string valueName = "enum", string typeName = null) => new(
            (valueName == null ? "Enum" : valueName.Length > 0 ? char.ToUpper(valueName[0]) + valueName[1..] : valueName) +
            (typeName == null ? null : $"of type '{typeName}'") +
            " must be valid.");

        public static ArgumentOutOfRangeException ArgumentEnumInvalid<T>(string valueName = "argument") where T : Enum => ArgumentEnumInvalid(typeof(T).Name, valueName);
        public static ArgumentOutOfRangeException ArgumentEnumInvalid(string valueName = "argument", string typeName = null) => new(
            (valueName == null ? "Argument" : valueName.Length > 0 ? char.ToUpper(valueName[0]) + valueName[1..] : valueName) +
            (typeName == null ? null : $"of type '{typeName}'") +
            " must be valid.");

        public static InvalidOperationException InvalidOperation(string message = "Operation cannot be performed.") => new(message);

        public static NotSupportedException NotSupported(string message = "Operation is not supported.") => new(message);
    }
}