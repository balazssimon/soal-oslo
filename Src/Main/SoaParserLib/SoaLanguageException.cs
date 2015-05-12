using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sb.Meta;

namespace SoaMetaModel
{
    /// <summary>
    /// Base class of exceptions related to parsing phases.
    /// </summary>
    public class SoaLanguageException : Exception
    {
        /// <summary>
        /// Constructs the exception.
        /// </summary>
        /// <param name="message">The textual description of the message.</param>
        public SoaLanguageException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructs the exception.
        /// </summary>
        /// <param name="message">The textual description of the message.</param>
        /// <param name="inner">The exception causing this one to be thrown.</param>
        public SoaLanguageException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

}
