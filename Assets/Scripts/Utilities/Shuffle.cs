using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static partial class Utilities
    {
        /// <summary>
        /// Shuffles the elements in the given list.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns>Shuffled passed collection.</returns>
        public static IList<T> Shuffle<T>(this IList<T> collection)
        {
            return PerformShuffle(collection);
        }

        /// <summary>
        /// Shuffles the elements in the given array.
        /// </summary>
        /// <returns>Shuffled passed array.</returns>
        public static T[] Shuffle<T>(this T[] collection)
        {
            return (T[]) PerformShuffle(collection);
        }

        private static IList<T> PerformShuffle<T>(IList<T> collection)
        {
            var collectionLength = collection.Count;

            if (collectionLength < 2)
                return collection;

            for (var i = 0; i < collectionLength - 2; i++)
            {
                var randomIndex = Random.Range(i, collectionLength);
                var elementTemp = collection[randomIndex];
                collection[randomIndex] = collection[i];
                collection[i] = elementTemp;
            }

            return collection;
        }
    }
}