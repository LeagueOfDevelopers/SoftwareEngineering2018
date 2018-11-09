using System;
using System.Collections.Generic;
using BusinessEntities;

namespace BusinessServices.Interfaces
{
    public interface IUserService
    {
        Guid RegisterUser(string nickName);
        IEnumerable<Word> GetUserLearnedWords(Guid userId);
        IEnumerable<StudiedWord> GetUserStudiedWords(Guid userId);
    }
}
