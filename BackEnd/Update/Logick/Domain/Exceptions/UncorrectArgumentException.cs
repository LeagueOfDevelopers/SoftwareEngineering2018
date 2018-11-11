using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearner
{
    [System.Serializable]
    public class UncorrectArgumentException : Exception
    {
        public UncorrectArgumentException() { }
        public UncorrectArgumentException(string message) : base(message) { }
        public UncorrectArgumentException(string message, Exception inner) : base(message, inner) { }
        protected UncorrectArgumentException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
