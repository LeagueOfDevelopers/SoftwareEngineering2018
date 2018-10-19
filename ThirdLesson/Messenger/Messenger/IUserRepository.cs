using System;
using System.Collections.Generic;

namespace Messenger
{
   public interface IUserRepository
   {
      bool Exists(User user);
      
      void Add(User user);
      void Remove(User user);
   }
}