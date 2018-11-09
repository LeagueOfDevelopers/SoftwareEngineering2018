using System.Collections.Generic;

namespace WordContext
{
    public interface IWordsRepository
    {
        IEnumerable<WordPair> GetAllWords();
        IEnumerable<WordPair> GetRandomPairWords(int amount);
        void AddWordPairs(IEnumerable<WordPair> wordsWithTranslations);
        void AddWordPair(WordPair wordPair);
        IEnumerable<WordPair> GetUnlearnedWords(IEnumerable<WordPair> learnedWords);
        void RemovePair(WordPair wordPair);
    }
}