using Newtonsoft.Json;
using System;
using System.IO;

namespace Leo_sprint
{
    public class UserRepository
    {
        public void Create(Guid id)
        {
            File.Create(id.ToString() + ".txt");
        }
        public void Update(User user)
        {
            File.WriteAllText(user._id.ToString() + ".txt", JsonConvert.SerializeObject(user));
        }
        public IUser Get(Guid id)//Authentication
        {
            var json = File.ReadAllText(id.ToString() + ".txt");
            return JsonConvert.DeserializeObject<User>(json);
        }
    }
}
