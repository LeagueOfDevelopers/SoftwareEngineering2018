using System;
using EnglishTrainer.Repositories;

namespace EnglishTrainer.Application
{
   public class UserService
   {
      private readonly UserRepository _userRepository;

      public UserService(UserRepository userRepository = null)
      {
         _userRepository = userRepository ?? new UserRepository();
      }

      public Guid Register(string name)
      {
         var guid = Guid.NewGuid();
         _userRepository.Save(new User(guid, name));
         return guid;
      }
   }
}