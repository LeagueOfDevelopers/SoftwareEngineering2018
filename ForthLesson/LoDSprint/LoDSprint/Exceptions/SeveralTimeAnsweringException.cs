using System;

namespace LoDSprint.Exceptions
{
    public class SeveralTimeAnsweringException : Exception
    {
        public SeveralTimeAnsweringException()
        {
        }

        public SeveralTimeAnsweringException(string message) : base(message)
        {
        }
    }
}
