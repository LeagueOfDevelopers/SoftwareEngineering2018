using System;
using System.Collections.Generic;
using English.Domain.Exception;

namespace English.Domain
{
    public class User : IUser
    {
        public User(
            Guid id,
            string name, 
            Dictionary<Word, int> learningWords,
            Dictionary<Word, int> learnedWords)
        {
            Id = id;
            Name = name;
            LearningWords = learningWords ?? throw new ArgumentNullException(nameof(learningWords));
            LearnedWords = learnedWords ?? throw new ArgumentNullException(nameof(learnedWords));

            if (name == "")
            {
                throw new NameOfUserIsEmptyException();
            }
        }

        public Guid Id { get; }

        public string Name { get; }
        
        public Dictionary<Word, int> LearningWords { get; set; }
        
        public Dictionary<Word, int> LearnedWords { get; set; }

        public void UpdateLearningWord(Word word)
        {
            LearningWords[word]++;

            if (LearningWords[word] >= word.CountToBeLearned)
            {
                LearningWords.Remove(word);
                LearnedWords.Add(word, word.CountToBeLearned);
            }
        }
    }
}
