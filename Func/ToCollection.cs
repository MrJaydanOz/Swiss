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
            Assert.IsNotNull(collection);
            if (collection == null)
                throw new NullReferenceException();

            TResult[] result = null;

            if (collection is ICollection<TSource> collectionAsCollection)
                result = new TResult[collectionAsCollection.Count];
            else if (collection is IReadOnlyCollection<TSource> collectionAsReadOnlyCollection)
                result = new TResult[collectionAsReadOnlyCollection.Count];

            return result ?? new List<TResult>(collection.Select(selector)).ToArray();
        }
    }
}