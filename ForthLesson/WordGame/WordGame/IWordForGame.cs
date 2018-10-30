using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordGame
{
    interface IWordForGame
    {
        string Word { get; }
        string TrueOrFakeTranslation { get; }
    }
}
