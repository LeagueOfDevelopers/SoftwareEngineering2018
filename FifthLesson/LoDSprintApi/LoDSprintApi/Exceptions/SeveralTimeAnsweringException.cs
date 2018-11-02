using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoDSprintApi.Exceptions
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
