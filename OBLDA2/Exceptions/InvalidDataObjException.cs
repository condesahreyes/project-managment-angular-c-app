using System;

namespace Exceptions
{
    public class InvalidDataObjException : Exception
    {
        public InvalidDataObjException(string message) : base(message) { }
    }
}
