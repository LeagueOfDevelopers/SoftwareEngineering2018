using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearner
{
    public class UserPareOfWords : PareOfWords
    {
        public int DepthOfLearning { get; private set; }

        public UserPareOfWords(Guid id, string enWord, string transWord, int  depthOfLearning) : base(id, enWord, transWord)
        {
            DepthOfLearning = depthOfLearning;
        }

        public void LearnWord()
        {
            DepthOfLearning++;
        }
    }
}
