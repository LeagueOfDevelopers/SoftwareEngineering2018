using System;

namespace EnglishTrainer
{
   public class User
   {
      public Guid Id { get; }
      public string Name { get; }

      public User(Guid id, string name)
      {
         Id = id;
         Name = name;
      }
   }
}