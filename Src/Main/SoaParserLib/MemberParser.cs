using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dataflow;
using System.Reflection;
using OsloExtensions;
using Sb.Meta;

namespace SoaMetaModel
{
    using MetaInfo;

    /// <summary>
    /// Parser class for member declarations and references.
    /// </summary>
    /// <remarks>
    /// This is the second phase of parsing. The members of declarations and name references are resolved.
    /// Apart from contracts, authorizations and expressions, almost the whole metamodel is built by the end of the phase.
    /// </remarks>
    public class MemberParser : SoaLanguageParser
    {
        /// <summary>
        /// Constructs the member parser.
        /// </summary>
        /// <param name="errorReporter">The error reporter instance.</param>
        /// <param name="context">The parser context.</param>
        public MemberParser(ErrorReporter errorReporter, SoaLanguageContext context)
            : base(errorReporter, context)
        {
        }

        /// <summary>
        /// The main parser method.
        /// </summary>
        /// <param name="root">The root of the M object hierarchy.</param>
        public override void Parse(DynamicObjectNode root)
        {
            // A global scope instance is needed for namespace search, so we use the global namespace.
            using (new NameContextScope(ModelContext<SoaModel>.Current.Model.GlobalNamespace))
            {
                this.Process(root);
            }
        }

        /// <summary>
        /// Processes namespace node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_Namespace(dynamic node)
        {
            // Find object
            Namespace @namespace = context.GetObject(node);

            // Uri is processed already

            // Enter scope
            using (new NameContextScope(@namespace))
            {
                // Imports (optional)
                if (node.Imports != null)
                {
                    foreach (dynamic import in node.Imports)
                    {
                        SourceLocationInfo location = new SourceLocationInfo(import, context);
                        try
                        {
                            Namespace importedNamespace = this.Process_NamespaceReference(import.Name);
                            if (!NameContext.Current.Imports.Add(importedNamespace))
                            {
                                Error_NamespaceImport(location, importedNamespace);
                            }
                        }
                        catch (NameNotFoundException exception)
                        {
                            Error_NameNotFound(location, exception);
                        }
                    }
                }

                // Declarations (optional)
                if (node.Declarations != null)
                {
                    foreach (dynamic decl in node.Declarations)
                    {
                        this.Process(decl);
                    }
                }
            }
        }

        /// <summary>
        /// Retrieves the namespace instance associated with the namespace reference node.
        /// </summary>
        /// <param name="node">The namespace reference node.</param>
        /// <returns>The namespace identified by the node.</returns>
        /// <exception cref="NameNotFoundException">If the namespace is not found.</exception>
        private Namespace Process_NamespaceReference(dynamic node)
        {
            return this.Process_NamespaceReference(this.Process_IdentifierList(node), NameContext.Root.Scope);
        }

        /// <summary>
        /// Processes one identifier of a dotted namespace name.
        /// </summary>
        /// <remarks>
        /// The method is called recursively until only the last identifier remains.
        /// </remarks>
        /// <param name="identifiers">The list of identifiers.</param>
        /// <param name="scope">The parent namespace to search next identifier in.</param>
        /// <returns>The namespace that the list identifies.</returns>
        /// <exception cref="NameNotFoundException">If a namespace is not found along the path.</exception>
        private Namespace Process_NamespaceReference(List<string> identifiers, SoaObject scope)
        {
            Namespace @namespace = (Namespace)NameContext.ResolveName(scope, identifiers[0], typeof(Namespace));;
            if (identifiers.Count == 1)
            {
                // If this is the last part, simply return it
                return @namespace;
            }
            else
            {
                // Otherwise continue searching/creating with the next part of the name
                identifiers.RemoveAt(0);
                return this.Process_NamespaceReference(identifiers, @namespace);
            }
        }

