using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearner
{
    public class PareOfWords : IIdentificator, IPareOfWords
    {
        public PareOfWords(Guid id, string enWord, string transWord)
        {
            Id = id;
            EnWord = enWord ?? throw new ArgumentNullException(nameof(enWord));
            TransWord = transWord ?? throw new ArgumentNullException(nameof(transWord));
        }

        public Guid Id { get; }
        public string EnWord { get; }
        public string TransWord { get; set; }

        public override bool Equals(object obj)
        {
            PareOfWords word = (PareOfWords)obj;
            return (word.Id == Id) && (word.TransWord == TransWord) && (word.EnWord == EnWord);
        }
    }
}
