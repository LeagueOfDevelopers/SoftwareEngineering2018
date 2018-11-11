using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EnglishLessons
{
    public class RepWord
    {
        public string Rus { get; set; }
        public string Eng { get; }

        public RepWord(string eng, string rus)
        {
            Rus = rus;
            Eng = eng;
        }
    }
}
