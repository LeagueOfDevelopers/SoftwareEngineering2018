using System;

namespace BusinessServices.Exceptions
{
    public class IncorrectAnswersException : Exception
    {
        public IncorrectAnswersException(string message) : base(message)
        {
        }
    }
}
