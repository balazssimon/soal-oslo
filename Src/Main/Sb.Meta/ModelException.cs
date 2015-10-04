using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sb.Meta
{
    public class ModelException : Exception
    {
        public ModelException()
        {

        }

        public ModelException(string message)
            : base(message)
        {

        }

        public ModelException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
