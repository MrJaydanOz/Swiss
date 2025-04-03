///
/// This is a template result file that has been filled by <see cref="Swiss.Templates._Template_MinMax.Generate"/>.
///

using System;
using System.Collections.Generic;
using System.Linq;

namespace Swiss
{
    public static partial class Func
    {
        /// <inheritdoc cref="Min{T}(T, T)"/>
        public static byte Min(byte a, byte b) => a < b ? a : b;
        /// <inheritdoc cref="Min{T}(T, T, T)"/>
        public static byte Min(byte a, byte b, byte c) => a < b ? (a < c ? a : c) : (b < c ? b : c);
        /// <inheritdoc cref="Enumerable.Min(IEnumerable{byte})"/>
        public static byte Min(this IEnumerable<byte> collection)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            var result = default(byte);
            var isFirst = true;

            foreach (var item in collection)
            {
                if (isFirst)
                {
                    result = item;
                    isFirst = false;
                }
                else if (result < item)
                    result = item;
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Enumerable.Min(IEnumerable{byte})"/>
        public static byte Min<T>(this IEnumerable<T> collection, Func<T, byte> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            var result = default(byte);
            var isFirst = true;

            foreach (var item in collection)
            {
                var selectedItem = selector.Invoke(item);

                if (isFirst)
                {
                    result = selectedItem;
                    isFirst = false;
                }
                else if (result < selectedItem)
                    result = selectedItem;
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Min{T}(IEnumerable{T}, Func{T, byte})"/>
        public static T FindMin<T>(this IEnumerable<T> collection, Func<T, byte> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            if (selector == null)
                throw Expt.ArgumentNull(nameof(selector));

            var result = default(T);
            var resultComparer = default(byte);
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
                else if (resultComparer < itemComparer)
                {
                    result = item;
                    resultComparer = itemComparer;
                }
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Min{T}(T, T)"/>
        public static sbyte Min(sbyte a, sbyte b) => a < b ? a : b;
        /// <inheritdoc cref="Min{T}(T, T, T)"/>
        public static sbyte Min(sbyte a, sbyte b, sbyte c) => a < b ? (a < c ? a : c) : (b < c ? b : c);
        /// <inheritdoc cref="Enumerable.Min(IEnumerable{sbyte})"/>
        public static sbyte Min(this IEnumerable<sbyte> collection)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            var result = default(sbyte);
            var isFirst = true;

            foreach (var item in collection)
            {
                if (isFirst)
                {
                    result = item;
                    isFirst = false;
                }
                else if (result < item)
                    result = item;
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Enumerable.Min(IEnumerable{sbyte})"/>
        public static sbyte Min<T>(this IEnumerable<T> collection, Func<T, sbyte> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            var result = default(sbyte);
            var isFirst = true;

            foreach (var item in collection)
            {
                var selectedItem = selector.Invoke(item);

                if (isFirst)
                {
                    result = selectedItem;
                    isFirst = false;
                }
                else if (result < selectedItem)
                    result = selectedItem;
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Min{T}(IEnumerable{T}, Func{T, sbyte})"/>
        public static T FindMin<T>(this IEnumerable<T> collection, Func<T, sbyte> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            if (selector == null)
                throw Expt.ArgumentNull(nameof(selector));

            var result = default(T);
            var resultComparer = default(sbyte);
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
                else if (resultComparer < itemComparer)
                {
                    result = item;
                    resultComparer = itemComparer;
                }
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Min{T}(T, T)"/>
        public static char Min(char a, char b) => a < b ? a : b;
        /// <inheritdoc cref="Min{T}(T, T, T)"/>
        public static char Min(char a, char b, char c) => a < b ? (a < c ? a : c) : (b < c ? b : c);
        /// <inheritdoc cref="Enumerable.Min(IEnumerable{char})"/>
        public static char Min(this IEnumerable<char> collection)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            var result = default(char);
            var isFirst = true;

            foreach (var item in collection)
            {
                if (isFirst)
                {
                    result = item;
                    isFirst = false;
                }
                else if (result < item)
                    result = item;
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Enumerable.Min(IEnumerable{char})"/>
        public static char Min<T>(this IEnumerable<T> collection, Func<T, char> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            var result = default(char);
            var isFirst = true;

            foreach (var item in collection)
            {
                var selectedItem = selector.Invoke(item);

                if (isFirst)
                {
                    result = selectedItem;
                    isFirst = false;
                }
                else if (result < selectedItem)
                    result = selectedItem;
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Min{T}(IEnumerable{T}, Func{T, char})"/>
        public static T FindMin<T>(this IEnumerable<T> collection, Func<T, char> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            if (selector == null)
                throw Expt.ArgumentNull(nameof(selector));

            var result = default(T);
            var resultComparer = default(char);
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
                else if (resultComparer < itemComparer)
                {
                    result = item;
                    resultComparer = itemComparer;
                }
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Min{T}(T, T)"/>
        public static decimal Min(decimal a, decimal b) => a < b ? a : b;
        /// <inheritdoc cref="Min{T}(T, T, T)"/>
        public static decimal Min(decimal a, decimal b, decimal c) => a < b ? (a < c ? a : c) : (b < c ? b : c);
        /// <inheritdoc cref="Min{T}(IEnumerable{T}, Func{T, decimal})"/>
        public static T FindMin<T>(this IEnumerable<T> collection, Func<T, decimal> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            if (selector == null)
                throw Expt.ArgumentNull(nameof(selector));

            var result = default(T);
            var resultComparer = default(decimal);
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
                else if (resultComparer < itemComparer)
                {
                    result = item;
                    resultComparer = itemComparer;
                }
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Min{T}(T, T)"/>
        public static double Min(double a, double b) => a < b ? a : b;
        /// <inheritdoc cref="Min{T}(T, T, T)"/>
        public static double Min(double a, double b, double c) => a < b ? (a < c ? a : c) : (b < c ? b : c);
        /// <inheritdoc cref="Min{T}(IEnumerable{T}, Func{T, double})"/>
        public static T FindMin<T>(this IEnumerable<T> collection, Func<T, double> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            if (selector == null)
                throw Expt.ArgumentNull(nameof(selector));

            var result = default(T);
            var resultComparer = default(double);
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
                else if (resultComparer < itemComparer)
                {
                    result = item;
                    resultComparer = itemComparer;
                }
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Min{T}(T, T)"/>
        public static float Min(float a, float b) => a < b ? a : b;
        /// <inheritdoc cref="Min{T}(T, T, T)"/>
        public static float Min(float a, float b, float c) => a < b ? (a < c ? a : c) : (b < c ? b : c);
        /// <inheritdoc cref="Min{T}(IEnumerable{T}, Func{T, float})"/>
        public static T FindMin<T>(this IEnumerable<T> collection, Func<T, float> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            if (selector == null)
                throw Expt.ArgumentNull(nameof(selector));

            var result = default(T);
            var resultComparer = default(float);
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
                else if (resultComparer < itemComparer)
                {
                    result = item;
                    resultComparer = itemComparer;
                }
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Min{T}(T, T)"/>
        public static int Min(int a, int b) => a < b ? a : b;
        /// <inheritdoc cref="Min{T}(T, T, T)"/>
        public static int Min(int a, int b, int c) => a < b ? (a < c ? a : c) : (b < c ? b : c);
        /// <inheritdoc cref="Min{T}(IEnumerable{T}, Func{T, int})"/>
        public static T FindMin<T>(this IEnumerable<T> collection, Func<T, int> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            if (selector == null)
                throw Expt.ArgumentNull(nameof(selector));

            var result = default(T);
            var resultComparer = default(int);
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
                else if (resultComparer < itemComparer)
                {
                    result = item;
                    resultComparer = itemComparer;
                }
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Min{T}(T, T)"/>
        public static uint Min(uint a, uint b) => a < b ? a : b;
        /// <inheritdoc cref="Min{T}(T, T, T)"/>
        public static uint Min(uint a, uint b, uint c) => a < b ? (a < c ? a : c) : (b < c ? b : c);
        /// <inheritdoc cref="Enumerable.Min(IEnumerable{uint})"/>
        public static uint Min(this IEnumerable<uint> collection)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            var result = default(uint);
            var isFirst = true;

            foreach (var item in collection)
            {
                if (isFirst)
                {
                    result = item;
                    isFirst = false;
                }
                else if (result < item)
                    result = item;
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Enumerable.Min(IEnumerable{uint})"/>
        public static uint Min<T>(this IEnumerable<T> collection, Func<T, uint> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            var result = default(uint);
            var isFirst = true;

            foreach (var item in collection)
            {
                var selectedItem = selector.Invoke(item);

                if (isFirst)
                {
                    result = selectedItem;
                    isFirst = false;
                }
                else if (result < selectedItem)
                    result = selectedItem;
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Min{T}(IEnumerable{T}, Func{T, uint})"/>
        public static T FindMin<T>(this IEnumerable<T> collection, Func<T, uint> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            if (selector == null)
                throw Expt.ArgumentNull(nameof(selector));

            var result = default(T);
            var resultComparer = default(uint);
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
                else if (resultComparer < itemComparer)
                {
                    result = item;
                    resultComparer = itemComparer;
                }
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Min{T}(T, T)"/>
        public static long Min(long a, long b) => a < b ? a : b;
        /// <inheritdoc cref="Min{T}(T, T, T)"/>
        public static long Min(long a, long b, long c) => a < b ? (a < c ? a : c) : (b < c ? b : c);
        /// <inheritdoc cref="Min{T}(IEnumerable{T}, Func{T, long})"/>
        public static T FindMin<T>(this IEnumerable<T> collection, Func<T, long> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            if (selector == null)
                throw Expt.ArgumentNull(nameof(selector));

            var result = default(T);
            var resultComparer = default(long);
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
                else if (resultComparer < itemComparer)
                {
                    result = item;
                    resultComparer = itemComparer;
                }
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Min{T}(T, T)"/>
        public static ulong Min(ulong a, ulong b) => a < b ? a : b;
        /// <inheritdoc cref="Min{T}(T, T, T)"/>
        public static ulong Min(ulong a, ulong b, ulong c) => a < b ? (a < c ? a : c) : (b < c ? b : c);
        /// <inheritdoc cref="Enumerable.Min(IEnumerable{ulong})"/>
        public static ulong Min(this IEnumerable<ulong> collection)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            var result = default(ulong);
            var isFirst = true;

            foreach (var item in collection)
            {
                if (isFirst)
                {
                    result = item;
                    isFirst = false;
                }
                else if (result < item)
                    result = item;
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Enumerable.Min(IEnumerable{ulong})"/>
        public static ulong Min<T>(this IEnumerable<T> collection, Func<T, ulong> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            var result = default(ulong);
            var isFirst = true;

            foreach (var item in collection)
            {
                var selectedItem = selector.Invoke(item);

                if (isFirst)
                {
                    result = selectedItem;
                    isFirst = false;
                }
                else if (result < selectedItem)
                    result = selectedItem;
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Min{T}(IEnumerable{T}, Func{T, ulong})"/>
        public static T FindMin<T>(this IEnumerable<T> collection, Func<T, ulong> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            if (selector == null)
                throw Expt.ArgumentNull(nameof(selector));

            var result = default(T);
            var resultComparer = default(ulong);
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
                else if (resultComparer < itemComparer)
                {
                    result = item;
                    resultComparer = itemComparer;
                }
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Min{T}(T, T)"/>
        public static short Min(short a, short b) => a < b ? a : b;
        /// <inheritdoc cref="Min{T}(T, T, T)"/>
        public static short Min(short a, short b, short c) => a < b ? (a < c ? a : c) : (b < c ? b : c);
        /// <inheritdoc cref="Enumerable.Min(IEnumerable{short})"/>
        public static short Min(this IEnumerable<short> collection)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            var result = default(short);
            var isFirst = true;

            foreach (var item in collection)
            {
                if (isFirst)
                {
                    result = item;
                    isFirst = false;
                }
                else if (result < item)
                    result = item;
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Enumerable.Min(IEnumerable{short})"/>
        public static short Min<T>(this IEnumerable<T> collection, Func<T, short> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            var result = default(short);
            var isFirst = true;

            foreach (var item in collection)
            {
                var selectedItem = selector.Invoke(item);

                if (isFirst)
                {
                    result = selectedItem;
                    isFirst = false;
                }
                else if (result < selectedItem)
                    result = selectedItem;
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Min{T}(IEnumerable{T}, Func{T, short})"/>
        public static T FindMin<T>(this IEnumerable<T> collection, Func<T, short> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            if (selector == null)
                throw Expt.ArgumentNull(nameof(selector));

            var result = default(T);
            var resultComparer = default(short);
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
                else if (resultComparer < itemComparer)
                {
                    result = item;
                    resultComparer = itemComparer;
                }
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Min{T}(T, T)"/>
        public static ushort Min(ushort a, ushort b) => a < b ? a : b;
        /// <inheritdoc cref="Min{T}(T, T, T)"/>
        public static ushort Min(ushort a, ushort b, ushort c) => a < b ? (a < c ? a : c) : (b < c ? b : c);
        /// <inheritdoc cref="Enumerable.Min(IEnumerable{ushort})"/>
        public static ushort Min(this IEnumerable<ushort> collection)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            var result = default(ushort);
            var isFirst = true;

            foreach (var item in collection)
            {
                if (isFirst)
                {
                    result = item;
                    isFirst = false;
                }
                else if (result < item)
                    result = item;
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Enumerable.Min(IEnumerable{ushort})"/>
        public static ushort Min<T>(this IEnumerable<T> collection, Func<T, ushort> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            var result = default(ushort);
            var isFirst = true;

            foreach (var item in collection)
            {
                var selectedItem = selector.Invoke(item);

                if (isFirst)
                {
                    result = selectedItem;
                    isFirst = false;
                }
                else if (result < selectedItem)
                    result = selectedItem;
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Min{T}(IEnumerable{T}, Func{T, ushort})"/>
        public static T FindMin<T>(this IEnumerable<T> collection, Func<T, ushort> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            if (selector == null)
                throw Expt.ArgumentNull(nameof(selector));

            var result = default(T);
            var resultComparer = default(ushort);
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
                else if (resultComparer < itemComparer)
                {
                    result = item;
                    resultComparer = itemComparer;
                }
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }

        /// <inheritdoc cref="Max{T}(T, T)"/>
        public static byte Max(byte a, byte b) => a < b ? b : a;
        /// <inheritdoc cref="Max{T}(T, T, T)"/>
        public static byte Max(byte a, byte b, byte c) => a < b ? (b < c ? c : b) : (a < c ? c : a);
        /// <inheritdoc cref="Enumerable.Max(IEnumerable{byte})"/>
        public static byte Max(this IEnumerable<byte> collection)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            var result = default(byte);
            var isFirst = true;

            foreach (var item in collection)
            {
                if (isFirst)
                {
                    result = item;
                    isFirst = false;
                }
                else if (item < result)
                    result = item;
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Enumerable.Max(IEnumerable{byte})"/>
        public static byte Max<T>(this IEnumerable<T> collection, Func<T, byte> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            var result = default(byte);
            var isFirst = true;

            foreach (var item in collection)
            {
                var selectedItem = selector.Invoke(item);

                if (isFirst)
                {
                    result = selectedItem;
                    isFirst = false;
                }
                else if (selectedItem < result)
                    result = selectedItem;
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Max{T}(IEnumerable{T}, Func{T, byte})"/>
        public static T FindMax<T>(this IEnumerable<T> collection, Func<T, byte> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            if (selector == null)
                throw Expt.ArgumentNull(nameof(selector));

            var result = default(T);
            var resultComparer = default(byte);
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
                else if (itemComparer < resultComparer)
                {
                    result = item;
                    resultComparer = itemComparer;
                }
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Max{T}(T, T)"/>
        public static sbyte Max(sbyte a, sbyte b) => a < b ? b : a;
        /// <inheritdoc cref="Max{T}(T, T, T)"/>
        public static sbyte Max(sbyte a, sbyte b, sbyte c) => a < b ? (b < c ? c : b) : (a < c ? c : a);
        /// <inheritdoc cref="Enumerable.Max(IEnumerable{sbyte})"/>
        public static sbyte Max(this IEnumerable<sbyte> collection)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            var result = default(sbyte);
            var isFirst = true;

            foreach (var item in collection)
            {
                if (isFirst)
                {
                    result = item;
                    isFirst = false;
                }
                else if (item < result)
                    result = item;
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Enumerable.Max(IEnumerable{sbyte})"/>
        public static sbyte Max<T>(this IEnumerable<T> collection, Func<T, sbyte> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            var result = default(sbyte);
            var isFirst = true;

            foreach (var item in collection)
            {
                var selectedItem = selector.Invoke(item);

                if (isFirst)
                {
                    result = selectedItem;
                    isFirst = false;
                }
                else if (selectedItem < result)
                    result = selectedItem;
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Max{T}(IEnumerable{T}, Func{T, sbyte})"/>
        public static T FindMax<T>(this IEnumerable<T> collection, Func<T, sbyte> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            if (selector == null)
                throw Expt.ArgumentNull(nameof(selector));

            var result = default(T);
            var resultComparer = default(sbyte);
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
                else if (itemComparer < resultComparer)
                {
                    result = item;
                    resultComparer = itemComparer;
                }
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Max{T}(T, T)"/>
        public static char Max(char a, char b) => a < b ? b : a;
        /// <inheritdoc cref="Max{T}(T, T, T)"/>
        public static char Max(char a, char b, char c) => a < b ? (b < c ? c : b) : (a < c ? c : a);
        /// <inheritdoc cref="Enumerable.Max(IEnumerable{char})"/>
        public static char Max(this IEnumerable<char> collection)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            var result = default(char);
            var isFirst = true;

            foreach (var item in collection)
            {
                if (isFirst)
                {
                    result = item;
                    isFirst = false;
                }
                else if (item < result)
                    result = item;
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Enumerable.Max(IEnumerable{char})"/>
        public static char Max<T>(this IEnumerable<T> collection, Func<T, char> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            var result = default(char);
            var isFirst = true;

            foreach (var item in collection)
            {
                var selectedItem = selector.Invoke(item);

                if (isFirst)
                {
                    result = selectedItem;
                    isFirst = false;
                }
                else if (selectedItem < result)
                    result = selectedItem;
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Max{T}(IEnumerable{T}, Func{T, char})"/>
        public static T FindMax<T>(this IEnumerable<T> collection, Func<T, char> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            if (selector == null)
                throw Expt.ArgumentNull(nameof(selector));

            var result = default(T);
            var resultComparer = default(char);
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
                else if (itemComparer < resultComparer)
                {
                    result = item;
                    resultComparer = itemComparer;
                }
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Max{T}(T, T)"/>
        public static decimal Max(decimal a, decimal b) => a < b ? b : a;
        /// <inheritdoc cref="Max{T}(T, T, T)"/>
        public static decimal Max(decimal a, decimal b, decimal c) => a < b ? (b < c ? c : b) : (a < c ? c : a);
        /// <inheritdoc cref="Max{T}(IEnumerable{T}, Func{T, decimal})"/>
        public static T FindMax<T>(this IEnumerable<T> collection, Func<T, decimal> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            if (selector == null)
                throw Expt.ArgumentNull(nameof(selector));

            var result = default(T);
            var resultComparer = default(decimal);
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
                else if (itemComparer < resultComparer)
                {
                    result = item;
                    resultComparer = itemComparer;
                }
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Max{T}(T, T)"/>
        public static double Max(double a, double b) => a < b ? b : a;
        /// <inheritdoc cref="Max{T}(T, T, T)"/>
        public static double Max(double a, double b, double c) => a < b ? (b < c ? c : b) : (a < c ? c : a);
        /// <inheritdoc cref="Max{T}(IEnumerable{T}, Func{T, double})"/>
        public static T FindMax<T>(this IEnumerable<T> collection, Func<T, double> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            if (selector == null)
                throw Expt.ArgumentNull(nameof(selector));

            var result = default(T);
            var resultComparer = default(double);
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
                else if (itemComparer < resultComparer)
                {
                    result = item;
                    resultComparer = itemComparer;
                }
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Max{T}(T, T)"/>
        public static float Max(float a, float b) => a < b ? b : a;
        /// <inheritdoc cref="Max{T}(T, T, T)"/>
        public static float Max(float a, float b, float c) => a < b ? (b < c ? c : b) : (a < c ? c : a);
        /// <inheritdoc cref="Max{T}(IEnumerable{T}, Func{T, float})"/>
        public static T FindMax<T>(this IEnumerable<T> collection, Func<T, float> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            if (selector == null)
                throw Expt.ArgumentNull(nameof(selector));

            var result = default(T);
            var resultComparer = default(float);
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
                else if (itemComparer < resultComparer)
                {
                    result = item;
                    resultComparer = itemComparer;
                }
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Max{T}(T, T)"/>
        public static int Max(int a, int b) => a < b ? b : a;
        /// <inheritdoc cref="Max{T}(T, T, T)"/>
        public static int Max(int a, int b, int c) => a < b ? (b < c ? c : b) : (a < c ? c : a);
        /// <inheritdoc cref="Max{T}(IEnumerable{T}, Func{T, int})"/>
        public static T FindMax<T>(this IEnumerable<T> collection, Func<T, int> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            if (selector == null)
                throw Expt.ArgumentNull(nameof(selector));

            var result = default(T);
            var resultComparer = default(int);
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
                else if (itemComparer < resultComparer)
                {
                    result = item;
                    resultComparer = itemComparer;
                }
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Max{T}(T, T)"/>
        public static uint Max(uint a, uint b) => a < b ? b : a;
        /// <inheritdoc cref="Max{T}(T, T, T)"/>
        public static uint Max(uint a, uint b, uint c) => a < b ? (b < c ? c : b) : (a < c ? c : a);
        /// <inheritdoc cref="Enumerable.Max(IEnumerable{uint})"/>
        public static uint Max(this IEnumerable<uint> collection)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            var result = default(uint);
            var isFirst = true;

            foreach (var item in collection)
            {
                if (isFirst)
                {
                    result = item;
                    isFirst = false;
                }
                else if (item < result)
                    result = item;
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Enumerable.Max(IEnumerable{uint})"/>
        public static uint Max<T>(this IEnumerable<T> collection, Func<T, uint> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            var result = default(uint);
            var isFirst = true;

            foreach (var item in collection)
            {
                var selectedItem = selector.Invoke(item);

                if (isFirst)
                {
                    result = selectedItem;
                    isFirst = false;
                }
                else if (selectedItem < result)
                    result = selectedItem;
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Max{T}(IEnumerable{T}, Func{T, uint})"/>
        public static T FindMax<T>(this IEnumerable<T> collection, Func<T, uint> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            if (selector == null)
                throw Expt.ArgumentNull(nameof(selector));

            var result = default(T);
            var resultComparer = default(uint);
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
                else if (itemComparer < resultComparer)
                {
                    result = item;
                    resultComparer = itemComparer;
                }
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Max{T}(T, T)"/>
        public static long Max(long a, long b) => a < b ? b : a;
        /// <inheritdoc cref="Max{T}(T, T, T)"/>
        public static long Max(long a, long b, long c) => a < b ? (b < c ? c : b) : (a < c ? c : a);
        /// <inheritdoc cref="Max{T}(IEnumerable{T}, Func{T, long})"/>
        public static T FindMax<T>(this IEnumerable<T> collection, Func<T, long> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            if (selector == null)
                throw Expt.ArgumentNull(nameof(selector));

            var result = default(T);
            var resultComparer = default(long);
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
                else if (itemComparer < resultComparer)
                {
                    result = item;
                    resultComparer = itemComparer;
                }
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Max{T}(T, T)"/>
        public static ulong Max(ulong a, ulong b) => a < b ? b : a;
        /// <inheritdoc cref="Max{T}(T, T, T)"/>
        public static ulong Max(ulong a, ulong b, ulong c) => a < b ? (b < c ? c : b) : (a < c ? c : a);
        /// <inheritdoc cref="Enumerable.Max(IEnumerable{ulong})"/>
        public static ulong Max(this IEnumerable<ulong> collection)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            var result = default(ulong);
            var isFirst = true;

            foreach (var item in collection)
            {
                if (isFirst)
                {
                    result = item;
                    isFirst = false;
                }
                else if (item < result)
                    result = item;
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Enumerable.Max(IEnumerable{ulong})"/>
        public static ulong Max<T>(this IEnumerable<T> collection, Func<T, ulong> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            var result = default(ulong);
            var isFirst = true;

            foreach (var item in collection)
            {
                var selectedItem = selector.Invoke(item);

                if (isFirst)
                {
                    result = selectedItem;
                    isFirst = false;
                }
                else if (selectedItem < result)
                    result = selectedItem;
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Max{T}(IEnumerable{T}, Func{T, ulong})"/>
        public static T FindMax<T>(this IEnumerable<T> collection, Func<T, ulong> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            if (selector == null)
                throw Expt.ArgumentNull(nameof(selector));

            var result = default(T);
            var resultComparer = default(ulong);
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
                else if (itemComparer < resultComparer)
                {
                    result = item;
                    resultComparer = itemComparer;
                }
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Max{T}(T, T)"/>
        public static short Max(short a, short b) => a < b ? b : a;
        /// <inheritdoc cref="Max{T}(T, T, T)"/>
        public static short Max(short a, short b, short c) => a < b ? (b < c ? c : b) : (a < c ? c : a);
        /// <inheritdoc cref="Enumerable.Max(IEnumerable{short})"/>
        public static short Max(this IEnumerable<short> collection)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            var result = default(short);
            var isFirst = true;

            foreach (var item in collection)
            {
                if (isFirst)
                {
                    result = item;
                    isFirst = false;
                }
                else if (item < result)
                    result = item;
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Enumerable.Max(IEnumerable{short})"/>
        public static short Max<T>(this IEnumerable<T> collection, Func<T, short> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            var result = default(short);
            var isFirst = true;

            foreach (var item in collection)
            {
                var selectedItem = selector.Invoke(item);

                if (isFirst)
                {
                    result = selectedItem;
                    isFirst = false;
                }
                else if (selectedItem < result)
                    result = selectedItem;
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Max{T}(IEnumerable{T}, Func{T, short})"/>
        public static T FindMax<T>(this IEnumerable<T> collection, Func<T, short> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            if (selector == null)
                throw Expt.ArgumentNull(nameof(selector));

            var result = default(T);
            var resultComparer = default(short);
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
                else if (itemComparer < resultComparer)
                {
                    result = item;
                    resultComparer = itemComparer;
                }
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Max{T}(T, T)"/>
        public static ushort Max(ushort a, ushort b) => a < b ? b : a;
        /// <inheritdoc cref="Max{T}(T, T, T)"/>
        public static ushort Max(ushort a, ushort b, ushort c) => a < b ? (b < c ? c : b) : (a < c ? c : a);
        /// <inheritdoc cref="Enumerable.Max(IEnumerable{ushort})"/>
        public static ushort Max(this IEnumerable<ushort> collection)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            var result = default(ushort);
            var isFirst = true;

            foreach (var item in collection)
            {
                if (isFirst)
                {
                    result = item;
                    isFirst = false;
                }
                else if (item < result)
                    result = item;
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Enumerable.Max(IEnumerable{ushort})"/>
        public static ushort Max<T>(this IEnumerable<T> collection, Func<T, ushort> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            var result = default(ushort);
            var isFirst = true;

            foreach (var item in collection)
            {
                var selectedItem = selector.Invoke(item);

                if (isFirst)
                {
                    result = selectedItem;
                    isFirst = false;
                }
                else if (selectedItem < result)
                    result = selectedItem;
            }

            if (isFirst)
                throw Expt.InvalidOperation("Collection cannot be empty.");

            return result;
        }
        /// <inheritdoc cref="Max{T}(IEnumerable{T}, Func{T, ushort})"/>
        public static T FindMax<T>(this IEnumerable<T> collection, Func<T, ushort> selector)
        {
            if (collection == null)
                throw Expt.ArgumentNull(nameof(collection));

            if (selector == null)
                throw Expt.ArgumentNull(nameof(selector));

            var result = default(T);
            var resultComparer = default(ushort);
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
                else if (itemComparer < resultComparer)
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