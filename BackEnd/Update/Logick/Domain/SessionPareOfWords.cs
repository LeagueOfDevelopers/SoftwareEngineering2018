using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearner
{
    public class SessionPareOfWords : PareOfWords
    {
        
    public SessionPareOfWords(  Guid id,
                                string enWord,
                                string transWord,
                                string answer) : base(id, enWord, transWord)
                                {
                                    Answer = answer;
                                }

        public SessionPareOfWords(IPareOfWords pare) 
            : base (pare.Id, pare.EnWord, pare.TransWord)
        {
            Answer = TransWord;
        }

        public string Answer { get; }
    }
}
