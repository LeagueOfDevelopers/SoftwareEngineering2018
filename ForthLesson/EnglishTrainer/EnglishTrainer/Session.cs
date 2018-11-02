using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTrainer
{
    public class Session : ISession
    {
        public User User { get; set; }
        public WordsRepository _wordsRepository { get; set; }
        public UserRepository _userRepository { get; set; }
        public UserService _userService { get; set; }

        public Session(User user, WordsRepository wordsRepository, UserService userService)
        {
            User = user;
            _wordsRepository = wordsRepository;
            _userService = userService;
        }

        public List<Word> TrainWords(User user)
        {
            List<Word> need_arr = new List<Word>();

            var wordsRepository = _wordsRepository.GetWords();
            while (need_arr.Count < 3)
            {
                Random rnd = new Random();
                var numbofword = rnd.Next(0, wordsRepository.Count);

                var wordinlearning = _wordsRepository.GetWords()[numbofword];


                if (!IsLearned(user, wordinlearning))
                {
                    need_arr.Add(wordsRepository[numbofword]);
                }
                wordsRepository.RemoveAt(numbofword);
            }

            return need_arr;
        }

        public bool Select(int i, int indexoftrainword)
        {
            return i == indexoftrainword;
        }

        public void ShowWords(List<Word> trainwords, int indextword)
        {

            Console.WriteLine("Translate - {0} \n", trainwords[indextword]._enWord);
            int i = 0;
            foreach (Word item in trainwords)
            {
                i++;
                {
                    Console.WriteLine("{0}.{1}", i, item._ruWord);
                }
            }

        }

        public bool Train(User user)
        {
            Random rnd = new Random();
            var trainwords = TrainWords(user);
            var indexoftrainword = rnd.Next(0, 2);
            var trainingword = trainwords[indexoftrainword];

            ShowWords(trainwords, indexoftrainword);


            int i = Convert.ToInt32(Console.ReadLine());
            var select = Select(i, indexoftrainword + 1);
            if (select)
            {
                Plus(user, trainingword);
            }
            return select;

        }

        public bool IsLearned(User user, Word word)
        {
            var a = user._inMemoryWords.Where((itemWord) => (itemWord.Id_Word == word.Id_Word) && (itemWord._rightChoices >= 3));
            return a.Contains(word) ? true : false;

        }


        public int Findword(User user, Word word)
        {
            user._inMemoryWords.ForEach((itemWord) =>
            {
                if (itemWord._enWord == word._enWord && itemWord._ruWord == word._ruWord)
                {
                    return user._inMemoryWords.IndexOf(itemWord);
                }
            });
            return -1;

        }

        public void Plus(User user, Word word)
        {
            List<Word> learnedwords = new List<Word>();
            var mark = Findword(user, word);
            if (mark >= 0)
            {
                var oldword = user._inMemoryWords[mark];
                var newchoises = oldword._rightChoices + 1;
                Word newword = new Word(oldword.Id_Word, oldword._enWord, oldword._ruWord, newchoises);
                user._inMemoryWords.RemoveAt(mark);
                user._inMemoryWords.Insert(mark, newword);
            }

            else
            {
                word._rightChoices++;
                user._inMemoryWords.Add(word);
            }
        }

        public int FindWord(User user, Word word)
        {
            throw new NotImplementedException();
        }

        public void ShowWords(List<Word> words_for_training, Word training_word)
        {
            throw new NotImplementedException();
        }

        public void PlusRightChoice(User user, Word word)
        {
            throw new NotImplementedException();
        }

        bool ISession.Session(User user)
        {
            throw new NotImplementedException();
        }
    }
}