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
    using System.Globalization;

    /// <summary>
    /// Parser class for operation extensions and expressions.
    /// </summary>
    /// <remarks>
    /// This is the third phase of parsing. All the remaining metamodel elements are parsed.
    /// Apart from contracts, authorizations and expressions, almost the whole metamodel is built by the end of the phase.
    /// </remarks>
    public class ExpressionParser : SoaLanguageParser
    {
        /// <summary>
        /// Constructs the expression parser.
        /// </summary>
        /// <param name="errorReporter">The error reporter instance.</param>
        /// <param name="context">The parser context.</param>
        public ExpressionParser(ErrorReporter errorReporter, SoaLanguageContext context)
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
            return this.Process_TypeReference(node, typeof(EnumType), typeof(StructType));
        }

        /// <summary>
        /// Processes type reference node.
        /// </summary>
        /// <remarks>
        /// Searches for the given types and arrays or nullable types from them.
        /// </remarks>
        /// <param name="node">The node.</param>
        /// <param name="types">The type(s) to search for.</param>
        /// <returns>The type that matches the name.</returns>
        /// <exception cref="NameNotFoundException">If no matching type is not found.</exception>
        /// <exception cref="NameCollisionException">If more than one matching type is found.</exception>
        private Type Process_TypeReference(dynamic node, params System.Type[] types)
        {
            if (node.GetBrand() == "SimpleType")
            {
                Type type = node.IsBuiltInType ? BuiltInType.GetBuiltInType(node.Name) : (Type)this.Process_NamespacedTypeReference(node.Name, types);
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
            EnumValue enumValue = context.GetObject(node);

            // Enum is processed already

            // Name is processed already
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

            // SuperType (optional)
            if (node.SuperType != null)
            {
                try
                {
                    // Check for circular inheritance
                    structType.GetSuperTypes(true);
                }
                catch (InheritanceValidationException exception)
                {
                    Error_CircularInheritance(exception);
                }
            }

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
        }

        /// <summary>
        /// Processes struct field node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_StructField(dynamic node)
        {
            StructField structField = context.GetObject(node);

            // Struct is processed already

            // Name
            try
            {
                NameContext.Current.ResolveName(node.Name, typeof(StructField));
            }
            catch (NameCollisionException exception)
            {
                Error_NameRedefinition(structField, exception);
            }

            // Type is processed already
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

            // SuperType (optional)
            if (node.SuperType != null)
            {
                try
                {
                    // Check for circular inheritance
                    exceptionType.GetSuperTypes(true);
                }
                catch (InheritanceValidationException exception)
                {
                    Error_CircularInheritance(exception);
                }
            }

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
        }

        /// <summary>
        /// Processes exception field node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_ExceptionField(dynamic node)
        {
            ExceptionField exceptionField = context.GetObject(node);

            // Exception is processed already

            // Name
            try
            {
                NameContext.Current.ResolveName(node.Name, typeof(ExceptionField));
            }
            catch (NameCollisionException exception)
            {
                Error_NameRedefinition(exceptionField, exception);
            }

            // Type is processed already
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
            ClaimField claimField = context.GetObject(node);

            // Claim is processed already

            // Name is processed already

            // Uri is processed already

            // Type is processed already
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

            // SuperInterfaces (optional)
            if (node.SuperInterfaces != null)
            {
                try
                {
                    // Check for circular inheritance
                    @interface.GetSuperTypes(true);
                }
                catch (InheritanceValidationException exception)
                {
                    Error_CircularInheritance(exception);
                }
            }

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
        }

        private void Process_Operation(dynamic node)
        {
            Operation operation = context.GetObject(node);

            // Interface is processed already

            // Name
            try
            {
                NameContext.Current.ResolveName(node.Name, typeof(Operation));
            }
            catch (NameCollisionException exception)
            {
                Error_NameRedefinition(operation, exception);
            }

            // ReturnType is processed already

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

                // Exceptions are processed already
            }
        }

        /// <summary>
        /// Processes operation parameter node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_OperationParameter(dynamic node)
        {
            OperationParameter operationParameter = context.GetObject(node);

            // Operation is processed already

            // Name is processed already

            // Type is processed already
        }

        /// <summary>
        /// Processes operation signature node.
        /// </summary>
        /// <remarks>
        /// Find operation in scope that matches the signature.
        /// </remarks>
        /// <param name="node">The node.</param>
        /// <param name="scope">The scope to search for operations.</param>
        /// <returns>The operation.</returns>
        /// <exception cref="NameNotFoundException"
        private Operation Process_OperationSignature(dynamic node, Interface scope)
        {
            Operation operation = null;
            bool error = false;

            // Name
            string name = node.Name;

            // Parameters
            List<Type> parameterTypes = new List<Type>();
            if (node.Parameters != null)
            {
                foreach (dynamic parameter in node.Parameters)
                {
                    try
                    {
                        parameterTypes.Add(this.Process_TypeReference(parameter.Type));
                    }
                    catch (NameNotFoundException exception)
                    {
                        Error_NameNotFound(new SourceLocationInfo(parameter, context), exception);
                        error = true;
                    }
                    catch (NameCollisionException exception)
                    {
                        Error_NameCollision(new SourceLocationInfo(parameter, context), exception);
                        error = true;
                    }
                }
                if (error)
                {
                    throw new OperationValidationException();
                }
            }

            try
            {
                operation = (Operation)NameContext.ResolveOperation(scope, name, parameterTypes, false);
            }
            catch (NameNotFoundException)
            {
                Error_OperationSignatureMismatch(new SourceLocationInfo(node, context));
                throw new OperationValidationException();
            }

            // Return type
            try
            {
                Type returnType = this.Process_ReturnTypeReference(node.ReturnType);
                if (returnType != operation.ReturnType)
                {
                    Error_OperationReturnMismatch(new SourceLocationInfo(node.ReturnType, context));
                    error = true;
                }
            }
            catch (NameNotFoundException exception)
            {
                Error_NameNotFound(new SourceLocationInfo(node.Operation.ReturnType, context), exception);
                error = true;
            }
            catch (NameCollisionException exception)
            {
                Error_NameCollision(new SourceLocationInfo(node.Operation.ReturnType, context), exception);
                error = true;
            }

            // Exceptions
            List<ExceptionType> exceptionTypes = new List<ExceptionType>();
            if (node.Exceptions != null)
            {
                foreach (dynamic exception in node.Exceptions)
                {
                    try
                    {
                        ExceptionType exceptionType = this.Process_NamespacedTypeReference(exception, typeof(ExceptionType));
                        if (exceptionTypes.Contains(exceptionType))
                        {
                            Error_NameRedundant(new SourceLocationInfo(exception, context), typeof(ExceptionType), exceptionType.FullName);
                        }
                        else
                        {
                            exceptionTypes.Add(exceptionType);
                        }
                    }
                    catch (NameNotFoundException ex)
                    {
                        Error_NameNotFound(new SourceLocationInfo(exception, context), ex);
                        error = true;
                    }
                    catch (NameCollisionException ex)
                    {
                        Error_NameCollision(new SourceLocationInfo(exception, context), ex);
                        error = true;
                    }
                }
                if (error)
                {
                    throw new OperationValidationException();
                }
            }
            if (operation.Exceptions.Any(ex => !exceptionTypes.Contains(ex)) || exceptionTypes.Any(ex => !operation.Exceptions.Contains(ex)))
            {
                Error_OperationExceptionMismatch(new SourceLocationInfo((node.Exceptions != null) ? node.Exceptions : node, context));
                error = true;
            }

            if (error)
            {
                throw new OperationValidationException();
            }

            return operation;
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

            // Interface is processed already

            // OperationContracts (optional)
            if (node.OperationContracts != null)
            {
                // Enter scope
                using (new NameContextScope(contract))
                {
                    foreach (dynamic opc in node.OperationContracts)
                    {
                        this.Process_OperationContract(opc);
                    }
                }
            }
        }

        /// <summary>
        /// Processes operation contract node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_OperationContract(dynamic node)
        {
            OperationContract operationContract = new OperationContract();

            // Map source location and node to object
            operationContract.AddMetaInfo(new SourceLocationInfo(node, context));
            context.AddObject(node, operationContract);

            try
            {
                // Operation
                operationContract.Operation = this.Process_OperationSignature(node.Operation, ((Contract)NameContext.Current.Scope).Interface);

                // Contract
                NameContext.Current.CheckName(operationContract.Operation.Name, typeof(OperationContract));
                operationContract.Contract = (Contract)NameContext.Current.Scope;

                // Enter scope
                using (new NameContextScope(operationContract))
                {
                    // References
                    this.Process_OperationContractReferences(node);

                    // Statements
                    if (node.OperationContractStatements != null)
                    {
                        foreach (dynamic ocs in node.OperationContractStatements)
                        {
                            this.Process_OperationContractStatement(ocs);
                        }
                    }
                }
            }
            catch (OperationValidationException)
            {
            }
            catch (NameCollisionException)
            {
                Error_NameRedundant(operationContract, node.Operation.Name);
            }
        }

        /// <summary>
        /// Creates references for operation contract.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_OperationContractReferences(dynamic node)
        {
            OperationContract operationContract = context.GetObject(node);

            // This
            Reference thisReference = new Reference();
            thisReference.Name = "this";
            thisReference.Object = operationContract.Contract;
            operationContract.References.Add(thisReference);

            // Result
            if (operationContract.Operation.ReturnType != PseudoType.Async && operationContract.Operation.ReturnType != PseudoType.Void)
            {
                Reference resultReference = new Reference();
                resultReference.Name = "result";
                resultReference.Object = operationContract.Operation.ReturnType;
                resultReference.AddMetaInfo(new SourceLocationInfo(node.Operation.ReturnType, context));
                context.AddObject(node.Operation.ReturnType, resultReference);
                operationContract.References.Add(resultReference);
            }

            // Parameters
            if (node.Operation.Parameters != null)
            {
                int index = 0;
                foreach (dynamic parameter in node.Operation.Parameters)
                {
                    Reference parameterReference = new Reference();
                    parameterReference.Name = parameter.Name;
                    parameterReference.Object = operationContract.Operation.Parameters[index];
                    parameterReference.AddMetaInfo(new SourceLocationInfo(parameter, context));
                    context.AddObject(parameter, parameterReference);

                    try
                    {
                        NameContext.Current.CheckName(parameterReference.Name, typeof(Reference));
                        operationContract.References.Add(parameterReference);
                    }
                    catch (NameCollisionException exception)
                    {
                        Error_NameExists(parameterReference, exception);
                    }

                    index++;
                }
            }
        }

        /// <summary>
        /// Processes operation contract statement node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_OperationContractStatement(dynamic node)
        {
            if (node.GetBrand() == "Ensures")
            {
                Ensures ensures = new Ensures();

                // Map source location and node to object
                ensures.AddMetaInfo(new SourceLocationInfo(node, context));
                context.AddObject(node, ensures);

                // OperationContract
                ensures.OperationContract = (OperationContract)NameContext.Current.Scope;

                // Text
                ensures.Text = node.Text;

                // Rule
                ensures.Rule = this.Process(node.Rule);
                ensures.Rule.ExpectedType = BuiltInType.Bool;
            }
            if (node.GetBrand() == "Requires")
            {
                Requires requires = new Requires();

                // Map source location and node to object
                requires.AddMetaInfo(new SourceLocationInfo(node, context));
                context.AddObject(node, requires);

                // OperationContract
                requires.OperationContract = (OperationContract)NameContext.Current.Scope;

                // Text
                requires.Text = node.Text;

                // Rule
                requires.Rule = this.Process(node.Rule);
                requires.Rule.ExpectedType = BuiltInType.Bool;

                // Otherwise
                requires.Otherwise = this.Process(node.Otherwise);
                requires.Otherwise.ExpectedType = PseudoType.Object;
            }
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

            // Interface is processed already

            // OperationAuthorizations (optional)
            if (node.OperationAuthorizations != null)
            {
                // Enter scope
                using (new NameContextScope(authorization))
                {
                    foreach (dynamic opc in node.OperationAuthorizations)
                    {
                        this.Process_OperationAuthorization(opc);
                    }
                }
            }
        }

        /// <summary>
        /// Processes operation authorization node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_OperationAuthorization(dynamic node)
        {
            OperationAuthorization operationAuthorization = new OperationAuthorization();

            // Map source location and node to object
            operationAuthorization.AddMetaInfo(new SourceLocationInfo(node, context));
            context.AddObject(node, operationAuthorization);

            try
            {
                // Operation
                operationAuthorization.Operation = this.Process_OperationSignature(node.Operation, ((Authorization)NameContext.Current.Scope).Interface);

                // Contract
                NameContext.Current.CheckName(operationAuthorization.Operation.Name, typeof(OperationAuthorization));
                operationAuthorization.Authorization = (Authorization)NameContext.Current.Scope;

                // Enter scope
                using (new NameContextScope(operationAuthorization))
                {
                    // References
                    this.Process_OperationAuthorizationReferences(node);

                    // Statements
                    if (node.OperationAuthorizationStatements != null)
                    {
                        foreach (dynamic oas in node.OperationAuthorizationStatements)
                        {
                            this.Process_OperationAuthorizationStatement(oas);
                        }
                    }
                }
            }
            catch (OperationValidationException)
            {
            }
            catch (NameCollisionException)
            {
                Error_NameRedundant(operationAuthorization, node.Operation.Name);
            }
        }

        /// <summary>
        /// Creates references for operation authorization.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_OperationAuthorizationReferences(dynamic node)
        {
            OperationAuthorization operationAuthorization = context.GetObject(node);

            // This
            Reference thisReference = new Reference();
            thisReference.Name = "this";
            thisReference.Object = operationAuthorization.Authorization;
            operationAuthorization.References.Add(thisReference);

            // Parameters
            if (node.Operation.Parameters != null)
            {
                int index = 0;
                foreach (dynamic parameter in node.Operation.Parameters)
                {
                    Reference parameterReference = new Reference();
                    parameterReference.Name = parameter.Name;
                    parameterReference.Object = operationAuthorization.Operation.Parameters[index];
                    parameterReference.AddMetaInfo(new SourceLocationInfo(parameter, context));
                    context.AddObject(parameter, parameterReference);

                    try
                    {
                        NameContext.Current.CheckName(parameterReference.Name, typeof(Reference));
                        operationAuthorization.References.Add(parameterReference);
                    }
                    catch (NameCollisionException exception)
                    {
                        Error_NameExists(parameterReference, exception);
                    }

                    index++;
                }
            }

            // Claims
            if (node.OperationAuthorizationClaims != null)
            {
                foreach (dynamic claim in node.OperationAuthorizationClaims)
                {
                    Reference claimReference = new Reference();
                    claimReference.Name = claim.Name;
                    claimReference.AddMetaInfo(new SourceLocationInfo(claim, context));
                    context.AddObject(claim, claimReference);

                    try
                    {
                        List<string> identifiers = this.Process_IdentifierList(claim.Type);
                        SoaObject scope = NameContext.Current.ResolveFirstName(identifiers[0], typeof(Namespace), typeof(ClaimsetType));
                        for (int i = 1; i < identifiers.Count; i++)
                        {
                            scope = NameContext.ResolveName(scope, identifiers[i], typeof(Namespace), typeof(ClaimsetType), typeof(ClaimField));
                        }
                        claimReference.Object = scope;
                    }
                    catch (NameNotFoundException ex)
                    {
                        Error_NameNotFound(new SourceLocationInfo(claim, context), ex);
                    }
                    catch (NameCollisionException ex)
                    {
                        Error_NameCollision(new SourceLocationInfo(claim, context), ex);
                    }

                    try
                    {
                        NameContext.Current.CheckName(claimReference.Name, typeof(Reference));
                        operationAuthorization.References.Add(claimReference);
                    }
                    catch (NameCollisionException exception)
                    {
                        Error_NameExists(claimReference, exception);
                    }
                }
            }
        }

        /// <summary>
        /// Processes operation authorization statement node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_OperationAuthorizationStatement(dynamic node)
        {
            if (node.GetBrand() == "Demand")
            {
                Demand demand = new Demand();

                // Map source location and node to object
                demand.AddMetaInfo(new SourceLocationInfo(node, context));
                context.AddObject(node, demand);

                // OperationContract
                demand.OperationAuthorization = (OperationAuthorization)NameContext.Current.Scope;

                // Text
                demand.Text = node.Text;

                // Rule
                demand.Rule = this.Process(node.Rule);
                demand.Rule.ExpectedType = BuiltInType.Bool;
            }
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

            // Enter scope
            using (new NameContextScope(binding))
            {
                // Transport (obligatory)
                this.Process_Transport(node.Transport);

                // Encoding (obligatory) 
                this.Process_Encoding(node.Encoding);

                // Protocols (optional)
                if (node.Protocols != null)
                {
                    foreach (dynamic protocol in node.Protocols)
                    {
                        this.Process_Protocol(protocol);
                    }
                }
            }
        }

        /// <summary>
        /// Processes transport element node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_Transport(dynamic node)
        {
            TransportBindingElement transportBindingElement = null;

            // Type
            if (node.Name == AttributeHelpers.GetTypeName(typeof(HttpTransportBindingElement)))
            {
                transportBindingElement = new HttpTransportBindingElement();
            }
            else if (node.Name == AttributeHelpers.GetTypeName(typeof(HttpsTransportBindingElement)))
            {
                transportBindingElement = new HttpsTransportBindingElement();
            }
            else
            {
                transportBindingElement = new TransportBindingElement();
            }

            // Map source location and node to object
            transportBindingElement.AddMetaInfo(new SourceLocationInfo(node, context));
            context.AddObject(node, transportBindingElement);

            // Name
            transportBindingElement.Name = node.Name;

            // Binding
            transportBindingElement.Binding = (Binding)NameContext.Current.Scope;

            // Properties (optional)
            if (node.Properties != null)
            {
                // Enter scope
                using (new NameContextScope(transportBindingElement))
                {
                    foreach (dynamic property in node.Properties)
                    {
                        this.Process_BindingProperty(property);
                    }
                }
            }
        }

        /// <summary>
        /// Processes encoding element node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_Encoding(dynamic node)
        {
            EncodingBindingElement encodingBindingElement = null;

            // Type
            if (node.Name == AttributeHelpers.GetTypeName(typeof(SoapEncodingBindingElement)))
            {
                encodingBindingElement = new SoapEncodingBindingElement();
            }
            else
            {
                encodingBindingElement = new EncodingBindingElement();
            }

            // Map source location and node to object
            encodingBindingElement.AddMetaInfo(new SourceLocationInfo(node, context));
            context.AddObject(node, encodingBindingElement);

            // Name
            encodingBindingElement.Name = node.Name;

            // Binding
            encodingBindingElement.Binding = (Binding)NameContext.Current.Scope;

            // Properties (optional)
            if (node.Properties != null)
            {
                // Enter scope
                using (new NameContextScope(encodingBindingElement))
                {
                    foreach (dynamic property in node.Properties)
                    {
                        this.Process_BindingProperty(property);
                    }
                }
            }
        }

        /// <summary>
        /// Processes protocol element node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_Protocol(dynamic node)
        {
            ProtocolBindingElement protocolBindingElement = null;

            // Type
            if (node.Name == AttributeHelpers.GetTypeName(typeof(AddressingProtocolBindingElement)))
            {
                protocolBindingElement = new AddressingProtocolBindingElement();
            }
            else if (node.Name == AttributeHelpers.GetTypeName(typeof(ReliableMessagingProtocolBindingElement)))
            {
                protocolBindingElement = new ReliableMessagingProtocolBindingElement();
            }
            else if (node.Name == AttributeHelpers.GetTypeName(typeof(AtomicTransactionProtocolBindingElement)))
            {
                protocolBindingElement = new AtomicTransactionProtocolBindingElement();
            }
            else if (node.Name == AttributeHelpers.GetTypeName(typeof(MutualCertificateSecurityProtocolBindingElement)))
            {
                protocolBindingElement = new MutualCertificateSecurityProtocolBindingElement();
            }
            else if (node.Name == AttributeHelpers.GetTypeName(typeof(StsSecurityProtocolBindingElement)))
            {
                protocolBindingElement = new StsSecurityProtocolBindingElement();
            }
            else if (node.Name == AttributeHelpers.GetTypeName(typeof(SamlSecurityProtocolBindingElement)))
            {
                protocolBindingElement = new SamlSecurityProtocolBindingElement();
            }
            else if (node.Name == AttributeHelpers.GetTypeName(typeof(SecureConversationSecurityProtocolBindingElement)))
            {
                protocolBindingElement = new SecureConversationSecurityProtocolBindingElement();
            }
            else if (node.Name == AttributeHelpers.GetTypeName(typeof(MutualCertificateBootstrapProtocolBindingElement)))
            {
                protocolBindingElement = new MutualCertificateBootstrapProtocolBindingElement();
            }
            else if (node.Name == AttributeHelpers.GetTypeName(typeof(StsBootstrapProtocolBindingElement)))
            {
                protocolBindingElement = new StsBootstrapProtocolBindingElement();
            }
            else if (node.Name == AttributeHelpers.GetTypeName(typeof(SamlBootstrapProtocolBindingElement)))
            {
                protocolBindingElement = new SamlBootstrapProtocolBindingElement();
            }
            else
            {
                protocolBindingElement = new ProtocolBindingElement();
            }

            // Map source location and node to object
            protocolBindingElement.AddMetaInfo(new SourceLocationInfo(node, context));
            context.AddObject(node, protocolBindingElement);

            // Name
            protocolBindingElement.Name = node.Name;

            // Binding
            try
            {
                NameContext.Current.CheckName(protocolBindingElement.Name, typeof(ProtocolBindingElement));
                protocolBindingElement.Binding = (Binding)NameContext.Current.Scope;
            }
            catch (NameCollisionException)
            {
                Error_NameRedundant(protocolBindingElement, typeof(ProtocolBindingElement), protocolBindingElement.Name);
            }

            // Properties (optional)
            if (node.Properties != null)
            {
                // Enter scope
                using (new NameContextScope(protocolBindingElement))
                {
                    foreach (dynamic property in node.Properties)
                    {
                        this.Process_BindingProperty(property);
                    }
                }
            }
        }

        /// <summary>
        /// Processes binding element property node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_BindingProperty(dynamic node)
        {
            BindingElementProperty bindingElementProperty = new BindingElementProperty();

            // Map source location and node to object
            bindingElementProperty.AddMetaInfo(new SourceLocationInfo(node, context));
            context.AddObject(node, bindingElementProperty);

            // BindingElement
            try
            {
                NameContext.Current.CheckName(node.Name, typeof(BindingElementProperty));
                bindingElementProperty.BindingElement = (BindingElement)NameContext.Current.Scope;
            }
            catch (NameCollisionException exception)
            {
                Error_NameExists(bindingElementProperty, exception);
            }

            // Name
            bindingElementProperty.Name = node.Name;

            // Value
            bindingElementProperty.Value = this.Process(node.Value);
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

            // Interface is processed already

            // Binding is processed already

            // Authorization (optional)
            if (node.Properties.Authorization != null)
            {
                SourceLocationInfo authorizationLocation = new SourceLocationInfo(node.Properties.Authorization, context);
                try
                {
                    endpoint.Authorization = this.Process_NamespacedTypeReference(node.Properties.Authorization, typeof(Authorization));
                    if (endpoint.Authorization.Interface != endpoint.Interface)
                    {
                        if (endpoint.Authorization.Interface.GetSuperTypes().Contains(endpoint.Interface))
                        {
                            // Authorization's interface is stronger than endpoint's
                            Warning_InterfaceMismatch(authorizationLocation, typeof(Authorization));
                        } else if(endpoint.Interface.GetSuperTypes().Contains(endpoint.Authorization.Interface)) {
                            // Authorization's interface is weaker than endpoints's
                            Warning_InterfaceMismatch(authorizationLocation, typeof(Authorization));
                        }
                        else
                        {
                            // Authorization's interface does not match endpoint's
                            Error_InterfaceMismatch(authorizationLocation, typeof(Authorization));
                        }
                    }
                }
                catch (NameNotFoundException exception)
                {
                    Error_NameNotFound(authorizationLocation, exception);
                }
                catch (NameCollisionException exception)
                {
                    Error_NameCollision(authorizationLocation, exception);
                }
            }

            // Contract (optional)
            if (node.Properties.Contract != null)
            {
                SourceLocationInfo contractLocation = new SourceLocationInfo(node.Properties.Contract, context);
                try
                {
                    endpoint.Contract = this.Process_NamespacedTypeReference(node.Properties.Contract, typeof(Contract));
                    if (endpoint.Contract.Interface != endpoint.Interface)
                    {
                        if (endpoint.Contract.Interface.GetSuperTypes().Contains(endpoint.Interface))
                        {
                            // Contract's interface is stronger than endpoint's
                            Warning_InterfaceMismatch(contractLocation, typeof(Contract));
                        }
                        else if (endpoint.Interface.GetSuperTypes().Contains(endpoint.Contract.Interface))
                        {
                            // Contract's interface is weaker than endpoints's
                            Warning_InterfaceMismatch(contractLocation, typeof(Contract));
                        }
                        else
                        {
                            // Contract's interface does not match endpoint's
                            Error_InterfaceMismatch(contractLocation, typeof(Contract));
                        }
                    }
                }
                catch (NameNotFoundException exception)
                {
                    Error_NameNotFound(contractLocation, exception);
                }
                catch (NameCollisionException exception)
                {
                    Error_NameCollision(contractLocation, exception);
                }
            }

            // Address is processed already
        }

        /// <summary>
        /// Processes unary expression node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <remarks>The expression.</remarks>
        private Expression Process_UnaryExpression(dynamic node)
        {
            UnaryExpression expression = null;
            try
            {
                ExpressionType type = Enum.Parse(typeof(ExpressionType), node.NodeType);
                switch (type)
                {
                    case ExpressionType.Negate:
                    case ExpressionType.UnaryPlus:
                    case ExpressionType.Not:
                    case ExpressionType.OnesComplement:
                        expression = new UnaryExpression(type, this.Process(node.Expression));
                        break;
                    case ExpressionType.Convert:
                    case ExpressionType.TypeAs:
                        expression = new UnaryExpression(type, this.Process(node.Expression), this.Process_TypeReference(node.Type));
                        break;
                }
                expression.AddMetaInfo(new SourceLocationInfo(node, context));
                context.AddObject(node, expression);
            }
            catch (NameNotFoundException exception)
            {
                Error_NameNotFound(new SourceLocationInfo(node.Type, context), exception);
            }
            catch (NameCollisionException exception)
            {
                Error_NameCollision(new SourceLocationInfo(node.Type, context), exception);
            }
            return expression;
        }

        /// <summary>
        /// Processes new expression node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>The expression.</returns>
        private Expression Process_NewExpression(dynamic node)
        {
            //List<Expression> arguments = new List<Expression>();
            //if (node.Arguments != null)
            //{
            //    foreach (var argument in node.Arguments)
            //    {
            //        arguments.Add(this.Process(argument));
            //    }
            //}
            List<MemberInitExpression> members = new List<MemberInitExpression>();
            if (node.Members != null)
            {
                foreach (var member in node.Members)
                {
                    members.Add(this.Process(member));
                }
            }

            NewExpression expression = null;
            try
            {
                expression = new NewExpression(/*arguments, */members, this.Process_TypeReference(node.Type, typeof(StructType), typeof(EnumType), typeof(ExceptionType)));
                expression.AddMetaInfo(new SourceLocationInfo(node, context));
                context.AddObject(node, expression);
            }
            catch (NameNotFoundException exception)
            {
                Error_NameNotFound(new SourceLocationInfo(node.Type, context), exception);
            }
            catch (NameCollisionException exception)
            {
                Error_NameCollision(new SourceLocationInfo(node.Type, context), exception);
            }
            return expression;
        }

        /// <summary>
        /// Processes member init expression node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>The expression.</returns>
        private Expression Process_MemberInitExpression(dynamic node)
        {
            MemberInitExpression expression = new MemberInitExpression(node.Name, this.Process(node.Value));
            expression.AddMetaInfo(new SourceLocationInfo(node, context));
            context.AddObject(node, expression);
            return expression;
        }

        /// <summary>
        /// Processes new array expression node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>The expression.</returns>
        private Expression Process_NewArrayExpression(dynamic node)
        {
            List<Expression> expressions = new List<Expression>();
            if (node.Expressions != null)
            {
                foreach (var expr in node.Expressions)
                {
                    expressions.Add(this.Process(expr));
                }
            }

            NewArrayExpression expression = null;
            try
            {
                expression = new NewArrayExpression(Enum.Parse(typeof(ExpressionType), node.NodeType), expressions, this.Process_TypeReference(node.Type));
                expression.AddMetaInfo(new SourceLocationInfo(node, context));
                context.AddObject(node, expression);
            }
            catch (NameNotFoundException exception)
            {
                Error_NameNotFound(new SourceLocationInfo(node.Type, context), exception);
            }
            catch (NameCollisionException exception)
            {
                Error_NameCollision(new SourceLocationInfo(node.Type, context), exception);
            }
            return expression;
        }

        /// <summary>
        /// Processes binary expression node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>The expression.</returns>
        private Expression Process_BinaryExpression(dynamic node)
        {
            BinaryExpression expression = new BinaryExpression(Enum.Parse(typeof(ExpressionType), node.NodeType), this.Process(node.Left), this.Process(node.Right));
            expression.AddMetaInfo(new SourceLocationInfo(node, context));
            context.AddObject(node, expression);
            return expression;
        }

        /// <summary>
        /// Processes type binary expression node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>The expression.</returns>
        private Expression Process_TypeBinaryExpression(dynamic node)
        {
            TypeBinaryExpression expression = null;
            try
            {
                expression = new TypeBinaryExpression(this.Process(node.Expression), this.Process_TypeReference(node.Type));
                expression.AddMetaInfo(new SourceLocationInfo(node, context));
                context.AddObject(node, expression);
            }
            catch (NameNotFoundException exception)
            {
                Error_NameNotFound(new SourceLocationInfo(node.Type, context), exception);
            }
            catch (NameCollisionException exception)
            {
                Error_NameCollision(new SourceLocationInfo(node.Type, context), exception);
            }
            return expression;
        }

        /// <summary>
        /// Processes conditional expression node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>The expression.</returns>
        private Expression Process_ConditionalExpression(dynamic node)
        {
            ConditionalExpression expression = new ConditionalExpression(this.Process(node.Test), this.Process(node.IfThen), this.Process(node.IfElse));
            expression.AddMetaInfo(new SourceLocationInfo(node, context));
            context.AddObject(node, expression);
            return expression;
        }

        /// <summary>
        /// Processes lambda expression node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>The expression.</returns>
        private Expression Process_LambdaExpression(dynamic node)
        {
            LambdaExpression expression = new LambdaExpression();
            expression.AddMetaInfo(new SourceLocationInfo(node, context));
            context.AddObject(node, expression);

            // Enter scope
            using (new NameContextScope(expression))
            {

                // Parameters (optional)
                if (node.Parameters != null)
                {
                    foreach(dynamic param in node.Parameters)
                    {
                        expression.Parameters.Add(this.Process(param));
                    }
                }

                // Body
                expression.Body = this.Process(node.Body);

                expression.BindLazyProperties();
            }

            return expression;
        }

        /// <summary>
        /// Processes lambda parameter node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>The expression.</returns>
        private Expression Process_LambdaParameter(dynamic node)
        {
            LambdaParameter expression = new LambdaParameter(node.Name, node.Type);
            expression.AddMetaInfo(new SourceLocationInfo(node, context));
            context.AddObject(node, expression);
            return expression;
        }

        /// <summary>
        /// Processes constant expression node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>The expression.</returns>
        private Expression Process_ConstantExpression(dynamic node)
        {
            ConstantExpression expression = null;

            dynamic literal = node.Value;
            switch ((string)literal.GetBrand().ToString())
            {
                case "Null": expression = new ConstantExpression(PseudoType.Object, null); break;
                case "Boolean": expression = new ConstantExpression(BuiltInType.Bool, bool.Parse(literal.StringValue[0])); break;
                case "Integer": expression = new ConstantExpression(BuiltInType.Long, long.Parse(literal.StringValue)); break;
                case "Float": expression = new ConstantExpression(BuiltInType.Double, double.Parse(literal.StringValue, CultureInfo.InvariantCulture)); break;
                case "String": expression = new ConstantExpression(BuiltInType.String, literal.StringValue.Replace("\"","")); break;
                case "Guid": expression = new ConstantExpression(BuiltInType.Guid, Guid.Parse(literal.StringValue)); break;
                case "Date": expression = new ConstantExpression(BuiltInType.Date, literal.StringValue); break;
                case "Time": expression = new ConstantExpression(BuiltInType.Time, literal.StringValue); break;
                case "DateTime": expression = new ConstantExpression(BuiltInType.DateTime, DateTime.Parse(literal.StringValue)); break;
                case "TimeSpan": expression = new ConstantExpression(BuiltInType.TimeSpan, TimeSpan.Parse(literal.StringValue)); break;
            }
            expression.AddMetaInfo(new SourceLocationInfo(node, context));
            context.AddObject(node, expression);
            return expression;
        }

        /// <summary>
        /// Processes default expression node.
        /// </summary>
        /// <param name="expression">The node.</param>
        /// <returns>The expression.</returns>
        private Expression Process_DefaultExpression(dynamic node)
        {
            DefaultExpression expression = null;
            try
            {
                expression = new DefaultExpression(this.Process_TypeReference(node.Type));
                expression.AddMetaInfo(new SourceLocationInfo(node, context));
                context.AddObject(node, expression);
            }
            catch (NameNotFoundException exception)
            {
                Error_NameNotFound(new SourceLocationInfo(node.Type, context), exception);
            }
            catch (NameCollisionException exception)
            {
                Error_NameCollision(new SourceLocationInfo(node.Type, context), exception);
            }
            return expression;
        }

        /// <summary>
        /// Processes old expression node.
        /// </summary>
        /// <param name="expression">The node.</param>
        /// <returns>The expression.</returns>
        private Expression Process_OldExpression(dynamic node)
        {
            OldExpression expression = null;
            try
            {
                expression = new OldExpression(NameContext.Current.ResolveFirstName(node.Name, typeof(Reference)));
                expression.AddMetaInfo(new SourceLocationInfo(node, context));
                context.AddObject(node, expression);
            }
            catch (NameNotFoundException ex)
            {
                Error_NameNotFound(new SourceLocationInfo(node, context), ex);
            }
            catch (NameCollisionException ex)
            {
                Error_NameCollision(new SourceLocationInfo(node, context), ex);
            }
            return expression;
        }

        /// <summary>
        /// Processes identifier expression node.
        /// </summary>
        /// <param name="expression">The node.</param>
        /// <returns>The expression.</returns>
        private Expression Process_IdentifierExpression(dynamic node)
        {
            IdentifierExpression expression = null;
            try
            {
                expression = new IdentifierExpression(NameContext.Current.ResolveFirstName(node.Name, typeof(Reference), typeof(Namespace), typeof(EnumType)));
                expression.AddMetaInfo(new SourceLocationInfo(node, context));
                context.AddObject(node, expression);
            }
            catch (NameNotFoundException ex)
            {
                Error_NameNotFound(new SourceLocationInfo(node, context), ex);
            }
            catch (NameCollisionException ex)
            {
                Error_NameCollision(new SourceLocationInfo(node, context), ex);
            }
            return expression;
        }

        /// <summary>
        /// Processes index expression node.
        /// </summary>
        /// <param name="expression">The node.</param>
        /// <returns>The expression.</returns>
        private Expression Process_IndexExpression(dynamic node)
        {
            IndexExpression expression = new IndexExpression(this.Process(node.Object), this.Process(node.Argument));
            expression.AddMetaInfo(new SourceLocationInfo(node, context));
            context.AddObject(node, expression);
            return expression;
        }

        ///// <summary>
        ///// Processes invocation expression node.
        ///// </summary>
        ///// <param name="expression">The node.</param>
        ///// <returns>The expression.</returns>
        //private Expression Process_InvocationExpression(dynamic node)
        //{
        //    List<Expression> expressions = new List<Expression>();
        //    if (node.Expressions != null)
        //    {
        //        foreach (var expr in node.Arguments)
        //        {
        //            expressions.Add(this.Process(expr));
        //        }
        //    }
        //    InvocationExpression expression = new InvocationExpression(this.Process(node.Expression), expressions);
        //    expression.AddMetaInfo(new SourceLocationInfo(node, context));
        //    context.AddObject(node, expression);
        //    return expression;
        //}

        /// <summary>
        /// Processes member expression node.
        /// </summary>
        /// <param name="expression">The node.</param>
        /// <returns>The expression.</returns>
        private Expression Process_MemberExpression(dynamic node)
        {
            MemberExpression expression = new MemberExpression(this.Process(node.Object), node.Name);
            expression.AddMetaInfo(new SourceLocationInfo(node, context));
            context.AddObject(node, expression);
            return expression;
        }

        /// <summary>
        /// Processes method call expression node.
        /// </summary>
        /// <param name="expression">The node.</param>
        /// <returns>The expression.</returns>
        private Expression Process_MethodCallExpression(dynamic node)
        {
            List<Expression> expressions = new List<Expression>();
            if (node.Arguments != null)
            {
                foreach (var expr in node.Arguments)
                {
                    expressions.Add(this.Process(expr));
                }
            }
            MethodCallExpression expression = new MethodCallExpression(this.Process(node.Object), node.Operation, expressions);
            expression.AddMetaInfo(new SourceLocationInfo(node, context));
            context.AddObject(node, expression);
            return expression;
        }
    }
}
