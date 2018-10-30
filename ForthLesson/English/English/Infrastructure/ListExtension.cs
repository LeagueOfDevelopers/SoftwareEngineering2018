using System;
using System.Collections.Generic;
using System.Linq;

namespace English.Infrastructure
{
    public static class ListExtension
    {
        public static List<T> ShuffleWordsAndTakeKeys<T>(this List<KeyValuePair<T, int>> input, int amount)
        {
            var rand = new Random();
            return input.OrderBy(_ => rand.Next())
                .Take(amount)
                .Select(x => x.Key)
                .ToList();
        }
    }
}
