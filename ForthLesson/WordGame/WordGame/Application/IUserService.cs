using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordGame.Application
{
    interface IUserService
    {
        Guid RegisterUser(string name);
        IEnumerable<string> GetInProcessWords(Guid idUser);
        List<string> GetStudiedWords(Guid idUser);
    }
}
