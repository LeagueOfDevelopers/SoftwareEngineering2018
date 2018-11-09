using System;
using System.Collections.Generic;
using BusinessEntities;

namespace BusinessServices.Interfaces
{
    public interface IAdministratorService
    {
        Guid AddDictionaryPair(Guid whoAddId, string word, string translation);
        void DeleteDictionaryPair(Guid whoDeleteId, Guid pairId);
        IEnumerable<DictionaryPair> LoadDictionary(Guid whoLoadId);
    }
}
