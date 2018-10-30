using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordGame.Application
{
    class UserService : IUserService
    {
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
        public Guid RegisterUser(string name)
        {
            Guid id = Guid.NewGuid();
            Dictionary<string, int> InProcessWords = new Dictionary<string, int>();
            List<string> StudiedWords = new List<string>();
            User user = new User(id, name, InProcessWords, StudiedWords);
            _userRepository.SaveUser(user);
            return id;
        }
        public IEnumerable<string> GetInProcessWords(Guid idUser)
        {
            User user = _userRepository.LoadUser(idUser);
            IEnumerable<string> InProcessWords = user.InProcessWords.Select(words => words.Key);
            return InProcessWords;
        }
        public List<string> GetStudiedWords(Guid idUser)
        {
            User user = _userRepository.LoadUser(idUser);
            return user.StudiedWords;
        }
        private readonly IUserRepository _userRepository;
    }
}
