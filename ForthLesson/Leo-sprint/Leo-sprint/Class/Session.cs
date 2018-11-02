using System;
using System.Collections.Generic;

namespace Leo_sprint
{
    public class Session
    {
        private Word[] _words;
        private Random randomizer;

        public Session(Word[] words, Random randomizer)
        {
            _words = words;
            this.randomizer = randomizer;
        }

        public static Session Create(Word[] words)
        {
            Word[] words_in_tasks = new Word[words.Length];
            var random = new Random();
            for (int i = 0; i < words.Length; i++)
            {
                if (random.Next(2) == 1)
                {
                    words_in_tasks[i] = GetWordWithWrongTranslation(words, i, random);
                }
            }

            words_in_tasks = MixedWords(words_in_tasks, random);
            return new Session(words_in_tasks, random);
        }    

        private static Word[] MixedWords(Word[] words_in_tasks, Random random)
        {
            var new_words = words_in_tasks;
            Word temp;
            int random_index;
            for (int i = words_in_tasks.Length; i > 2; i++)
            {
                random_index = random.Next(i) + 1;
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
            var task = new List<string>();
            foreach (var word in _words)
            {
                task.Add(word._in_english + "-" + word._in_russian);
            }
            return task;
        }

        public string ShowWhatsWrongInAnswers(List<Word> wrong_answers)
        {
            var words = $"You answered wrong. Right translations are\n";
            foreach (var Word in wrong_answers)
            {
                words  += Word._in_russian + "\n" ;
            }
            return words;
        }

        private static Word GetWordWithWrongTranslation(Word[] words, int index, Random random)
        {
            var first_index = index;
            var second_index = random.Next(0, words.Length);
            while (first_index == second_index)
            {
                second_index = random.Next(0, words.Length);
            }
            return new Word(words[first_index]._in_english, words[second_index]._in_russian, 0);

        }

    }
}
