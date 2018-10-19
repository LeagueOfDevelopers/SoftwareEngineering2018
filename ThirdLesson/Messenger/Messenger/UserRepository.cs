using System;
using System.Collections.Generic;

namespace Messenger
{
   public class UserRepository : IUserRepository
   {
      private readonly List<User> _users = new List<User>();
      
      public bool Exists(User user)
      {
         return _users.Find(usr => usr == user) != null;
      }

      public void Add(User user)
      {
         _users.Add(user);
      }

      public void Remove(User user)
      {
         _users.Remove(user);
      }

      public List<User> GetAll()
      {
         return _users;
      }
   }
}