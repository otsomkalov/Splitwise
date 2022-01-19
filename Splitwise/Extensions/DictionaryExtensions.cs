using System.Collections.Generic;

namespace Splitwise.Extensions
{
    internal static class DictionaryExtensions
    {
        internal static void Add<TKey, TValue>(this IDictionary<TKey, TValue> source,
            IDictionary<TKey, TValue> values)
        {
            foreach (var pair in values)
            {
                source.Add(pair.Key, pair.Value);
            }
        }
    }
}