using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordGame.Application
{
    public class GameService : IGameService
    {
        public GameService(IUserRepository userRepository, IDictionaryRepository dictionaryRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _dictionaryRepository = dictionaryRepository ?? throw new ArgumentNullException(nameof(dictionaryRepository));
        }

        public WordForGame GetWord(Guid idOfUser)
        {
            WordForGame word = _dictionaryRepository.GetRandomWord();
            User user = _userRepository.LoadUser(idOfUser);
            return (user.WordAlreadyStudied(word.Word))
                ? GetWord(idOfUser)
                : word;
        }
        public void CheckAnswer(Guid idOfUser, WordForGame word)
        {
            if (word.CheckAnswer())
            {
                SaveResults(true, idOfUser, word);
            }
            SaveResults(false, idOfUser, word);
        }
        private void SaveResults(bool CheckAnswer, Guid idOfUser, WordForGame word)
        {
            User user = _userRepository.LoadUser(idOfUser);
            if(user.WordStudiedNow(word))
            {
                user.StudiedWords.Add(word.Word);
            }
            else
            {
                user.AddOneValueToProcess(word);
            }
            _userRepository.UpdateUser(user);
        }
 
       
        private readonly IUserRepository _userRepository;
        private readonly IDictionaryRepository _dictionaryRepository;
    }
}
