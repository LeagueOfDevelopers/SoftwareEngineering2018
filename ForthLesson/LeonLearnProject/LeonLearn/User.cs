using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LeonLearn
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public DateTimeOffset RegisterDate { get; private set; }
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
                int index = InProgressWords.FindIndex(pair => pair == markedPair);

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
            if (obj.GetType() != typeof(User)) return false;
            return Id == ((User) obj).Id;
        }

        public static bool operator ==(User first, User second)
        {
            return first.Id == second.Id;
        }

        public static bool operator !=(User first, User second)
        {
            return !(first == second);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}