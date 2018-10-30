using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTrainer
{
    public class Word : IWord
    {
        public Word(Guid id_word, string enWord, string ruWord, int rightChoices)
        {
            Id_Word = id_word;
            _enWord = enWord;
            _ruWord = ruWord;
            _rightChoices = rightChoices;
        }

        public Guid Id_Word { get; set; }
        public string _enWord { get; set; }
        public string _ruWord { get; set; }
        public int _rightChoices { get; set; }
    }
}
