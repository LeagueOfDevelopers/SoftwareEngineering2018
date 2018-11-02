using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoDSprintApi.Models
{
    public class DictionaryPairModel
    {
        public DictionaryPairModel(WordModel word, TranslationModel translation)
        {
            Word = word ?? throw new ArgumentNullException(nameof(word));
            Translation = translation ?? throw new ArgumentNullException(nameof(translation));
        }

        public WordModel Word { get; }

        public TranslationModel Translation { get; }
    }
}
