using System;

namespace WordContext
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
            return String.Equals(Origin, second.Origin, StringComparison.CurrentCultureIgnoreCase) 
                   && String.Equals(Translation, second.Translation, StringComparison.CurrentCultureIgnoreCase);
        }

        public override int GetHashCode()
        {
            //return (Origin.ToLower() + Translation.ToLower()).GetHashCode();
            return base.GetHashCode();
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