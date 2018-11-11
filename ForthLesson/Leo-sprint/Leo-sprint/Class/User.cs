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
        public IEnumerator<Word> _learned_words => learned_words.GetEnumerator();
        public IEnumerator<Word> _words_in_process => words_in_process.GetEnumerator();

        public UserPub ToUserPub()
        {
            var l_word = new List<string>();
            var p_words = new List<string>();
            if (learned_words != null)
                l_word = learned_words.Select(m => m._in_english + "." + m._in_russian + "." + m._flag).ToList();
            if (words_in_process!=null)
                p_words = words_in_process.Select(m => m._in_english + "." + m._in_russian + "." + m._flag).ToList();
            return new UserPub(_nickname, _id, l_word, p_words );
        }

        public User(string nickname, Guid id, List<Word> learned_words, List<Word> words_in_process)
        {
            _nickname = nickname;
            _id = id;
            this.learned_words = learned_words;
            this.words_in_process = words_in_process;
        }

        public Session StartSession(int number_of_words)
        {
            var words = GetSomeWords(number_of_words);
            return Session.Create(words);
        }

        private Word[] GetSomeWords(int number_of_words)
        {
            if (number_of_words > words_in_process.Count())
            {
                return words_in_process.ToArray();
            }
            var words = words_in_process;
            var count = words.Count() - number_of_words;
            var random = new Random();
            for (int i = 0; i < count; i++)
            {                
               var random_index = random.Next(words.Count());
                words.RemoveAt(random_index);                    
            }
            return words.ToArray();
        }

        public void AddNewWordInDictionary(Word word)
        {
            if (!learned_words.Contains(word))
            {
                words_in_process.Add(word);
            }

        }
        public void RemoveWordFromDictionary(Word word)
        {
            if (!learned_words.Contains(word))
            {
                words_in_process.Remove(word);
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

        public List<Word> CheckAnswers(bool[] user_answers, Word[] words_in_question)
        {
            var right_words = new List<Word>();
            var answers = words_in_question
                .Zip(user_answers, (word, answer) => { return new KeyValuePair<Word, bool>(word, answer); })
                .ToDictionary(item => item.Key, item => item.Value);
            var wrong_answers = new List<Word>();
            foreach (var word in words_in_question)
            {
               if ( words_in_process.Contains(word) == !answers[word])
                {
                    var right_word = words_in_process.First(this_word => word._in_english == this_word._in_english);
                        wrong_answers.Add(right_word);
                }               
                else right_words.Add(word);
            }
            foreach (var word in right_words)
            {
                FlagGrowsOnePoint(word);
            }
            return wrong_answers;
        }

        private void FlagGrowsOnePoint(Word word)
        {
            var new_word = new Word(word._in_english, word._in_russian, word._flag + 1);
            words_in_process.Remove(new_word);
            if (!IsWordLearned(new_word))
            {
                words_in_process.Add(new_word);
            }
            else
                learned_words.Add(new_word);
        }

        private bool IsWordLearned(Word new_word)
        {
            if (new_word._flag < 3) return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            var user = obj as User;
            return user != null &&
                   _nickname == user._nickname &&
                   _id.Equals(user._id) &&
                   EqualityComparer<List<Word>>.Default.Equals(learned_words, user.learned_words) &&
                   EqualityComparer<List<Word>>.Default.Equals(words_in_process, user.words_in_process);
        }
    }
}
