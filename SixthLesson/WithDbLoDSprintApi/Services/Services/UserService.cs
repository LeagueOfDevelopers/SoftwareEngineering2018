using System;
using System.Collections.Generic;
using BusinessEntities;
using BusinessServices.Interfaces;
using Data.Interfaces;

namespace BusinessServices.Services
{
    public class UserService : IUserService
    {
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public IEnumerable<Word> GetUserLearnedWords(Guid userId)
        {
            var user = _userRepository.LoadUser(userId);
            return user.LearnedWords;
        }

        public IEnumerable<StudiedWord> GetUserStudiedWords(Guid userId)
        {
            var user = _userRepository.LoadUser(userId);
            return user.StudiedWords;
        }

        public Guid RegisterUser(string nickName)
        {
            var userId = Guid.NewGuid();
            var newUser = new TraineeUser(
                userId,
                nickName,
                new List<Word>(),
                new List<StudiedWord>()
            );
            _userRepository.SaveUser(newUser);
            return userId;
        }

        private readonly IUserRepository _userRepository;
    }
}
