namespace LoDSprint.Repositories
{
    public interface IInFileDictionaryRepository
    {
        Translation GetWordTranslation(Word word);
        Word GetRandomWord();
        void SaveDictionaryPair(Word word, Translation translation);
    }
}
