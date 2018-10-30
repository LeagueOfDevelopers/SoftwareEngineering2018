using System;
using System.Collections.Generic;
using System.Linq;

namespace EnglishTrainer.Repositories
{
   public class UserRepository : IRepository<User>
   {
      private readonly List<User> _users;

      public UserRepository(List<User> users = null)
      {
         _users = users ?? new List<User>();
      }

      public User Get(Guid id)
      {
         return _users.FirstOrDefault(user => user.Id == id);
      }

      public void Save(User user)
      {
         if (!_users.Contains(user))
            _users.Add(user);
      }
   }
}