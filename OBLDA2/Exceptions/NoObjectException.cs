using System;

namespace Exceptions
{
    public class NoObjectException : Exception
    {
        public NoObjectException(string message) : base(message) { }
    }
}
