using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoDSprintApi.Exceptions
{
    public class PermissionDeniedException : Exception
    {
        public PermissionDeniedException()
        {
        }

        public PermissionDeniedException(string message) : base(message)
        {
        }
    }
}
