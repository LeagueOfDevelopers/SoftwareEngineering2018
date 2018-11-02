using System;

namespace EnglishTrainer
{
    public interface IUserService
    {
        WordsRepository _wordsRepository { get; set; }
        UserRepository _userRepository { get; set; }

        User Registration(string name);

        void AddWordAtUserWordsRepository(User user, Word word);

        int FindWord(User user, Word word);

        void AddWordAtWordsRepository(string en, string ru);
    }
}
