using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTrainer
{
    interface IWord
    {
        Guid Id_Word { get; set; }
        string _enWord { get; set; }
        string _ruWord { get; set; }
        int _rightChoices { get; set; }
    }
}
