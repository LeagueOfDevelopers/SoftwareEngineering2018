using System;
using System.Collections.Generic;
using WordContext;

namespace UserContext
{
    public class User
    {
        public readonly Guid Id; //{ get; private set; }
        public string Name { get; private set; }
        public readonly DateTimeOffset RegisterDate; //{ get; private set; }
        public List<WordPair> InProgressWords { get; private set; }
        public List<int> InProgressCounter { get; private set; }
        public List<WordPair> LearnedWords { get; private set; }

        public User(
            Guid id,
            string name,
            DateTimeOffset registerDate,
            List<WordPair> inProgressWords,
            List<int> inProgressCounter,
            List<WordPair> learnedWords
        )
        {
            Id = id;
            Name = name;
            RegisterDate = registerDate;
            InProgressWords = inProgressWords;
            InProgressCounter = inProgressCounter;
            LearnedWords = learnedWords;

            if (inProgressWords.Count != inProgressCounter.Count)
                throw new ArrayTypeMismatchException("Words in progress should be the same length as their counters array");
        }

        public void MarkWord(WordPair markedPair)
        {
            if (InProgressWords.Contains(markedPair))
            {
                var index = InProgressWords.FindIndex(pair => pair == markedPair);

                if (InProgressCounter[index] == 2)
                {
                    InProgressWords.RemoveAt(index);
                    InProgressCounter.RemoveAt(index);

                    LearnedWords.Add(markedPair);
                }
                else
                {
                    InProgressCounter[index]++;
                }
            }
            else
            {
                InProgressWords.Add(markedPair);
                InProgressCounter.Add(1);
            }
        }

        public override bool Equals(object obj)
        {
            try
            {
                var second = (User) obj;
                return Equals(Id, second.Id);
            }
            catch
            {
                return false;
            }
        }

        public static bool operator ==(User first, User second)
        {
            return second != null && first != null && Equals(first.Id, second.Id);
        }

        public static bool operator !=(User first, User second)
        {
            return !first.Equals(second);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}