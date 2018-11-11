using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearner.Application
{
    public interface IUserService
    {
        Guid RegisterUser(string name);
        IEnumerable<IPareOfWords> GetAllLearnedWords(Guid userId);
        IEnumerable<IPareOfWords> GetAllWordsOnLearning(Guid userId);
        void AddWordOnLearning(Guid userId, Guid wordId);
        void EditUser(Guid id, string name);
    }
}
