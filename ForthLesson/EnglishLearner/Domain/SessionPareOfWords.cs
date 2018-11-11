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
                                bool isAnswerRight,
                                bool isRight) : base(id, enWord, transWord)
                                {
                                    IsAnswerRight = isAnswerRight;
                                    IsRight = isRight;
                                }

        public SessionPareOfWords(IPareOfWords pare, bool isAnswerRight, bool isRight) 
            : base (pare.Id, pare.EnWord, pare.TransWord)
        {
            IsAnswerRight = isAnswerRight;
            IsRight = isRight;
        }

        public bool IsRight { get; }
        public bool IsAnswerRight { get; set; }
    }
}
