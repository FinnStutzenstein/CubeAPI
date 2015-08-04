using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CubeAPI
{
    class NoInitialisationException : Exception
    {
        public NoInitialisationException()
        {
        }
        public NoInitialisationException(string message) : base(message)
        {
        }
        public NoInitialisationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    class NoConnectionException : Exception
    {
        public NoConnectionException()
        {
        }
        public NoConnectionException(string message)
            : base(message)
        {
        }
        public NoConnectionException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    class ConnectionRefusedException : Exception
    {
        public ConnectionRefusedException()
        {
        }
        public ConnectionRefusedException(string message)
            : base(message)
        {
        }
        public ConnectionRefusedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