        /// <summary>
        /// Processes namespaced type reference node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="types">The expected type(s).</param>
        /// <returns>The object that matches the name and type.</returns>
        /// <exception cref="NameNotFoundException">If no matching object is not found.</exception>
        /// <exception cref="NameCollisionException">If more than one matching object is found.</exception>
        private SoaObject Process_NamespacedTypeReference(dynamic node, params System.Type[] types)
        {
            if (node.NamespaceReference != null)
            {
                Namespace @namespace = this.Process_NamespaceReference(node.NamespaceReference);
                return NameContext.ResolveName(@namespace, node.Type, types);
            }
            else
            {
                return NameContext.Current.ResolveFirstName(node.Type, types);
            }
        }

        /// <summary>
        /// Processes type reference node.
        /// </summary>
        /// <remarks>
        /// Searches for built-in types, structs or enums and arrays or nullable types from them.
        /// </remarks>
        /// <param name="node">The node.</param>
        /// <returns>The type that matches the name.</returns>
        /// <exception cref="NameNotFoundException">If no matching type is not found.</exception>
        /// <exception cref="NameCollisionException">If more than one matching type is found.</exception>
        private Type Process_TypeReference(dynamic node)
        {
            if (node.GetBrand() == "SimpleType")
            {
                Type type = node.IsBuiltInType ? BuiltInType.GetBuiltInType(node.Name) : (Type)this.Process_NamespacedTypeReference(node.Name, typeof(EnumType), typeof(StructType));
                return node.IsNullable ? NullableType.CreateFrom(type) : type;
            }
            else if (node.GetBrand() == "ArrayType")
            {
                return ArrayType.CreateFrom(this.Process_TypeReference(node.ItemType));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Processes return type reference node.
        /// </summary>
        /// <remarks>
        /// Searches for pseudo types, built-in types, structs or enums and arrays or nullable types from them.
        /// </remarks>
        /// <param name="node">The node.</param>
        /// <returns>The type that matches the name.</returns>
        /// <exception cref="NameNotFoundException">If no matching type is not found.</exception>
        /// <exception cref="NameCollisionException">If more than one matching type is found.</exception>
        private Type Process_ReturnTypeReference(dynamic node)
        {
            if (node.GetBrand() == "VoidType")
            {
                return (node.Async) ? PseudoType.Async : PseudoType.Void;
            }
            else
            {
                return this.Process_TypeReference(node);
            }
        }

        /// <summary>
        /// Processes enum node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_EnumType(dynamic node)
        {
            EnumType enumType = context.GetObject(node);

            // Namespace is processed already

            // Name is processed already

            // Values (optional)
            if (node.Values != null)
            {
                // Enter scope
                using (new NameContextScope(enumType))
                {
                    foreach (dynamic value in node.Values)
                    {
                        this.Process_EnumValue(value);
                    }
                }
            }
        }

        /// <summary>
        /// Processes enum value node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_EnumValue(dynamic node)
        {
            EnumValue enumValue = new EnumValue();

            // Map source location and node to object
            enumValue.AddMetaInfo(new SourceLocationInfo(node, context));
            context.AddObject(node, enumValue);

            // Enum
            try
            {
                NameContext.Current.CheckName(node.Name, typeof(EnumValue));
                enumValue.Enum = (EnumType)NameContext.Current.Scope;
            }
            catch (NameCollisionException exception)
            {
                Error_NameExists(enumValue, exception);
            }

            // Name
            enumValue.Name = node.Name;
        }

        /// <summary>
        /// Processes struct node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_StructType(dynamic node)
        {
            StructType structType = context.GetObject(node);

            // Namespace is processed already

            // Name is processed already

            // Fields (optional)
            if (node.Fields != null)
            {
                // Enter scope
                using (new NameContextScope(structType))
                {
                    foreach (dynamic field in node.Fields)
                    {
                        this.Process_StructField(field);
                    }
                }
            }

            // SuperType (optional)
            if (node.SuperType != null)
            {
                SourceLocationInfo location = new SourceLocationInfo(node.SuperType, context);
                try
                {
                    structType.SuperType = this.Process_NamespacedTypeReference(node.SuperType, typeof(StructType));
                }
                catch (NameNotFoundException exception)
                {
                    Error_NameNotFound(location, exception);
                }
                catch (NameCollisionException exception)
                {
                    Error_NameCollision(location, exception);
                }
            }
        }

        /// <summary>
        /// Processes struct field node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_StructField(dynamic node)
        {
            StructField structField = new StructField();

            // Map source location and node to object
            structField.AddMetaInfo(new SourceLocationInfo(node, context));
            context.AddObject(node, structField);

            // Struct
            try
            {
                NameContext.Current.CheckName(node.Name, typeof(StructField));
                structField.Struct = (StructType)NameContext.Current.Scope;
            }
            catch (NameCollisionException exception)
            {
                Error_NameExists(structField, exception);
            }

            // Name
            structField.Name = node.Name;

            // Type
            try
            {
                structField.Type = this.Process_TypeReference(node.Type);
            }
            catch (NameNotFoundException exception)
            {
                Error_NameNotFound(structField, exception);
            }
            catch (NameCollisionException exception)
            {
                Error_NameCollision(structField, exception);
            }
        }

        /// <summary>
        /// Processes exception node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_ExceptionType(dynamic node)
        {
            ExceptionType exceptionType = context.GetObject(node);

            // Namespace is processed already

            // Name is processed already

            // Fields (optional)
            if (node.Fields != null)
            {
                // Enter scope
                using (new NameContextScope(exceptionType))
                {
                    foreach (dynamic field in node.Fields)
                    {
                        this.Process_ExceptionField(field);
                    }
                }
            }

            // SuperType (optional)
            if (node.SuperType != null)
            {
                SourceLocationInfo location = new SourceLocationInfo(node.SuperType, context);
                try
                {
                    exceptionType.SuperType = this.Process_NamespacedTypeReference(node.SuperType, typeof(ExceptionType));
                }
                catch (NameNotFoundException exception)
                {
                    Error_NameNotFound(location, exception);
                }
                catch (NameCollisionException exception)
                {
                    Error_NameCollision(location, exception);
                }
            }
        }

        /// <summary>
        /// Processes exception field node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_ExceptionField(dynamic node)
        {
            ExceptionField exceptionField = new ExceptionField();

            // Map source location and node to object
            exceptionField.AddMetaInfo(new SourceLocationInfo(node, context));
            context.AddObject(node, exceptionField);

            // Exception
            try
            {
                NameContext.Current.CheckName(node.Name, typeof(ExceptionField));
                exceptionField.Exception = (ExceptionType)NameContext.Current.Scope;
            }
            catch (NameCollisionException exception)
            {
                Error_NameExists(exceptionField, exception);
            }

            // Name
            exceptionField.Name = node.Name;

            // Type
            try
            {
                exceptionField.Type = this.Process_TypeReference(node.Type);
            }
            catch (NameNotFoundException exception)
            {
                Error_NameNotFound(exceptionField, exception);
            }
            catch (NameCollisionException exception)
            {
                Error_NameCollision(exceptionField, exception);
            }
        }

        /// <summary>
        /// Processes claim node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_ClaimsetType(dynamic node)
        {
            ClaimsetType claimsetType = context.GetObject(node);

            // Namespace is processed already

            // Name is processed already

            // Uri is processed already

            // Fields (optional)
            if (node.Fields != null)
            {
                // Enter scope
                using (new NameContextScope(claimsetType))
                {
                    foreach (dynamic field in node.Fields)
                    {
                        this.Process_ClaimField(field);
                    }
                }
            }
        }

        /// <summary>
        /// Processes claim field node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_ClaimField(dynamic node)
        {
            ClaimField claimField = new ClaimField();

            // Map source location and node to object
            claimField.AddMetaInfo(new SourceLocationInfo(node, context));
            context.AddObject(node, claimField);

            // Claim
            try
            {
                NameContext.Current.CheckName(node.Name, typeof(ClaimField));
                claimField.Claimset = (ClaimsetType)NameContext.Current.Scope;
            }
            catch (NameCollisionException exception)
            {
                Error_NameExists(claimField, exception);
            }

            // Name
            claimField.Name = node.Name;

            // Uri (optional)
            if (node.Uri != null)
            {
                claimField.Uri = node.Uri.Replace("\"", "");
            }

            // Type
            try
            {
                claimField.Type = this.Process_TypeReference(node.Type);
            }
            catch (NameNotFoundException exception)
            {
                Error_NameNotFound(claimField, exception);
            }
            catch (NameCollisionException exception)
            {
                Error_NameCollision(claimField, exception);
            }
        }

        /// <summary>
        /// Processes interface node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_Interface(dynamic node)
        {
            Interface @interface = context.GetObject(node);

            // Namespace is processed already

            // Name is processed already

            // Version is processed already

            // Operations (optional)
            if (node.Operations != null)
            {
                // Enter scope
                using (new NameContextScope(@interface))
                {
                    foreach (dynamic op in node.Operations)
                    {
                        this.Process_Operation(op);
                    }
                }
            }

            // SuperInterfaces (optional)
            if (node.SuperInterfaces != null)
            {
                foreach (dynamic super in node.SuperInterfaces)
                {
                    SourceLocationInfo location = new SourceLocationInfo(super, context);
                    try
                    {
                        @interface.SuperInterfaces.Add(this.Process_NamespacedTypeReference(super, typeof(Interface)));
                    }
                    catch (NameNotFoundException exception)
                    {
                        Error_NameNotFound(location, exception);
                    }
                    catch (NameCollisionException exception)
                    {
                        Error_NameCollision(location, exception);
                    }
                }
            }
        }

        private void Process_Operation(dynamic node)
        {
            Operation operation = new Operation();

            // Map source location and node to object
            operation.AddMetaInfo(new SourceLocationInfo(node, context));
            context.AddObject(node, operation);

            // Interface
            try
            {
                NameContext.Current.CheckName(node.Name, typeof(Operation));
                operation.Interface = (Interface)NameContext.Current.Scope;
            }
            catch (NameCollisionException exception)
            {
                Error_NameExists(operation, exception);
            }

            // Name (obligatory)
            operation.Name = node.Name;

            // ReturnType (obligatory)
            try
            {
                operation.ReturnType = this.Process_ReturnTypeReference(node.ReturnType);
            }
            catch (NameNotFoundException exception)
            {
                Error_NameNotFound(operation, exception);
            }
            catch (NameCollisionException exception)
            {
                Error_NameCollision(operation, exception);
            }

            // Enter scope
            using (new NameContextScope(operation))
            {
                // Parameters (optional)
                if (node.Parameters != null)
                {
                    foreach (dynamic param in node.Parameters)
                    {
                        this.Process_OperationParameter(param);
                    }
                }

                // Exceptions (optional)
                if (node.Exceptions != null)
                {
                    foreach (dynamic exc in node.Exceptions)
                    {
                        SourceLocationInfo location = new SourceLocationInfo(exc, context);
                        try
                        {
                            ExceptionType exceptionType = this.Process_NamespacedTypeReference(exc, typeof(ExceptionType));
                            if (operation.Exceptions.Contains(exceptionType))
                            {
                                Error_NameRedundant(location, typeof(ExceptionType), exceptionType.FullName);
                            }
                            else
                            {
                                operation.Exceptions.Add(exceptionType);
                            }
                        }
                        catch (NameNotFoundException exception)
                        {
                            Error_NameNotFound(location, exception);
                        }
                        catch (NameCollisionException exception)
                        {
                            Error_NameCollision(location, exception);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Processes operation parameter node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_OperationParameter(dynamic node)
        {
            OperationParameter operationParameter = new OperationParameter();

            // Map source location and node to object
            operationParameter.AddMetaInfo(new SourceLocationInfo(node, context));
            context.AddObject(node, operationParameter);

            // Operation
            try
            {
                NameContext.Current.CheckName(node.Name, typeof(OperationParameter));
                operationParameter.Operation = (Operation)NameContext.Current.Scope;
            }
            catch (NameCollisionException exception)
            {
                Error_NameExists(operationParameter, exception);
            }

            // Name
            operationParameter.Name = node.Name;

            // Type
            try
            {
                operationParameter.Type = Process_TypeReference(node.Type);
            }
            catch (NameNotFoundException exception)
            {
                Error_NameNotFound(operationParameter, exception);
            }
            catch (NameCollisionException exception)
            {
                Error_NameCollision(operationParameter, exception);
            }
        }

        /// <summary>
        /// Processes contract node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_Contract(dynamic node)
        {
            Contract contract = context.GetObject(node);

            // Namespace is processed already

            // Name is processed already

            // Interface (obligatory)
            SourceLocationInfo location = new SourceLocationInfo(node.Interface, context);
            try
            {
                contract.Interface = this.Process_NamespacedTypeReference(node.Interface, typeof(Interface));
            }
            catch (NameNotFoundException exception)
            {
                Error_NameNotFound(location, exception);
            }
            catch (NameCollisionException exception)
            {
                Error_NameCollision(location, exception);
            }

            // OperationContracts are not processed yet
        }

        /// <summary>
        /// Processes authorization node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_Authorization(dynamic node)
        {
            Authorization authorization = context.GetObject(node);

            // Namespace is processed already

            // Name is processed already

            // Interface (obligatory)
            SourceLocationInfo location = new SourceLocationInfo(node.Interface, context);
            try
            {
                authorization.Interface = this.Process_NamespacedTypeReference(node.Interface, typeof(Interface));
            }
            catch (NameNotFoundException exception)
            {
                Error_NameNotFound(location, exception);
            }
            catch (NameCollisionException exception)
            {
                Error_NameCollision(location, exception);
            }

            // OperationAuthorizations are not processed in this phase
        }

        /// <summary>
        /// Processes binding node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_Binding(dynamic node)
        {
            Binding binding = context.GetObject(node);

            // Namespace is processed already

            // Name is processed already

            // Transport is not processed yet

            // Encoding is not processed yet

            // Protocols are not processed yet
        }

        /// <summary>
        /// Processes endpoint node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_Endpoint(dynamic node)
        {
            Endpoint endpoint = context.GetObject(node);

            // Namespace is processed already

            // Name is processed already

            // Interface (obligatory)
            SourceLocationInfo interfaceLocation = new SourceLocationInfo(node.Interface, context);
            try
            {
                endpoint.Interface = this.Process_NamespacedTypeReference(node.Interface, typeof(Interface));
            }
            catch (NameNotFoundException exception)
            {
                Error_NameNotFound(interfaceLocation, exception);
            }
            catch (NameCollisionException exception)
            {
                Error_NameCollision(interfaceLocation, exception);
            }

            // Binding (optional)
            if (node.Properties.Binding != null)
            {
                SourceLocationInfo bindingLocation = new SourceLocationInfo(node.Properties.Binding, context);
                try
                {
                    endpoint.Binding = this.Process_NamespacedTypeReference(node.Properties.Binding, typeof(Binding));
                }
                catch (NameNotFoundException exception)
                {
                    Error_NameNotFound(bindingLocation, exception);
                }
                catch (NameCollisionException exception)
                {
                    Error_NameCollision(bindingLocation, exception);
                }
            }

            // Authorization is not processed yet

            // Contract is not processed yet

            // Address is processed already
        }
 
    }
}
