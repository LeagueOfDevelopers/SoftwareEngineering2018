namespace English.Domain
{
    public interface IWord : IStoredItem
    {
        string Body { get; }

        string Translation { get; }

        int CountToBeLearned { get; }
    }
}
