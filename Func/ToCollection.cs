using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace Swiss
{
    public static partial class Func
    {
        public static TResult[] ToArray<TSource, TResult>(IEnumerable<TSource> collection, Func<TSource, TResult> selector)
        {
            if (collection == null)
                throw new NullReferenceException();
            if (selector == null)
                throw new NullReferenceException();

            TResult[] result = null;

            if (collection is ICollection<TSource> collectionAsCollection)
                result = new TResult[collectionAsCollection.Count];
            else if (collection is IReadOnlyCollection<TSource> collectionAsReadOnlyCollection)
                result = new TResult[collectionAsReadOnlyCollection.Count];

            if (result == null)
                return new List<TResult>(collection.Select(selector)).ToArray();

            var enumerator = collection.GetEnumerator();
            for (int i = 0; i < result.Length; i++)
            {
                enumerator.MoveNext();
                result[i] = selector.Invoke(enumerator.Current);
            }
            enumerator.Dispose();

            return result;
        }

        public static HashSet<TResult> ToHashSet<TSource, TResult>(IEnumerable<TSource> collection, Func<TSource, TResult> selector)
        {
            if (collection == null)
                throw new NullReferenceException();

            HashSet<TResult> result;

            if (collection is ICollection<TSource> collectionAsCollection)
                result = new HashSet<TResult>(collectionAsCollection.Count);
            else if (collection is IReadOnlyCollection<TSource> collectionAsReadOnlyCollection)
                result = new HashSet<TResult>(collectionAsReadOnlyCollection.Count);
            else
                result = new HashSet<TResult>();

            foreach (var item in collection)
                result.Add(selector.Invoke(item));

            return result;
        }
    }
}