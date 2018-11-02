using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordGame
{
    public interface IDictionaryRepository
    {
        WordForGame GetRandomWord();
        void SaveWord(string word, string translation);
    }
}