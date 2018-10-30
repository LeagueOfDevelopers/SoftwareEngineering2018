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
                if (random.Next(1) == 0)
                {
                    words_in_tasks[i] = GetWordWithWrongTranslation(words, i, random);
                }
            }

            words_in_tasks = MixedWords(words_in_tasks, random);
            return new Session(words_in_tasks, random);



        }
        public void Show()
        {
            var user_answers = PrintWordsAndGetAnswers(_words);
            var wrong_answers = CheckAnswers(user_answers, words, _words);
            ShowWhatsWrongInAnswers(wrong_answers);
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

        private void ShowWhatsWrongInAnswers(List<Word> wrong_answers)
        {
            foreach(var Word in wrong_answers)
            {
                Console.WriteLine("You answered wrong. Right translation is"+ Word._in_russian);
            }
        }

        private bool[] PrintWordsAndGetAnswers(Word[] _words)
        {
            var size = _words.Length;
            var i = 0;
            var answers = new bool[size];
            while (i < size)
            {
                PrintQuestion(_words[i]);
                ConvertAnswer(GetAnswers());
            }
            return answers;

        }

        private bool ConvertAnswer(string answer)
        {
            switch (answer)
            {
                case "Да":
                    return true;
                case "Нет":
                    return false;
                default:
                    return false;
            }
        }

        private string GetAnswers()
        {
            return Console.ReadLine();
        }


        private void PrintQuestion(Word word)
        {
            Console.WriteLine(word._in_english + "-" + word._in_russian + "is right?");
        }

        private List<Word> CheckAnswers(bool[] user_answers, Word[] words, Word[] words_in_question)//неправильный алгоритм
        {

            var wrong_answers = new List<Word>();
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Equals(words_in_question[i]) != user_answers[i])
                {
                    wrong_answers.Add(words[i]);
                }
            }
            return wrong_answers;
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
