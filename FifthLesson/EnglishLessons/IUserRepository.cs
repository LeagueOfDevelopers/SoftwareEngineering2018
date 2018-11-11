using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLessons
{
    public interface IUserRepository
    {
        User LoadUser(Guid id);

        void SaveUser(User user);
    }
}
