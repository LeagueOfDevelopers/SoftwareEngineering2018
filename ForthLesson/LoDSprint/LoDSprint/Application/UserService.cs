using LoDSprint.Exceptions;
using LoDSprint.Repositories;
using System;
using System.Collections.Generic;

namespace LoDSprint.Application
{
    public class UserService : IUserService
    {
        public UserService(InFileUsersRepository usersRepository)
        {
            _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
        }

        public IEnumerable<Word> GetUserLearnedWords(Guid getterId, Guid userId)
        {
            return (userId == getterId) 
                ? _usersRepository.LoadUser(userId).GetLearnedWords() 
                : throw new PermissionDeniedException(
                      $"User with id {getterId} doesn't have rights to get user learned words");
        }

        public IEnumerable<Word> GetUserStudiedWords(Guid getterId, Guid userId)
        {
            return (userId == getterId) 
                ? _usersRepository.LoadUser(userId).GetStudiedWords() 
                : throw new PermissionDeniedException(
                      $"User with id {getterId} doesn't have rights to get user studied words");
        }

        public Guid RegisterUser(string nickName)
        {
            var newUserId = Guid.NewGuid();
            var newUser = new User(
                newUserId,
                nickName,
                new List<Word>(),
                new Dictionary<Word, int>()
                );

            _usersRepository.SaveUser(newUser);

            return newUserId;
        }

        private readonly InFileUsersRepository _usersRepository;
    }
}
