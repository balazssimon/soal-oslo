using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OsloExtensions
{
    public class OsloCodeGeneratorException : Exception
    {
        public OsloCodeGeneratorException()
        {

        }

        public OsloCodeGeneratorException(string message) : base(message)
        {

        }

        public OsloCodeGeneratorException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
