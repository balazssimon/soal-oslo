using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sb.Meta;

namespace SoaMetaModel
{
    using MetaInfo;

    public class Interface : Declaration
    {
        private List<Interface> superInterfaces = new List<Interface>();

        public Interface()
        {
            this.SuperInterfaces = new ModelList<Interface>(this, SuperInterfacesProperty);
            this.Operations = new ModelList<Operation>(this, OperationsProperty);
        }


        public ModelList<Interface> SuperInterfaces
        {
            get { return (ModelList<Interface>)GetValue(SuperInterfacesProperty); }
            private set { SetValue(SuperInterfacesProperty, value); }
        }
        public static readonly ModelProperty SuperInterfacesProperty = ModelProperty.Register("SuperInterfaces", typeof(ModelList<Interface>), typeof(Interface));


        [Opposite(typeof(Operation), "Interface")]
        public ModelList<Operation> Operations
        {
            get { return (ModelList<Operation>)GetValue(OperationsProperty); }
            private set { SetValue(OperationsProperty, value); }
        }
        public static readonly ModelProperty OperationsProperty = ModelProperty.Register("Operations", typeof(ModelList<Operation>), typeof(Interface));

        
        public VersionInfo Version
        {
            get;
            set;
        }

        public virtual IEnumerable<Interface> GetSuperTypes(bool throws = false)
        {
            List<Interface> superTypes = new List<Interface>();
            this.GetSuperTypes(superTypes, new Stack<Interface>(), throws);
            return superTypes;
        }

        protected virtual void GetSuperTypes(List<Interface> superTypes, Stack<Interface> branch, bool throws = false)
        {
            branch.Push(this);
            foreach (Interface superType in this.SuperInterfaces)
            {
                // Circular inheritance condition
                if (!branch.Contains(superType))
                {
                    // Multiple-path inheritance condition
                    if (!superTypes.Contains(superType))
                    {
                        superTypes.Add(superType);
                        superType.GetSuperTypes(superTypes, branch, throws);
                    }
                }
                else
                {
                    if (throws)
                    {
                        throw new InheritanceValidationException(branch.Last());
                    }
                }
            }
            branch.Pop();
        }
    }

    
    public class Operation : SoaObject
    {

        public Operation()
        {
            this.Parameters = new ModelList<OperationParameter>(this, ParametersProperty);
            this.Exceptions = new ModelList<ExceptionType>(this, ExceptionsProperty);
        }


        private List<StructType> exceptions = new List<StructType>();


        [Opposite(typeof(Interface), "Operations")]
        public Interface Interface
        {
            get { return (Interface)GetValue(InterfaceProperty); }
            set { SetValue(InterfaceProperty, value); }
        }
        public static readonly ModelProperty InterfaceProperty = ModelProperty.Register("Interface", typeof(Interface), typeof(Operation));

        public string Name
        {
            get;
            set;
        }


        [Opposite(typeof(OperationParameter), "Operation")]
        public ModelList<OperationParameter> Parameters
        {
            get { return (ModelList<OperationParameter>)GetValue(ParametersProperty); }
            private set { SetValue(ParametersProperty, value); }
        }
        public static readonly ModelProperty ParametersProperty = ModelProperty.Register("Parameters", typeof(ModelList<OperationParameter>), typeof(Operation));


        public Type ReturnType
        {
            get { return (Type)GetValue(ReturnTypeProperty); }
            set { SetValue(ReturnTypeProperty, value); }
        }
        public static readonly ModelProperty ReturnTypeProperty = ModelProperty.Register("ReturnType", typeof(Type), typeof(Operation));


        public ModelList<ExceptionType> Exceptions
        {
            get { return (ModelList<ExceptionType>)GetValue(ExceptionsProperty); }
            private set { SetValue(ExceptionsProperty, value); }
        }
        public static readonly ModelProperty ExceptionsProperty = ModelProperty.Register("Exceptions", typeof(ModelList<ExceptionType>), typeof(Operation));


        public StructType InputType
        {
            get;
            set;
        }

        public StructType OutputType
        {
            get;
            set;
        }

    }

    [ModelLiteral("Parameter")]
    public class OperationParameter : Variable
    {
        [Opposite(typeof(Operation), "Parameters")]
        public Operation Operation
        {
            get { return (Operation)GetValue(OperationProperty); }
            set { SetValue(OperationProperty, value); }
        }
        public static readonly ModelProperty OperationProperty = ModelProperty.Register("Operation", typeof(Operation), typeof(OperationParameter));
    }

    public class VersionInfo : SoaObject
    {
        public int Major
        {
            get;
            set;
        }

        public int Minor
        {
            get;
            set;
        }

        override public string ToString()
        {
            return Major + "." + Minor;
        }
    }

    public class OperationImplementation : SoaObject
    {
        public OperationImplementation()
        {
            this.References = new ModelList<Reference>(this, ReferencesProperty);
        }

        public Operation Operation
        {
            get { return (Operation)GetValue(OperationProperty); }
            set { SetValue(OperationProperty, value); }
        }
        public static readonly ModelProperty OperationProperty = ModelProperty.Register("Operation", typeof(Operation), typeof(OperationImplementation));

        public ModelList<Reference> References
        {
            get { return (ModelList<Reference>)GetValue(ReferencesProperty); }
            private set { SetValue(ReferencesProperty, value); }
        }
        public static readonly ModelProperty ReferencesProperty = ModelProperty.Register("References", typeof(ModelList<Reference>), typeof(OperationImplementation));
        
    }
}
