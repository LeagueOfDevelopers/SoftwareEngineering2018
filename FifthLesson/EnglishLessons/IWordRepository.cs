using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLessons
{
    public interface IWordRepository
    {
        RepWord LoadWord();

        void SaveWord(RepWord repword);
    }
}
