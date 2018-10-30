using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTrainer
{
    interface IUserRepository
    {
        string _jsonFile { get; set; }

        List<User> GetUsers();

        void SaveUsers(List<User> user);

        void SaveUser(User user);

    }
}

