using System;
using System.Collections.Generic;
using System.Linq;

namespace Leo_sprint
{
    public class Session
    {
        private Word[] words;
        private Random randomizer;
        private bool[] answers;
        public Guid _id { get; }

        public Session(Word[] words, Random randomizer, Guid id)
        {
            this.words = words;
            this.randomizer = randomizer;
            _id = id;
        }

        public static Session Create(Word[] words)
        {
            var random = new Random();
            var id = Guid.NewGuid();
            var words_in_tasks = words.Select(word =>
            {
                if (random.Next(2) == 1)
                {
                    word = GetWordWithWrongTranslation(words, word, random);
                }
                return word;
            }).ToArray();
            words_in_tasks = MixedWords(words_in_tasks, random);
            return new Session(words_in_tasks, random,id);
        }

        private static Word[] MixedWords(Word[] words_in_tasks, Random random)
        {
            var new_words = words_in_tasks;
            Word temp;
            int random_index;
            for (int i = words_in_tasks.Length - 1; i > 2; i--)
            {
                random_index = random.Next(i);
                if (random_index != i)
                {
                    temp = new_words[i];
                    new_words[i] = new_words[random_index];
                    new_words[random_index] = temp;
                }
            }
            
            return new_words;
        }
        public IEnumerable<string> ShowTask()
        {
            var task = words.Select(word => word._in_english + "-" + word._in_russian);
            return task;
        }
        public void GetAnswers(bool[] answers)
        {
            this.answers = answers;
        }

        public List<Word> CheckAnswers(User user)
        {
            return user.CheckAnswers(answers, words);

        }

        private static Word GetWordWithWrongTranslation(Word[] words, Word word, Random random)
        {
            var index = random.Next(0, words.Length);
            while (words[index]._in_russian == word._in_russian)
            {
                index = random.Next(0, words.Length);
            }
            return new Word(word._in_english, words[index]._in_russian, 0);
        }
    }
}
