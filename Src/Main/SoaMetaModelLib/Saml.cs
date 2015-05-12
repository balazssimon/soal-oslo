using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sb.Meta;

namespace SoaMetaModel
{
    [ModelLiteral("SamlSecurity")]
    public class SamlSecurityProtocolBindingElement : IssuedTokenSecurityProtocolBindingElement
    {
        public SamlSecurityProtocolBindingElement()
            : base()
        {
            this.Claims = new ModelList<ClaimsetType>(this, ClaimsProperty);
        }

        public ModelList<ClaimsetType> Claims 
        {
            get { return (ModelList<ClaimsetType>)GetValue(ClaimsProperty); }
            private set { SetValue(ClaimsProperty, value); }
        }
        public static readonly ModelProperty ClaimsProperty = ModelProperty.Register("Claims", typeof(ModelList<ClaimsetType>), typeof(SamlSecurityProtocolBindingElement));
    }

    [ModelLiteral("SamlBootstrap")]
    public class SamlBootstrapProtocolBindingElement : IssuedTokenBootstrapProtocolBindingElement
    {
        public SamlBootstrapProtocolBindingElement()
            : base()
        {
            this.Claims = new ModelList<ClaimsetType>(this, ClaimsProperty);
        }

        public ModelList<ClaimsetType> Claims
        {
            get { return (ModelList<ClaimsetType>)GetValue(ClaimsProperty); }
            private set { SetValue(ClaimsProperty, value); }
        }
        public static readonly ModelProperty ClaimsProperty = ModelProperty.Register("Claims", typeof(ModelList<ClaimsetType>), typeof(SamlBootstrapProtocolBindingElement));
    }
}
