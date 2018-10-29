using System.Collections;
using System.Collections.Generic;

namespace LeonLearn
{
    public interface IWordsRepository
    {
        IEnumerable<WordPair> GetWords(int amount);
        IEnumerable<WordPair> GetAllWords();
        IEnumerable<WordPair> GetRandomPairWords(int amount);
        void AddWordPairs(IEnumerable<WordPair> wordsWithTranslations);
        void AddWordPair(WordPair wordPair);
        IEnumerable<WordPair> GetUnlearnedWords(IEnumerable<WordPair> learnedWords);
        WordPair GetRandomPairFromSource(IEnumerable<WordPair> sourceWords);
        void RemoveWords();
    }
}