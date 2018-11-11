using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leo_sprint
{
    public class Word
    {
        public string _in_english { get; }
        public string _in_russian { get; }

        public Word(string in_english, string in_russian, int flag)
        {
            _in_english = in_english;
            _in_russian = in_russian;
            _flag = flag;
        }

        public int _flag { private set; get; }

        public override bool Equals(object obj)
        {
            var word = obj as Word;
            return word != null &&
                   _in_english == word._in_english &&
                   _in_russian == word._in_russian;
        }

        public override int GetHashCode()
        {
            var hashCode = -1432052003;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_in_english);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_in_russian);
            hashCode = hashCode * -1521134295 + _flag.GetHashCode();
            return hashCode;
        }

        private void FlagGrowsOnePoint()
        {
            _flag++;
        }
    }
}
