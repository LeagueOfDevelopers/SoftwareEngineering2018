using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearner
{
    public interface IPareOfWords : IIdentificator
    {
        string EnWord { get; }
        string TransWord { get; }
    }
}
