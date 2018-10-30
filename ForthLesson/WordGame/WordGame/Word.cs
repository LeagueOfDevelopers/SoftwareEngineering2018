using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordGame
{
    public class Word
    {
        public Word(string currentWord, string translation)
        {
            CurrentWord = currentWord ?? throw new ArgumentNullException(nameof(currentWord));
            Translation = translation ?? throw new ArgumentNullException(nameof(translation));
        }

        public string CurrentWord { get; }
        public string Translation { get; }
    }
}
