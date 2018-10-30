using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTrainer
{
    interface IUser
    {
        string Name { get; set; }
        Guid Id { get; set; }
        List<Word> _inMemoryWords { get; set; }

    }
}
