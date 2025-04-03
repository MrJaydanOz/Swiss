#if UNITY_EDITOR
using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Swiss.Editor.Templates
{
    public enum OperatorType : byte
    {
        Implicit,
        Explicit,
        Addition,
        Subtraction,
        Multiply,
        Division,
        Modulus,
        ExclusiveOr,
        BitwiseAnd,
        BitwiseOr,
        LogicalAnd,
        LogicalOr,
        Assign,
        LeftShift,
        RightShift,
        Equality,
        Inequality,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual,
        MultiplicationAssignment,
        SubtractionAssignment,
        ExclusiveOrAssignment,
        LeftShiftAssignment,
        ModulusAssignment,
        AdditionAssignment,
        BitwiseAndAssignment,
        BitwiseOrAssignment,
        Comma,
        DivisionAssignment,
        Decrement,
        Increment,
        UnaryNegation,
        UnaryPlus,
        OnesComplement,
    }

    public static partial class Func
    {
        public static string ToMethodName(this OperatorType operatorType) => operatorType switch
        {
            OperatorType.Implicit => "op_Implicit",
            OperatorType.Explicit => "op_Explicit",
            OperatorType.Addition => "op_Addition",
            OperatorType.Subtraction => "op_Subtraction",
            OperatorType.Multiply => "op_Multiply",
            OperatorType.Division => "op_Division",
            OperatorType.Modulus => "op_Modulus",
            OperatorType.ExclusiveOr => "op_ExclusiveOr",
            OperatorType.BitwiseAnd => "op_BitwiseAnd",
            OperatorType.BitwiseOr => "op_BitwiseOr",
            OperatorType.LogicalAnd => "op_LogicalAnd",
            OperatorType.LogicalOr => "op_LogicalOr",
            OperatorType.Assign => "op_Assign",
            OperatorType.LeftShift => "op_LeftShift",
            OperatorType.RightShift => "op_RightShift",
            OperatorType.Equality => "op_Equality",
            OperatorType.Inequality => "op_Inequality",
            OperatorType.GreaterThan => "op_GreaterThan",
            OperatorType.LessThan => "op_LessThan",
            OperatorType.GreaterThanOrEqual => "op_GreaterThanOrEqual",
            OperatorType.LessThanOrEqual => "op_LessThanOrEqual",
            OperatorType.MultiplicationAssignment => "op_MultiplicationAssignment",
            OperatorType.SubtractionAssignment => "op_SubtractionAssignment",
            OperatorType.ExclusiveOrAssignment => "op_ExclusiveOrAssignment",
            OperatorType.LeftShiftAssignment => "op_LeftShiftAssignment",
            OperatorType.ModulusAssignment => "op_ModulusAssignment",
            OperatorType.AdditionAssignment => "op_AdditionAssignment",
            OperatorType.BitwiseAndAssignment => "op_BitwiseAndAssignment",
            OperatorType.BitwiseOrAssignment => "op_BitwiseOrAssignment",
            OperatorType.Comma => "op_Comma",
            OperatorType.DivisionAssignment => "op_DivisionAssignment",
            OperatorType.Decrement => "op_Decrement",
            OperatorType.Increment => "op_Increment",
            OperatorType.UnaryNegation => "op_UnaryNegation",
            OperatorType.UnaryPlus => "op_UnaryPlus",
            OperatorType.OnesComplement => "op_OnesComplement",
            _ => throw new ArgumentOutOfRangeException(),
        };

        public static bool TryGetOperatorFromMethodName(string methodName, out OperatorType operatorType)
        {
            switch (methodName)
            {
                case "op_Implicit": operatorType = OperatorType.Implicit; return true;
                case "op_Explicit": operatorType = OperatorType.Explicit; return true;
                case "op_Addition": operatorType = OperatorType.Addition; return true;
                case "op_Subtraction": operatorType = OperatorType.Subtraction; return true;
                case "op_Multiply": operatorType = OperatorType.Multiply; return true;
                case "op_Division": operatorType = OperatorType.Division; return true;
                case "op_Modulus": operatorType = OperatorType.Modulus; return true;
                case "op_ExclusiveOr": operatorType = OperatorType.ExclusiveOr; return true;
                case "op_BitwiseAnd": operatorType = OperatorType.BitwiseAnd; return true;
                case "op_BitwiseOr": operatorType = OperatorType.BitwiseOr; return true;
                case "op_LogicalAnd": operatorType = OperatorType.LogicalAnd; return true;
                case "op_LogicalOr": operatorType = OperatorType.LogicalOr; return true;
                case "op_Assign": operatorType = OperatorType.Assign; return true;
                case "op_LeftShift": operatorType = OperatorType.LeftShift; return true;
                case "op_RightShift": operatorType = OperatorType.RightShift; return true;
                case "op_Equality": operatorType = OperatorType.Equality; return true;
                case "op_Inequality": operatorType = OperatorType.Inequality; return true;
                case "op_GreaterThan": operatorType = OperatorType.GreaterThan; return true;
                case "op_LessThan": operatorType = OperatorType.LessThan; return true;
                case "op_GreaterThanOrEqual": operatorType = OperatorType.GreaterThanOrEqual; return true;
                case "op_LessThanOrEqual": operatorType = OperatorType.LessThanOrEqual; return true;
                case "op_MultiplicationAssignment": operatorType = OperatorType.MultiplicationAssignment; return true;
                case "op_SubtractionAssignment": operatorType = OperatorType.SubtractionAssignment; return true;
                case "op_ExclusiveOrAssignment": operatorType = OperatorType.ExclusiveOrAssignment; return true;
                case "op_LeftShiftAssignment": operatorType = OperatorType.LeftShiftAssignment; return true;
                case "op_ModulusAssignment": operatorType = OperatorType.ModulusAssignment; return true;
                case "op_AdditionAssignment": operatorType = OperatorType.AdditionAssignment; return true;
                case "op_BitwiseAndAssignment": operatorType = OperatorType.BitwiseAndAssignment; return true;
                case "op_BitwiseOrAssignment": operatorType = OperatorType.BitwiseOrAssignment; return true;
                case "op_Comma": operatorType = OperatorType.Comma; return true;
                case "op_DivisionAssignment": operatorType = OperatorType.DivisionAssignment; return true;
                case "op_Decrement": operatorType = OperatorType.Decrement; return true;
                case "op_Increment": operatorType = OperatorType.Increment; return true;
                case "op_UnaryNegation": operatorType = OperatorType.UnaryNegation; return true;
                case "op_UnaryPlus": operatorType = OperatorType.UnaryPlus; return true;
                case "op_OnesComplement": operatorType = OperatorType.OnesComplement; return true;
                default:
                    operatorType = default;
                    return false;
            };
        }
    }

    public class TemplateType
    {
        private readonly Type _type = null;

        private TemplateType(Type type)
        {
            _type = type ?? throw new NullReferenceException();
        }

        public static string NameOf(Type type) =>
            type.IsGenericParameter ? "" :
            type == typeof(void) ? "void" :
            type == typeof(bool) ? "bool" :
            type == typeof(byte) ? "byte" :
            type == typeof(sbyte) ? "sbyte" :
            type == typeof(char) ? "char" :
            type == typeof(decimal) ? "decimal" :
            type == typeof(double) ? "double" :
            type == typeof(float) ? "float" :
            type == typeof(int) ? "int" :
            type == typeof(uint) ? "uint" :
            type == typeof(nint) ? "nint" :
            type == typeof(nuint) ? "nuint" :
            type == typeof(long) ? "long" :
            type == typeof(ulong) ? "ulong" :
            type == typeof(short) ? "short" :
            type == typeof(ushort) ? "ushort" :
            type == typeof(object) ? "object" :
            type == typeof(string) ? "string" :
            type.GetGenericArguments().Length > 0 ? $"{type.Name[..^2]}<{new StringBuilder().AppendJoin(", ", type.GetGenericArguments().Select(NameOf))}>" : type.Name;

        public string Name => NameOf(_type);

        public string Namespace =>
            _type == typeof(void) ? null :
            _type == typeof(bool) ? null :
            _type == typeof(byte) ? null :
            _type == typeof(sbyte) ? null :
            _type == typeof(char) ? null :
            _type == typeof(decimal) ? null :
            _type == typeof(double) ? null :
            _type == typeof(float) ? null :
            _type == typeof(int) ? null :
            _type == typeof(uint) ? null :
            _type == typeof(nint) ? null :
            _type == typeof(nuint) ? null :
            _type == typeof(long) ? null :
            _type == typeof(ulong) ? null :
            _type == typeof(short) ? null :
            _type == typeof(ushort) ? null :
            _type == typeof(object) ? null :
            _type == typeof(string) ? null :
            _type.Namespace;

        public string FullName
        {
            get
            {
                string namespaceString = Namespace;
                return string.IsNullOrWhiteSpace(namespaceString) ? Name : namespaceString + '.' + Name;
            }
        }

        public bool HasMethod(Type returnType, string name, Type[] parameterTypes, bool explicitCastResult = false) =>
            _type.GetMethods(BindingFlags.Public | BindingFlags.Instance).Any((v) =>
                v.Name == name
                && (explicitCastResult ? TemplateContext.OperatorExists(OperatorType.Explicit, returnType, v.ReturnType) : returnType.IsAssignableFrom(v.ReturnType))
                && v.GetParameters().Aggregate((min: 0, max: 0), (p, v) => (v.IsOptional ? p.min : p.min + 1, p.max + 1)) is var parameterLengthRange
                && v.GetParameters().Length >= parameterLengthRange.min
                && v.GetParameters().Length <= parameterLengthRange.max
                && parameterTypes.Select((parameterType, i) => (parameterType, targetParameter: v.GetParameters()[i])).Any((v) =>
                    v.targetParameter.ParameterType.IsAssignableFrom(v.parameterType)));

        public static implicit operator TemplateType(Type type) => new(type);
    }
}
#endif