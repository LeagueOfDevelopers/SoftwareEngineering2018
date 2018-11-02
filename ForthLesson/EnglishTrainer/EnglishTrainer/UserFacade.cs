using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTrainer
{
    public class UserFacade
    {
        UserService _userService { get; set; }
        Session _session { get; set; }

        public UserFacade(UserService userService, Session session)
        {
            _userService = userService;
            _session = session;
        }


        public void Registration(string name)
        {
            _userService.Registration(name);
        }

        public void AddWordAtUserWordsRepository(User user, Word word)
        {
            _userService.AddWordAtUserWordsRepository(user, word);
        }

        public void AddWordWordsRepository(string enWord, string ruWord)
        {
            _userService.AddWordAtWordsRepository(enWord, ruWord);
        }

        public bool StartSession(User user)
        {
            return _session.Train(user);
        }
    }
}
