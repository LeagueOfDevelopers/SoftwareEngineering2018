using LoDSprintApi.Models;

namespace LoDSprintApi.Repositories
{
    public interface IDictionaryRepository
    {
        WordModel GetRandomWord();
        TranslationModel GetWordTranslation(WordModel word);
        void SaveDictionaryPair(DictionaryPairModel dictionaryPair);
    }
}