using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sb.Meta;

namespace SoaMetaModel
{
    [ModelLiteral("StsSecurity")]
    public class StsSecurityProtocolBindingElement : IssuedTokenSecurityProtocolBindingElement
    {
        public StsSecurityProtocolBindingElement()
            : base()
        {
            this.DerivedKeys = false;
        }

        public bool DerivedKeys
        {
            get;
            set;
        }
    }

    [ModelLiteral("StsBootstrap")]
    public class StsBootstrapProtocolBindingElement : IssuedTokenBootstrapProtocolBindingElement
    {
        public StsBootstrapProtocolBindingElement()
            : base()
        {
            this.DerivedKeys = false;
        }

        public bool DerivedKeys
        {
            get;
            set;
        }
    }
}
