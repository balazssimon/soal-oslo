using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoaMetaModel.MetaInfo
{
    /// <summary>
    /// Base class of exceptions related to names and declarations.
    /// </summary>
    public class NameException : Exception
    {
        /// <summary>
        /// The name related to the exception.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// The type(s) related to the exception.
        /// </summary>
        public System.Type[] Types
        {
            get;
            private set;
        }

        /// <summary>
        /// The scope where the name is looked for.
        /// </summary>
        public SoaObject Scope
        {
            get;
            private set;
        }

        /// <summary>
        /// Constructs the exception.
        /// </summary>
        /// <param name="name">The name related to the exception.</param>
        /// <param name="types">The metatype(s) related to the exception.</param>
        /// <param name="message">Description of the exception.</param>
        /// <param name="inner">The inner exception causing this one (optional).</param>
        public NameException(SoaObject scope, string name, System.Type[] types, string message, Exception inner)
            : base(message, inner)
        {
            Scope = scope;
            Name = name;
            Types = types;
        }
    }

    /// <summary>
    /// Exception signalling that a name with a specified metatype was not found among the declarations.
    /// </summary>
    /// <remarks>
    /// Occurs mostly during name resolution tasks.
    /// </remarks>
    public class NameNotFoundException : NameException
    {
        /// <summary>
        /// Constructs the exception.
        /// </summary>
        /// <param name="name">The name not found with a given metatype.</param>
        /// <param name="types">The metatype(s) not found with a given name.</param>
        /// <param name="inner">The inner exception causing this one (optional).</param>
        public NameNotFoundException(SoaObject scope, string name, System.Type[] types, Exception inner = null)
            : base(scope, name, types, "Name not found", inner)
        {
        }
    }

    /// <summary>
    /// Exception signalling that a name are declared multiple times with compatible metatypes.
    /// </summary>
    /// <remarks>
    /// Occurs during name declaration or resolution of names in imported namespaces.
    /// </remarks>
    public class NameCollisionException : NameException
    {
        /// <summary>
        /// Constructs the exception.
        /// </summary>
        /// <param name="name">The name causing the collision among the given metatypes.</param>
        /// <param name="types">The metatype(s) having a same name.</param>
        /// <param name="inner">The inner exception causing this one (optional).</param>
        public NameCollisionException(SoaObject scope, string name, System.Type[] types, IEnumerable<SoaObject> collidingObjects, Exception inner = null)
            : base(scope, name, types, "Name collides with another", inner)
        {
            this.CollidingObjects = collidingObjects;
        }

        /// <summary>
        /// The colliding objects.
        /// </summary>
        public IEnumerable<SoaObject> CollidingObjects
        {
            get;
            private set;
        }
    }
}
