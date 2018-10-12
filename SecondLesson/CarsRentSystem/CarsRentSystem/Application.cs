using System.Collections.Generic;
using System.Linq;

namespace CarsRentSystem
{
   public class Application
   {
      public string Name;

      public List<User> Users { get; } = new List<User>();
      public List<Park> Parks { get; } = new List<Park>();

      public Application(string name)
      {
         Name = name;
      }

      public User CreateUser(string name)
      {
         var user = new User(Users.Count, name);
         
         Users.Add(user);

         return user;
      }

      public Park CreatePark(string name)
      {
         var park = new Park(Parks.Count, name);
         
         Parks.Add(park);

         return park;
      }
   }
}