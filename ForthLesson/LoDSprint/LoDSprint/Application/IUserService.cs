using System;
using System.Collections.Generic;

namespace LoDSprint.Application
{
    public interface IUserService
    {
        Guid RegisterUser(string nickName);
        IEnumerable<Word> GetUserLearnedWords(Guid getterId, Guid userId);
        IEnumerable<Word> GetUserStudiedWords(Guid getterId, Guid userId);
    }
}
