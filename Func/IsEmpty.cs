using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Swiss
{
    public static partial class Func
    {
        public static bool TryGetCount(this IEnumerable collection, out int count)
        {
            if (collection == null)
                throw Expt.Null(nameof(collection));
            else if (collection is IReadOnlyCollection<object> collectionAsReadonlyCollection)
            {
                count = collectionAsReadonlyCollection.Count;
                return true;
            }
            else if (collection is ICollection<object> collectionAsCollection)
            {
                count = collectionAsCollection.Count;
                return true;
            }
            else
            {
                count = -1;
                return false;
            }
        }

        public static bool IsNullOrEmpty(this IEnumerable collection)
        {
            if (collection == null)
                return true;

            if (collection.TryGetCount(out var collectionLength))
                return collectionLength == 0;

            var enumerator = collection.GetEnumerator();
            return !enumerator.MoveNext();
        }
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            if (collection == null)
                return true;

            if (collection.TryGetCount(out var collectionLength))
                return collectionLength == 0;

            var enumerator = collection.GetEnumerator();
            var result = !enumerator.MoveNext();
            enumerator.Dispose();
            return result;
        }
        public static bool IsNullOrEmpty<T>(this IReadOnlyCollection<T> collection) => collection == null || collection.Count == 0;
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection) => collection == null || collection.Count == 0;
        public static bool IsNullOrEmpty<T>(this T[] collection) => collection == null || collection.Length == 0;
        public static bool IsNullOrEmpty<T>(this Span<T> collection) => collection == null || collection.Length == 0;
        public static bool IsNullOrEmpty<T>(this ReadOnlySpan<T> collection) => collection == null || collection.Length == 0;

        public static bool IsNullOrHasLessThan(this IEnumerable collection, int count)
        {
            if (collection == null)
                return true;

            if (collection.TryGetCount(out var collectionLength))
                return collectionLength < count;

            var enumerator = collection.GetEnumerator();
            while (count > 0 && enumerator.MoveNext())
                count--;

            return count > 0;
        }
        public static bool IsNullOrHasLessThan<T>(this IEnumerable<T> collection, int count)
        {
            if (collection == null)
                return true;

            if (collection.TryGetCount(out var collectionLength))
                return collectionLength < count;

            var enumerator = collection.GetEnumerator();
            while (count > 0 && enumerator.MoveNext())
                count--;

            enumerator.Dispose();
            return count > 0;
        }
        public static bool IsNullOrHasLessThan<T>(this IReadOnlyCollection<T> collection, int count) => collection == null || collection.Count < count;
        public static bool IsNullOrHasLessThan<T>(this ICollection<T> collection, int count) => collection == null || collection.Count < count;
        public static bool IsNullOrHasLessThan<T>(this T[] collection, int count) => collection == null || collection.Length < count;
        public static bool IsNullOrHasLessThan<T>(this Span<T> collection, int count) => collection == null || collection.Length < count;
        public static bool IsNullOrHasLessThan<T>(this ReadOnlySpan<T> collection, int count) => collection == null || collection.Length < count;
        
        public static bool IsNullOrHasNotExactly(this IEnumerable collection, int count)
        {
            if (collection == null)
                return true;

            if (collection.TryGetCount(out var collectionLength))
                return collectionLength < count;

            var enumerator = collection.GetEnumerator();
            while (count >= 0 && enumerator.MoveNext())
                count--;

            return count != 0;
        }
        public static bool IsNullOrHasNotExactly<T>(this IEnumerable<T> collection, int count)
        {
            if (collection == null)
                return true;

            if (collection.TryGetCount(out var collectionLength))
                return collectionLength < count;

            var enumerator = collection.GetEnumerator();
            while (count >= 0 && enumerator.MoveNext())
                count--;

            enumerator.Dispose();
            return count != 0;
        }
        public static bool IsNullOrHasNotExactly<T>(this IReadOnlyCollection<T> collection, int count) => collection == null || collection.Count != count;
        public static bool IsNullOrHasNotExactly<T>(this ICollection<T> collection, int count) => collection == null || collection.Count != count;
        public static bool IsNullOrHasNotExactly<T>(this T[] collection, int count) => collection == null || collection.Length != count;
        public static bool IsNullOrHasNotExactly<T>(this Span<T> collection, int count) => collection == null || collection.Length != count;
        public static bool IsNullOrHasNotExactly<T>(this ReadOnlySpan<T> collection, int count) => collection == null || collection.Length != count;

        public static bool IsEmpty(this IEnumerable collection)
        {
            if (collection == null)
                throw Expt.Null(nameof(collection));

            if (collection.TryGetCount(out var collectionLength))
                return collectionLength == 0;

            var enumerator = collection.GetEnumerator();
            return !enumerator.MoveNext();
        }
        public static bool IsEmpty<T>(this IEnumerable<T> collection)
        {
            if (collection == null)
                throw Expt.Null(nameof(collection));

            if (collection.TryGetCount(out var collectionLength))
                return collectionLength == 0;

            var enumerator = collection.GetEnumerator();
            var result = !enumerator.MoveNext();
            enumerator.Dispose();
            return result;
        }
        public static bool IsEmpty<T>(this IReadOnlyCollection<T> collection) => collection == null ? throw Expt.Null(nameof(collection)) : collection.Count == 0;
        public static bool IsEmpty<T>(this ICollection<T> collection) => collection == null ? throw Expt.Null(nameof(collection)) : collection.Count == 0;
        public static bool IsEmpty<T>(this T[] collection) => collection == null ? throw Expt.Null(nameof(collection)) : collection.Length == 0;
        public static bool IsEmpty<T>(this Span<T> collection) => collection == null ? throw Expt.Null(nameof(collection)) : collection.Length == 0;
        public static bool IsEmpty<T>(this ReadOnlySpan<T> collection) => collection == null ? throw Expt.Null(nameof(collection)) : collection.Length == 0;

        public static bool HasLessThan(this IEnumerable collection, int count)
        {
            if (collection == null)
                throw Expt.Null(nameof(collection));

            if (collection.TryGetCount(out var collectionLength))
                return collectionLength < count;

            var enumerator = collection.GetEnumerator();
            while (count > 0 && enumerator.MoveNext())
                count--;

            return count > 0;
        }
        public static bool HasLessThan<T>(this IEnumerable<T> collection, int count)
        {
            if (collection == null)
                throw Expt.Null(nameof(collection));

            if (collection.TryGetCount(out var collectionLength))
                return collectionLength < count;

            var enumerator = collection.GetEnumerator();
            while (count > 0 && enumerator.MoveNext())
                count--;

            enumerator.Dispose();
            return count > 0;
        }
        public static bool HasLessThan<T>(this IReadOnlyCollection<T> collection, int count) => collection == null ? throw Expt.Null(nameof(collection)) : collection.Count < count;
        public static bool HasLessThan<T>(this ICollection<T> collection, int count) => collection == null ? throw Expt.Null(nameof(collection)) : collection.Count < count;
        public static bool HasLessThan<T>(this T[] collection, int count) => collection == null ? throw Expt.Null(nameof(collection)) : collection.Length < count;
        public static bool HasLessThan<T>(this Span<T> collection, int count) => collection == null ? throw Expt.Null(nameof(collection)) : collection.Length < count;
        public static bool HasLessThan<T>(this ReadOnlySpan<T> collection, int count) => collection == null ? throw Expt.Null(nameof(collection)) : collection.Length < count;

        public static bool HasMoreThan(this IEnumerable collection, int count)
        {
            if (collection == null)
                throw Expt.Null(nameof(collection));

            if (collection.TryGetCount(out var collectionLength))
                return collectionLength < count;

            var enumerator = collection.GetEnumerator();
            while (count >= 0 && enumerator.MoveNext())
                count--;

            return count < 0;
        }
        public static bool HasMoreThan<T>(this IEnumerable<T> collection, int count)
        {
            if (collection == null)
                throw Expt.Null(nameof(collection));

            if (collection.TryGetCount(out var collectionLength))
                return collectionLength < count;

            var enumerator = collection.GetEnumerator();
            while (count >= 0 && enumerator.MoveNext())
                count--;

            enumerator.Dispose();
            return count < 0;
        }
        public static bool HasMoreThan<T>(this IReadOnlyCollection<T> collection, int count) => collection == null ? throw Expt.Null(nameof(collection)) : collection.Count > count;
        public static bool HasMoreThan<T>(this ICollection<T> collection, int count) => collection == null ? throw Expt.Null(nameof(collection)) : collection.Count > count;
        public static bool HasMoreThan<T>(this T[] collection, int count) => collection == null ? throw Expt.Null(nameof(collection)) : collection.Length > count;
        public static bool HasMoreThan<T>(this Span<T> collection, int count) => collection == null ? throw Expt.Null(nameof(collection)) : collection.Length > count;
        public static bool HasMoreThan<T>(this ReadOnlySpan<T> collection, int count) => collection == null ? throw Expt.Null(nameof(collection)) : collection.Length > count;

        public static bool HasExactly(this IEnumerable collection, int count)
        {
            if (collection == null)
                throw Expt.Null(nameof(collection));

            if (collection.TryGetCount(out var collectionLength))
                return collectionLength < count;

            var enumerator = collection.GetEnumerator();
            while (count >= 0 && enumerator.MoveNext())
                count--;

            return count == 0;
        }
        public static bool HasExactly<T>(this IEnumerable<T> collection, int count)
        {
            if (collection == null)
                throw Expt.Null(nameof(collection));

            if (collection.TryGetCount(out var collectionLength))
                return collectionLength < count;

            var enumerator = collection.GetEnumerator();
            while (count >= 0 && enumerator.MoveNext())
                count--;

            enumerator.Dispose();
            return count == 0;
        }
        public static bool HasExactly<T>(this IReadOnlyCollection<T> collection, int count) => collection == null ? throw Expt.Null(nameof(collection)) : collection.Count == count;
        public static bool HasExactly<T>(this ICollection<T> collection, int count) => collection == null ? throw Expt.Null(nameof(collection)) : collection.Count == count;
        public static bool HasExactly<T>(this T[] collection, int count) => collection == null ? throw Expt.Null(nameof(collection)) : collection.Length == count;
        public static bool HasExactly<T>(this Span<T> collection, int count) => collection == null ? throw Expt.Null(nameof(collection)) : collection.Length == count;
        public static bool HasExactly<T>(this ReadOnlySpan<T> collection, int count) => collection == null ? throw Expt.Null(nameof(collection)) : collection.Length == count;
    }
}