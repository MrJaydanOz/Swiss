using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Swiss
{
    public enum Casing : byte
    {
        [InspectorName("camelCase")] CamelCase,
        [InspectorName("PascalCase")] PascalCase,
        [InspectorName("snake_case")] SnakeCase,
        [InspectorName("UPPER_SNAKE_CASE")] UpperSnakeCase,
        [InspectorName("web-case")] WebCase,
        [InspectorName("Sentence case")] SentenceCase,
        [InspectorName("Title Case")] TitleCase,
    }

    public static partial class Func
    {
        public static string ToCasing(this string text, Casing casing) => text == null ? throw Expt.ArgumentNull(nameof(text)) : ToCasing((ReadOnlySpan<char>)text, casing);
        public static string ToCasing(this Span<char> text, Casing casing) => ToCasing((ReadOnlySpan<char>)text, casing);
        public static string ToCasing(this ReadOnlySpan<char> text, Casing casing)
        {
            if (text == null)
                throw Expt.ArgumentNull(nameof(text));

            var stringBuilder = new StringBuilder();
            var lastChar = ' ';
            var currentChar = ' ';
            var nextChar = ' ';

            var enumerator = text.GetEnumerator();
            var state = (byte)0;
            var isFirstWord = true;
            while (true)
            {
                if (!enumerator.MoveNext())
                    state = 2;
                nextChar = state == 2 ? ' ' : enumerator.Current;

                if (state == 0)
                    state = 1;
                else if ((char.IsUpper(currentChar) && !char.IsUpper(nextChar))
                    || (char.IsLetter(currentChar) && !char.IsLetter(lastChar))
                    || (!(char.IsWhiteSpace(currentChar) || currentChar is '_' or '-') && (char.IsWhiteSpace(lastChar) || lastChar is '_' or '-')))
                {
                    switch (casing)
                    {
                        case Casing.CamelCase: stringBuilder.Append(isFirstWord ? char.ToLower(currentChar) : char.ToUpper(currentChar)); break;
                        case Casing.PascalCase: stringBuilder.Append(char.ToUpper(currentChar)); break;
                        case Casing.SnakeCase: if (!isFirstWord) stringBuilder.Append('_'); stringBuilder.Append(char.ToLower(currentChar)); break;
                        case Casing.UpperSnakeCase: if (!isFirstWord) stringBuilder.Append('_'); stringBuilder.Append(char.ToUpper(currentChar)); break;
                        case Casing.WebCase: if (!isFirstWord) stringBuilder.Append('-'); stringBuilder.Append(char.ToLower(currentChar)); break;
                        case Casing.SentenceCase: if (!isFirstWord) stringBuilder.Append(' '); stringBuilder.Append(isFirstWord || (char.IsUpper(currentChar) && char.IsUpper(nextChar)) ? char.ToUpper(currentChar) : char.ToLower(currentChar)); break;
                        case Casing.TitleCase: if (!isFirstWord) stringBuilder.Append(' '); stringBuilder.Append(char.ToUpper(currentChar)); break;
                        default:
                            throw Expt.EnumInvalid<Casing>(nameof(casing));
                    }

                    isFirstWord = false;
                }
                else if (!(char.IsWhiteSpace(currentChar) || currentChar is '_' or '-'))
                    switch (casing)
                    {
                        case Casing.CamelCase:
                        case Casing.PascalCase: stringBuilder.Append(char.IsUpper(currentChar) && char.IsUpper(nextChar) ? char.ToUpper(currentChar) : char.ToLower(currentChar)); break;
                        case Casing.SnakeCase: stringBuilder.Append(char.ToLower(currentChar)); break;
                        case Casing.UpperSnakeCase: stringBuilder.Append(char.ToUpper(currentChar)); break;
                        case Casing.WebCase: stringBuilder.Append(char.ToLower(currentChar)); break;
                        case Casing.SentenceCase: stringBuilder.Append(char.IsUpper(currentChar) && char.IsUpper(nextChar) ? char.ToUpper(currentChar) : char.ToLower(currentChar)); break;
                        case Casing.TitleCase: stringBuilder.Append(char.IsUpper(currentChar) && char.IsUpper(nextChar) ? char.ToUpper(currentChar) : char.ToLower(currentChar)); break;
                        default:
                            throw Expt.EnumInvalid<Casing>(nameof(casing));
                    }

                if (state == 2)
                    break;

                lastChar = currentChar;
                currentChar = nextChar;
            }

            return stringBuilder.ToString();
        }
        public static string ToCasing(this IEnumerable<char> text, Casing casing)
        {
            if (text == null)
                throw Expt.ArgumentNull(nameof(text));

            var stringBuilder = new StringBuilder();
            var lastChar = ' ';
            var currentChar = ' ';
            var nextChar = ' ';

            var enumerator = text.GetEnumerator();
            var state = (byte)0;
            var isFirstWord = true;
            while (true)
            {
                if (!enumerator.MoveNext())
                    state = 2;
                nextChar = state == 2 ? ' ' : enumerator.Current;

                if (state == 0)
                    state = 1;
                else if ((char.IsUpper(currentChar) && !char.IsUpper(nextChar))
                    || (char.IsLetter(currentChar) && !char.IsLetter(lastChar))
                    || (!(char.IsWhiteSpace(currentChar) || currentChar is '_' or '-') && (char.IsWhiteSpace(lastChar) || lastChar is '_' or '-')))
                {
                    switch (casing)
                    {
                        case Casing.CamelCase: stringBuilder.Append(isFirstWord ? char.ToLower(currentChar) : char.ToUpper(currentChar)); break;
                        case Casing.PascalCase: stringBuilder.Append(char.ToUpper(currentChar)); break;
                        case Casing.SnakeCase: if (!isFirstWord) stringBuilder.Append('_'); stringBuilder.Append(char.ToLower(currentChar)); break;
                        case Casing.UpperSnakeCase: if (!isFirstWord) stringBuilder.Append('_'); stringBuilder.Append(char.ToUpper(currentChar)); break;
                        case Casing.WebCase: if (!isFirstWord) stringBuilder.Append('-'); stringBuilder.Append(char.ToLower(currentChar)); break;
                        case Casing.SentenceCase: if (!isFirstWord) stringBuilder.Append(' '); stringBuilder.Append(isFirstWord || (char.IsUpper(currentChar) && char.IsUpper(nextChar)) ? char.ToUpper(currentChar) : char.ToLower(currentChar)); break;
                        case Casing.TitleCase: if (!isFirstWord) stringBuilder.Append(' '); stringBuilder.Append(char.ToUpper(currentChar)); break;
                        default:
                            throw Expt.EnumInvalid<Casing>(nameof(casing));
                    }

                    isFirstWord = false;
                }
                else if (!(char.IsWhiteSpace(currentChar) || currentChar is '_' or '-'))
                    switch (casing)
                    {
                        case Casing.CamelCase:
                        case Casing.PascalCase: stringBuilder.Append(char.IsUpper(currentChar) && char.IsUpper(nextChar) ? char.ToUpper(currentChar) : char.ToLower(currentChar)); break;
                        case Casing.SnakeCase: stringBuilder.Append(char.ToLower(currentChar)); break;
                        case Casing.UpperSnakeCase: stringBuilder.Append(char.ToUpper(currentChar)); break;
                        case Casing.WebCase: stringBuilder.Append(char.ToLower(currentChar)); break;
                        case Casing.SentenceCase: stringBuilder.Append(char.IsUpper(currentChar) && char.IsUpper(nextChar) ? char.ToUpper(currentChar) : char.ToLower(currentChar)); break;
                        case Casing.TitleCase: stringBuilder.Append(char.IsUpper(currentChar) && char.IsUpper(nextChar) ? char.ToUpper(currentChar) : char.ToLower(currentChar)); break;
                        default:
                            throw Expt.EnumInvalid<Casing>(nameof(casing));
                    }

                if (state == 2)
                    break;

                lastChar = currentChar;
                currentChar = nextChar;
            }

            return stringBuilder.ToString();
        }
    }
}