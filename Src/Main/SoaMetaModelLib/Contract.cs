using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sb.Meta;

namespace SoaMetaModel
{
    using MetaInfo;

    public class Contract : Declaration
    {
        public Contract()
        {
            this.OperationContracts = new ModelList<OperationContract>(this, OperationContractsProperty);
        }

        public Interface Interface
        {
            get { return (Interface)GetValue(InterfaceProperty); }
            set { SetValue(InterfaceProperty, value); }
        }
        public static readonly ModelProperty InterfaceProperty = ModelProperty.Register("Interface", typeof(Interface), typeof(Contract));

        [Opposite(typeof(OperationContract), "Contract")]
        public ModelList<OperationContract> OperationContracts
        {
            get { return (ModelList<OperationContract>)GetValue(OperationContractsProperty); }
            private set { SetValue(OperationContractsProperty, value); }
        }
        public static readonly ModelProperty OperationContractsProperty = ModelProperty.Register("OperationContracts", typeof(ModelList<OperationContract>), typeof(Contract));

    }

    [ModelLiteral("Operation")]
    public class OperationContract : OperationImplementation
    {

        public OperationContract()
            : base()
        {
            this.OperationContractStatements = new ModelList<OperationContractStatement>(this, OperationContractStatementsProperty);
        }

        [Opposite(typeof(Contract), "OperationContracts")]
        public Contract Contract
        {
            get { return (Contract)GetValue(ContractProperty); }
            set { SetValue(ContractProperty, value); }
        }
        public static readonly ModelProperty ContractProperty = ModelProperty.Register("Contract", typeof(Contract), typeof(OperationContract));

        [Opposite(typeof(OperationContractStatement), "OperationContract")]
        public ModelList<OperationContractStatement> OperationContractStatements
        {
            get { return (ModelList<OperationContractStatement>)GetValue(OperationContractStatementsProperty); }
            private set { SetValue(OperationContractStatementsProperty, value); }
        }
        public static readonly ModelProperty OperationContractStatementsProperty = ModelProperty.Register("OperationContractStatements", typeof(ModelList<OperationContractStatement>), typeof(OperationContract));
    }

    public class OperationContractStatement : RuleStatement
    {
        [Opposite(typeof(OperationContract), "OperationContractStatements")]
        public OperationContract OperationContract
        {
            get { return (OperationContract)GetValue(OperationContractProperty); }
            set { SetValue(OperationContractProperty, value); }
        }
        public static readonly ModelProperty OperationContractProperty = ModelProperty.Register("OperationContract", typeof(OperationContract), typeof(OperationContractStatement));
    }

    public class Ensures : OperationContractStatement
    {
    }

    public class Requires : OperationContractStatement
    {
        public Expression Otherwise
        {
            get;
            set;
        }
    }
}
