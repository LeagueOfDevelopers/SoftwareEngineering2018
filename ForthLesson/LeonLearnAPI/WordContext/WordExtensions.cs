using System;
using System.Collections.Generic;
using System.Linq;

namespace WordContext
{
    public static class WordExtensions
    {
        public static WordPair GetRandom(this IEnumerable<WordPair> sourceWords)
        {
            Random r = new Random();
            return sourceWords
                .ToArray()
                [r.Next(sourceWords.Count())];
        }
    }
}