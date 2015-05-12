using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sb.Meta;

namespace SoaMetaModel
{
    public class Endpoint : Declaration
    {

        public Interface Interface
        {
            get { return (Interface)GetValue(InterfaceProperty); }
            set { SetValue(InterfaceProperty, value); }
        }
        public static readonly ModelProperty InterfaceProperty = ModelProperty.Register("Interface", typeof(Interface), typeof(Endpoint));


        public Binding Binding
        {
            get { return (Binding)GetValue(BindingProperty); }
            set { SetValue(BindingProperty, value); }
        }
        public static readonly ModelProperty BindingProperty = ModelProperty.Register("Binding", typeof(Binding), typeof(Endpoint));


        public Authorization Authorization
        {
            get { return (Authorization)GetValue(AuthorizationProperty); }
            set { SetValue(AuthorizationProperty, value); }
        }
        public static readonly ModelProperty AuthorizationProperty = ModelProperty.Register("Authorization", typeof(Authorization), typeof(Endpoint));


        public Contract Contract
        {
            get { return (Contract)GetValue(ContractProperty); }
            set { SetValue(ContractProperty, value); }
        }
        public static readonly ModelProperty ContractProperty = ModelProperty.Register("Contract", typeof(Contract), typeof(Endpoint));

        public EndpointAddress Address
        {
            get;
            set;
        }

    }

    public class EndpointAddress : SoaObject
    {
        public EndpointAddress(string uri = null)
        {
            Uri = uri;
        }

        public string Uri
        {
            get;
            set;
        }
    }
}
