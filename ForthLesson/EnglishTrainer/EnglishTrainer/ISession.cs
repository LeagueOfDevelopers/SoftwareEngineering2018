using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTrainer
{
    interface ISession
    {
        User User { get; set; }
        WordsRepository _wordsRepository { get; set; }
        UserService _userService { get; set; }

        int FindWord(User user, Word word);

        bool IsLearned(User user, Word word);

        bool Select(int i, int indexoftrainword);

        void ShowWords(List<Word> words_for_training, Word training_word);

        void PlusRightChoice(User user, Word word);

        bool Session(User user);

    }
}
