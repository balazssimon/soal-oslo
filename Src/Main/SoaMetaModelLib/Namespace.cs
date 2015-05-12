using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sb.Meta;
using SoaMetaModel.MetaInfo;

namespace SoaMetaModel
{
    public class Namespace : Declaration
    {
        public Namespace()
        {
            this.Declarations = new ModelList<Declaration>(this, DeclarationsProperty);
        }

        public string Uri
        {
            get;
            set;
        }

        private static int prefixCounter = 0;
        private string prefix;

        public string Prefix
        {
            get
            {
                if (this.prefix == null)
                {
                    this.prefix = "ns" + Namespace.prefixCounter++;
                }
                return this.prefix;
            }
            set
            {
                this.prefix = value;
            }
        }

        [Opposite(typeof(Declaration), "Namespace")]
        public ModelList<Declaration> Declarations
        {
            get { return (ModelList<Declaration>)GetValue(DeclarationsProperty); }
            private set { SetValue(DeclarationsProperty, value); }
        }
        public static readonly ModelProperty DeclarationsProperty = ModelProperty.Register("Declarations", typeof(ModelList<Declaration>), typeof(Namespace));

        public IEnumerable<Namespace> Imports
        {
            get
            {
                return GetReferencedNamespaces();
            }
        }

        private IEnumerable<Namespace> GetReferencedNamespaces()
        {
            HashSet<Namespace> imports = new HashSet<Namespace>();
            imports.Add(this);

            foreach (Declaration declaration in Declarations)
            {
                if (declaration is StructType)
                {
                    StructType @struct = ((StructType)declaration);
                    if (@struct.SuperType != null)
                    {
                        imports.Add(@struct.SuperType.Namespace);
                    }
                    foreach (StructField field in @struct.Fields)
                    {
                        imports.Add(field.Type.Namespace);
                    }
                }
                else if (declaration is ExceptionType)
                {
                    ExceptionType @exception = ((ExceptionType)declaration);
                    if (@exception.SuperType != null)
                    {
                        imports.Add(@exception.SuperType.Namespace);
                    }
                    foreach (ExceptionField field in @exception.Fields)
                    {
                        imports.Add(field.Type.Namespace);
                    }
                }
                else if (declaration is ClaimsetType)
                {
                    ClaimsetType claimset = ((ClaimsetType)declaration);
                    foreach (ClaimField field in claimset.Fields)
                    {
                        imports.Add(field.Type.Namespace);
                    }
                }
                else if (declaration is Interface)
                {
                    Interface @interface = (Interface)declaration;
                    foreach (Interface super in @interface.SuperInterfaces)
                    {
                        imports.Add(super.Namespace);
                    }
                    foreach (Operation operation in @interface.Operations)
                    {
                        imports.Add(operation.ReturnType.Namespace);
                        foreach (OperationParameter parameter in operation.Parameters)
                        {
                            imports.Add(parameter.Type.Namespace);
                        }
                        foreach (ExceptionType @exception in operation.Exceptions)
                        {
                            imports.Add(@exception.Namespace);
                        }
                    }
                }
                else if (declaration is Authorization)
                {
                    Authorization authorization = (Authorization)declaration;
                    imports.Add(authorization.Interface.Namespace);
                    foreach (OperationAuthorization operation in authorization.OperationAuthorizations)
                    {
                        imports.Add(operation.Operation.ReturnType.Namespace);
                        foreach (OperationParameter parameter in operation.Operation.Parameters)
                        {
                            imports.Add(parameter.Type.Namespace);
                        }
                        foreach (ExceptionType @exception in operation.Operation.Exceptions)
                        {
                            imports.Add(@exception.Namespace);
                        }
                        foreach (Reference reference in operation.References)
                        {
                            if (reference.Object is ClaimsetType)
                            {
                                imports.Add(((ClaimsetType)reference.Object).Namespace);
                            }
                            if (reference.Object is ClaimField)
                            {
                                imports.Add(((ClaimField)reference.Object).Claimset.Namespace);
                            }
                        }
                        foreach (OperationAuthorizationStatement statement in operation.OperationAuthorizationStatements)
                        {
                            if (statement is Demand)
                            {
                                Demand demand = (Demand)statement;
                                GetReferencedNamespaces(imports, demand.Rule);
                            }
                        }
                    }
                }
                else if (declaration is Contract)
                {
                    Contract contract = (Contract)declaration;
                    imports.Add(contract.Interface.Namespace);
                    foreach (OperationContract operation in contract.OperationContracts)
                    {
                        imports.Add(operation.Operation.ReturnType.Namespace);
                        foreach (OperationParameter parameter in operation.Operation.Parameters)
                        {
                            imports.Add(parameter.Type.Namespace);
                        }
                        foreach (ExceptionType @exception in operation.Operation.Exceptions)
                        {
                            imports.Add(@exception.Namespace);
                        }
                        foreach (OperationContractStatement statement in operation.OperationContractStatements)
                        {
                            if(statement is Requires) {
                                Requires requires = (Requires)statement;
                                GetReferencedNamespaces(imports, requires.Rule);
                                if (requires.Otherwise != null)
                                {
                                    GetReferencedNamespaces(imports, requires.Otherwise);
                                }
                            }
                            else if (statement is Ensures)
                            {
                                Ensures ensures = (Ensures)statement;
                                GetReferencedNamespaces(imports, ensures.Rule);
                            }
                        }
                    }
                }
                else if (declaration is Endpoint)
                {
                    Endpoint endpoint = (Endpoint)declaration;
                    imports.Add(endpoint.Interface.Namespace);
                    imports.Add(endpoint.Binding.Namespace);
                    if (endpoint.Authorization != null)
                    {
                        imports.Add(endpoint.Authorization.Namespace);
                    }
                    if (endpoint.Contract != null)
                    {
                        imports.Add(endpoint.Contract.Namespace);
                    }
                }
                else if (declaration is Namespace)
                {
                    imports.Add((Namespace)declaration);
                }
            }

            imports.Remove(null);
            imports.Remove(this);
            return imports;
        }

        private void GetReferencedNamespaces(HashSet<Namespace> imports, Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Convert:
                case ExpressionType.TypeAs:
                    imports.Add(((UnaryExpression)expression).Type.Namespace);
                    break;
                case ExpressionType.New:
                    imports.Add(((NewExpression)expression).Type.Namespace);
                    break;
                case ExpressionType.NewArrayInit:
                case ExpressionType.NewArrayBounds:
                    imports.Add(((NewArrayExpression)expression).ItemType.Namespace);
                    break;
                case ExpressionType.TypeIs:
                    imports.Add(((TypeBinaryExpression)expression).TypeOperand.Namespace);
                    break;
                //case ExpressionType.Parameter:
                case ExpressionType.Default:
                    imports.Add(((DefaultExpression)expression).Type.Namespace);
                    break;
                default:
                    break;
            }
            foreach (Expression child in expression.Children)
            {
                GetReferencedNamespaces(imports, child);
            }
        }
    }
}
