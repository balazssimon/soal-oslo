using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sb.Meta;

namespace SoaMetaModel
{
    [ModelLiteral("AtomicTransaction")]
    public class AtomicTransactionProtocolBindingElement : ProtocolBindingElement
    {
        public AtomicTransactionProtocolBindingElement()
        {
            this.Version = AtomicTransactionVersion.AtomicTransaction10;
        }

        public AtomicTransactionVersion Version
        {
            get;
            set;
        }
    }

    public enum AtomicTransactionVersion
    {
        AtomicTransaction10
    }
}
