using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLessons
{
    public class UserWord : RepWord
    {
        public bool Learned { get; private set; }    
        private int _count { get; set; }

        public void Upgrade()
        {
            _count++;
            if (_count >= 3) Learned = true;
        }

        public bool Check (string toCheck)
        {
            return (base.Rus == toCheck);
        }

        public UserWord (bool learned, int count, string eng, string rus) : base(eng, rus)
        {
            Learned = learned;
            _count = count;
        }
    }
}
