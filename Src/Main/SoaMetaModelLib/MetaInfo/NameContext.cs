using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoaMetaModel.MetaInfo
{
    public class NameContext
    {
        /// <summary>
        /// The object whose scope is represented by the context.
        /// </summary>
        public SoaObject Scope
        {
            get;
            private set;
        }

        /// <summary>
        /// Additional scopes to search in.
        /// </summary>
        /// <remarks>
        /// This includes the main scope also.
        /// </remarks>
        public HashSet<SoaObject> Imports
        {
            get;
            private set;
        }

        /// <summary>
        /// Constructs a new context.
        /// </summary>
        /// <remarks>
        /// The method is accessible only by the scope class.
        /// </remarks>
        /// <param name="scope">The object whose scope is represented by the context.</param>
        internal NameContext(SoaObject scope)
        {
            this.Scope = scope;
            this.Imports = new HashSet<SoaObject>();

            this.Imports.Add(this.Scope);
        }

        /// <summary>
        /// Initializes the static objects.
        /// </summary>
        static NameContext()
        {
            NameContext.Contexts = new Stack<NameContext>();
        }

        /// <summary>
        /// The stack of contexts.
        /// </summary>
        internal static Stack<NameContext> Contexts
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the root context.
        /// </summary>
        /// <remarks>
        /// Returns the context at the bottom of the stack.
        /// </remarks>
        public static NameContext Root
        {
            get
            {
                if (NameContext.Contexts.Count > 0)
                {
                    return NameContext.Contexts.Last();
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets the current context.
        /// </summary>
        /// <remarks>
        /// Returns the context at the top of the stack.
        /// </remarks>
        public static NameContext Current
        {
            get
            {
                if (NameContext.Contexts.Count > 0)
                {
                    return NameContext.Contexts.First();
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Checks whether the name exists in the current context.
        /// </summary>
        /// <remarks>
        /// Does not search in parent scopes.
        /// </remarks>
        /// <param name="name">The name to find</param>
        /// <param name="types">The types to find.</param>
        /// <exception cref="NameCollisionException">If the name exists.</exception>
        public void CheckName(string name, params System.Type[] types)
        {
            NameContext.CheckName(this.Scope, name, types);
        }

        /// <summary>
        /// Resolves name in the current context.
        /// </summary>
        /// <remarks>
        /// Does not search in parent contexts.
        /// </remarks>
        /// <param name="name">The name to find</param>
        /// <param name="types">The types to find.</param>
        /// <returns>The objects found.</returns>
        /// <exception cref="NameNotFoundException">If the object is not found.</exception>
        /// <exception cref="NameCollisionException">If multiple objects are found.</exception>
        public SoaObject ResolveName(string name, params System.Type[] types)
        {
            return NameContext.ResolveName(this.Scope, name, types);
        }

        public SoaObject ResolveOperation(string name, List<Type> paramTypes, bool exact = false)
        {
            return NameContext.ResolveOperation(this.Scope, name, paramTypes, exact);
        }

        /// <summary>
        /// Checks whether the name exists in the given scope.
        /// </summary>
        /// <remarks>
        /// Does not search in parent scopes.
        /// </remarks>
        /// <param name="scope">The scope to search in.</param>
        /// <param name="name">The name to find</param>
        /// <param name="types">The types to find.</param>
        /// <exception cref="NameCollisionException">If the name exists.</exception>
        public static void CheckName(SoaObject scope, string name, params System.Type[] types)
        {
            NameHelpers.CheckName(NameContext.ResolveNameMany(scope, name, types), scope, name, types);
        }

        /// <summary>
        /// Resolves name in the given scope.
        /// </summary>
        /// <remarks>
        /// Does not search in parent scopes.
        /// </remarks>
        /// <param name="scope">The scope to search in.</param>
        /// <param name="name">The name to find</param>
        /// <param name="types">The types to find.</param>
        /// <returns>The objects found.</returns>
        /// <exception cref="NameNotFoundException">If the object is not found.</exception>
        /// <exception cref="NameCollisionException">If multiple objects are found.</exception>
        public static SoaObject ResolveName(SoaObject scope, string name, params System.Type[] types)
        {
            return NameHelpers.SelectName(NameContext.ResolveNameMany(scope, name, types), scope, name, types);
        }

        public static SoaObject ResolveOperation(SoaObject scope, string name, List<Type> paramTypes, bool exact = false)
        {
            return NameHelpers.SelectName(NameContext.ResolveOperationMany(scope, name, paramTypes, exact), scope, name, typeof(Operation));
        }

        /// <summary>
        /// Returns the objects that matches the name and type in the given scope.
        /// </summary>
        /// <remarks>
        /// Does not search in parent scopes.
        /// </remarks>
        /// <param name="scope">The scope to search in.</param>
        /// <param name="name">The name to find</param>
        /// <param name="types">The types to find.</param>
        /// <returns>The objects found.</returns>
        public static IEnumerable<SoaObject> ResolveNameMany(SoaObject scope, string name, params System.Type[] types)
        {
            return NameHelpers.LookupNameMany(scope, name, types);
        }

        public static IEnumerable<SoaObject> ResolveOperationMany(SoaObject scope, string name, List<Type> paramTypes, bool exact)
        {
            return NameHelpers.LookupOperationMany(scope, name, paramTypes, exact);
        }

        /// <summary>
        /// Resolves name in the current context.
        /// </summary>
        /// <remarks>
        /// Searches in parent contexts, but stops if an object is found.
        /// </remarks>
        /// <param name="name">The name to find</param>
        /// <param name="types">The types to find.</param>
        /// <returns>The objects found.</returns>
        /// <exception cref="NameNotFoundException">If the object is not found.</exception>
        /// <exception cref="NameCollisionException">If multiple objects are found.</exception>
        public SoaObject ResolveFirstName(string name, params System.Type[] types)
        {
            return NameHelpers.SelectName(NameContext.ResolveFirstNameMany(name, types), this.Scope, name, types);
        }

        public SoaObject ResolveFirstOperation(string name, List<Type> paramTypes, bool exact = false)
        {
            return NameHelpers.SelectName(NameContext.ResolveFirstOperationMany(name, paramTypes, exact), this.Scope, name, typeof(Operation));
        }

        /// <summary>
        /// Returns the objects that matches the name and type in the current context.
        /// </summary>
        /// <remarks>
        /// Searches in parent and imported contexts, but stops if an object is found.
        /// </remarks>
        /// <param name="name">The name to find</param>
        /// <param name="types">The types to find.</param>
        /// <returns>The objects found.</returns>
        public static IEnumerable<SoaObject> ResolveFirstNameMany(string name, params System.Type[] types)
        {
            List<SoaObject> result = new List<SoaObject>();
            // From the current context towards the root
            foreach (NameContext context in NameContext.Contexts)
            {
                // Search in main and imported scopes
                foreach (SoaObject scope in context.Imports)
                {
                    result.AddRange(NameContext.ResolveNameMany(scope, name, types));
                }
                // If at least one object is found by the end of the step, we're done
                if (result.Count > 0)
                {
                    break;
                }
            }
            return result;
        }

        public static IEnumerable<SoaObject> ResolveFirstOperationMany(string name, List<Type> paramTypes, bool exact = false)
        {
            List<SoaObject> result = new List<SoaObject>();
            // From the current context towards the root
            foreach (NameContext context in NameContext.Contexts)
            {
                // Search in main and imported scopes
                foreach (SoaObject scope in context.Imports)
                {
                    result.AddRange(NameContext.ResolveOperationMany(scope, name, paramTypes, exact));
                }
                // If at least one object is found by the end of the step, we're done
                if (result.Count > 0)
                {
                    break;
                }
            }
            return result;
        }

    }

    public class NameContextScope : IDisposable
    {
        public NameContextScope(SoaObject scope)
        {
            NameContext.Contexts.Push(new NameContext(scope));
        }

        public void Dispose()
        {
            NameContext.Contexts.Pop();
        }
    }
}
