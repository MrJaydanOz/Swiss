using System.Collections.Generic;
using System.Text;

namespace Swiss
{
    public static partial class Func
    {
        public static string JoinToString(this IEnumerable<string> collection) => collection == null ? "" : new StringBuilder().AppendJoin(null, collection).ToString();
        public static string JoinToString(this IEnumerable<string> collection, char separator) => collection == null ? "" : new StringBuilder().AppendJoin(separator, collection).ToString();
        public static string JoinToString(this IEnumerable<string> collection, string separator) => collection == null ? "" : new StringBuilder().AppendJoin(separator, collection).ToString();
        public static string JoinToString<T>(this IEnumerable<T> collection) => collection == null ? "" : new StringBuilder().AppendJoin(null, collection).ToString();
        public static string JoinToString<T>(this IEnumerable<T> collection, char separator) => collection == null ? "" : new StringBuilder().AppendJoin(separator, collection).ToString();
        public static string JoinToString<T>(this IEnumerable<T> collection, string separator) => collection == null ? "" : new StringBuilder().AppendJoin(separator, collection).ToString();
    }
}