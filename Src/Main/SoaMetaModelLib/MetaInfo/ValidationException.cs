using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoaMetaModel.MetaInfo
{
    public class ValidationException : Exception
    {
        public ValidationException()
        {

        }

        public ValidationException(string message)
            : base(message)
        {

        }

        public ValidationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }

    public class InheritanceValidationException : ValidationException
    {
        public Declaration Type
        {
            get;
            private set;
        }

        public InheritanceValidationException(Declaration type, Exception inner = null)
            : base("Circular inheritance", inner)
        {
            Type = type;
        }
    }

    public class OperationValidationException : ValidationException
    {
        public OperationValidationException(Exception inner = null)
            : base("Operation signature mismatch", inner)
        {
        }
    }

    public class ExpressionValidationException : ValidationException
    {
        public SoaObject Site
        {
            get;
            private set;
        }

        public ExpressionValidationException(SoaObject site, string message, Exception inner)
            : base(message, inner)
        {
            Site = site;
        }
    }

    public class NameValidationException : ExpressionValidationException
    {
        public NameValidationException(SoaObject site, NameException inner)
            : base(site, inner.Message, inner)
        {
        }
    }

    public class TypeInvalidException : ExpressionValidationException
    {
        public TypeInvalidException(SoaObject site, Exception inner = null)
            : base(site, "Object has no type", inner)
        {
        }
    }

    public class TypeMismatchException : ExpressionValidationException
    {
        public Declaration Type
        {
            get;
            private set;
        }

        public Declaration ExpectedType
        {
            get;
            private set;
        }

        public TypeMismatchException(SoaObject site, Declaration type, Declaration expected, Exception inner = null)
            : base(site, "Type mismatch", inner)
        {
            Type = type;
            ExpectedType = expected;
        }
    }

    public class EvaluationException : ExpressionValidationException
    {
        public EvaluationException(SoaObject site, Exception inner = null)
            : base(site, "Expression cannot be evaluated", inner)
        {
        }
    }
}
