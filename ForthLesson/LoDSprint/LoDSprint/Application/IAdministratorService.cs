using System;

namespace LoDSprint.Application
{
    public interface IAdministratorService
    {
        void AddNewWord(Guid whoAddId, string word, string translation);
    }
}
