using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sb.Meta;

namespace SoaMetaModel
{
    [ModelLiteral("ReliableMessaging")]
    public class ReliableMessagingProtocolBindingElement : ProtocolBindingElement
    {
        public ReliableMessagingProtocolBindingElement()
        {
            this.Version = ReliableMessagingVersion.ReliableMessaging11;
            this.Delivery = ReliableMessagingDelivery.ExactlyOnce;
            this.InOrder = true;
        }

        public ReliableMessagingVersion Version
        {
            get;
            set;
        }

        public ReliableMessagingDelivery Delivery
        {
            get;
            set;
        }

        public bool InOrder
        {
            get;
            set;
        }
    }

    public enum ReliableMessagingVersion
    {
        ReliableMessaging11
    }

    public enum ReliableMessagingDelivery
    {
        AtLeastOnce,
        AtMostOnce,
        ExactlyOnce
    }
}
