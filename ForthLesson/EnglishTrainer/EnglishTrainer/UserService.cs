using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTrainer
{
    public class UserService
    {
        public WordsRepository _wordsRepository { get; set; }
        public UserRepository _userRepository { get; set; }

        public UserService(WordsRepository wordsRepository, UserRepository userRepository)
        {
            _wordsRepository = wordsRepository;
            _userRepository = userRepository;
        }

        public User Registration(string name)
        {
            List<Word> words = new List<Word>() { };
            var newuser = new User(name, Guid.NewGuid(), words);
            _userRepository.SaveUser(newuser);
            return newuser;
        }


        public void AddWordAtUserWordsRepository(User user, Word word)
        {
            var users = _userRepository.GetUsers();
            users.ForEach((itemUser) => 
            {
                if (itemUser.Id == user.Id)
                {
                    var foundUser = itemUser;
                    var indexOfFoundWord = FindWord(foundUser, word);

                    if (indexOfFoundWord > 0)
                    {
                        var rightChoises = foundUser._inMemoryWords[indexOfFoundWord]._rightChoices;
                        var newRightChoise = rightChoises + 1;
                        Word newWord = new Word(foundUser._inMemoryWords[indexOfFoundWord].Id_Word, 
                                                 foundUser._inMemoryWords[indexOfFoundWord]._enWord, 
                                                 foundUser._inMemoryWords[indexOfFoundWord]._ruWord, 
                                                newRightChoise
                                                );
                        foundUser._inMemoryWords.RemoveAt(indexOfFoundWord);
                        foundUser._inMemoryWords.Insert(indexOfFoundWord, newWord);
                    }

                    else foundUser._inMemoryWords.Add(word);
                }
            });
            _userRepository.SaveUsers(users);
        }

        public int FindWord(User user, Word word)
        {
            return user._inMemoryWords.FindIndex(item => item.Id_Word == word.Id_Word);
          
        }

        public void AddWordAtWordsRepository(string en, string ru)
        {
            var newWord = new Word(Guid.NewGuid(), en, ru, 0);
            _wordsRepository.SaveWord(newWord);
        }

    }
}

