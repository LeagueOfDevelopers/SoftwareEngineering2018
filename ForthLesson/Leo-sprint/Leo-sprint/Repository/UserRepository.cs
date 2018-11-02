using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;


namespace Leo_sprint
{
    public class UserRepository
    {

        public static IEnumerable<string> GetAllUsers()
        {
            var json_users = new List<string>();
            string[] users_paths;
            if (File.Exists("users.txt"))
            {
                users_paths = File.ReadAllLines("users.txt");
                foreach (var path in users_paths)
                {
                    var json = File.ReadAllText(path);                    
                    json_users.Add(json);
                }
            }
            return json_users;

        }

        public static Guid Create(string nickname)
        {
            var id = Guid.NewGuid();
            var new_user = new User(nickname, id, new List<Word>(), new List<Word>());
            File.AppendAllText("users.txt", id.ToString() + ".txt\n");
            File.WriteAllText(new_user._id.ToString() + ".txt", JsonConvert.SerializeObject(new_user));
            return id;
        }
        public static void Update(User user)
        {
            File.AppendAllText(user._id.ToString() + ".txt", JsonConvert.SerializeObject(user));
        }
       
        public static string GetJson(Guid id)
        {
            return File.ReadAllText(id.ToString() + ".txt");

        }
    }
}
