using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sb.Meta;

namespace SoaMetaModel
{
    [ModelLiteral("Addressing")]
    public class AddressingProtocolBindingElement : ProtocolBindingElement
    {
        public AddressingProtocolBindingElement()
        {
            this.Version = AddressingVersion.Addressing10;
        }

        public AddressingVersion Version
        {
            get;
            set;
        }
    }

    public enum AddressingVersion
    {
        Addressing10,
        AddressingAugust2004
    }
}
