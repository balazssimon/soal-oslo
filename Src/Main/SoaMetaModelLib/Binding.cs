using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sb.Meta;

namespace SoaMetaModel
{
    public class Binding : Declaration
    {
        public Binding()
        {
            this.Protocols = new ModelList<ProtocolBindingElement>(this, ProtocolsProperty);
        }

        [Opposite(typeof(TransportBindingElement), "Binding")]
        public TransportBindingElement Transport
        {
            get { return (TransportBindingElement)GetValue(TransportProperty); }
            set { SetValue(TransportProperty, value); }
        }
        public static readonly ModelProperty TransportProperty = ModelProperty.Register("Transport", typeof(TransportBindingElement), typeof(Binding));

        [Opposite(typeof(EncodingBindingElement), "Binding")]
        public EncodingBindingElement Encoding
        {
            get { return (EncodingBindingElement)GetValue(EncodingProperty); }
            set { SetValue(EncodingProperty, value); }
        }
        public static readonly ModelProperty EncodingProperty = ModelProperty.Register("Encoding", typeof(EncodingBindingElement), typeof(Binding));

        [Opposite(typeof(ProtocolBindingElement), "Binding")]
        public ModelList<ProtocolBindingElement> Protocols
        {
            get { return (ModelList<ProtocolBindingElement>)GetValue(ProtocolsProperty); }
            private set { SetValue(ProtocolsProperty, value); }
        }
        public static readonly ModelProperty ProtocolsProperty = ModelProperty.Register("Protocols", typeof(ModelList<ProtocolBindingElement>), typeof(Binding));
    }

    public class BindingElement : SoaObject
    {
        public BindingElement()
        {
            this.Properties = new ModelList<BindingElementProperty>(this, PropertiesProperty);
        }

        public string Name
        {
            get;
            set;
        }

        [Opposite(typeof(BindingElementProperty), "BindingElement")]
        public ModelList<BindingElementProperty> Properties
        {
            get { return (ModelList<BindingElementProperty>)GetValue(PropertiesProperty); }
            private set { SetValue(PropertiesProperty, value); }
        }
        public static readonly ModelProperty PropertiesProperty = ModelProperty.Register("Properties", typeof(ModelList<BindingElementProperty>), typeof(BindingElement));
    }

    [ModelLiteral("Property")]
    public class BindingElementProperty : SoaObject
    {
        [Opposite(typeof(BindingElement), "Properties")]
        public BindingElement BindingElement
        {
            get { return (BindingElement)GetValue(BindingElementProperty2); }
            set { SetValue(BindingElementProperty2, value); }
        }
        public static readonly ModelProperty BindingElementProperty2 = ModelProperty.Register("BindingElement", typeof(BindingElement), typeof(BindingElementProperty));

        public string Name
        {
            get;
            set;
        }

        public Expression Value
        {
            get;
            set;
        }
    }

    [ModelLiteral("Transport")]
    public class TransportBindingElement : BindingElement
    {
        [Opposite(typeof(Binding), "Transport")]
        public Binding Binding
        {
            get { return (Binding)GetValue(BindingProperty); }
            set { SetValue(BindingProperty, value); }
        }
        public static readonly ModelProperty BindingProperty = ModelProperty.Register("Binding", typeof(Binding), typeof(TransportBindingElement));
    }

    [ModelLiteral("Encoding")]
    public class EncodingBindingElement : BindingElement
    {
        [Opposite(typeof(Binding), "Encoding")]
        public Binding Binding
        {
            get { return (Binding)GetValue(BindingProperty); }
            set { SetValue(BindingProperty, value); }
        }
        public static readonly ModelProperty BindingProperty = ModelProperty.Register("Binding", typeof(Binding), typeof(EncodingBindingElement));
    }

    [ModelLiteral("Protocol")]
    public class ProtocolBindingElement : BindingElement
    {
        [Opposite(typeof(Binding), "Protocols")]
        public Binding Binding
        {
            get { return (Binding)GetValue(BindingProperty); }
            set { SetValue(BindingProperty, value); }
        }
        public static readonly ModelProperty BindingProperty = ModelProperty.Register("Binding", typeof(Binding), typeof(ProtocolBindingElement));
    }
}
