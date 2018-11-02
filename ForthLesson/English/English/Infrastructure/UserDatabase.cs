using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using English.Domain;

namespace English.Infrastructure
{
    public class UserDatabase : IUserDatabase
    {
        private static UserDatabase _database;

        private readonly JsonSerializerSettings _settings;
        private readonly ItemRepository<User> _userRepository;

        public Guid Id { get; }

        protected UserDatabase(ItemRepository<User> userRepository)
        {
            Id = Guid.NewGuid();
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _settings = new JsonSerializerSettings
            {
                ContractResolver = new DictionaryAsArrayResolver()
            };
        }

        public static UserDatabase GetInstance()
        {
            if (_database == null)
                _database = new UserDatabase(new ItemRepository<User>(new List<User>()));

            return _database;
        }

        public void SaveUserToFile(IUser user)
        {
            var filepath = $"user_{user.Id}.txt";
            string data = JsonConvert.SerializeObject(user, _settings);
            File.WriteAllText(filepath, data);
        }

        public IUser LoadUserFromFile(Guid userId)
        {
            var filepath = $"user_{userId}.txt";
            var data = File.ReadAllText(filepath);
            var user = JsonConvert.DeserializeObject<User>(data, _settings);
            return user;
        }
    }
}
