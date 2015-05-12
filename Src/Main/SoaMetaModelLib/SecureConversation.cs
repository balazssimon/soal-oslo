using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sb.Meta;

namespace SoaMetaModel
{
    [ModelLiteral("SecureConversationSecurity")]
    public class SecureConversationSecurityProtocolBindingElement : SecurityProtocolBindingElement
    {
        public SecureConversationSecurityProtocolBindingElement()
        {
            this.DerivedKeys = true;
            this.RequireSecurityContextCancellation = true;
        }

        public bool DerivedKeys
        {
            get;
            set;
        }

        public bool RequireSecurityContextCancellation
        {
            get;
            set;
        }

        [Opposite(typeof(BootstrapProtocolBindingElement), "SecureConversation")]
        public BootstrapProtocolBindingElement Bootstrap
        {
            get { return (BootstrapProtocolBindingElement)GetValue(BootstrapProperty); }
            set { SetValue(BootstrapProperty, value); }
        }
        public static readonly ModelProperty BootstrapProperty = ModelProperty.Register("Bootstrap", typeof(BootstrapProtocolBindingElement), typeof(SecureConversationSecurityProtocolBindingElement));

    }

    public class BootstrapProtocolBindingElement : ProtocolBindingElement
    {

        [Opposite(typeof(SecureConversationSecurityProtocolBindingElement), "Bootstrap")]
        public SecureConversationSecurityProtocolBindingElement SecureConversation
        {
            get { return (SecureConversationSecurityProtocolBindingElement)GetValue(SecureConversationProperty); }
            set { SetValue(SecureConversationProperty, value); }
        }
        public static readonly ModelProperty SecureConversationProperty = ModelProperty.Register("SecureConversation", typeof(SecureConversationSecurityProtocolBindingElement), typeof(BootstrapProtocolBindingElement));

        public SecurityProtectionOrder ProtectionOrder
        {
            get;
            set;
        }

        public bool RequireSignatureConfirmation
        {
            get;
            set;
        }
    }
}
