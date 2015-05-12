using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sb.Meta;

namespace SoaMetaModel
{
    public class IssuedTokenSecurityProtocolBindingElement : SecurityProtocolBindingElement
    {
        public IssuedTokenSecurityProtocolBindingElement()
        {
            this.TokenVersion = IssuedTokenVersion.Token10;
            this.TokenType = IssuedTokenType.Symmetric128;
            this.RequireSignatureConfirmation = false;
        }

        public IssuedTokenVersion TokenVersion
        {
            get;
            set;
        }

        public IssuedTokenType TokenType
        {
            get;
            set;
        }

        public IssuedTokenIssuer TokenIssuer
        {
            get;
            set;
        }

    }

    public class IssuedTokenBootstrapProtocolBindingElement : BootstrapProtocolBindingElement
    {
        public IssuedTokenBootstrapProtocolBindingElement()
        {
            this.TokenVersion = IssuedTokenVersion.Token10;
            this.TokenType = IssuedTokenType.Symmetric128;
            this.RequireSignatureConfirmation = false;
        }

        public IssuedTokenVersion TokenVersion
        {
            get;
            set;
        }

        public IssuedTokenType TokenType
        {
            get;
            set;
        }

        public IssuedTokenIssuer TokenIssuer
        {
            get;
            set;
        }

    }

    [ModelLiteral("TokenIssuer")]
    public class IssuedTokenIssuer
    {
        public string Address
        {
            get;
            set;
        }

        public string MetadataAddress
        {
            get;
            set;
        }
    }

    [ModelLiteral("TokenType")]
    public enum IssuedTokenType
    {
        Symmetric128,
        Symmetric192,
        Symmetric256,
        Asymmetric1024,
        Asymmetric2048,
        Asymmetric3072
    }

    [ModelLiteral("TokenVersion")]
    public enum IssuedTokenVersion
    {
        Token10,
        Token11,
        Token20
    }
}
