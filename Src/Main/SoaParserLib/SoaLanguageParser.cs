using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dataflow;
using OsloExtensions;
using SoaMetaModel.MetaInfo;
using Sb.Meta;

namespace SoaMetaModel
{
    /// <summary>
    /// Abstract base class for parsers.
    /// </summary>
    public abstract class SoaLanguageParser : LanguageProcessor
    {
        /// <summary>
        /// The error reporter instance.
        /// </summary>
        protected ErrorReporter errorReporter;

        /// <summary>
        /// The parser context instance.
        /// </summary>
        protected SoaLanguageContext context;

        /// <summary>
        /// Constructs the parser.
        /// </summary>
        /// <param name="errorReporter">The error reporter instance.</param>
        /// <param name="context">The parser context.</param>
        public SoaLanguageParser(ErrorReporter errorReporter, SoaLanguageContext context)
            : base()
        {
            this.errorReporter = errorReporter;
            this.context = context;
        }

        /// <summary>
        /// The main parser method.
        /// </summary>
        /// <param name="root">The root of the M object hierarchy.</param>
        public abstract void Parse(DynamicObjectNode root);

        /// <summary>
        /// Converts namespace reference node to list of identifiers.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>The list of identifiers.</returns>
        protected List<string> Process_IdentifierList(dynamic node)
        {
            // Build list of identifiers from the namespace name
            List<string> identifiers = new List<string>();
            foreach (dynamic identifier in node)
            {
                identifiers.Add(identifier);
            }
            return identifiers;
        }

        protected void Error_NameNotFound(SoaObject site, NameNotFoundException exception)
        {
            Error_NameNotFound(site.GetMetaInfo<SourceLocationInfo>(), exception);
        }

        protected void Error_NameNotFound(ISourceLocation site, NameNotFoundException exception)
        {
            // Build type string
            StringBuilder sb = new StringBuilder();
            sb.Append(AttributeHelpers.GetTypeName(exception.Types[0]));
            for(int i = 1; i < exception.Types.Length - 1; i++) sb.Append(", " + AttributeHelpers.GetTypeName(exception.Types[i]).ToLower());
            if(exception.Types.Length > 1) sb.Append(" or " + AttributeHelpers.GetTypeName(exception.Types[exception.Types.Length - 1]).ToLower());

            errorReporter.Error(site, "{0} \"{1}\" is not declared.", sb.ToString(), exception.Name);
        }

        protected void Error_NameCollision(SoaObject site, NameCollisionException exception)
        {
            Error_NameCollision(site.GetMetaInfo<SourceLocationInfo>(), exception);
        }

        protected void Error_NameCollision(ISourceLocation site, NameCollisionException exception)
        {
            // Build type string
            StringBuilder sb = new StringBuilder();
            sb.Append(AttributeHelpers.GetTypeName(exception.Types[0]).ToLower() + "s");
            for (int i = 1; i < exception.Types.Length - 1; i++) sb.Append(", " + AttributeHelpers.GetTypeName(exception.Types[i]).ToLower() + "s");
            if (exception.Types.Length > 1) sb.Append(" or " + AttributeHelpers.GetTypeName(exception.Types[exception.Types.Length - 1]).ToLower() + "s");

            errorReporter.Error(site, "\"{1}\" can refer to multiple {0}. Remove unused usings or prefix the name with namespace qualifier.", sb.ToString(), exception.Name);
        }

        protected void Error_NameExists(SoaObject site, NameCollisionException exception)
        {
            Error_NameExists(site.GetMetaInfo<SourceLocationInfo>(), exception);
        }

        protected void Error_NameExists(ISourceLocation site, NameCollisionException exception)
        {
            // Build type string
            StringBuilder sb = new StringBuilder();
            sb.Append(AttributeHelpers.GetTypeName(exception.Types[0]).ToLower());
            for (int i = 1; i < exception.Types.Length - 1; i++) sb.Append(", " + AttributeHelpers.GetTypeName(exception.Types[i]).ToLower());
            if (exception.Types.Length > 1) sb.Append(" or " + AttributeHelpers.GetTypeName(exception.Types[exception.Types.Length - 1]).ToLower());

            errorReporter.Error(site, "A {0} with name \"{1}\" already exists.", sb.ToString(), exception.Name);
        }

        protected void Error_NameRedefinition(SoaObject site, NameCollisionException exception)
        {
            Error_NameRedefinition(site.GetMetaInfo<SourceLocationInfo>(), exception);
        }

        protected void Error_NameRedefinition(ISourceLocation site, NameCollisionException exception)
        {
            errorReporter.Error(site, "Redefinition of inherited {0} \"{1}\".", AttributeHelpers.GetTypeName(exception.Types[0]).ToLower(), exception.Name);
        }

        protected void Error_CircularInheritance(InheritanceValidationException exception)
        {
            errorReporter.Error(exception.Type.GetMetaInfo<SourceLocationInfo>(), "{0} \"{1}\" is involved in circular inheritance.", AttributeHelpers.GetTypeName(exception.Type.GetType()), exception.Type.Name);
        }

        protected void Error_NamespaceUri(ISourceLocation site, Namespace @namespace)
        {
            errorReporter.Error(site, "URI for namespace \"{0}\" is redefined.", @namespace.FullName);
        }

        protected void Error_NamespaceImport(ISourceLocation site, Namespace @namespace)
        {
            errorReporter.Error(site, "Using namespace \"{0}\" is redundant.", @namespace.FullName);
        }

        protected void Error_NameRedundant(SoaObject site, string name)
        {
            Error_NameRedundant(site, site.GetType(), name);
        }

        protected void Error_NameRedundant(SoaObject site, System.Type type, string name)
        {
            Error_NameRedundant(site.GetMetaInfo<SourceLocationInfo>(), type, name);
        }

        protected void Error_NameRedundant(ISourceLocation site, System.Type type, string name)
        {
            errorReporter.Error(site, "{0} \"{1}\" is already included.", AttributeHelpers.GetTypeName(type), name);
        }

        protected void Error_OperationSignatureMismatch(ISourceLocation site)
        {
            errorReporter.Error(site, "No operation with the specified name and parameter types can be found in the interface.");
        }

        protected void Error_OperationReturnMismatch(ISourceLocation site)
        {
            errorReporter.Error(site, "Return type of operation does not match with the one defined in the interface.");
        }

        protected void Error_OperationExceptionMismatch(ISourceLocation site)
        {
            errorReporter.Error(site, "Thrown exceptions of operation do not match with the ones defined in the interface.");
        }

        protected void Error_InterfaceMismatch(ISourceLocation site, System.Type ownerType)
        {
            errorReporter.Error(site, "Interface of endpoint and {0} does not match.", AttributeHelpers.GetTypeName(ownerType).ToLower());
        }

        protected void Warning_InterfaceMismatch(ISourceLocation site, System.Type ownerType)
        {
            errorReporter.Warning(site, "Interface of endpoint and {0} does not match exactly.", AttributeHelpers.GetTypeName(ownerType).ToLower());
        }
    }

}
