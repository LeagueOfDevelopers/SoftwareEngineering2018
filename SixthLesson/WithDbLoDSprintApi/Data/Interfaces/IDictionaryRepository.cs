using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Data.Interfaces
{
    public interface IDictionaryRepository
    {
        Word LoadRandomWord();
        Translation GetWordTranslation(Word word);
        void SaveDictionaryPair(DictionaryPair dictionaryPair);
        void DeleteDictionaryPair(Guid pairId);
        IEnumerable<DictionaryPair> LoadDictionary();
    }
}
