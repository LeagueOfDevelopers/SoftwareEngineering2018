using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordGame
{
    public class WordForGame : IWordForGame
    {
        public WordForGame(string word, string trueTranslation, string trueOrFakeTranslation)
        {
            Word = word ?? throw new ArgumentNullException(nameof(word));
            TrueTranslation = trueTranslation ?? throw new ArgumentNullException(nameof(trueTranslation));
            TrueOrFakeTranslation = trueOrFakeTranslation ?? throw new ArgumentNullException(nameof(trueOrFakeTranslation));
        }

        public string Word { get; }

        public string TrueOrFakeTranslation { get; }

        private bool CheckForEqualityTranslations()
        {
            if (TrueTranslation == TrueOrFakeTranslation)
            {
                return true;
            }
            return false;
        }

        public void Answer(bool UserAnswer)
        {
            if(CheckForEqualityTranslations() == UserAnswer)
            {
                _userAnswer = true;
            }
            _userAnswer = false;
        }

        public bool CheckAnswer()
        {
            if (_userAnswer)
            {
                return true;
            }
            return false;
        }

        private bool _userAnswer;
        private string TrueTranslation { get; }
    }
}
