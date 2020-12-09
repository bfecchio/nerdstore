using System;
using System.Collections.Generic;

namespace NerdStore.Core.Extensions
{
    public static class EnumerableExtensions
    {
        #region Extension Methods

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
                action(item);
        }

        #endregion
    }
}
