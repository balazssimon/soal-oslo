using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sb.Meta;

namespace SoaMetaModel
{
    [ModelLiteral("MutualCertificateSecurity")]
    public class MutualCertificateSecurityProtocolBindingElement : SecurityProtocolBindingElement
    {
        public MutualCertificateSecurityProtocolBindingElement()
            : base()
        {
        }
    }

    [ModelLiteral("MutualCertificateBootstrap")]
    public class MutualCertificateBootstrapProtocolBindingElement : BootstrapProtocolBindingElement
    {
        public MutualCertificateBootstrapProtocolBindingElement()
            : base()
        {
        }
    }
}
