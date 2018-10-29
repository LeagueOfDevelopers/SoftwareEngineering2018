using Microsoft.VisualBasic.CompilerServices;

namespace LeonLearn
{
    public class WordPair
    {
        public string Origin { get; private set; }
        public string Translation { get; private set; }

        public WordPair(string origin, string translation)
        {
            Origin = origin;
            Translation = translation;
        }

        public override bool Equals(object obj)
        {
            var second = (WordPair) obj;
            return Origin == second.Origin && Translation == second.Translation;
        }

        public override int GetHashCode()
        {
            return (Origin + Translation).GetHashCode();
        }

        public static bool operator ==(WordPair first, WordPair second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(WordPair first, WordPair second)
        {
            return !first.Equals(second);
        }

        public override string ToString()
        {
            return $"Origin: {Origin}, translation: {Translation}.";
        }
    }
}