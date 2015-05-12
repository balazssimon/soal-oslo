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
    /// Parser class for the declarations only.
    /// </summary>
    /// <remarks>
    /// This is the first phase of parsing. Only the declarations are processed, thus they can be resolved
    /// in the next phase.
    /// </remarks>
    public class DeclarationParser : SoaLanguageParser
    {
        /// <summary>
        /// Constructs the declaration parser.
        /// </summary>
        /// <param name="errorReporter">The error reporter instance.</param>
        /// <param name="context">The parser context.</param>
        public DeclarationParser(ErrorReporter errorReporter, SoaLanguageContext context)
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
                this.Process_ModelTypes();
                this.Process(root);
            }
        }

        /// <summary>
        /// Declare predefined types representing existing model types.
        /// </summary>
        private void Process_ModelTypes()
        {
            this.Process_ModelEnum(typeof(HttpsClientAuthentication));
            this.Process_ModelEnum(typeof(SoapVersion));
            this.Process_ModelEnum(typeof(AddressingVersion));
            this.Process_ModelEnum(typeof(ReliableMessagingVersion));
            this.Process_ModelEnum(typeof(ReliableMessagingDelivery));
            this.Process_ModelEnum(typeof(AtomicTransactionVersion));
            this.Process_ModelEnum(typeof(SecurityAlgorithmSuite));
            this.Process_ModelEnum(typeof(SecurityHeaderLayout));
            this.Process_ModelEnum(typeof(SecurityProtectionOrder));
            this.Process_ModelEnum(typeof(IssuedTokenVersion));
            this.Process_ModelEnum(typeof(IssuedTokenType));
            this.Process_ModelStruct(typeof(IssuedTokenIssuer));
        }

        /// <summary>
        /// Declare a new struct type in the current scope based on an actual class.
        /// </summary>
        /// <remarks>
        /// Used to declare predefined struct types representing existing classes in metamodel.
        /// </remarks>
        /// <param name="type">The class type.</param>
        private void Process_ModelStruct(System.Type type)
        {
            if (NameContext.Current.Scope.Model.Instances.OfType<Type>().Count(t => t.UnderlyingType == type) == 0)
            {
                StructType structType = new StructType();

                // Namespace
                structType.Namespace = (Namespace)NameContext.Current.Scope;

                // Name
                structType.Name = AttributeHelpers.GetTypeName(type);

                // Underlying type
                structType.UnderlyingType = type;

                // Hidden
                structType.AddMetaInfo(new HiddenInfo());

                // Enter scope
                using (new NameContextScope(structType))
                {
                    // Fields
                    foreach (PropertyInfo property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                    {
                        StructField structField = new StructField();
                        structField.Name = AttributeHelpers.GetMemberName(property);
                        structField.Type = BuiltInType.GetBuiltInType(property.PropertyType);
                        structField.Struct = structType;
                        // Hidden
                        structField.AddMetaInfo(new HiddenInfo());
                    }
                }
            }
        }

        /// <summary>
        /// Declare a new enum type in the current scope based on an actual enum.
        /// </summary>
        /// <remarks>
        /// Used to declare predefined enum types representing existing enums in metamodel.
        /// </remarks>
        /// <param name="type">The enum type.</param>
        private void Process_ModelEnum(System.Type type)
        {
            if (NameContext.Current.Scope.Model.Instances.OfType<Type>().Count(t => t.UnderlyingType == type) == 0)
            {
                EnumType enumType = new EnumType();

                // Namespace
                enumType.Namespace = (Namespace)NameContext.Current.Scope;

                // Name
                enumType.Name = AttributeHelpers.GetTypeName(type);

                // Underlying type
                enumType.UnderlyingType = type;

                // Hidden
                enumType.AddMetaInfo(new HiddenInfo());

                // Enter scope
                using (new NameContextScope(enumType))
                {
                    // Values
                    foreach (FieldInfo field in type.GetFields(BindingFlags.Static | BindingFlags.Public))
                    {
                        EnumValue enumValue = new EnumValue();
                        enumValue.Name = AttributeHelpers.GetMemberName(field);
                        enumValue.Enum = enumType;
                        // Hidden
                        enumValue.AddMetaInfo(new HiddenInfo());
                    }
                }
            }
        }

        /// <summary>
        /// Processes namespace node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_Namespace(dynamic node)
        {
            // Find or create the namespace (and its parents, if necessary)
            Namespace @namespace = this.Process_NamespaceReference(node.Name);

            // Map source location and node to object
            SourceLocationInfo location = new SourceLocationInfo(node, context);
            @namespace.AddMetaInfo(location);
            context.AddObject(node, @namespace);

            // Uri (optional)
            if (node.Uri != null)
            {
                if (@namespace.Uri != null)
                {
                    Error_NamespaceUri(location, @namespace);
                }
                else
                {
                    @namespace.Uri = node.Uri.Replace("\"", "");
                }
            }

            // Enter scope
            using (new NameContextScope(@namespace))
            {
                // Imports are not processed yet

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
        /// <remarks>
        /// If the namespace (or any of its parent namespaces) does not exist, it will be created.
        /// </remarks>
        /// <param name="node">The namespace reference node.</param>
        /// <returns>The namespace identified by the node.</returns>
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
        private Namespace Process_NamespaceReference(List<string> identifiers, SoaObject scope)
        {
            Namespace @namespace = null;
            try
            {
                // Try to find the namespace identified by the first of the remaining name parts
                @namespace = (Namespace)NameContext.ResolveName(scope, identifiers[0], typeof(Namespace));
            }
            catch (NameNotFoundException)
            {
                // If not found, create
                @namespace = new Namespace();
                @namespace.Name = identifiers[0];
                @namespace.Namespace = (Namespace)scope;
            }

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
        /// Processes enum node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_EnumType(dynamic node)
        {
            EnumType enumType = new EnumType();

            // Map source location and node to object
            enumType.AddMetaInfo(new SourceLocationInfo(node, context));
            context.AddObject(node, enumType);

            // Namespace (from parent)
            try
            {
                NameContext.Current.CheckName(node.Name, typeof(EnumType), typeof(StructType), typeof(ExceptionType), typeof(ClaimsetType));
                enumType.Namespace = (Namespace)NameContext.Current.Scope;
            }
            catch (NameCollisionException exception)
            {
                Error_NameExists(enumType, exception);
            }

            // Name (obligatory)
            enumType.Name = node.Name;

            // Values are not processed yet
        }

        /// <summary>
        /// Processes struct node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_StructType(dynamic node)
        {
            StructType structType = new StructType();

            // Map source location and node to object
            structType.AddMetaInfo(new SourceLocationInfo(node, context));
            context.AddObject(node, structType);

            // Namespace (from parent)
            try
            {
                NameContext.Current.CheckName(node.Name, typeof(EnumType), typeof(StructType), typeof(ExceptionType), typeof(ClaimsetType));
                structType.Namespace = (Namespace)NameContext.Current.Scope;
            }
            catch (NameCollisionException exception)
            {
                Error_NameExists(structType, exception);
            }

            // Name (obligatory)
            structType.Name = node.Name;

            // Fields are not processed yet

            // SuperType is not processed yet
        }

        /// <summary>
        /// Processes exception node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_ExceptionType(dynamic node)
        {
            ExceptionType exceptionType = new ExceptionType();

            // Map source location and node to object
            exceptionType.AddMetaInfo(new SourceLocationInfo(node, context));
            context.AddObject(node, exceptionType);

            // Namespace (from parent)
            try
            {
                NameContext.Current.CheckName(node.Name, typeof(EnumType), typeof(StructType), typeof(ExceptionType), typeof(ClaimsetType));
                exceptionType.Namespace = (Namespace)NameContext.Current.Scope;
            }
            catch (NameCollisionException exception)
            {
                Error_NameExists(exceptionType, exception);
            }

            // Name (obligatory)
            exceptionType.Name = node.Name;

            // Fields are not processed yet

            // SuperType is not processed yet
        }

        /// <summary>
        /// Processes claim node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_ClaimsetType(dynamic node)
        {
            ClaimsetType claimsetType = new ClaimsetType();

            // Map source location and node to object
            claimsetType.AddMetaInfo(new SourceLocationInfo(node, context));
            context.AddObject(node, claimsetType);

            // Namespace (from parent)
            try
            {
                NameContext.Current.CheckName(node.Name, typeof(EnumType), typeof(StructType), typeof(ExceptionType), typeof(ClaimsetType));
                claimsetType.Namespace = (Namespace)NameContext.Current.Scope;
            }
            catch (NameCollisionException exception)
            {
                Error_NameExists(claimsetType, exception);
            }

            // Name (obligatory)
            claimsetType.Name = node.Name;

            // Uri (optional)
            if (node.Uri != null)
            {
                claimsetType.Uri = node.Uri.Replace("\"", "");
            }

            // Fields are not processed yet
        }

        /// <summary>
        /// Processes interface node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_Interface(dynamic node)
        {
            Interface @interface = new Interface();

            // Map source location and node to object
            @interface.AddMetaInfo(new SourceLocationInfo(node, context));
            context.AddObject(node, @interface);

            // Namespace (from parent)
            try
            {
                NameContext.Current.CheckName(node.Name, typeof(Interface));
                @interface.Namespace = (Namespace)NameContext.Current.Scope;
            }
            catch (NameCollisionException exception)
            {
                Error_NameExists(@interface, exception);
            }

            // Name (obligatory)
            @interface.Name = node.Name;

            // Version (obligatory)
            string s = node.Version;
            string[] versions = s.Split('.');
            @interface.Version = new VersionInfo();
            @interface.Version.Major = System.Convert.ToInt32(versions[0]);
            @interface.Version.Minor = System.Convert.ToInt32(versions[1]);

            // Operations are not processed yet

            // SuperInterfaces are not processed yet
        }

        /// <summary>
        /// Processes contract node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_Contract(dynamic node)
        {
            Contract contract = new Contract();

            // Map source location and node to object
            contract.AddMetaInfo(new SourceLocationInfo(node, context));
            context.AddObject(node, contract);

            // Namespace (from parent)
            try
            {
                NameContext.Current.CheckName(node.Name, typeof(Contract));
                contract.Namespace = (Namespace)NameContext.Current.Scope;
            }
            catch (NameCollisionException exception)
            {
                Error_NameExists(contract, exception);
            }

            // Name (obligatory)
            contract.Name = node.Name;

            // Interface is not processed yet

            // OperationContracts are not processed yet
        }

        /// <summary>
        /// Processes authorization node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_Authorization(dynamic node)
        {
            Authorization authorization = new Authorization();

            // Map source location and node to object
            authorization.AddMetaInfo(new SourceLocationInfo(node, context));
            context.AddObject(node, authorization);

            // Namespace (from parent)
            try
            {
                NameContext.Current.CheckName(node.Name, typeof(Authorization));
                authorization.Namespace = (Namespace)NameContext.Current.Scope;
            }
            catch (NameCollisionException exception)
            {
                Error_NameExists(authorization, exception);
            }

            // Name (obligatory)
            authorization.Name = node.Name;

            // Interface is not processed yet

            // OperationAuthorizations are not processed yet
        }

        /// <summary>
        /// Processes binding node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void Process_Binding(dynamic node)
        {
            Binding binding = new Binding();

            // Map source location and node to object
            binding.AddMetaInfo(new SourceLocationInfo(node, context));
            context.AddObject(node, binding);

            // Namespace (from parent)
            try
            {
                NameContext.Current.CheckName(node.Name, typeof(Binding));
                binding.Namespace = (Namespace)NameContext.Current.Scope;
            }
            catch (NameCollisionException exception)
            {
                Error_NameExists(binding, exception);
            }

            // Name (obligatory)
            binding.Name = node.Name;

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
            Endpoint endpoint = new Endpoint();

            // Map source location and node to object
            endpoint.AddMetaInfo(new SourceLocationInfo(node, context));
            context.AddObject(node, endpoint);

            // Namespace (from parent)
            try
            {
                NameContext.Current.CheckName(node.Name, typeof(Endpoint));
                endpoint.Namespace = (Namespace)NameContext.Current.Scope;
            }
            catch (NameCollisionException exception)
            {
                Error_NameExists(endpoint, exception);
            }

            // Name (obligatory)
            endpoint.Name = node.Name;

            // Interface is not processed yet

            // Binding is not processed yet

            // Authorization is not processed yet

            // Contract is not processed yet

            // Address (obligatory)
            endpoint.Address = new EndpointAddress();
            endpoint.Address.Uri = node.Properties.Location.Replace("\"", "");
        }
    }
}
