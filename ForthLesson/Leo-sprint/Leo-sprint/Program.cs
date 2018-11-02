using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Leo_sprint
{
    class Program
    {
        static void Main(string[] args)
        {
            var user = new User("sss", Guid.NewGuid(), new List<Word>(), new List<Word>());
            var a = JsonConvert.SerializeObject(user);
            UserRepository.Create("foo");
            File.WriteAllLines("C:\\Users\\Татьяна\\Desktop\\i.txt", UserRepository.GetAllUsers());
            
        }
    }
}
