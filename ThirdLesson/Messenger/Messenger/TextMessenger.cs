using System;

namespace Messenger
{
   public class TextMessenger
   {
      public readonly UserRepository Users = new UserRepository();
      public string Name { get; }

      public TextMessenger(string name)
      {
         Name = name;
      }
      
      public User CreateUser(string name)
      {
         var user = new User(Guid.NewGuid(), name);
         Users.Add(user);
         return user;
      }
   }
}