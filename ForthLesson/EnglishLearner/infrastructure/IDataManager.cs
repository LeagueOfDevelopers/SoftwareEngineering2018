using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearner
{
    public interface IDataManager
    {
        void LoadUsers();
        void LoadWords();
        void SaveUsers();
        void SaveWords();
    }
}
