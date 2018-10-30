using System;
using System.Collections.Generic;
using System.Linq;

namespace Leo_sprint
{
    public class User : IUser
    {
        public string _nickname { get; }
        public Guid _id { get; }

        private List<Word> learned_words;
        private List<Word> words_in_process;

        public Session StartSession()
        {
            return Session.Create(words_in_process.ToArray());
        }
        public void AddNewWordInDictionary(Word word)
        {
            if (!learned_words.Contains(word))
            {
                words_in_process.Add(word);
            }
            
        }
       public IEnumerable<Word> ShowWordInProgress()
       {
           return words_in_process.AsEnumerable();
       }
       public IEnumerable<Word> ShowLearnedWord()
       {
           return learned_words.AsEnumerable();
       }
       
       public List<Word> CheckAnswers(bool[] user_answers, Word[] words_in_question)//неправильный алгоритм
        {
            var answers = new Dictionary<Word, bool>();
            for (int i=0;i<user_answers.Count(); i++)
            {
                answers.Add(words_in_process[i], user_answers[i]);
            }
            var wrong_answers = new List<Word>();

            foreach (var word in words_in_process)
            {
                if (!answers.ContainsKey(word))
                {
                    var checked_word = answers.Single(s => s.Key._in_english==word._in_english).Key;
                    if (answers[checked_word]==true)
                    {
                        wrong_answers.Add(word);
                    }
                }
                if (answers.ContainsKey(word) && answers[word] == false)
                {
                    wrong_answers.Add(word);
                }

            }
            return wrong_answers;
        }

    }
}
