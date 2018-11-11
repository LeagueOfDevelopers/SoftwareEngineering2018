using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearner
{
    [System.Serializable]
    public class OutOfWordsException : Exception
    {
        public OutOfWordsException() { }
        public OutOfWordsException(string message) : base(message) { }
        public OutOfWordsException(string message, Exception inner) : base(message, inner) { }
        protected OutOfWordsException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
